using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Tests.Builder;
using BusinessSafe.WebSite.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.GeneralRiskAssessment
{
    [TestFixture]
    public class CreateTests
    {
        private Mock<IGeneralRiskAssessmentService> _generalRiskAssessmentService;
        private Mock<ISearchRiskAssessmentViewModelFactory> _searchRiskAssessmentViewModelFactory;

        [SetUp]
        public void SetUp()
        {
            _generalRiskAssessmentService = new Mock<IGeneralRiskAssessmentService>();
            _searchRiskAssessmentViewModelFactory = new Mock<ISearchRiskAssessmentViewModelFactory>();
        }

        [Test]
        public void When_get_create_Then_should_return_correct_view()
        {
            //Given
            var target = GetTarget();

            //When
            var result = target.Create(1) as ViewResult;

            //Then
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.Model, Is.TypeOf<CreatingRiskAssessmentSummaryViewModel>());
        }

      

        [Test]
        public void Given_that_risk_assessment_service_encouters_validation_when_creating_Then_should_return_to_same_view_with_correct_view_model()
        {
            //Given
            var target = GetTarget();

            var request = CreatingRiskAssessmentSummaryViewModelBuilder
                               .Create()
                               .Build();

            const string expectedValidationFail = "some validation fail";
            _generalRiskAssessmentService.Setup(r => r.CreateRiskAssessment(It.IsAny<CreateRiskAssessmentRequest>()))
                                  .Throws(new ValidationException(new List<ValidationFailure>(){ new ValidationFailure("",expectedValidationFail)}));
                

            //When
            var result = target.Create(request) as ViewResult;

            //Then
            Assert.That(result.ViewName, Is.EqualTo("Create"));

            var viewModel = result.Model as CreatingRiskAssessmentSummaryViewModel;

            var errorMessages = target.ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);
            Assert.That(target.ModelState.Count(), Is.EqualTo(1));
            Assert.That(errorMessages.Count(x => x == expectedValidationFail), Is.EqualTo(1));
            
            Assert.That(viewModel.CompanyId, Is.EqualTo(request.CompanyId));
            Assert.That(viewModel.Reference, Is.EqualTo(request.Reference));
            Assert.That(viewModel.Title, Is.EqualTo(request.Title));

        }

        [Test]
        public void Given_valid_request_Then_risk_assessment_is_saved()
        {
            //Given
            var target = GetTarget();

            var request = CreatingRiskAssessmentSummaryViewModelBuilder
                               .Create()
                               .Build();

            //When
            target.Create(request);

            //Then
            _generalRiskAssessmentService.Verify(r => r.CreateRiskAssessment(It.IsAny<CreateRiskAssessmentRequest>()), Times.Once());
        }
        
        [Test]
        public void Given_valid_request_Then_risk_assessment_is_saved_and_redirected_to_correct_view()
        {
            //Given
            var target = GetTarget();

            var request = CreatingRiskAssessmentSummaryViewModelBuilder
                               .Create()
                               .WithCompanyId(100)
                               .Build();

            const long expectedRiskAssessmentId = 2020;
            _generalRiskAssessmentService.Setup(x => x.CreateRiskAssessment(It.IsAny<CreateRiskAssessmentRequest>()))
                                  .Returns(expectedRiskAssessmentId);

            //When
            var result = target.Create(request) as RedirectToRouteResult;

            //Then
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Summary"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["riskAssessmentId"], Is.EqualTo(expectedRiskAssessmentId));
            
        }
      
        private RiskAssessmentController GetTarget()
        {
            var result = new RiskAssessmentController(_generalRiskAssessmentService.Object, _searchRiskAssessmentViewModelFactory.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}