using System;
using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;

using FluentValidation;
using FluentValidation.Results;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.DescriptionController
{
    [TestFixture]
    [Category("Unit")]
    public class SaveTests
    {
        private Mock<IHazardousSubstanceDescriptionViewModelFactory> _viewModelFactory;
        private Mock<IHazardousSubstanceRiskAssessmentService> _hazardousSubstanceRiskAssessmentService;
        private long _companyId;
        private long _riskAssessmentId;

        [SetUp]
        public void Setup()
        {
            _viewModelFactory = new Mock<IHazardousSubstanceDescriptionViewModelFactory>();
            _hazardousSubstanceRiskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _companyId = 200;
            _riskAssessmentId = 400;
        }

        [Test]
        public void Given_service_encouters_validation_exception_When_save_is_called_Then_should_return_correct_result()
        {
            //Given
            var target = CreateController();
            var viewModel = new DescriptionViewModel()
            {
                CompanyId = _companyId,
                RiskAssessmentId = _riskAssessmentId
            };

            var failures = new List<ValidationFailure>();
            _hazardousSubstanceRiskAssessmentService
                .Setup(x => x.UpdateRiskAssessmentDescription(It.IsAny<SaveHazardousSubstanceRiskAssessmentRequest>()))
                .Throws(new ValidationException(failures));

            //When
            var result = target.Save(viewModel) as ViewResult;

            //Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            var returnedViewModel = result.Model as DescriptionViewModel;
            Assert.That(returnedViewModel, Is.EqualTo(viewModel));
        }

        [Test]
        public void Given_valid_request_When_save_is_called_Then_should_call_the_correct_methods()
        {
            //Given
            var target = CreateController();
            var request = new DescriptionViewModel()
                              {
                                  CompanyId = _companyId,
                                  RiskAssessmentId = _riskAssessmentId
                              };

            //When
            target.Save(request);

            //Then
            _hazardousSubstanceRiskAssessmentService
                .Verify(x => x.UpdateRiskAssessmentDescription(It.Is<SaveHazardousSubstanceRiskAssessmentRequest>(r => 
                                                                                                       r.CompanyId == request.CompanyId && 
                                                                                                       r.Id == request.RiskAssessmentId && 
                                                                                                       r.UserId == target.CurrentUser.UserId)));

        }

        [Test]
        public void Given_valid_request_Then_risk_assessment_is_saved_and_redirected_to_correct_view()
        {
            //Given
            var target = CreateController();

            var request = new DescriptionViewModel()
            {
                CompanyId = _companyId,
                RiskAssessmentId = _riskAssessmentId
            };
            
            //When
            var result = target.Save(request) as RedirectToRouteResult;

            //Then
            Assert.That(target.TempData["Notice"], Is.Not.Null);
            Assert.That(result.RouteValues["controller"], Is.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["riskAssessmentId"], Is.EqualTo(request.RiskAssessmentId));
            Assert.That(result.RouteValues["CompanyId"], Is.EqualTo(request.CompanyId));

        }

        private WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers.DescriptionController CreateController()
        {
            var result = new WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers.DescriptionController(_hazardousSubstanceRiskAssessmentService.Object, _viewModelFactory.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}