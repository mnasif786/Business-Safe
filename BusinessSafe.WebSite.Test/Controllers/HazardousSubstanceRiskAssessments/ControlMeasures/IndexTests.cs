using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.ControlMeasures
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private Mock<IControlMeasuresViewModelFactory> _viewModelFactory;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;
        
        [SetUp]
        public void SetUp()
        {
            _viewModelFactory = new Mock<IControlMeasuresViewModelFactory>();
            _viewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                    .Setup(x => x.WithHazardousSubstanceRiskAssessmentId(_riskAssessmentId))
                    .Returns(_viewModelFactory.Object);
        }

        [Test]
        public void Given_get_When_control_measures_index_page_Then_should_correct_view()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Index(_companyId, _riskAssessmentId) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_get_When_control_measures_index_page_Then_should_return_correct_viewmodel()
        {
            // Given
            var controller = GetTarget();

            _viewModelFactory
               .Setup(x => x.GetViewModel())
               .Returns(new ControlMeasuresViewModel());

            // When
            var result = controller.Index(_companyId, _riskAssessmentId) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<ControlMeasuresViewModel>());
        }

        [Test]
        public void Given_get_When_hazardous_description_index_page_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();
            
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new ControlMeasuresViewModel());        

           // When
           controller.Index(_companyId, _riskAssessmentId);

           // Then
            _viewModelFactory.VerifyAll();
        }

        private WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers.ControlMeasuresController GetTarget()
        {
            return new WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers.ControlMeasuresController(_viewModelFactory.Object, null);
        }    
    }
}