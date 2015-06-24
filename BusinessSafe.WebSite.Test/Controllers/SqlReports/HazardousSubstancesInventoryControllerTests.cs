using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.SqlReports.Controllers;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.SqlReports
{
    [TestFixture]
    public class HazardousSubstancesInventoryControllerTests
    {
        private Mock<ISqlReportExecutionServiceFacade> _sqlReportFacade;        
        private Mock<IHazardousSubstancesService> _hazardousSubstancesService;
        private DocumentViewModel _documentViewModel;

        [SetUp]
        public void Setup()
        {
            _sqlReportFacade = new Mock<ISqlReportExecutionServiceFacade>();
            _sqlReportFacade
                .Setup(x => x.GetReport(It.IsAny<SqlReportHelper.ReportType>(), It.IsAny<object[]>(), It.IsAny<SqlReportHelper.ReportFormatType>()))
                .Returns(() => _documentViewModel);


            _documentViewModel = new DocumentViewModel();
            _documentViewModel.MimeType = "pdf";
            _documentViewModel.FileName = "";
            _documentViewModel.FileStream = new MemoryStream();

            _hazardousSubstancesService = new Mock<IHazardousSubstancesService>();
        }

        [Test]
        public void Given_hazardous_substance_search_criteria_when_print_then_file_is_generated_and_returned()
        {
            //GIVEN
            string substanceName = "The Stuff";
            long supplierId = 1234;

            var hazardousSubstanceDtos = new List<HazardousSubstanceDto>()
                                             {
                                                 new HazardousSubstanceDto() { Id = 2 }, 
                                                 new HazardousSubstanceDto() { Id = 3 }
                                             };

            _hazardousSubstancesService
                .Setup( x => x.Search( It.IsAny<SearchHazardousSubstancesRequest>() ) )
                .Returns( () => hazardousSubstanceDtos );

            object[] reportParameters = null;

            _sqlReportFacade
                .Setup( x => x.GetReport( It.IsAny<SqlReportHelper.ReportType>(), It.IsAny<object[]>(), It.IsAny<SqlReportHelper.ReportFormatType>() ) )
                .Callback<SqlReportHelper.ReportType, object[], SqlReportHelper.ReportFormatType>((p1, p2, p3) => reportParameters = p2)
                .Returns(() => _documentViewModel);

            var target = GetTarget();

            //WHEN
            target.Index( substanceName, supplierId);

            //THEN
            _sqlReportFacade.Verify(x => x.GetReport(SqlReportHelper.ReportType.HazardousSubstancesInventory, It.IsAny<object[]>(), SqlReportHelper.ReportFormatType.PDF));
            Assert.That(reportParameters.Length, Is.EqualTo(1));
            Assert.That(((string)reportParameters[0]), Is.EqualTo("2,3"));
        }

        [Test]
        public void Given_hazardous_substance_search_criteria_when_print_then_correct_search_request_generated()
        {
            //GIVEN
            string substanceName = "The Stuff";
            long supplierId = 1234;

            var hazardousSubstanceDtos = new List<HazardousSubstanceDto>()
                                             {
                                                 new HazardousSubstanceDto() { Id = 2 }, 
                                                 new HazardousSubstanceDto() { Id = 3 }
                                             };

            SearchHazardousSubstancesRequest searchRequest = null;
            _hazardousSubstancesService
                .Setup(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()))
                .Callback<SearchHazardousSubstancesRequest>((p1) => searchRequest = p1)
                .Returns(() => hazardousSubstanceDtos);


            var target = GetTarget();

            //WHEN
            target.Index( substanceName, supplierId);

            //THEN
            Assert.That(searchRequest.CompanyId, Is.EqualTo(target.CurrentUser.CompanyId));
            Assert.That(searchRequest.SubstanceNameLike, Is.EqualTo(substanceName));
            Assert.That(searchRequest.SupplierId, Is.EqualTo(supplierId));
            //Assert.That(searchRequest.ShowDeleted, Is.EqualTo(showDeleted));            
        }



        private HazardousSubstancesInventoryController GetTarget()
        {
            var controller = new HazardousSubstancesInventoryController(_sqlReportFacade.Object, _hazardousSubstancesService.Object);
            TestControllerHelpers.AddUserToController(controller);
            return controller;
        }

    }
}
