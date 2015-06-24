using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    public class ValidateFireRiskAssessmentReviewTests
    {
        private Mock<IFireAnswerRepository> _fireAnswerRepository;

        [SetUp]
        public void Setup()
        {
            _fireAnswerRepository = new Mock<IFireAnswerRepository>();
            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionIds(It.IsAny<long>(), It.IsAny<IEnumerable<long>>()))
                .Returns(new List<FireAnswer>());
        }

        [Test]
        public void When_Validate_Then_retrieve_required_fire_answers()
        {
            // Given
            var target = GetTarget();
            var request = new ValidateCompleteFireRiskAssessmentChecklistRequest()
                          {
                              ChecklistId = 1234L,
                              QuestionIds = new long[] { 123L, 456L, 789L }
                          };

            // When
            target.ValidateFireRiskAssessmentChecklist(request);

            // Then
            _fireAnswerRepository.Verify(x => x.GetByChecklistIdAndQuestionIds(request.ChecklistId, request.QuestionIds));
        }

        [Test]
        public void Given_no_corresponding_answers_for_requested_question_ids_When_Validate_Then_add_error_for_each_one()
        {
            // Given
            var target = GetTarget();
            var request = new ValidateCompleteFireRiskAssessmentChecklistRequest()
            {
                ChecklistId = 1234L,
                QuestionIds = new long[] { 123L, 456L, 789L }
            };

            // When
            var result = target.ValidateFireRiskAssessmentChecklist(request);

            // Then
            Assert.That(result.Errors.Count, Is.EqualTo(request.QuestionIds.Count()));
        }

        [Test]
        public void Given_no_corresponding_answers_for_requested_question_ids_When_Validate_Then_set_question_id_for_each_one()
        {
            // Given
            var target = GetTarget();
            var request = new ValidateCompleteFireRiskAssessmentChecklistRequest()
            {
                ChecklistId = 1234L,
                QuestionIds = new long[] { 123L, 456L, 789L }
            };

            // When
            var result = target.ValidateFireRiskAssessmentChecklist(request);

            // Then
            for (var i = 0; i < result.Errors.Count; i++)
            {
                Assert.That(result.Errors.ElementAt(i).PropertyName, Is.EqualTo(request.QuestionIds.ElementAt(i).ToString()));
            }
        }

        [Test]
        public void Given_no_corresponding_answers_for_requested_question_ids_When_Validate_Then_set_error_message_for_each_one()
        {
            // Given
            var target = GetTarget();
            var request = new ValidateCompleteFireRiskAssessmentChecklistRequest()
            {
                ChecklistId = 1234L,
                QuestionIds = new long[] { 123L, 456L, 789L }
            };

            // When
            var result = target.ValidateFireRiskAssessmentChecklist(request);

            // Then
            for (var i = 0; i < result.Errors.Count; i++)
            {
                Assert.That(result.Errors.ElementAt(i).ErrorMessage, Is.EqualTo("Please select a response"));
            }
        }

        [Test]
        public void Given_a_valid_no_answer_for_requested_question_id_When_Validate_Then_no_error_is_set()
        {
            // Given
            var target = GetTarget();

            const long questionId = 123L;

            var invalidFireAnswer = new Mock<FireAnswer>();
            invalidFireAnswer.Setup(x => x.IsValidateForCompleteChecklist()).Returns(true);
            invalidFireAnswer.Setup(x => x.YesNoNotApplicableResponse).Returns(YesNoNotApplicableEnum.No);
            invalidFireAnswer.Setup(x => x.Question).Returns(new Question() { Id = questionId });

            var request = new ValidateCompleteFireRiskAssessmentChecklistRequest()
            {
                ChecklistId = 1234L,
                QuestionIds = new long[] { questionId }
            };
            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionIds(It.IsAny<long>(), It.IsAny<IEnumerable<long>>()))
                .Returns(new List<FireAnswer>()
                         {
                             invalidFireAnswer.Object
                         });

            // When
            var result = target.ValidateFireRiskAssessmentChecklist(request);

            // Then
            Assert.That(result.Errors.Count, Is.EqualTo(0));
        }

        [Test]
        public void Given_an_invalid_no_answer_for_requested_question_id_When_Validate_Then_error_for_no_is_set()
        {
            // Given
            var target = GetTarget();

            const long questionId = 123L;

            var invalidFireAnswer = new Mock<FireAnswer>();
            invalidFireAnswer.Setup(x => x.IsValidateForCompleteChecklist()).Returns(false);
            invalidFireAnswer.Setup(x => x.YesNoNotApplicableResponse).Returns(YesNoNotApplicableEnum.No);
            invalidFireAnswer.Setup(x => x.Question).Returns(new Question() { Id = questionId });

            var request = new ValidateCompleteFireRiskAssessmentChecklistRequest()
            {
                ChecklistId = 1234L,
                QuestionIds = new long[] { questionId }
            };
            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionIds(It.IsAny<long>(), It.IsAny<IEnumerable<long>>()))
                .Returns(new List<FireAnswer>()
                         {
                             invalidFireAnswer.Object
                         });

            // When
            var result = target.ValidateFireRiskAssessmentChecklist(request);

            // Then
            Assert.That(result.Errors.First().PropertyName, Is.EqualTo(questionId.ToString(CultureInfo.InvariantCulture)));
            Assert.That(result.Errors.First().ErrorMessage, Is.EqualTo("Please add a Further Control Measure Task"));
        }

        [Test]
        public void Given_a_valid_yes_answer_for_requested_question_id_When_Validate_Then_no_error_is_set()
        {
            // Given
            var target = GetTarget();

            const long questionId = 123L;

            var invalidFireAnswer = new Mock<FireAnswer>();
            invalidFireAnswer.Setup(x => x.IsValidateForCompleteChecklist()).Returns(true);
            invalidFireAnswer.Setup(x => x.YesNoNotApplicableResponse).Returns(YesNoNotApplicableEnum.Yes);
            invalidFireAnswer.Setup(x => x.AdditionalInfo).Returns(string.Empty);
            invalidFireAnswer.Setup(x => x.Question).Returns(new Question() { Id = questionId });

            var request = new ValidateCompleteFireRiskAssessmentChecklistRequest()
            {
                ChecklistId = 1234L,
                QuestionIds = new[] { questionId }
            };
            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionIds(It.IsAny<long>(), It.IsAny<IEnumerable<long>>()))
                .Returns(new List<FireAnswer>()
                         {
                             invalidFireAnswer.Object
                         });

            // When
            var result = target.ValidateFireRiskAssessmentChecklist(request);

            // Then
            Assert.That(result.Errors.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Given_an_invalid_yes_answer_for_requested_question_id_When_Validate_Then_error_for_yes_is_set()
        {
            // Given
            var target = GetTarget();

            const long questionId = 123L;

            var invalidFireAnswer = new Mock<FireAnswer>();
            invalidFireAnswer.Setup(x => x.IsValidateForCompleteChecklist()).Returns(false);
            invalidFireAnswer.Setup(x => x.YesNoNotApplicableResponse).Returns(YesNoNotApplicableEnum.Yes);
            invalidFireAnswer.Setup(x => x.AdditionalInfo).Returns(string.Empty);
            invalidFireAnswer.Setup(x => x.Question).Returns(new Question() { Id = questionId });

            var request = new ValidateCompleteFireRiskAssessmentChecklistRequest()
            {
                ChecklistId = 1234L,
                QuestionIds = new[] { questionId }
            };
            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionIds(It.IsAny<long>(), It.IsAny<IEnumerable<long>>()))
                .Returns(new List<FireAnswer>()
                         {
                             invalidFireAnswer.Object
                         });

            // When
            var result = target.ValidateFireRiskAssessmentChecklist(request);

            // Then
            Assert.That(result.Errors.First().PropertyName, Is.EqualTo(questionId.ToString(CultureInfo.InvariantCulture)));
            Assert.That(result.Errors.First().ErrorMessage, Is.EqualTo("Please enter a comment"));
        }

        private FireRiskAssessmentChecklistService GetTarget()
        {
            return new FireRiskAssessmentChecklistService(
                null,
                null,
                null,
                null,
                _fireAnswerRepository.Object);
        }
    }
}
