using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.PremisesInformationTests
{
    [TestFixture]
    public class ViewTests
    {
        private Mock<IPremisesInformationViewModelFactory> _viewModelFactory;
        private Mock<IMultiHazardRiskAssessmentService> _riskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _viewModelFactory = new Mock<IPremisesInformationViewModelFactory>();
            
            _viewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithCurrentUserId(It.IsAny<Guid>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new PremisesInformationViewModel());

            _riskAssessmentService = new Mock<IMultiHazardRiskAssessmentService>();
        }

        [Test]
        public void Sets_IsReadOnly_to_true()
        {
            // Given
            var riskAssessmentId = 1234;
            var companyId = 5678;
            var target = GetTarget();

            // When
            var result = target.View(riskAssessmentId, companyId) as ViewResult;

            // Then
            dynamic viewBag = result.ViewBag;
            Assert.That(viewBag.IsReadOnly, Is.True);
        }

        private PremisesInformationController GetTarget()
        {
            var controller = new PremisesInformationController(_viewModelFactory.Object, _riskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
