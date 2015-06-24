using System;
using System.IO;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.Models;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Helpers
{
    [TestFixture]
    public class ReviewAuditDocumentHelperTests
    {
        private ReviewAuditDocumentHelper target;
        private Mock<ISqlReportExecutionServiceFacade> _sqlReportsService;
        private Mock<IDocumentLibraryUploader> _documentLibraryUploader;

        [SetUp]
        public void Setup()
        {
            _sqlReportsService = new Mock<ISqlReportExecutionServiceFacade>();
            _documentLibraryUploader = new Mock<IDocumentLibraryUploader>();
            target = new ReviewAuditDocumentHelper(_sqlReportsService.Object, _documentLibraryUploader.Object);
        }

        [Test]
        [TestCase(1, 999, RiskAssessmentType.GRA, SqlReportHelper.ReportType.GRA)]
        [TestCase(1, 999, RiskAssessmentType.HSRA, SqlReportHelper.ReportType.HSRA)]
        [TestCase(1, 999, RiskAssessmentType.PRA, SqlReportHelper.ReportType.PRA)]
        [TestCase(1, 999, RiskAssessmentType.FRA, SqlReportHelper.ReportType.FRA)]
        public void When_CreateReviewAuditDocument_Then_should_call_correct_methods(long riskAssessmentId, long reviewId, RiskAssessmentType raType, SqlReportHelper.ReportType sqlReportType)
        {
            // Given
            var riskAssessmentDto = new RiskAssessmentDto()
                                        {
                                            Id = riskAssessmentId,
                                            Reference = "Test Reference",
                                            Title = "Test Tile"
                                        };

            var documentViewModel = new DocumentViewModel()
                                        {
                                            FileName = "Test File Name.pdf",
                                            FileStream = new MemoryStream(100)
                                        };

            var expectedStartOfFileName = string.Format("{0}_{1}_", riskAssessmentDto.Title, riskAssessmentDto.Reference);
            var expectedEndOfFileName = ".pdf";

            _sqlReportsService
                .Setup(x => x.GetReport(sqlReportType, It.IsAny<object[]>(), SqlReportHelper.ReportFormatType.PDF))
                .Returns(documentViewModel);

            //When
            target.CreateReviewAuditDocument(raType, riskAssessmentDto);



            //Then
            _sqlReportsService.VerifyAll();
            _documentLibraryUploader.Verify(x => x.Upload(It.Is<string>(y => y.StartsWith(expectedStartOfFileName) && y.EndsWith(expectedEndOfFileName) && y.Contains(DateTime.Now.ToString("dd_MM_yyyy"))), It.IsAny<Stream>()));

        }

        //[Test]
        //public void when_risk_assessment_has_no_site()
        //{
        //    // Given
        //    _riskAssessmentReviewService
        //        .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), TestControllerHelpers.CompanyIdAssigned))
        //        .Returns(new RiskAssessmentReviewDto()
        //        {
        //            RiskAssessment = new RiskAssessmentDto()
        //            {
        //                Title = "GRA title",
        //                Reference = "GRA ref"
        //            }
        //        });

        //    var passedCompleteRiskAssessmentReviewRequest = new CompleteRiskAssessmentReviewRequest();

        //    _riskAssessmentReviewService
        //        .Setup(x => x.CompleteRiskAssessementReview(It.IsAny<CompleteRiskAssessmentReviewRequest>()))
        //        .Callback<CompleteRiskAssessmentReviewRequest>(y => passedCompleteRiskAssessmentReviewRequest = y);

        //    var target = GetTarget();

        //    //When
        //    target.Complete(
        //        new CompleteReviewViewModel()
        //        {
        //            CompanyId = TestControllerHelpers.CompanyIdAssigned,
        //            RiskAssessmentId = 1,
        //            RiskAssessmentReviewId = 2,
        //            IsComplete = true,
        //            CompletedComments = "complete",
        //            NextReviewDate = DateTime.Today,
        //            RiskAssessmentType = RiskAssessmentType.GRA
        //        });

        //    var passedCreateDocRequest = passedCompleteRiskAssessmentReviewRequest.CreateDocumentRequests.First();

        //    //Then
        //    Assert.That(passedCreateDocRequest.SiteId, Is.EqualTo(default(long)));
        //}
    }
}