using System;
using System.Collections.Generic;
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
    public class SaveTests
    {
        private PremisesInformationViewModel _viewModel;
        private Mock<IPremisesInformationViewModelFactory> _premisesInformationViewModelFactory;
        private Mock<IFireRiskAssessmentService> _riskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _viewModel = new PremisesInformationViewModel();

            _premisesInformationViewModelFactory = new Mock<IPremisesInformationViewModelFactory>();
            _riskAssessmentService = new Mock<IFireRiskAssessmentService>();

        }

        [Test]
        public void Given_valid_viewmodel_When_Save_called_Then_should_call_correct_methods()
        {
            // Arrange
            var controller = GetTarget();
            Guid userId = controller.CurrentUser.UserId;

            _viewModel.RiskAssessmentId = 500;
            _viewModel.CompanyId = 700;
            _viewModel.PremisesProvidesSleepingAccommodation = true;
            _viewModel.PremisesProvidesSleepingAccommodationConfirmed = true;
            _viewModel.NumberOfFloors = 1;
            _viewModel.NumberOfPeople = 20;
            _viewModel.ElectricityEmergencyShutOff = "Elec";
            _viewModel.WaterEmergencyShutOff = "water";
            _viewModel.GasEmergencyShutOff = "Gas";
            _viewModel.OtherEmergencyShutOff = "Other";

            // Act
            controller.Save(_viewModel);

            // Assert
            _riskAssessmentService.Verify(
                x =>
                x.UpdatePremisesInformation(
                    It.Is<UpdateFireRiskAssessmentPremisesInformationRequest>(
                        y => 
                             y.FireRiskAssessmentId == _viewModel.RiskAssessmentId &&
                             y.CompanyId == _viewModel.CompanyId &&
                             y.PremisesProvidesSleepingAccommodation == _viewModel.PremisesProvidesSleepingAccommodation &&
                             y.PremisesProvidesSleepingAccommodationConfirmed == _viewModel.PremisesProvidesSleepingAccommodationConfirmed &&
                             y.Location == _viewModel.Location &&
                             y.BuildingUse == _viewModel.BuildingUse &&
                             y.NumberOfFloors == _viewModel.NumberOfFloors &&
                             y.NumberOfPeople == _viewModel.NumberOfPeople &&
                             y.ElectricityEmergencyShutOff == _viewModel.ElectricityEmergencyShutOff &&
                             y.WaterEmergencyShutOff == _viewModel.WaterEmergencyShutOff &&
                             y.GasEmergencyShutOff == _viewModel.GasEmergencyShutOff &&
                             y.OtherEmergencyShutOff == _viewModel.OtherEmergencyShutOff &&
                             y.CurrentUserId == userId)));
        }

        [Test]
        [Ignore("Not yet implemented")]
        public void Given_invalid_viewmodel_When_Save_called_Then_should_return_correct_result()
        {
            // Arrange
            var controller = GetTarget();

            controller.ModelState.AddModelError("Anything", "Anything");

            var expectedViewModel = new PremisesInformationViewModel();
            _premisesInformationViewModelFactory
                .Setup(x => x.GetViewModel(_viewModel))
                .Returns(expectedViewModel);

            // Act
            var result = controller.Save(_viewModel) as ViewResult;

            // Assert
            Assert.That(result.Model, Is.EqualTo(expectedViewModel));
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_valid_viewmodel_When_Save_called_Then_should_return_correct_result()
        {
            // Arrange
            var controller = GetTarget();
            Guid userId = controller.CurrentUser.UserId;

            _viewModel.RiskAssessmentId = 500;
            _viewModel.CompanyId = 700;
            _viewModel.PremisesProvidesSleepingAccommodation = true;
            _viewModel.PremisesProvidesSleepingAccommodationConfirmed = true;

            // Act
            var result = controller.Save(_viewModel) as RedirectToRouteResult;

            // Assert
            Assert.That(result.RouteValues["controller"], Is.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["riskAssessmentId"], Is.EqualTo(_viewModel.RiskAssessmentId));
            Assert.That(result.RouteValues["companyId"], Is.EqualTo(_viewModel.CompanyId));
            Assert.That(controller.TempData["Notice"], Is.EqualTo("Premises Information Successfully Saved"));
        }

        private PremisesInformationController GetTarget()
        {
            var result = new PremisesInformationController(_premisesInformationViewModelFactory.Object, _riskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}