﻿using PSModule.AlmLabMgmtClient.Result;
using PSModule.AlmLabMgmtClient.Result.Model;
using PSModule.AlmLabMgmtClient.SDK.Auth;
using PSModule.AlmLabMgmtClient.SDK.Factory;
using PSModule.AlmLabMgmtClient.SDK.Handler;
using PSModule.AlmLabMgmtClient.SDK.Interface;
using PSModule.AlmLabMgmtClient.SDK.Util;
using System;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;

namespace PSModule.AlmLabMgmtClient.SDK
{
    using C = Constants;
    public class RunManager
    {
        private readonly RunHandler _runHandler;
        private readonly PollHandler _pollHandler;
        private bool _isRunning = false;
        private bool _isPolling = false;
        private bool _isLoggedIn = false;
        private readonly ILogger _logger;

        private readonly RestClient _client;
        private readonly Args _args;

        public bool IsRunning => _isRunning;
        public bool IsPolling => _isPolling;

        public RunManager(RestClient client, Args args)
        {
            _client = client;
            _logger = client.Logger;
            _args = args;
            _runHandler = new RunHandlerFactory().Create(client, args.RunType, args.EntityId);
            _pollHandler = new PollHandlerFactory().Create(client, args.RunType, args.EntityId);
        }
        public async Task<TestSuites> Execute()
        {
            TestSuites res = null;
            _isRunning = true;
            var authHandler = AuthManager.Instance;
            _isLoggedIn = await authHandler.Authenticate(_client);
            if (_isLoggedIn)
            {
                if (await Start())
                {
                    _isPolling = true;
                    if (await _pollHandler.Poll())
                    {
                        var publisher = new LabPublisher(_client, _args.EntityId, _runHandler.RunId, _runHandler.NameSuffix);
                        res = await publisher.Publish(_args.ServerUrl, _args.Domain, _args.Project);
                    }
                    _isPolling = false;
                }
                await authHandler.Logout(_client);
                _isLoggedIn = false;
            }
            _isRunning = false;

            return res;
        }

        private async Task<bool> Start()
        {
            bool ok = false;
            Response res = await _runHandler.Start(
                            _args.Duration,
                            _args.EnvironmentConfigurationId);
            if (IsOK(res))
            {
                RunResponse runResponse = _runHandler.GetRunResponse(res);
                SetRunId(runResponse);
                ok = runResponse.HasSucceeded;
            }
            //_logger.Info($"{res.Data}");
            await LogReportUrl(ok);
            return ok;
        }

        private async Task LogReportUrl(bool hasSucceeded)
        {
            if (hasSucceeded)
            {
                string reportUrl = await _runHandler.ReportUrl(_args);
                await _logger.LogInfo($"{_args.RunType} run report for run id {_runHandler.RunId} is at: {reportUrl}");
            }
            else
            {
                string errMsg = $"Failed to prepare timeslot for run. No entity of type {_args.RunType} with id {_args.EntityId} exists.";
                string note = "Note: You can run only functional test sets and build verification suites using this task. Check to make sure that the configured ID is valid (and that it is not a performance test ID).";
                await _logger.LogError($"{errMsg}{Environment.NewLine}{note}");
            }
        }

        private void SetRunId(RunResponse runResponse)
        {
            string runId = runResponse.RunId;
            if (runId.IsNullOrWhiteSpace())
            {
                _logger.LogError(C.NO_RUN_ID);
                throw new AlmException(C.NO_RUN_ID, ErrorCategory.InvalidResult);
            }
            else
            {
                _runHandler.SetRunId(runId);
                _pollHandler.SetRunId(runId);
            }
        }

        private bool IsOK(Response response)
        {
            bool ok = false;
            if (response.IsOK)
            {
                _logger.LogInfo($"Executing {_args.RunType} ID: {_args.EntityId} in {_args.Domain}/{_args.Project} {Environment.NewLine}Description: {_args.Description}");
                ok = true;
            }
            else
            {
                if (response.Error.IsNullOrWhiteSpace())
                    _logger.LogError($"Failed to execute {_args.RunType} ID: {_args.EntityId}, ALM Server URL: {_args.ServerUrl} (Response: {response.StatusCode}");
                else
                    _logger.LogError($"Failed to start {_args.RunType} ID: {_args.EntityId}, ALM Server URL: {_args.ServerUrl} (Error: {response.Error})");
            }
            return ok;
        }

        public async Task Stop()
        {
            if (_runHandler != null && _isRunning)
            {
                try
                {
                    await _logger.LogInfo("Stopping run...");
                    if (_isLoggedIn)
                    {
                        await AuthManager.Instance.Logout(_client);
                        _isLoggedIn = false;
                    }
                    await _runHandler.Stop();
                }
                catch (ThreadInterruptedException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    await _logger.LogError(e.Message);
                }
                _isRunning = false;
                _isPolling = false;
            }
        }

    }
}
