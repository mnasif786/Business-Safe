using System.Web.Mvc;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;
using BusinessSafe.Application.Contracts.FireRiskAssessments;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.PremisesInformationTests
{
    [TestFixture]
    [Category("Unit")]
    public class SaveAndNextTests 
    {
        private Mock<IPremisesInformationViewModelFactory> _premisesInformationViewModelFactory;
        private Mock<IFireRiskAssessmentService> _riskAssessmentService;

        [SetUp]
        public void Setup()
        {
        
            _premisesInformationViewModelFactory = new Mock<IPremisesInformationViewModelFactory>();
            _riskAssessmentService = new Mock<IFireRiskAssessmentService>();

        }

        [Test]
        public void When_post_to_save_next_Then_should_return_correct_result()
        {
            //Given
            var target = GetTarget();
            var request = new PremisesInformationViewModel()
                              {
                                  RiskAssessmentId = 100,
                                  CompanyId = 200,
                                  PremisesProvidesSleepingAccommodation = true,
                                  PremisesProvidesSleepingAccommodationConfirmed = true
                              };

            //When
            var result = target.SaveAndNext(request) as JsonResult;

            //Then
            // Assert
            dynamic data = result.Data;
            Assert.That(data.ToString(), Contains.Substring("Success = True"));

        }

        [Test]
        public void Given_that_save_and_next_is_called_when_valid_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();

            var request = new PremisesInformationViewModel()
                              {
                                  RiskAssessmentId = 100,
                                  CompanyId = 200,
                                  PremisesProvidesSleepingAccommodation = true,
                                  PremisesProvidesSleepingAccommodationConfirmed = true
                              };

            //When
            target.SaveAndNext(request);

            //Then
            _riskAssessmentService.Verify(x => x.UpdatePremisesInformation(It.IsAny<UpdateFireRiskAssessmentPremisesInformationRequest>()), Times.Once());
        }

        private PremisesInformationController GetTarget()
        {
            var result = new PremisesInformationController(_premisesInformationViewModelFactory.Object, _riskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }

    }
}