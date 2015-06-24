using System.Xml.Linq;

using NUnit.Framework;

namespace BusinessSafe.Application.IntegrationTests.Config.Tests.BusinessSafe.MessageHandlers
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

        [TestCase(@"http://pbswebbetaint1:8040/EmailPusherService.svc", "EmailPusherService_basicHttpBinding")]
        [TestCase(@"http://pbswebbetaint1:8040/EmailPusherService.svc/mex", "EmailPusherService_mexHttpBinding")]
        public void Then_endpoints_are_as_expected(string address, string name)
        {
            // Given
            var elements = _target
                .Elements("configuration")
                .Elements("system.serviceModel")
                .Elements("client");

            // When
            var element = FindElementByAttributesFromElements("name", name, elements);

            // Then
            Assert.That(element.Attribute("address").Value, Is.EqualTo(address));
        }

        //[TestCase("Peninsula.Online.Messages.Events.RegistrationCompleted, Peninsula.Online.Messages", "peninsula.online.messagehandlers")]
        //[TestCase("Peninsula.Online.Messages.Events.UserDeleted, Peninsula.Online.Messages", "peninsula.online.messagehandlers")]
        [TestCase("Peninsula.Online.Messages", "peninsula.online.messagehandlers")]
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

        [TestCase("DBSubscriptionStorageConfig", "connection.connection_string", @"Initial Catalog=NServiceBus;Data Source=localhost\sql2008r2;UID=intranetadmin;PWD=intadpas;Connect Timeout=20;")]
        [TestCase("TimeoutPersisterConfig", "connection.connection_string", @"Initial Catalog=NServiceBus;Data Source=localhost\sql2008r2;UID=intranetadmin;PWD=intadpas;Connect Timeout=20;")]
        [TestCase("NHibernateSagaPersisterConfig", "connection.connection_string", @"Initial Catalog=NServiceBus;Data Source=localhost\sql2008r2;UID=intranetadmin;PWD=intadpas;Connect Timeout=20;")]
        public void Then_NHibernateProperties_are_as_expected(string containingElementName, string keyAttributeValue, string value)
        {
            // Given
            var elements = _target
                .Elements("configuration")
                .Elements(containingElementName)
                .Elements("NHibernateProperties");

            // When
            var element = FindElementByAttributesFromElements("Key", keyAttributeValue, elements);

            // Then
            Assert.That(element.Attribute("Value").Value, Is.EqualTo(value));
        }

        private XDocument GetTarget()
        {
            string transformedFilePath = string.Empty;

            switch (System.Environment.MachineName.ToLower())
            {
                case "pbs43758":
                    transformedFilePath = @"D:\Code\BusinessSafe\BusinessSafe.MessageHandlers\App.CI.Transformed.config";
                    break;
                default:
                    transformedFilePath = @"C:\BusinessSafe\Artifacts\BusinessSafe.MessageHandlers\Configs\App.CI.Transformed.config";
                    break;
            }

            return XDocument.Load(transformedFilePath, LoadOptions.None);
        }
    }
}
