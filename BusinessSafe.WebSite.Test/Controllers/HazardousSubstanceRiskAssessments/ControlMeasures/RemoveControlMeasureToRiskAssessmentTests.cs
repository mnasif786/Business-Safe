using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.ControlMeasures
{
    [TestFixture]
    [Category("Unit")]
    public class RemoveControlMeasureToRiskAssessmentTests
    {
        private Mock<IHazardousSubstanceRiskAssessmentService> _riskAssessmentService;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;
        private long _controlMeasureId = 300;
        private ControlMeasuresController _target;
        
        [SetUp]
        public void SetUp()
        {
            _riskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _target = GetTarget();
        }

        [Test]
        public void When_remove_control_measures_Then_should_call_correct_methods()
        {
            // Given

            var viewModel = new SaveControlMeasureViewModel()
                                {
                                    ControlMeasureId = _controlMeasureId,
                                    RiskAssessmentId = _riskAssessmentId,
                                    CompanyId = _companyId,
                                };

            _riskAssessmentService
                .Setup(x => x.RemoveControlMeasureFromRiskAssessment(It.Is<RemoveControlMeasureRequest>(r =>
                                                                                      r.ControlMeasureId == viewModel.ControlMeasureId &&
                                                                                      r.CompanyId == viewModel.CompanyId &&
                                                                                      r.RiskAssessmentId == viewModel.RiskAssessmentId )));


            // When
            _target.RemoveControlMeasureFromRiskAssessment(_riskAssessmentId, _controlMeasureId, _companyId);

            // Then
            _riskAssessmentService.VerifyAll();
        }

        [Test]
        public void When_remove_control_measures_Then_should_return_correct_result()
        {
            // Given
            var viewModel = new SaveControlMeasureViewModel()
                                {
                                    ControlMeasureId = 200,
                                    RiskAssessmentId = _riskAssessmentId,
                                    CompanyId = _companyId,
                                    ControlMeasure = "Hello Control Measure"
                                };

            _riskAssessmentService
                .Setup(
                    x =>
                    x.RemoveControlMeasureFromRiskAssessment(
                        It.Is<RemoveControlMeasureRequest>(r => r.ControlMeasureId == viewModel.ControlMeasureId &&
                                                                r.CompanyId ==
                                                                viewModel.CompanyId &&
                                                                r.RiskAssessmentId ==
                                                                viewModel.
                                                                    RiskAssessmentId )));

            // When
            var result = _target.RemoveControlMeasureFromRiskAssessment(_riskAssessmentId, _controlMeasureId, _companyId) as JsonResult;


            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ Success = True }"));
        }

        private ControlMeasuresController GetTarget()
        {
            var result = new ControlMeasuresController(null, _riskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}