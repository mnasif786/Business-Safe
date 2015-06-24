using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities.SafeCheck;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.QuestionTests
{
    [TestFixture]
    public class CopyQuestionTests
    {

        public Question CreateQuestion()
        {
            var question = Question.Create(Guid.NewGuid()
                , "Test question"
                , new Category() {Id = Guid.NewGuid()}, true, null);

            question.CreatedOn = DateTime.Now.AddDays(-123);
            question.CreatedBy = null;
            question.ActionRequired = "ActionRequired";
            question.SupportingEvidence = "Supporting evidence";
            question.AreaOfNonCompliance = "Area of non-compliance";
            question.OrderNumber = 123;
            question.CustomQuestion = true;
            question.Mandatory = true;

            return question;
        }

        [Test]
        public void given_question_when_copy_then_new_id_set()
        {
            //GIVEN
            var question = CreateQuestion();

            //when
            var result = question.Copy();

            //then
            Assert.That(result.Id,Is.Not.EqualTo(question.Id));
        }

        [Test]
        public void given_question_when_copy_then_createdOn_set()
        {
            //GIVEN
            var question = CreateQuestion();

            //when
            var result = question.Copy();

            //then
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(result.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void given_question_when_copy_then_properties_copied()
        {
            //GIVEN
            var question = CreateQuestion();

            //when
            var result = question.Copy();

            //then
            Assert.That(result.CreatedBy, Is.EqualTo(question.CreatedBy));
            Assert.That(result.Title, Is.EqualTo(question.Title));
            Assert.That(result.CustomQuestion, Is.EqualTo(question.CustomQuestion));
            Assert.That(result.Category, Is.EqualTo(question.Category));
            Assert.That(result.OrderNumber, Is.EqualTo(question.OrderNumber));
            Assert.That(result.SpecificToClientId, Is.EqualTo(question.SpecificToClientId));
            Assert.That(result.CustomQuestion, Is.EqualTo(question.CustomQuestion));
            Assert.That(result.Mandatory, Is.EqualTo(question.Mandatory));
        }

        [Test]
        public void given_question_when_copy_then_new_responses_copied()
        {
            //GIVEN
            var question = CreateQuestion();

            //when
            var result = question.Copy();

            //then
            Assert.That(result.PossibleResponses.Count, Is.EqualTo(question.PossibleResponses.Count));
            Assert.That(result.PossibleResponses[0].Id, Is.Not.EqualTo(question.PossibleResponses[0].Id));
            Assert.That(result.PossibleResponses[0].Question.Id, Is.EqualTo(result.Id));
            Assert.That(result.PossibleResponses[0].Title, Is.EqualTo(result.PossibleResponses[0].Title));
            Assert.That(result.PossibleResponses[0].GuidanceNotes, Is.EqualTo(result.PossibleResponses[0].GuidanceNotes));
            Assert.That(result.PossibleResponses[0].ResponseType, Is.EqualTo(result.PossibleResponses[0].ResponseType));
            Assert.That(result.PossibleResponses[0].ActionRequired, Is.EqualTo(result.PossibleResponses[0].ActionRequired));
            Assert.That(result.PossibleResponses[0].SupportingEvidence, Is.EqualTo(result.PossibleResponses[0].SupportingEvidence));
            Assert.That(result.PossibleResponses[0].ReportLetterStatement, Is.EqualTo(result.PossibleResponses[0].ReportLetterStatement));
            Assert.That(result.PossibleResponses[0].ReportLetterStatementCategory, Is.EqualTo(result.PossibleResponses[0].ReportLetterStatementCategory));
            Assert.That(result.PossibleResponses[0].CreatedBy, Is.EqualTo(result.PossibleResponses[0].CreatedBy));
            Assert.That(result.PossibleResponses[0].CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(result.PossibleResponses[0].LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void given_question_with_deleted_response_then_response_not_copied()
        {
            //GIVEN
            var question = CreateQuestion();
            question.PossibleResponses.Clear();
            question.AddQuestionResponse( new QuestionResponse(){Deleted = true}  );

            //when
            var result = question.Copy();

            //then
            Assert.That(result.PossibleResponses.Count, Is.EqualTo(0));
        }

        [Test]
        public void given_question_when_copied_then_default_text_copied()
        {
            //GIVEN
            var question = CreateQuestion();

            //when
            var result = question.Copy();

            //then
            Assert.That(result.ActionRequired, Is.EqualTo(question.ActionRequired));
            Assert.That(result.SupportingEvidence, Is.EqualTo(question.SupportingEvidence));
            Assert.That(result.AreaOfNonCompliance, Is.EqualTo(question.AreaOfNonCompliance));
        }

    }
}

