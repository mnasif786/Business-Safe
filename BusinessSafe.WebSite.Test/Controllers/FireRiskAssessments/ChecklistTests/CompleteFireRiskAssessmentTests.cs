using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.ChecklistTests
{
    [TestFixture]
    public class CompleteFireRiskAssessmentTests
    {
        private Mock<IFireRiskAssessmentService> _fireRiskAssessmentService;
        private Mock<IFireRiskAssessmentChecklistService> _fireRiskAssessmentChecklistService;
        private IFireRiskAssessmentChecklistViewModelFactory _fireRiskAssessmentChecklistViewModelFactory;
        private FireRiskAssessmentChecklistViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new FireRiskAssessmentChecklistViewModel
                             {
                                 CompanyId = TestControllerHelpers.CompanyIdAssigned,
                                 FireRiskAssessmentChecklistId = 121L,
                                 RiskAssessmentId = 142L,
                                 Sections = new List<SectionViewModel>
                                                {
                                                    new SectionViewModel
                                                        {
                                                            Questions = new List<QuestionViewModel>
                                                                            {
                                                                                new QuestionViewModel
                                                                                    {
                                                                                        Id = 80L,
                                                                                        Answer = new FireAnswerViewModel
                                                                                                     {
                                                                                                         YesNoNotApplicableResponse
                                                                                                             =
                                                                                                             YesNoNotApplicableEnum
                                                                                                             .Yes,
                                                                                                         AdditionalInfo
                                                                                                             =
                                                                                                             "Test Additional Info 1"
                                                                                                     }
                                                                                    },
                                                                                new QuestionViewModel
                                                                                    {
                                                                                        Id = 81L,
                                                                                        Answer = new FireAnswerViewModel
                                                                                                     {
                                                                                                         YesNoNotApplicableResponse
                                                                                                             =
                                                                                                             YesNoNotApplicableEnum
                                                                                                             .No
                                                                                                     }
                                                                                    },
                                                                                new QuestionViewModel
                                                                                    {
                                                                                        Id = 82L,
                                                                                        Answer = new FireAnswerViewModel
                                                                                                     {
                                                                                                         YesNoNotApplicableResponse
                                                                                                             =
                                                                                                             YesNoNotApplicableEnum
                                                                                                             .
                                                                                                             NotApplicable
                                                                                                     }
                                                                                    },
                                                                                new QuestionViewModel
                                                                                    {
                                                                                        Id = 83L,
                                                                                        Answer = new FireAnswerViewModel
                                                                                                     {
                                                                                                         YesNoNotApplicableResponse
                                                                                                             = null
                                                                                                     }
                                                                                    },
                                                                            }
                                                        }
                                                }
                             };

            _fireRiskAssessmentService = new Mock<IFireRiskAssessmentService>();
            _fireRiskAssessmentChecklistService = new Mock<IFireRiskAssessmentChecklistService>();
            _fireRiskAssessmentChecklistViewModelFactory =
                new FireRiskAssessmentChecklistViewModelFactory(_fireRiskAssessmentService.Object, null);
        }

        [Test]
        public void Given_valid_view_model_When_SaveFireRiskAssessmentCheklist_called_Then_FireRiskAssessmentService_CompleteFireRiskAssessmentChecklist_is_called()
        {
            // Given
            var target = GetTarget();

            var validationResult = new ValidationResult();
            _fireRiskAssessmentChecklistService.Setup(
                x =>
                x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>())).
                Returns(validationResult);
                

            // When
            target.Complete(_viewModel);

            // Then
            _fireRiskAssessmentService.Verify(
                x => x.CompleteFireRiskAssessmentChecklist(It.Is<CompleteFireRiskAssessmentChecklistRequest>(
                    y => y.CompanyId == _viewModel.CompanyId
                         && y.Answers.Count == 4
                         && y.CurrentUserId != default(Guid)
                         && y.Answers[0].QuestionId == 80L
                         && y.Answers[0].YesNoNotApplicableResponse.Value == YesNoNotApplicableEnum.Yes
                         && y.Answers[0].AdditionalInfo == "Test Additional Info 1"
                         && y.Answers[1].QuestionId == 81L
                         && y.Answers[1].YesNoNotApplicableResponse.Value == YesNoNotApplicableEnum.No
                         && y.Answers[1].AdditionalInfo == null
                         && y.Answers[2].QuestionId == 82L
                         && y.Answers[2].YesNoNotApplicableResponse.Value == YesNoNotApplicableEnum.NotApplicable
                         && y.Answers[2].AdditionalInfo == null
                         && y.Answers[3].QuestionId == 83L
                         && y.Answers[3].YesNoNotApplicableResponse == null
                         && y.Answers[3].AdditionalInfo == null
                         )));
        }

        [Test]
        public void when_SaveFireRiskAssessment_called_Then_validation_is_checked()
        {
            // Given
            var validationResult = new ValidationResult();

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Returns(validationResult);

            var target = GetTarget();

            // When
            target.Complete(_viewModel);

            // Then
            _fireRiskAssessmentChecklistService.Verify(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()), Times.Once());
        }

        [Test]
        public void Given_answer_can_not_be_changed_when_Complete_called_Then_validation_result_is_added_to_viewmodel()
        {
            // Given
            var validationResult = new ValidationResult
                (
                new List<ValidationFailure>
                    {
                        new ValidationFailure("80", "error1"),
                    });

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Returns(validationResult);

            var target = GetTarget();

            // When
            var result = target.Complete(_viewModel) as ViewResult;

            // Then
            Assert.That((result.Model as FireRiskAssessmentChecklistViewModel).IsValid, Is.False);
        }

        [Test]
        public void Given_validation_failed_When_Complete_Then_should_mark_checklist_with_failure_to_complete_attempt()
        {
            // Given
            MarkChecklistWithCompleteFailureAttemptRequest passedMarkChecklistWithCompleteFailureAttemptRequest = null;
            var validationResult = new ValidationResult
                (
                new List<ValidationFailure>
                    {
                        new ValidationFailure("80", "error1"),
                    });
            _fireRiskAssessmentChecklistService
                .Setup(x => x.MarkChecklistWithCompleteFailureAttempt(It.IsAny<MarkChecklistWithCompleteFailureAttemptRequest>()))
                .Callback<MarkChecklistWithCompleteFailureAttemptRequest>(y => passedMarkChecklistWithCompleteFailureAttemptRequest = y);

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Returns(validationResult);

            var target = GetTarget();
            var userId = target.CurrentUser.UserId;


            // When
            target.Complete(_viewModel);

            // Then
            _fireRiskAssessmentChecklistService.Verify(x => x.MarkChecklistWithCompleteFailureAttempt(It.IsAny<MarkChecklistWithCompleteFailureAttemptRequest>()));

            Assert.That(passedMarkChecklistWithCompleteFailureAttemptRequest.ChecklistId, Is.EqualTo(_viewModel.FireRiskAssessmentChecklistId));
            Assert.That(passedMarkChecklistWithCompleteFailureAttemptRequest.CompanyId, Is.EqualTo(_viewModel.CompanyId));
            Assert.That(passedMarkChecklistWithCompleteFailureAttemptRequest.UserId, Is.EqualTo(userId));
        }

        private ChecklistController GetTarget()
        {
            var target = new ChecklistController(
                _fireRiskAssessmentChecklistViewModelFactory,
                _fireRiskAssessmentChecklistService.Object,
                _fireRiskAssessmentService.Object,
                null);

            return TestControllerHelpers.AddUserToController(target);
        }
    }
}
