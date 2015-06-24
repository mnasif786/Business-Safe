using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.DescriptionController
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private Mock<IHazardousSubstanceDescriptionViewModelFactory> _viewModelFactory;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;
        
        [SetUp]
        public void SetUp()
        {
            _viewModelFactory = new Mock<IHazardousSubstanceDescriptionViewModelFactory>();

            _viewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_riskAssessmentId))
                .Returns(_viewModelFactory.Object);

        }

        [Test]
        public void Given_get_When_hazardous_description_index_page_Then_should_correct_view()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Index(_riskAssessmentId, _companyId) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_get_When_hazardous_description_index_page_Then_should_return_correct_viewmodel()
        {
            // Given
            var controller = GetTarget();

            _viewModelFactory
               .Setup(x => x.GetViewModel())
               .Returns(new DescriptionViewModel());   

            // When
            var result = controller.Index(_riskAssessmentId, _companyId) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<DescriptionViewModel>());
        }

        [Test]
        public void Given_get_When_hazardous_description_index_page_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();

            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new DescriptionViewModel());        

            // When
            controller.Index(_riskAssessmentId, _companyId);

            // Then
            _viewModelFactory.VerifyAll();
        }

        private WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers.DescriptionController GetTarget()
        {
            var target = new WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers.DescriptionController(null, _viewModelFactory.Object);
            return TestControllerHelpers.AddUserToController(target);
        }    
    }
}