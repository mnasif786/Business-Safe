using System.Xml.Linq;
using System.Linq;

using NUnit.Framework;

namespace BusinessSafe.Application.IntegrationTests.Config.Tests.BusinessSafe.Checklists
{
    [TestFixture]
    [Category("Web.Configs")]
    [Category("UAT")]
    public class UATTests : BaseConfigTests
    {
        private XDocument _target;

        [SetUp]
        public void Setup()
        {
            _target = GetTarget();
        }

        [TestCase("ClientDetailsServices.Rest.BaseUrl", "http://localhost:8072/restservice/v1.0/")]
        public void Then_appSettings_are_as_expected(string key, string expectedValue)
        {
            // Given
            // When
            var elements = _target
                .Elements("configuration")
                .Elements("appSettings");

            var element = FindElementByAttributesFromElements("key", key, elements);


            // Then
            Assert.That(element.Attribute("value").Value, Is.EqualTo(expectedValue));
        }

        [TestCase("BusinessSafe.Messages.Emails", "businesssafe.messagehandlers.emails@UATBSO1")]
        [TestCase("BusinessSafe.Messages", "businesssafe.messagehandlers@UATBSO1")]
        public void Then_MessageEndpointMappings_are_as_expected(string messages, string endpoint)
        {
            // Given
            var elements = _target
                .Elements("configuration")
                .Elements("UnicastBusConfig")
                .Elements("MessageEndpointMappings");

            // When
            var element = FindElementByAttributesFromElements("Messages", messages, elements);

            // Then
            Assert.That(element.Attribute("Endpoint").Value, Is.EqualTo(endpoint));
        }

        [Test]
        public void Then_clear_element_exists_in_Then_MessageEndpointMappings_as_expected()
        {
            // Given
            var elements = _target
                .Elements("configuration")
                .Elements("UnicastBusConfig")
                .Elements("MessageEndpointMappings");

            // When
            var clearElement = elements.Descendants("clear");

            // Then
            Assert.IsNotNull(clearElement);
        }

        [Test]
        public void Then_Elmah_settings_are_correct_values()
        {
            // Given
            var elements = _target
                .Elements("configuration")
                .Elements("elmah");

            // When
            var securityElement = elements.Elements("security").SingleOrDefault();
            var errorLogElement = elements.Elements("errorLog").SingleOrDefault();

            // Then
            Assert.That(securityElement.Attribute("allowRemoteAccess").Value, Is.EqualTo("1"));

            Assert.That(errorLogElement.Attribute("type").Value, Is.EqualTo("Elmah.SqlErrorLog, Elmah"));
            Assert.That(errorLogElement.Attribute("applicationName").Value, Is.EqualTo("BusinessSafe_Checklists"));
            Assert.That(errorLogElement.Attribute("connectionString").Value,
                Is.EqualTo(@"Initial Catalog=ELMAH;Data Source=PBSITSQL\it_database;UID=intranetadmin;PWD=intadpas;Min Pool Size=2;Max Pool Size=60;Connect Timeout=2;"));
        }

        private XDocument GetTarget()
        {
            string transformedFilePath = string.Empty;

            switch (System.Environment.MachineName.ToLower())
            {
                case "pbs43758":
                    transformedFilePath = @"D:\Code\BusinessSafe\BusinessSafe.Checklists\Web.UAT.Transformed.config";
                    break;
                default:
                    transformedFilePath = @"C:\BusinessSafe\Artifacts\BusinessSafe.Checklists\Configs\Web.UAT.Transformed.config";
                    break;
            }


            return XDocument.Load(transformedFilePath, LoadOptions.None);
        }
    }
}
