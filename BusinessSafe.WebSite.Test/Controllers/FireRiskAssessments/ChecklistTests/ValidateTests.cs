using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;

using FluentValidation.Results;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.ChecklistTests
{
    [TestFixture]
    public class ValidateTests
    {
        private Mock<IFireRiskAssessmentChecklistService> _fireRiskAssessmentChecklistService;

        [SetUp]
        public void Setup()
        {
            _fireRiskAssessmentChecklistService = new Mock<IFireRiskAssessmentChecklistService>();
            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Returns(ValidationResultWithNoErrors());
        }

        [Test]
        public void When_Validate_Then_returns_JsonResult()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Validate(GetValidViewModel()) as JsonResult;

            // Then
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [Test]
        public void When_Validate_Then_calls_fireRiskAssessmentChecklistService_ValidateFireRiskAssessmentChecklist()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Validate(GetValidViewModel());

            // Then
            _fireRiskAssessmentChecklistService
                .Verify(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()));
        }

        [Test]
        public void When_Validate_Then_passes_checklist_id_in_request()
        {
            // Given
            var target = GetTarget();
            var model = GetValidViewModel();
            ValidateCompleteFireRiskAssessmentChecklistRequest passedRequest = null;

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Callback<ValidateCompleteFireRiskAssessmentChecklistRequest>(y => passedRequest = y)
                .Returns(ValidationResultWithNoErrors());

            // When
            var result = target.Validate(model);

            // Then
            Assert.That(passedRequest.ChecklistId, Is.EqualTo(model.ChecklistId));
        }

        [Test]
        public void When_Validate_Then_passes_all_no_answer_ids_list_in_request()
        {
            // Given
            var target = GetTarget();
            var model = GetValidViewModel();
            ValidateCompleteFireRiskAssessmentChecklistRequest passedRequest = null;

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Callback<ValidateCompleteFireRiskAssessmentChecklistRequest>(y => passedRequest = y)
                .Returns(ValidationResultWithNoErrors());

            // When
            var result = target.Validate(model);

            // Then
            Assert.That(passedRequest.QuestionIds, Is.EqualTo(model.AllNoAnswerQuestionIds));
        }

        [Test]
        public void Given_valid_viewModel_When_Validate_Then_return_json_object_with_success_equals_true()
        {
            // Given
            var target = GetTarget();
            var model = GetValidViewModel();

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Returns(new ValidationResult());

            // When
            dynamic result = target.Validate(model);

            // Then
            Assert.IsTrue(result.Data.Success);
        }

        [Test]
        public void Given_invalid_viewModel_When_Validate_Then_return_json_object_with_success_equals_false()
        {
            // Given
            var target = GetTarget();
            var model = GetValidViewModel();

            var validationResult = new ValidationResult(new List<ValidationFailure>
                                                        {
                                                            new ValidationFailure("prop1", "error 1"),
                                                            new ValidationFailure("prop2", "error 2"),
                                                            new ValidationFailure("prop3", "error 3")
                                                        });

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Returns(validationResult);

            // When
            dynamic result = target.Validate(model);

            // Then
            Assert.IsFalse(result.Data.Success);
        }

        [Test]
        public void Given_invalid_viewModel_When_Validate_Then_return_json_object_with_list_of_errors_in_validation_result()
        {
            // Given
            var target = GetTarget();
            var model = GetValidViewModel();

            var validationFailure1 = new ValidationFailure("prop1", "error 1");
            var validationFailure2 = new ValidationFailure("prop2", "error 2");
            var validationFailure3 = new ValidationFailure("prop3", "error 3");

            var validationResult = new ValidationResult(new List<ValidationFailure>
                                                        {
                                                            validationFailure1,
                                                            validationFailure2,
                                                            validationFailure3
                                                        });

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Returns(validationResult);

            // When
            dynamic result = target.Validate(model);
            // Then

            Assert.That(
                result.Data.Errors[0].PropertyName,
                Is.EqualTo(validationFailure1.PropertyName));
            Assert.That(
                result.Data.Errors[0].ErrorMessage,
                Is.EqualTo(validationFailure1.ErrorMessage));

            Assert.That(
                result.Data.Errors[1].PropertyName,
                Is.EqualTo(validationFailure2.PropertyName));
            Assert.That(
                result.Data.Errors[1].ErrorMessage,
                Is.EqualTo(validationFailure2.ErrorMessage));

            Assert.That(
                result.Data.Errors[2].PropertyName,
                Is.EqualTo(validationFailure3.PropertyName));
            Assert.That(
                result.Data.Errors[2].ErrorMessage,
                Is.EqualTo(validationFailure3.ErrorMessage));
        }

        [Test]
        public void Given_invalid_viewModel_When_Validate_Then_ask_service_to_mark_as_complete_attempted()
        {
            // Given
            var target = GetTarget();
            var model = GetValidViewModel();

            var validationFailure1 = new ValidationFailure("prop1", "error 1");

            var validationResult = new ValidationResult(new List<ValidationFailure>
                                                        {
                                                            validationFailure1
                                                        });

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Returns(validationResult);

            _fireRiskAssessmentChecklistService
                .Setup(x => x.MarkChecklistWithCompleteFailureAttempt(It.IsAny<MarkChecklistWithCompleteFailureAttemptRequest>()));

            // When
            target.Validate(model);

            // Then
            _fireRiskAssessmentChecklistService.Verify(x => x.MarkChecklistWithCompleteFailureAttempt(It.IsAny<MarkChecklistWithCompleteFailureAttemptRequest>()));
        }

        [Test]
        public void Given_invalid_viewModel_When_Validate_Then_MarkChecklistWithCompleteFailureAttemptRequest_parameters_are_set()
        {
            // Given
            var target = GetTarget();
            var model = GetValidViewModel();
            MarkChecklistWithCompleteFailureAttemptRequest passedRequest = null;

            var validationFailure1 = new ValidationFailure("prop1", "error 1");

            var validationResult = new ValidationResult(new List<ValidationFailure>
                                                        {
                                                            validationFailure1
                                                        });

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Returns(validationResult);

            _fireRiskAssessmentChecklistService
                .Setup(x => x.MarkChecklistWithCompleteFailureAttempt(It.IsAny<MarkChecklistWithCompleteFailureAttemptRequest>()))
                .Callback<MarkChecklistWithCompleteFailureAttemptRequest>(y => passedRequest = y);

            // When
            target.Validate(model);

            // Then
            Assert.That(passedRequest.ChecklistId, Is.EqualTo(model.ChecklistId));
            Assert.IsNotNull(passedRequest.UserId);
            Assert.That(passedRequest.CompanyId, Is.EqualTo(TestControllerHelpers.CompanyIdAssigned));
        }

        [Test]
        public void Given_no_question_ids_passed_When_Validate_Then_pass_empty_list_to_service()
        {

            // Given
            var target = GetTarget();

            ValidateCompleteFireRiskAssessmentChecklistRequest passedRequest = null;

            _fireRiskAssessmentChecklistService
                .Setup(x => x.ValidateFireRiskAssessmentChecklist(It.IsAny<ValidateCompleteFireRiskAssessmentChecklistRequest>()))
                .Callback<ValidateCompleteFireRiskAssessmentChecklistRequest>(y => passedRequest = y)
                .Returns(ValidationResultWithNoErrors);


            // When
            target.Validate(new ValidateCompleteFireRiskAssessmentChecklistViewModel
                   {
                       ChecklistId = 1234L
                   });

            // Then
            Assert.NotNull(passedRequest.QuestionIds);
        }

        private ChecklistController GetTarget()
        {
            var controller = new ChecklistController(
                null,
                _fireRiskAssessmentChecklistService.Object,
                null,
                null
                );
            return TestControllerHelpers.AddUserToController(controller);
        }

        private ValidationResult ValidationResultWithNoErrors()
        {
            return new ValidationResult(new List<ValidationFailure>());
        }

        private ValidateCompleteFireRiskAssessmentChecklistViewModel GetValidViewModel()
        {
            return new ValidateCompleteFireRiskAssessmentChecklistViewModel
                   {
                       ChecklistId = 1234L,
                       AllNoAnswerQuestionIds = new long[] { 2L, 34L, 56L, 78L },
                   };
        }
    }
}
