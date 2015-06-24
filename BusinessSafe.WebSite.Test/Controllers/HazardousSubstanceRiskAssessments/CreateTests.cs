using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Tests.Builder;
using BusinessSafe.WebSite.ViewModels;

using FluentValidation;
using FluentValidation.Results;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments
{
    [TestFixture]
    public class CreateTests
    {
        private Mock<IHazardousSubstanceRiskAssessmentService> _hazardousSubstanceAssessmentService;
        private Mock<ISearchRiskAssessmentsViewModelFactory> _searchViewModelFactory;
        private Mock<ICreateRiskAssessmentViewModelFactory> _createViewModelFactory;

        [SetUp]
        public void SetUp()
        {
            _hazardousSubstanceAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _searchViewModelFactory = new Mock<ISearchRiskAssessmentsViewModelFactory>();

            _searchViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithCreatedFrom(It.IsAny<string>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithCreatedTo(It.IsAny<string>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithHazardousSubstanceId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.GetViewModel(_hazardousSubstanceAssessmentService.Object))
                .Returns(new SearchRiskAssessmentsViewModel());

            _createViewModelFactory = new Mock<ICreateRiskAssessmentViewModelFactory>();

            _createViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_createViewModelFactory.Object);

            _createViewModelFactory
                .Setup(x => x.WithNewHazardousSubstanceId(It.IsAny<long>()))
                .Returns(_createViewModelFactory.Object);

            _createViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new CreateRiskAssessmentViewModel());
        }

        [Test]
        public void When_get_create_Then_should_return_correct_view()
        {
            //Given
            var target = GetTarget();

            //When
            var result = target.Create(1, 0) as ViewResult;

            //Then
            Assert.That(result.ViewName, Is.EqualTo("Create"));
            Assert.That(result.Model, Is.TypeOf<CreateRiskAssessmentViewModel>());
        }

        [Test]
        public void Given_valid_request_Then_hazardous_substance_assessment_is_saved()
        {
            //Given
            var target = GetTarget();
            var createRiskAssessmentViewModel = new CreateRiskAssessmentViewModel()
                                                {
                                                    CompanyId = 1234,
                                                    Reference = "Ref",
                                                    Title = "Title"
                                                };

            //When
            target.Create(createRiskAssessmentViewModel);

            //Then
            _hazardousSubstanceAssessmentService.Verify(r => r.CreateRiskAssessment(It.IsAny<SaveHazardousSubstanceRiskAssessmentRequest>()), Times.Once());
        }

        [Test]
        public void Given_valid_request_Then_selected_hazardous_substance_is_passed_to_service()
        {
            //Given
            var target = GetTarget();
            var passedSaveHazardousSubstanceRiskAssessmentRequest = new SaveHazardousSubstanceRiskAssessmentRequest();
            var createRiskAssessmentViewModel = new CreateRiskAssessmentViewModel()
            {
                CompanyId = 1234,
                Reference = "Ref",
                Title = "Title",
                NewHazardousSubstanceId = 5678
            };
            _hazardousSubstanceAssessmentService
                .Setup(r => r.CreateRiskAssessment(It.IsAny<SaveHazardousSubstanceRiskAssessmentRequest>()))
                .Callback<SaveHazardousSubstanceRiskAssessmentRequest>(y => passedSaveHazardousSubstanceRiskAssessmentRequest = y);

            //When
            target.Create(createRiskAssessmentViewModel);

            //Then
            Assert.That(passedSaveHazardousSubstanceRiskAssessmentRequest.HazardousSubstanceId, Is.EqualTo(createRiskAssessmentViewModel.NewHazardousSubstanceId));
        }

        [Test]
        public void Given_invalid_request_Then_viewModel_is_passed_to_factory_to_reset_hazardousSubstances_ddl()
        {
            //Given
            var target = GetTarget();
            var passedCreateRiskAssessmentViewModel = new CreateRiskAssessmentViewModel();
            var createRiskAssessmentViewModel = new CreateRiskAssessmentViewModel()
            {
                CompanyId = 1234,
                Reference = "Ref",
                Title = "Title"
            };
            _createViewModelFactory
                .Setup(x => x.GetViewModel(createRiskAssessmentViewModel))
                .Returns(new CreateRiskAssessmentViewModel())
                .Callback<CreateRiskAssessmentViewModel>(y => passedCreateRiskAssessmentViewModel = y);

            //When
            target.ModelState.AddModelError("an error", "there was an error with model validation");
            target.Create(createRiskAssessmentViewModel);

            //Then
            Assert.That(createRiskAssessmentViewModel, Is.EqualTo(passedCreateRiskAssessmentViewModel));
        }

        [Test]
        public void Given_valid_request_Then_hazardsous_substance_assessment_is_saved_and_redirected_to_correct_view()
        {
            //Given
            var target = GetTarget();

            const long expectedRiskAssessmentId = 2020;
            _hazardousSubstanceAssessmentService.Setup(x => x.CreateRiskAssessment(It.IsAny<SaveHazardousSubstanceRiskAssessmentRequest>()))
                                  .Returns(expectedRiskAssessmentId);
            
            var createRiskAssessmentViewModel = new CreateRiskAssessmentViewModel()
                                                {
                                                    CompanyId = 1234,
                                                    Reference = "Ref",
                                                    Title = "Title"
                                                };

            //When
            var result = target.Create(createRiskAssessmentViewModel) as RedirectToRouteResult;

            //Then
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Summary"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["riskAssessmentId"], Is.EqualTo(expectedRiskAssessmentId));
            Assert.That(result.RouteValues["CompanyId"], Is.EqualTo(1234));

        }

        private RiskAssessmentController GetTarget()
        {
            var result = new RiskAssessmentController(_searchViewModelFactory.Object, _createViewModelFactory.Object, _hazardousSubstanceAssessmentService.Object, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}