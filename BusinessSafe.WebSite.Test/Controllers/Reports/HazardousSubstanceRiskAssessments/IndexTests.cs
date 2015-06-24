using System;
using System.IO;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.SqlReports.Controllers;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using BusinessSafe.WebSite.Custom_Exceptions;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Reports.HazardousSubstanceRiskAssessments
{
    [TestFixture]
    public class IndexTests
    {
        private Mock<ISqlReportExecutionServiceFacade> _reportExecutionServiceFacade;
        private Mock<IHazardousSubstanceRiskAssessmentService> _riskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new HazardousSubstanceRiskAssessmentDto() { Site = new SiteStructureElementDto() { Id = 1234 }, Title = "Bilbo Baggins" });

            _reportExecutionServiceFacade = new Mock<ISqlReportExecutionServiceFacade>();
            _reportExecutionServiceFacade
                .Setup(x => x.GetReport(
                It.IsAny<SqlReportHelper.ReportType>(),
                It.IsAny<object[]>(),
                It.IsAny<SqlReportHelper.ReportFormatType>()
            ))
                .Returns(new DocumentViewModel()
                {
                    FileName = "filename",
                    FileStream = new MemoryStream(),
                    MimeType = "mimetype"
                });
        }

        [Test]
        public void When_Index_Then_calls_ReportService_with_expected_parameters()
        {
            // Given
            const long riskAssessmentId = 1234;

            var target = GetTarget();

            object[] passedParameters = new object[] { };
            
            _reportExecutionServiceFacade
                .Setup(x => x.GetReport(
                    It.IsAny<SqlReportHelper.ReportType>(),
                    It.IsAny<object[]>(),
                    It.IsAny<SqlReportHelper.ReportFormatType>()
                ))
                .Callback(
                    (SqlReportHelper.ReportType reportType, object[] parameters, SqlReportHelper.ReportFormatType reportFormatType) => passedParameters = parameters)
                .Returns(new DocumentViewModel()
                {
                    FileName = "filename",
                    FileStream = new MemoryStream(),
                    MimeType = "mimetype"
                });

            // When
            target.Index(riskAssessmentId);

            // Then
            _reportExecutionServiceFacade.Verify(x => x.GetReport(
                SqlReportHelper.ReportType.HSRA,
                It.IsAny<object[]>(),
                SqlReportHelper.ReportFormatType.PDF));
            Assert.That((long.Parse(passedParameters[0].ToString())), Is.EqualTo(riskAssessmentId));
        }

        [Test]
        public void When_Index_Then_retrieves_RiskAssessment_with_correct_name()
        {
            // Given
            const long riskAssessmentId = 1234;

            var target = GetTarget();

            var user = target.CurrentUser;

            // When
            target.Index(riskAssessmentId);

            // Then
            _riskAssessmentService.Verify(x => x.GetRiskAssessment(riskAssessmentId, user.CompanyId));
        }

        
        [Test]
        public void When_RiskAssessment_Not_In_Users_Allowed_Sites_Then_Throw_Exception()
        {
            // Given
            const long riskAssessmentId = 1234;

            var riskAssessment = new HazardousSubstanceRiskAssessmentDto()
            {
                Id = 1,
                Site = new SiteStructureElementDto(){Id = 2} 
            };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(riskAssessmentId, 1))
                .Returns(riskAssessment);

            var target = GetTarget();

            // When
            // Then
            Assert.Throws<SitePermissionsInvalidForUserException>(() => target.Index(riskAssessmentId));

            _riskAssessmentService.Verify(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()));
        }

        [Test]
        public void When_valid_RiskAssessment_requested_Then_return_FileResult()
        {
            // Given
            const long riskAssessmentId = 1234;
            var target = GetTarget();
            const string filename = "filename";
            const string mimeType = "mimetype";

            _reportExecutionServiceFacade
                .Setup(x => x.GetReport(
                    It.IsAny<SqlReportHelper.ReportType>(),
                    new object[] { riskAssessmentId },
                    It.IsAny<SqlReportHelper.ReportFormatType>()
                                ))
                .Returns(new DocumentViewModel()
                             {
                                 FileName = filename,
                                 FileStream = new MemoryStream(),
                                 MimeType = mimeType
                             });

            // When
            var result = target.Index(riskAssessmentId);

            // Then
            Assert.IsInstanceOf<FileResult>(result);
            Assert.That(result.ContentType, Is.EqualTo(mimeType));
        }

        [Test]
        public void When_RiskAssessment_With_No_SiteId_requested_That_The_User_Created_Then_return_FileResult()
        {
            // Given
            const long riskAssessmentId = 1234;

            var target = GetTarget();

            var riskAssessment = new HazardousSubstanceRiskAssessmentDto()
            {
                Id = 1,
                Site = null,
                Title = "My GRA",
                CreatedBy = new AuditedUserDto()
                {
                    Id = target.CurrentUser.UserId
                }
            };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(riskAssessmentId, 1))
                .Returns(riskAssessment);


            // When
            var result = target.Index(riskAssessmentId);

            // Then
            Assert.IsInstanceOf<FileResult>(result);
        }

        [Test]
        public void When_RiskAssessment_With_No_SiteId_requested_That_Was_Created_By_Another_User_Then_Throw_Exception()
        {
            // Given
            const long riskAssessmentId = 1234;

            var target = GetTarget();

            var riskAssessment = new HazardousSubstanceRiskAssessmentDto()
            {
                Id = 1,
                Site = null,
                Title = "My GRA",
                CreatedBy = new AuditedUserDto()
                {
                    Id = Guid.NewGuid()
                }
            };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(riskAssessmentId, 1))
                .Returns(riskAssessment);


            // When
            // Then
            Assert.Throws<SitePermissionsInvalidForUserException>(() => target.Index(riskAssessmentId));
        }

        [Test]
        [TestCase("My Risk Assessment!*", "My Risk Assessment!.pdf")]
        public void When_valid_RiskAssessment_requested_Then_returned_filename_is_RiskAssessment_Sanitised_Title(string input, string output)
        {
            // Given
            var target = GetTarget();
            const string filename = "filename";
            const string mimeType = "mimetype";

            var riskAssessment = new HazardousSubstanceRiskAssessmentDto()
            {
                Id = 1234,
                Title = input,
                CreatedBy = new AuditedUserDto()
                            {
                                Id = target.CurrentUser.UserId
                            }
            };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(riskAssessment.Id, 1))
                .Returns(riskAssessment);

            _reportExecutionServiceFacade
                .Setup(x => x.GetReport(
                    It.IsAny<SqlReportHelper.ReportType>(),
                    new object[] { riskAssessment.Id },
                    It.IsAny<SqlReportHelper.ReportFormatType>()
                                ))
                .Returns(new DocumentViewModel()
                {
                    FileName = filename,
                    FileStream = new MemoryStream(),
                    MimeType = mimeType
                });

            // When
            var result = target.Index(riskAssessment.Id);

            // Then
            Assert.That(result.FileDownloadName, Is.EqualTo(output));
        }

        private HazardousSubstanceRiskAssessmentController GetTarget()
        {
            var result = new HazardousSubstanceRiskAssessmentController(_riskAssessmentService.Object, _reportExecutionServiceFacade.Object);

            return TestControllerHelpers.AddUserWithPopulatedGetSitesFilterToController(result);
        }
    }
}