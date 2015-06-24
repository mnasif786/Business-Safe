﻿using System.Xml.Linq;

using NUnit.Framework;

namespace BusinessSafe.Application.IntegrationTests.Config.Tests.BusinessSafe.MessageHandlers.Emails
{
    [TestFixture]
    [Category("Web.Configs")]
    [Category("LIVE")]
    public class LIVETests : BaseConfigTests
    {
        private XDocument _target;

        [SetUp]
        public void Setup()
        {
            _target = GetTarget();
        }

        [TestCase("SendExternalEmail", "true")]
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

        [TestCase(@"http://pbsemailpusher/EmailPusherService.svc", "EmailPusherService_basicHttpBinding")]
        [TestCase(@"http://pbsemailpusher/EmailPusherService.svc/mex", "EmailPusherService_mexHttpBinding")]
        [TestCase(@"http://clientdocumentationservice/ClientDocumentService.svc", "ClientDocumentService")]
        [TestCase(@"http://pbsdoclibrary:8056/DocumentLibraryService.svc", "DocumentLibraryService")]        
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

        [TestCase("BusinessSafe.Messages.Events.EmployeeChecklistEmailGenerated, BusinessSafe.Messages", "businesssafe.messagehandlers@pbsservicebus1")]
        [TestCase("BusinessSafe.Messages.Events.EmployeeChecklistCompleted, BusinessSafe.Messages", "businesssafe.messagehandlers@pbsservicebus1")]
        [TestCase("BusinessSafe.Messages.Events.TaskAssigned, BusinessSafe.Messages", "businesssafe.messagehandlers.emails@pbsservicebus1")]
        [TestCase("BusinessSafe.Messages.Events.ReviewAssigned, BusinessSafe.Messages", "businesssafe.messagehandlers.emails@pbsservicebus1")]
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

        [TestCase("DBSubscriptionStorageConfig", "connection.connection_string", @"Initial Catalog=NServiceBus;Data Source=pbsprod2sql\prod2;Integrated Security=true;Connect Timeout=20;")]
        [TestCase("TimeoutPersisterConfig", "connection.connection_string", @"Initial Catalog=NServiceBus;Data Source=pbsprod2sql\prod2;Integrated Security=true;Connect Timeout=20;")]
        [TestCase("NHibernateSagaPersisterConfig", "connection.connection_string", @"Initial Catalog=NServiceBus;Data Source=pbsprod2sql\prod2;Integrated Security=true;Connect Timeout=20;")]
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
                    transformedFilePath = @"D:\Code\BusinessSafe\BusinessSafe.MessageHandlers.Emails\App.LIVE.Transformed.config";
                    break;
                default:
                    transformedFilePath = @"C:\BusinessSafe\Artifacts\BusinessSafe.MessageHandlers.Emails\Configs\App.LIVE.Transformed.config";
                    break;
            }

            return XDocument.Load(transformedFilePath, LoadOptions.None);
        }
    }
}