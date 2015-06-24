using System;
using System.ComponentModel;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.Summary
{
    [TestFixture]
    public class SaveAndNextTests
    {
        private Mock<IEditFireRiskAssessmentSummaryViewModelFactory> _viewModelFactory;
        private Mock<IFireRiskAssessmentService> _riskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _viewModelFactory = new Mock<IEditFireRiskAssessmentSummaryViewModelFactory>();
            _riskAssessmentService = new Mock<IFireRiskAssessmentService>();

        }

        [Test]
        public void Given_valid_model_When_SaveAndNext_Then_should_call_correct_methods()
        {
            // Given
            var model = new EditSummaryViewModel()
                            {
                                RiskAssessmentId = 100,
                                CompanyId = 200,
                                Title = "New Title",
                                Reference = "New Reference",
                                DateOfAssessment = DateTime.Now,
                                RiskAssessorId = 373L
                            };

            _riskAssessmentService
                .Setup(x => x.UpdateRiskAssessmentSummary(It.Is<SaveRiskAssessmentSummaryRequest>(y =>
                                                                                                  y.CompanyId == model.CompanyId &&
                                                                                                  y.Id == model.RiskAssessmentId &&
                                                                                                  y.Title == model.Title &&
                                                                                                  y.Reference == model.Reference &&
                                                                                                  y.RiskAssessorId == model.RiskAssessorId &&
                                                                                                  y.AssessmentDate == model.DateOfAssessment)));

            var target = GetTarget();

            // When
            target.SaveAndNext(model);

            // Then
            _riskAssessmentService.Verify(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveRiskAssessmentSummaryRequest>()));
        }

        [Test]
        public void Given_valid_model_When_SaveAndNext_Then_should_return_correct_result()
        {
            // Given
            var editSummaryViewModel = new EditSummaryViewModel()
                                           {
                                               RiskAssessmentId = 100,
                                               CompanyId = 200,
                                               Title = "New Title",
                                               Reference = "New Reference"
                                           };
            var target = GetTarget();

            // When
            var result = target.SaveAndNext(editSummaryViewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Is.EqualTo("{ Success = True }"));
        }

        [Test]
        public void Given_invalid_model_When_SaveAndNext_Then_should_return_correct_result()
        {
            // Given
            var editSummaryViewModel = new EditSummaryViewModel()
            {
                RiskAssessmentId = 100,
                CompanyId = 200,
                Title = "New Title",
                Reference = "New Reference"
            };
            var target = GetTarget();

            _riskAssessmentService
                .Setup(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveRiskAssessmentSummaryRequest>()))
                .Throws(new ValidationException(new BindingList<ValidationFailure>()));
            
            // When
            var result = target.SaveAndNext(editSummaryViewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Is.EqualTo("{ Success = false, Errors = System.String[] }"));
        }


        private SummaryController GetTarget()
        {
            var result = new SummaryController(_viewModelFactory.Object, _riskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}