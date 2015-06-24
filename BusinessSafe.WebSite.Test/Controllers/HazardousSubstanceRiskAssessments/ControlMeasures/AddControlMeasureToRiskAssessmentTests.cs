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
    public class AddControlMeasureToRiskAssessmentTests
    {
        private Mock<IHazardousSubstanceRiskAssessmentService> _riskAssessmentService;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;
        private ControlMeasuresController _target;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _target = GetTarget();
        }

        [Test]
        public void When_add_control_measures_Then_should_call_correct_methods()
        {
            // Given
            
            var viewModel = new SaveControlMeasureViewModel()
                                {
                                    RiskAssessmentId = _riskAssessmentId,
                                    CompanyId = _companyId,
                                    ControlMeasure = "Hello Control Measure"
                                };

            _riskAssessmentService
                .Setup(x => x.AddControlMeasureToRiskAssessment(It.Is<AddControlMeasureRequest>(r =>
                                                                                                r.CompanyId ==
                                                                                                viewModel.CompanyId &&
                                                                                                r.RiskAssessmentId ==
                                                                                                viewModel.
                                                                                                    RiskAssessmentId &&
                                                                                                r.ControlMeasure ==
                                                                                                viewModel.ControlMeasure)))
                .Returns(200);

            // When
            _target.AddControlMeasureToRiskAssessment(viewModel);

            // Then
            _riskAssessmentService.VerifyAll();
        }

        [Test]
        public void When_add_control_measures_Then_should_return_correct_result()
        {
            // Given
            var viewModel = new SaveControlMeasureViewModel()
            {
                RiskAssessmentId = _riskAssessmentId,
                CompanyId = _companyId,
                ControlMeasure = "Hello Control Measure"
            };

            _riskAssessmentService
                .Setup(x => x.AddControlMeasureToRiskAssessment(It.Is<AddControlMeasureRequest>(r =>
                                                                                    r.CompanyId ==
                                                                                    viewModel.CompanyId &&
                                                                                    r.RiskAssessmentId ==
                                                                                    viewModel.
                                                                                        RiskAssessmentId &&
                                                                                    r.ControlMeasure ==
                                                                                    viewModel.ControlMeasure)))
                .Returns(200);

            // When
            var result = _target.AddControlMeasureToRiskAssessment(viewModel) as JsonResult;
            

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ Success = True, Id = 200 }"));
        }
        
        private ControlMeasuresController GetTarget()
        {
            var result = new ControlMeasuresController( null, _riskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}