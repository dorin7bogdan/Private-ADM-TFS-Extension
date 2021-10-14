# UFT One Azure DevOps extension
Enables you to run UFT One tests as a build in an Azure DevOps build process. This extension includes 4 tasks.
## Table of contents
1. [Integration with UFT One](#Integration-with-UFT-One)
2. [Configuration](#Configuration)
3. [Extension functionality](#Extension-functionality)
4. [Resources](#Additional-resources)

# Integration with UFT One
In a build step, run UFT One tests stored in the local file system or on an ALM server. When running tests from ALM Lab Management, you can also include a build step that prepares the test environment before running the tests. After the build is complete, you can view comprehensive test results. 
#  Configuration
#### Prerequisites
- UFT One (version >=**14.00**)
- Powershell (version **4** or later)
- JRE installed

#### Setup
1. From [Visual Studio Marketplace][marketplace]: Install the **UFT One Azure DevOps extension** for the relevant organization
2. On our [GitHub][repository]: Navigate to a specific release (latest: **2.0.0**)
3. From [Azure DevOps][azure-devops]: Navigate to **agent pools** and set up an agent (interactive or run as a service) 
4. On your agent machine:    
4.1. Download the resources provided by a specific release (UFT.zip & unpack.ps1)    
4.2. Run the *unpack.ps1* script    

##### For extension version >=**2.0.0**:
5. From [Azure Portal][azure-portal]: Have available a Resource Group, a Storage Account and a Container (for storing report artifacts)
6. On your agent machine:
6.1. Install [Azure Powershell] [azure-powershell]
6.2. Connect to [Azure Portal][azure-connect]
6.3. To access the artifacts (HTML report, archive or both), change the *container's access level* to **blob**

# Extension Functionality
##### UFT One ALM Lab Management Run
- Use this task to run ALM server-side functional test sets.
- More information is available [here][alm-lab-docs]

#
#
# Additional Resources
For assistance or more information on configuring and using this extension, please consult the following resources:
- [Extension Marketplace page][marketplace]
- [Help Center][docs]
- [UFT One Forum][forum]
- [Support][support]

[//]: # (References)
   [docs]:<https://admhelp.microfocus.com/uft/en/latest/UFT_Help/Content/UFT_Tools/Azure_DevOps_Extension/uft-azure-devops.htm>
   [forum]:<https://community.microfocus.com/adtd/uft/f/sws-fun_test_sf/>
   [support]:<https://softwaresupport.softwaregrp.com/>
   [repository]:<https://github.com/MicroFocus/ADM-TFS-Extension/>
   [marketplace]:<https://marketplace.visualstudio.com/items?itemName=uftpublisher.UFT-Azure-extension>
   [fs-docs]:<https://admhelp.microfocus.com/uft/en/latest/UFT_Help/Content/UFT_Tools/Azure_DevOps_Extension/uft-azure-devops-run-local.htm>
   [alm-docs]:<https://admhelp.microfocus.com/uft/en/latest/UFT_Help/Content/UFT_Tools/Azure_DevOps_Extension/uft-azure-devops-run-alm.htm>
   [alm-lab-docs]:<https://admhelp.microfocus.com/uft/en/latest/UFT_Help/Content/UFT_Tools/Azure_DevOps_Extension/uft-azure-devops-run-alm-lm.htm#mt-item-1>
   [alm-env-docs]:<https://admhelp.microfocus.com/uft/en/latest/UFT_Help/Content/UFT_Tools/Azure_DevOps_Extension/uft-azure-devops-run-alm-lm.htm#mt-item-0>
   [azure-devops]:<https://dev.azure.com/>
   [azure-portal]:<http://portal.azure.com/>
   [azure-powershell]:<https://docs.microsoft.com/en-us/powershell/azure/install-az-ps?view=azps-6.0.0>
   [azure-connect]:<https://docs.microsoft.com/en-us/powershell/module/az.accounts/connect-azaccount?view=azps-6.0.0>
