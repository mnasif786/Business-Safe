using System.IO;
using System.Xml.Linq;
using System.Linq;

using NUnit.Framework;

namespace BusinessSafe.Application.IntegrationTests.Config.Tests.BusinessSafe.WebSite
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

        [TestCase("config_file", "UAT")]
        [TestCase("ClientDetailsServices.Rest.BaseUrl", "http://uatbso1:8072/restservice/v1.0/")]
        [TestCase("DocumentUploadHoldingPath", @"\\pbsw23datastore\DragonDropHoldingPath\")]
        [TestCase("AllowAutoLogin", "true")]
        [TestCase("SQLReportsSuffix", "_UAT")]
        [TestCase("FeatureSwitch_Responsibilities","true")]
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

        [TestCase("Version", "not_set")]
        public void Then_appSettings_has_been_transformed(string key, string unTransformedValue)
        {
            // Given
            // When
            var elements = _target
                .Elements("configuration")
                .Elements("appSettings");

            var element = FindElementByAttributesFromElements("key", key, elements);


            // Then
            Assert.That(element.Attribute("value").Value, Is.Not.EqualTo(unTransformedValue));
        }

        [TestCase("SqlReportsService")]
        [TestCase("DocumentTypeService")]
        [TestCase("DocumentSubTypeService")]
        [TestCase("ClientDocumentServiceType")]
        [TestCase("StreamingClientDocumentService")]
        public void Then_appSettings_are_not_available(string key)
        {
            // Given
            // When
            var elements = _target
                .Elements("configuration")
                .Elements("appSettings");

            var element = FindElementByAttributesFromElements("key", key, elements);


            // Then
            Assert.That(element, Is.Null);
        }

        [TestCase(@"http://uatmaintws1:8056/DocumentLibraryService.svc", "DocumentLibraryService")]
        [TestCase(@"http://uatmaintws1:8056/StreamingDocumentLibraryService.svc", "StreamingDocumentLibraryService")]
        [TestCase(@"http://uatmaintws1:8064/StreamingClientDocumentService.svc", "StreamingClientDocumentServiceClient")]
        [TestCase(@"http://uatmaintws1:8064/ClientDocumentService.svc", "ClientDocumentService")]
        [TestCase(@"http://uatmaintws1:8064/DocumentTypeService.svc", "DocumentTypeService")]
        [TestCase(@"http://uatmaintws1:8064/DocumentSubTypeService.svc", "DocumentSubTypeService")]
        [TestCase(@"http://pbsreports:80/SQLReports2008/ReportExecution2005.asmx", "ReportExecutionServiceSoap")]
        [TestCase(@"http://UATBSO1:8073/NewRegistrationRequestService.svc", "BasicHttpBinding_INewRegistrationRequestService")]
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

        [TestCase("BusinessSafe.Messages.Emails", "businesssafe.messagehandlers.emails@UATBSO1")]
        [TestCase("BusinessSafe.Messages", "businesssafe.messagehandlers@UATBSO1")]
        [TestCase("Peninsula.Online.Messages", "peninsula.online.messagehandlers@UATBSO1")]
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
            Assert.That(errorLogElement.Attribute("applicationName").Value, Is.EqualTo("BusinessSafe"));
            Assert.That(errorLogElement.Attribute("connectionString").Value,
                Is.EqualTo(@"Initial Catalog=ELMAH;Data Source=PBSITSQL\it_database;UID=intranetadmin;PWD=intadpas;Min Pool Size=2;Max Pool Size=60;Connect Timeout=2;"));
        }

        [TestCase("DBSubscriptionStorageConfig", "connection.connection_string", @"Initial Catalog=NServiceBus;Data Source=UATSQL2\uat;UID=intranetadmin;PWD=intadpas;Connect Timeout=20;")]
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
            string fileName = "Web.UAT.Transformed.config";
            string transformedFilePath = string.Empty;

            transformedFilePath = string.IsNullOrEmpty(BuildDirectoryLocations.GetBuildDirectoryLocation(System.Environment.MachineName)) ?
                @"C:\BusinessSafe\Artifacts\BusinessSafe.Website\Configs\Web.UAT.Transformed.config" 
                : string.Format(@"{0}\BusinessSafe.Website\{1}",BuildDirectoryLocations.GetBuildDirectoryLocation(System.Environment.MachineName),fileName);

            return XDocument.Load(transformedFilePath, LoadOptions.None);
        }
    }
}
