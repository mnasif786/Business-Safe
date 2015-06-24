using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Areas.SqlReports.Controllers;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.SqlReports
{
    [TestFixture]
    public class AccdientRecordsControllerTests
    {
        private Mock<ISqlReportExecutionServiceFacade> _sqlReportFacade;
        private Mock<IAccidentRecordService> _accidentRecordService;
        private DocumentViewModel _documentViewModel;

        [SetUp]
        public void Setup()
        {
            _sqlReportFacade = new Mock<ISqlReportExecutionServiceFacade>();
            _sqlReportFacade
                .Setup(x => x.GetReport(It.IsAny<SqlReportHelper.ReportType>(), It.IsAny<object[]>(), It.IsAny<SqlReportHelper.ReportFormatType>()))
                .Returns(() => _documentViewModel);


            _accidentRecordService = new Mock<IAccidentRecordService>();
            _documentViewModel = new DocumentViewModel();
            _documentViewModel.MimeType = "pdf";
            _documentViewModel.FileName = "";
            _documentViewModel.FileStream = new MemoryStream();
        }

        [Test]
        public void Given_accident_records_search_criteria_when_print_then_file_is_generated_and_returned()
        {
            //GIVEN
            var searchCriteria = new AccidentRecordsIndexViewModel() {SiteId = 145145L};
            var accidentRecordDtos = new List<AccidentRecordDto>() {new AccidentRecordDto() {Id = 4}, new AccidentRecordDto() {Id = 5}};

            _accidentRecordService.Setup(x => x.Search(It.IsAny<SearchAccidentRecordsRequest>()))
                .Returns(() => accidentRecordDtos);
            
            object[] reportParameters = null;

            _sqlReportFacade
                .Setup(x => x.GetReport(It.IsAny<SqlReportHelper.ReportType>(), It.IsAny<object[]>(), It.IsAny<SqlReportHelper.ReportFormatType>()))
                .Callback<SqlReportHelper.ReportType, object[], SqlReportHelper.ReportFormatType>((p1, p2, p3) => reportParameters = p2)
                .Returns(() => _documentViewModel);

            var target = GetTarget();

            //WHEN
            target.Pdf(searchCriteria);

            //THEN
            _sqlReportFacade.Verify(x => x.GetReport(SqlReportHelper.ReportType.AccidentRecords, It.IsAny<object[]>(), SqlReportHelper.ReportFormatType.PDF));
            Assert.That(reportParameters.Length, Is.EqualTo(1));
            Assert.That(((string)reportParameters[0]), Is.EqualTo("4,5"));
        }

        [Test]
        public void Given_accident_records_search_criteria_when_print_then_correct_search_request_generated()
        {
            //GIVEN
            var searchCriteria = new AccidentRecordsIndexViewModel() { SiteId = 145145L, CreatedFrom = "14/04/2013", CreatedTo = "15/04/2014"};
            var accidentRecordDtos = new List<AccidentRecordDto>() { new AccidentRecordDto() { Id = 12312 }, new AccidentRecordDto() { Id = 12312 } };

            SearchAccidentRecordsRequest searchRequest = null;

            _accidentRecordService.Setup(x => x.Search(It.IsAny<SearchAccidentRecordsRequest>()))
                .Callback<SearchAccidentRecordsRequest>((p1)=> searchRequest = p1)
               .Returns(() => accidentRecordDtos);


            var target = GetTarget();

            //WHEN
            target.Pdf(searchCriteria);

            //THEN
            Assert.That(searchRequest.CompanyId, Is.EqualTo(target.CurrentUser.CompanyId));
            Assert.That(searchRequest.SiteId, Is.EqualTo(searchCriteria.SiteId));
            Assert.That(searchRequest.CreatedFrom, Is.EqualTo(DateTime.Parse(searchCriteria.CreatedFrom)));
            Assert.That(searchRequest.CreatedTo, Is.EqualTo(DateTime.Parse(searchCriteria.CreatedTo)));
            Assert.That(searchRequest.AllowedSiteIds, Is.EqualTo(target.CurrentUser.GetSitesFilter()));

        }

        private AccidentRecordsController GetTarget()
        {
            var controller = new AccidentRecordsController(_sqlReportFacade.Object, _accidentRecordService.Object);
            TestControllerHelpers.AddUserToController(controller);
            return controller;
        }
    }
}
