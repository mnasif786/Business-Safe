using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.GeneralRiskAssessment.ControlMeasures
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private Mock<IGeneralRiskAssessmentService> _riskAssessmentService;
        private const long CompanyId = 500;
        private const long RiskAssessmentId = 9991;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentService = new Mock<IGeneralRiskAssessmentService>();
            var riskAssessment = new GeneralRiskAssessmentDto();
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessmentWithHazards(RiskAssessmentId, CompanyId))
                .Returns(riskAssessment);
        }

        [Test]
        public void When_get_control_measures_Then_should_return_index_view()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Index(CompanyId, RiskAssessmentId) as ViewResult;

            // Then
            Assert.That(result.ViewName,Is.EqualTo("Index"));
        }

        [Test]
        public void When_get_control_measures_Then_should_return_ControlMeasuresViewModel()
        {
            // Given
            var controller = GetTarget();
            
            // When
            var result = controller.Index(CompanyId, RiskAssessmentId) as ViewResult;

            // Then
            Assert.That(result.Model, Is.TypeOf<ControlMeasuresViewModel>());
            
        }

        [Test]
        public void When_get_control_measures_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();
            
            // When
            controller.Index(CompanyId, RiskAssessmentId);

            // Then
            _riskAssessmentService.VerifyAll();
            
        }
        

        private ControlMeasuresController GetTarget()
        {
            var result = new ControlMeasuresController(_riskAssessmentService.Object, null);
            return TestControllerHelpers.AddUserToController(result);
        }    
    }
}