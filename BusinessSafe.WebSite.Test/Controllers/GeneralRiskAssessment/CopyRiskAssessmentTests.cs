using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;

using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.GeneralRiskAssessment
{
    [TestFixture]
    [Category("Unit")]
    public class CopyRiskAssessmentTests
    {
        private Mock<IGeneralRiskAssessmentService> _riskAssessmentService;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;
        private string _reference = "Test Reference";
        
        [SetUp]
        public void SetUp()
        {
            _riskAssessmentService = new Mock<IGeneralRiskAssessmentService>();
        }

        [Test]
        public void Given_valid_request_When_copy_risk_assessment_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();
            controller.CurrentUser.CompanyId = _companyId;
            var title = "the title";
            var viewModel = new CopyRiskAssessmentViewModel {Reference = _reference, RiskAssessmentId = _riskAssessmentId, Title = title};

            // When
            controller.Copy(viewModel);

            // Then
            _riskAssessmentService.Verify(x => x.CopyRiskAssessment(It.Is<CopyRiskAssessmentRequest>(y => y.CompanyId == _companyId 
                && y.RiskAssessmentToCopyId == _riskAssessmentId 
                && y.Reference == _reference
                && y.Title == title)));
        }

        [Test]
        public void Given_valid_request_When_copy_risk_assessment_Then_should_return_correct_result()
        {
            // Given
            var controller = GetTarget();
            
            long expectedResult = 300;
            _riskAssessmentService
                .Setup(x => x.CopyRiskAssessment(It.IsAny<CopyRiskAssessmentRequest>()))
                .Returns(expectedResult);

            var viewModel = new CopyRiskAssessmentViewModel { Reference = _reference, RiskAssessmentId = _riskAssessmentId, Title = "" };

            // When
            var result = controller.Copy(viewModel) as JsonResult;

            // Then
            dynamic data = result.Data;
            Assert.That(data.ToString(), Contains.Substring("Success = True"));
            Assert.That(data.ToString(), Contains.Substring("Id = " + expectedResult));
        }

        private RiskAssessmentController GetTarget()
        {
            var result = new RiskAssessmentController(_riskAssessmentService.Object,  null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}