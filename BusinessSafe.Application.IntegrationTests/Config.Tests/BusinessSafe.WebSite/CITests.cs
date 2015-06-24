using System.Xml.Linq;

using NUnit.Framework;

namespace BusinessSafe.Application.IntegrationTests.Config.Tests.BusinessSafe.WebSite
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

        [TestCase("config_file", "local")]
        [TestCase("ClientDetailsServices.Rest.BaseUrl", "http://clientdetailsservicesrest/restservice/v1.0/")]
        [TestCase("DocumentUploadHoldingPath", @"\\pbsw23datastore\DragonDropHoldingPath\")]
        [TestCase("AllowAutoLogin", "true")]
        [TestCase("SqlReportsService", "BusinessSafe.WebSite.WebsiteMoqs.FakeSqlReportExecutionServiceFacade")]
        [TestCase("DocumentTypeService", "BusinessSafe.WebSite.WebsiteMoqs.FakeDocumentTypeService")]
        [TestCase("DocumentSubTypeService", "BusinessSafe.WebSite.WebsiteMoqs.FakeDocumentSubTypeService")]
        [TestCase("ClientDocumentServiceType", "BusinessSafe.WebSite.WebsiteMoqs.FakeClientDocumentService")]
        [TestCase("StreamingClientDocumentService", "BusinessSafe.WebSite.WebsiteMoqs.FakeStreamingClientDocumentService")]
        [TestCase("FeatureSwitch_Responsibilities", "true")]
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

        [TestCase(@"http://uatmaintws1:8056/DocumentLibraryService.svc", "DocumentLibraryService")]
        [TestCase(@"http://uatmaintws1:8056/StreamingDocumentLibraryService.svc", "StreamingDocumentLibraryService")]
        [TestCase(@"http://uatmaintws1:8064/StreamingClientDocumentService.svc", "StreamingClientDocumentServiceClient")]
        [TestCase(@"http://uatmaintws1:8064/ClientDocumentService.svc", "ClientDocumentService")]
        [TestCase(@"http://uatmaintws1:8064/DocumentTypeService.svc", "DocumentTypeService")]
        [TestCase(@"http://uatmaintws1:8064/DocumentSubTypeService.svc", "DocumentSubTypeService")]
        [TestCase(@"http://pbsreports:80/SQLReports2008/ReportExecution2005.asmx", "ReportExecutionServiceSoap")]
        [TestCase(@"http://localhost:8073/NewRegistrationRequestService.svc", "BasicHttpBinding_INewRegistrationRequestService")]
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

        [TestCase("BusinessSafe.Messages.Emails", "businesssafe.messagehandlers.emails")]
        [TestCase("BusinessSafe.Messages", "businesssafe.messagehandlers")]
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

        [TestCase("DBSubscriptionStorageConfig", "connection.connection_string", @"Initial Catalog=NServiceBus;Data Source=UATSQL2;UID=intranetadmin;PWD=intadpas;Connect Timeout=20;")]
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
            string fileName = "Web.CI.Transformed.config";
            string transformedFilePath = string.Empty;

            transformedFilePath = string.IsNullOrEmpty(BuildDirectoryLocations.GetBuildDirectoryLocation(System.Environment.MachineName)) ?
                @"C:\BusinessSafe\Artifacts\BusinessSafe.Website\Configs\Web.CI.Transformed.config"
                : string.Format(@"{0}\BusinessSafe.Website\{1}", BuildDirectoryLocations.GetBuildDirectoryLocation(System.Environment.MachineName), fileName);

            return XDocument.Load(transformedFilePath, LoadOptions.None);
        }
    }
}
