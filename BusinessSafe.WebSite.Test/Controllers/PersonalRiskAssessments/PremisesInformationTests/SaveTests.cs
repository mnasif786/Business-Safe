using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.PremisesInformationTests
{
    [TestFixture]
    [Category("Unit")]
    public class SaveTests
    {
        private PremisesInformationViewModel _viewModel;
        private Mock<IPremisesInformationViewModelFactory> _premisesInformationViewModelFactory;
        private Mock<IMultiHazardRiskAssessmentService> _riskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _viewModel = new PremisesInformationViewModel();

            _premisesInformationViewModelFactory = new Mock<IPremisesInformationViewModelFactory>();
            _riskAssessmentService = new Mock<IMultiHazardRiskAssessmentService>();

        }

        [Test]
        public void Given_valid_viewmodel_When_Save_called_Then_should_call_correct_methods()
        {
            // Arrange
            var controller = GetTarget();
            Guid userId = controller.CurrentUser.UserId;

            _viewModel.RiskAssessmentId = 500;
            _viewModel.CompanyId = 700;
            _viewModel.LocationAreaDepartment = "test location";
            _viewModel.TaskProcessDescription = "test task";

            // Act
            controller.Save(_viewModel);

            // Assert
            _riskAssessmentService.Verify(x => x.UpdateRiskAssessmentPremisesInformation(It.Is<SaveRiskAssessmentPremisesInformationRequest>(y => y.Id == _viewModel.RiskAssessmentId &&
                                                                                                                               y.CompanyId == _viewModel.CompanyId && 
                                                                                                                               y.LocationAreaDepartment == _viewModel.LocationAreaDepartment &&
                                                                                                                               y.TaskProcessDescription == _viewModel.TaskProcessDescription &&
                                                                                                                               y.UserId == userId)));    
        }

        [Test]
        public void Given_invalid_viewmodel_When_Save_called_Then_should_return_correct_result()
        {
            // Arrange
            var target = GetTarget();

            target.ModelState.AddModelError("Anything", "Anything");
            
            _premisesInformationViewModelFactory
               .Setup(x => x.WithCompanyId(_viewModel.CompanyId))
               .Returns(_premisesInformationViewModelFactory.Object);

            var currentUserId = target.CurrentUser.UserId;
            _premisesInformationViewModelFactory
                .Setup(x => x.WithCurrentUserId(currentUserId))
                .Returns(_premisesInformationViewModelFactory.Object);

            var expectedViewModel = new PremisesInformationViewModel();
            _premisesInformationViewModelFactory
                .Setup(x => x.GetViewModel(_viewModel))
                .Returns(expectedViewModel);

            // Act
            var result = target.Save(_viewModel) as ViewResult;

            // Assert
            Assert.That(result.Model, Is.EqualTo(expectedViewModel));
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_valid_viewmodel_When_Save_called_Then_should_return_correct_result()
        {
            // Arrange
            var controller = GetTarget();
            
            _viewModel.RiskAssessmentId = 500;
            _viewModel.CompanyId = 700;
            _viewModel.LocationAreaDepartment = "test location";
            _viewModel.TaskProcessDescription = "test task";

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