using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.Summary
{
    [TestFixture]
    public class SaveAndNextTests
    {
        private SummaryController target;
        private Mock<IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory> _viewModelFactory;
        private Mock<IHazardousSubstanceRiskAssessmentService> _riskAssessmentService;
        private EditSummaryViewModel _genericEditSummaryViewModel;

        [SetUp]
        public void Setup()
        {
            _genericEditSummaryViewModel = new EditSummaryViewModel()
            {
                CompanyId = 123L,
                RiskAssessmentId = 789L,
                DateOfAssessment = DateTime.Now,
                HazardousSubstanceId = 456L,
                Title = "title",
                Reference = "reference",
                RiskAssessorId = 567L
            };
            _viewModelFactory = new Mock<IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory>();
            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new EditSummaryViewModel());

            _riskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _riskAssessmentService.Setup(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveHazardousSubstanceRiskAssessmentRequest>()));

            target = GetTarget();
        }

        [Test]
        public void When_SaveAndNext_Then_return_json()
        {
            // Given

            // When
            var result = target.SaveAndNext(_genericEditSummaryViewModel);

            // Then
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

        [Test]
        public void Given_no_hazardous_substance_selected_When_SaveAndNext_Then_return_success_equals_false_in_json_object()
        {
            // Given
            _genericEditSummaryViewModel.HazardousSubstanceId = 0;

            // When
            dynamic result = target.SaveAndNext(_genericEditSummaryViewModel);

            // Then
            Assert.IsFalse(result.Data.Success);
        }

        [Test]
        public void Given_no_hazardous_substance_selected_When_SaveAndNext_Then_return_errors_in_json_object()
        {
            // Given
            _genericEditSummaryViewModel.HazardousSubstanceId = 0;

            // When
            dynamic result = target.SaveAndNext(_genericEditSummaryViewModel);

            // Then
            Assert.That(result.Data.Errors[0], Is.EqualTo("The Hazardous Substance is required"));
        }

        private SummaryController GetTarget()
        {
            var result = new SummaryController(_riskAssessmentService.Object, _viewModelFactory.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
