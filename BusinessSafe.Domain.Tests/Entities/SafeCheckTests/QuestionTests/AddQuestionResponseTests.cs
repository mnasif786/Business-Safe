using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities.SafeCheck;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.QuestionTests
{
    [TestFixture]
    public class AddQuestionResponseTests
    {

        [Test]
        public void Given_response_not_on_question_then_response_added()
        {
            var questionResponse = new QuestionResponse() {Id = Guid.NewGuid(), Title = "Acceptable"};
            var question = new Question();
            question.AddQuestionResponse(questionResponse);

            Assert.That(question.PossibleResponses.Count,Is.EqualTo(1));
        }

        [Test]
        public void Given_we_are_updating_the_action_required_then_action_required_updated()
        {
            //Given
            var questionResponseId = Guid.NewGuid();
            var expectedActionRequired = "the new action";
            var question = new Question();
            question.PossibleResponses.Add(new QuestionResponse() { Id = questionResponseId, Title = "Acceptable", ActionRequired = "Original" });

            //When
            question.AddQuestionResponse(new QuestionResponse() { Id = questionResponseId, Title = "Acceptable", ActionRequired = expectedActionRequired });

            //Then
            Assert.That(question.PossibleResponses.Count, Is.EqualTo(1));
            Assert.That(question.PossibleResponses[0].ActionRequired,Is.EqualTo(expectedActionRequired));
            Assert.That(question.PossibleResponses[0].LastModifiedOn, Is.Not.Null,"Last modified date should be set after update");
            Assert.That(question.PossibleResponses[0].LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));

        }

        [Test]
        public void Given_we_are_updating_and_action_required_not_changed_then_lastmodifiedOn_is_not_changed()
        {
            //Given
            var questionResponseId = Guid.NewGuid();
            var question = new Question();
            question.PossibleResponses.Add(new QuestionResponse() { Id = questionResponseId, Title = "Acceptable", ActionRequired = "Original", LastModifiedOn = DateTime.Now.AddDays(-10)});

            //When
            question.AddQuestionResponse(new QuestionResponse() { Id = questionResponseId, Title = "Acceptable", ActionRequired = "Original" });

            //Then
            Assert.That(question.PossibleResponses.Count, Is.EqualTo(1));
            Assert.That(question.PossibleResponses[0].ActionRequired, Is.EqualTo("Original"));
            Assert.That(question.PossibleResponses[0].LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.AddDays(-10).Date));

        }

        [Test]
        public void Given_we_are_updating_the_supporting_evidence_then_supporting_evidence_updated()
        {
            //Given
            var questionResponseId = Guid.NewGuid();
            var expectedSupportingEvidence = "the new supporting evidence";
            var question = new Question();
            question.PossibleResponses.Add(new QuestionResponse() { Id = questionResponseId, Title = "Acceptable", SupportingEvidence = "Original" });

            //When
            question.AddQuestionResponse(new QuestionResponse() { Id = questionResponseId, Title = "Acceptable", SupportingEvidence = expectedSupportingEvidence });

            //Then
            Assert.That(question.PossibleResponses.Count, Is.EqualTo(1));
            Assert.That(question.PossibleResponses[0].SupportingEvidence, Is.EqualTo(expectedSupportingEvidence));
            Assert.That(question.PossibleResponses[0].LastModifiedOn, Is.Not.Null, "Last modified date should be set after update");
            Assert.That(question.PossibleResponses[0].LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_we_are_removing_the_response_then_response_mark_as_deleted()
        {
            //Given
            var questionResponseId = Guid.NewGuid();
            var question = new Question();
            question.PossibleResponses.Add(new QuestionResponse() { Id = questionResponseId, Title = "Acceptable", Deleted = false});

            //When
            question.AddQuestionResponse(new QuestionResponse() { Id = questionResponseId, Title = "Acceptable", Deleted = true});

            //Then
            Assert.That(question.PossibleResponses.Count, Is.EqualTo(1));
            Assert.IsTrue(question.PossibleResponses[0].Deleted);
        }
    }
}
