using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities.SafeCheck;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.ChecklistTemplateTests
{
    [TestFixture]
    public class RemoveQuestionTests
    {
        
        public class ChecklistTemplateForTest: ChecklistTemplate 
        {
            public override IList<ChecklistTemplateQuestion> Questions
            {
                get { return _questions; }
            }
        }

        [Test]
        public void Given_no_questions_when_AddQuestion_then_question_added_to_list()
        {
            //GIVEN
            var question = new Question() {Id = Guid.NewGuid()};
            var checklistTemplate = new ChecklistTemplateForTest();
            checklistTemplate.Questions.Add(new ChecklistTemplateQuestion() {Id = Guid.NewGuid(), Question = question, Deleted = true});
            checklistTemplate.Questions.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question, Deleted = true });
            checklistTemplate.Questions.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question, Deleted = true });
            checklistTemplate.Questions.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question, Deleted = true });
            checklistTemplate.Questions.Add(new ChecklistTemplateQuestion() { Id = Guid.NewGuid(), Question = question });

            //WHEN
            checklistTemplate.RemoveQuestion(question,null);

            //THEN
            Assert.That(checklistTemplate.Questions.Count(x => !x.Deleted), Is.EqualTo(0));
            Assert.That(checklistTemplate.Questions.Count(x=> x.Deleted), Is.EqualTo(5));
        }
    }
}
