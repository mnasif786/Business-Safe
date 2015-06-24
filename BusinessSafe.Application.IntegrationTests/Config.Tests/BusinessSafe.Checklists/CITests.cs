using System.Xml.Linq;

using NUnit.Framework;

namespace BusinessSafe.Application.IntegrationTests.Config.Tests.BusinessSafe.Checklists
{
    [TestFixture]
    [Category("Web.Configs")]
    [Category("CI")]
    public class CITests : BaseConfigTests
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

        [TestCase("BusinessSafe.Messages.Emails", "businesssafe.messagehandlers.emails")]
        [TestCase("BusinessSafe.Messages", "businesssafe.messagehandlers")]
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

        private XDocument GetTarget()
        {
            string transformedFilePath = string.Empty;

            switch (System.Environment.MachineName.ToLower())
            {
                case "pbs43758":
                    transformedFilePath = @"D:\Code\BusinessSafe\BusinessSafe.Checklists\Web.CI.Transformed.config";
                    break;
                default:
                    transformedFilePath = @"C:\BusinessSafe\Artifacts\BusinessSafe.Checklists\Configs\Web.CI.Transformed.config";
                    break;
            }


            return XDocument.Load(transformedFilePath, LoadOptions.None);
        }
    }
}
