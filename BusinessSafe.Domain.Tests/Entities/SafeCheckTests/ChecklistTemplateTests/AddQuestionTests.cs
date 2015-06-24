using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities.SafeCheck;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.ChecklistTemplateTests
{
    [TestFixture]
    public class AddQuestionTests
    {

        public class ChecklistTemplateForTest : ChecklistTemplate
        {

            public IList<ChecklistTemplateQuestion> QuestionsFromProtectedVariable
            {
                get { return _questions; }
            }
        }

        [Test]
        public void Given_no_questions_when_AddQuestion_then_question_added_to_list()
        {
            //GIVEN
            var question = new Question() {Id = Guid.NewGuid()};
            var checklistTemplate = new ChecklistTemplate();
            

            //WHEN
            checklistTemplate.AddQuestion(question,null);

            //THEN
            Assert.That(checklistTemplate.Questions.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_question_added_when_AddQuestion_then_question_not_duplicated()
        {
            //GIVEN
            var question = new Question() { Id = Guid.NewGuid() };
            var checklistTemplate = new ChecklistTemplate();
            checklistTemplate.AddQuestion(question, null);

            //WHEN
            checklistTemplate.AddQuestion(new Question(){Id = question.Id}, null);

            //THEN
            Assert.That(checklistTemplate.Questions.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_question_deleted_when_AddQuestion_then_question_is_restored()
        {
            //GIVEN
            var question = new Question() { Id = Guid.NewGuid() };
            var checklistTemplate = new ChecklistTemplateForTest();
            checklistTemplate.QuestionsFromProtectedVariable.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question, Deleted = true });
            checklistTemplate.QuestionsFromProtectedVariable.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question, Deleted = true });
            checklistTemplate.QuestionsFromProtectedVariable.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question, Deleted = true });
            checklistTemplate.QuestionsFromProtectedVariable.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question, Deleted = true });
            
            
            //WHEN
            checklistTemplate.AddQuestion(new Question() { Id = question.Id }, null);
            checklistTemplate.AddQuestion(new Question() { Id = question.Id }, null);
            checklistTemplate.AddQuestion(new Question() { Id = question.Id }, null);

            //THEN
            Assert.That(checklistTemplate.QuestionsFromProtectedVariable.Count(x => !x.Deleted), Is.EqualTo(1));
            Assert.That(checklistTemplate.QuestionsFromProtectedVariable.Count(x => x.Deleted), Is.EqualTo(3));
        }

        [Test]
        public void Given_duplciate_questions_when_get_Questions_then_distinct_list_returned()
        {
            var question1 = new Question() { Id = Guid.NewGuid() };
            var question2 = new Question() { Id = Guid.NewGuid() };
            var checklistTemplate = new ChecklistTemplateForTest();
            checklistTemplate.QuestionsFromProtectedVariable.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question1, Deleted = true});
            checklistTemplate.QuestionsFromProtectedVariable.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question1, });
            checklistTemplate.QuestionsFromProtectedVariable.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question1, });
            checklistTemplate.QuestionsFromProtectedVariable.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question1, });
            checklistTemplate.QuestionsFromProtectedVariable.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question2, });
            checklistTemplate.QuestionsFromProtectedVariable.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question2, });

            Assert.That(checklistTemplate.Questions.Count, Is.EqualTo(2));
        }
    }
}
