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
    public class RemoveControlMeasureFromRiskAssessmentHazardTests
    {
        private Mock<IRiskAssessmentHazardService> _riskAssessmentHazardService;
        private long RiskAssessmentHazardId = 757;
        private long ControlMeasureId = 88;
        private const long CompanyId = 500;
        private const long RiskAssessmentId = 9991;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentHazardService = new Mock<IRiskAssessmentHazardService>();
        }

        [Test]
        public void When_RemoveControlMeasureFromRiskAssessmentHazard_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();
            var viewModel = new SaveControlMeasureViewModel()
                                {
                                    CompanyId = CompanyId,
                                    RiskAssessmentId = RiskAssessmentId,
                                    ControlMeasureId = ControlMeasureId,
                                    RiskAssessmentHazardId = RiskAssessmentHazardId,
                                };
            var userId = controller.CurrentUser.UserId;

            // When
            controller.RemoveControlMeasureFromRiskAssessmentHazard(RiskAssessmentId, RiskAssessmentHazardId, ControlMeasureId,CompanyId);

            // Then
            _riskAssessmentHazardService
                .Verify(x => x.RemoveControlMeasureFromRiskAssessmentHazard(It.Is<RemoveControlMeasureRequest>(y => y.CompanyId == viewModel.CompanyId &&
                                                                                                            y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                                            y.UserId == userId &&
                                                                                                            y.RiskAssessmentHazardId == viewModel.RiskAssessmentHazardId &&
                                                                                                            y.ControlMeasureId == ControlMeasureId)));
        }

        [Test]
        public void When_RemoveControlMeasureFromRiskAssessmentHazard_Then_should_return_correct_result()
        {
            // Given
            var controller = GetTarget();
            
            // When
            var result = controller.RemoveControlMeasureFromRiskAssessmentHazard(RiskAssessmentId, RiskAssessmentHazardId, ControlMeasureId,CompanyId) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ Success = True }"));
        }
        
        private ControlMeasuresController GetTarget()
        {
            var result = new ControlMeasuresController(null, _riskAssessmentHazardService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}