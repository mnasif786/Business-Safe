using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ControlMeasures
{
    [TestFixture]
    [Category("Unit")]
    public class AddControlMeasureToRiskAssessmentHazardTests
    {
        private Mock<IRiskAssessmentHazardService> _riskAssessmentHazardService;
        private const long CompanyId = 500;
        private const long RiskAssessmentId = 9991;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentHazardService = new Mock<IRiskAssessmentHazardService>();
        }

        [Test]
        public void When_AddControlMeasureToRiskAssessmentHazard_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new SaveControlMeasureViewModel()
                                {
                                    CompanyId =  CompanyId,
                                    RiskAssessmentId = RiskAssessmentId,
                                    ControlMeasure = "Hello",
                                    RiskAssessmentHazardId = 10,
                                };
            var userId = controller.CurrentUser.UserId;

            // When
            controller.AddControlMeasureToRiskAssessmentHazard(viewModel);

            // Then
            _riskAssessmentHazardService
                .Verify(x => x.AddControlMeasureToRiskAssessmentHazard(It.Is<AddControlMeasureRequest>(y => y.CompanyId == viewModel.CompanyId &&
                                                                                                            y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                                            y.ControlMeasure == viewModel.ControlMeasure &&
                                                                                                            y.UserId == userId &&
                                                                                                            y.RiskAssessmentHazardId == viewModel.RiskAssessmentHazardId)));
        }

        [Test] public void When_AddControlMeasureToRiskAssessmentHazard_Then_should_return_correct_result()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new SaveControlMeasureViewModel()
            {
                CompanyId = CompanyId,
                RiskAssessmentId = RiskAssessmentId,
                ControlMeasure = "Hello",
                RiskAssessmentHazardId = 10,
            };

            _riskAssessmentHazardService
                .Setup(x => x.AddControlMeasureToRiskAssessmentHazard(It.IsAny<AddControlMeasureRequest>()))
                .Returns(400);

            // When
            var result = controller.AddControlMeasureToRiskAssessmentHazard(viewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(),Contains.Substring("{ Success = True, Id = 400 }"));
        }

        [Test]
        public void Given_invalid_viewmodel_When_AddControlMeasureToRiskAssessmentHazard_Then_should_return_correct_result()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new SaveControlMeasureViewModel();

            controller.ModelState.AddModelError("Any", "Any");

            // When
            var result = controller.UpdateControlMeasureForRiskAssessmentHazard(viewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("Success = false"));
        }

        private ControlMeasuresController GetTarget()
        {
            var result = new ControlMeasuresController(null,_riskAssessmentHazardService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}