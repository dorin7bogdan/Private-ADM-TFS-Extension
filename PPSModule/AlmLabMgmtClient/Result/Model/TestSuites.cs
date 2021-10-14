using System.Collections.Generic;
using System.Xml.Serialization;
/**
 * The following schema fragment specifies the expected content contained within this class.
 * 
 * <complexType>
 *   <complexContent>
 *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       <sequence>
 *         <element ref="{}testsuite" maxOccurs="unbounded" minOccurs="0"/>
 *       </sequence>
 *       <attribute name="name" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       <attribute name="time" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       <attribute name="tests" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       <attribute name="failures" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       <attribute name="disabled" type="{http://www.w3.org/2001/XMLSchema}string" />
 *       <attribute name="errors" type="{http://www.w3.org/2001/XMLSchema}string" />
 *     </restriction>
 *   </complexContent>
 * </complexType>
 * 
 */
namespace PSModule.AlmLabMgmtClient.Result.Model
{
    [XmlRoot(ElementName = "testsuites")]
    public class TestSuites
    {
        [XmlElement(ElementName = "testsuite")]
        public List<TestSuite> ListOfTestSuites { get; set; } = new List<TestSuite>();

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "time")]
        public string Time { get; set; }

        [XmlAttribute(AttributeName = "tests")]
        public string Tests { get; set; }

        [XmlAttribute(AttributeName = "failures")]
        public string Failures { get; set; }

        [XmlAttribute(AttributeName = "disabled")]
        public string Disabled { get; set; }

        [XmlAttribute(AttributeName = "errors")]
        public string Errors { get; set; }

    }

}
