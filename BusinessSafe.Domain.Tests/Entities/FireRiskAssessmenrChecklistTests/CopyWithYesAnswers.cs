using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmenrChecklistTests
{
    [TestFixture]
    public class CopyWithYesAnswersTests
    {

        [Test]
        public void Given_a_FRA_checklist_when_copied_then_only_the_Yes_answers_are_copied()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraChecklist = new FireRiskAssessmentChecklist
            {
                Id = 123123
                ,
                Checklist = new Checklist { Id = 123123 }
                ,
                CreatedOn = DateTime.Now.AddDays(-123)
                ,
                CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }
                ,
                Answers = new List<FireAnswer>()
            };

            fraChecklist.Answers.Add(new FireAnswer
            {
                CreatedBy = fraChecklist.CreatedBy,
                CreatedOn = fraChecklist.CreatedOn
                ,
                Id = 21312,
                Deleted = false,
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
                ,
                Question = new Question { Id = 1 }
            });

            fraChecklist.Answers.Add(new FireAnswer
            {
                CreatedBy = fraChecklist.CreatedBy,
                CreatedOn = fraChecklist.CreatedOn
                ,
                Id = 213122,
                Deleted = false,
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.No,
                Question = new Question { Id = 1 }
            });

            fraChecklist.Answers.Add(new FireAnswer
            {
                CreatedBy = fraChecklist.CreatedBy,
                CreatedOn = fraChecklist.CreatedOn
                ,
                Id = 214,
                Deleted = false,
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.NotApplicable
                ,
                Question = new Question { Id = 2 }
            });


            var fraToClone = fraChecklist.CopyWithYesAnswers(currentUser);

            //then
            Assert.IsTrue(fraToClone.Answers.Any(x => x.YesNoNotApplicableResponse == YesNoNotApplicableEnum.Yes || x.YesNoNotApplicableResponse == YesNoNotApplicableEnum.NotApplicable));
            Assert.IsFalse(fraToClone.Answers.Any(x => x.YesNoNotApplicableResponse == YesNoNotApplicableEnum.No));
            Assert.IsTrue(fraToClone.Answers.Any(x => x.Id == 0)); //ensure that all cloned answers are new entities
            Assert.IsTrue(fraToClone.Answers.All(x => x.CreatedBy.Id == currentUser.Id)); //ensure that all cloned answers are new entities

        }

        [Test]
        public void Given_a_FRA_checklist_when_copied_then_createdon_and_createdby_set()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraChecklist = new FireRiskAssessmentChecklist
            {
                Id = 123123
                ,
                Checklist = new Checklist { Id = 123123 }
                ,
                CreatedOn = DateTime.Now.AddDays(-123)
                ,
                CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }
                ,
                Answers = new List<FireAnswer>()
            };

            var fraToClone = fraChecklist.CopyWithYesAnswers(currentUser);

            //then
            Assert.AreEqual(currentUser, fraToClone.CreatedBy);
            Assert.AreEqual(DateTime.Now.Date, fraToClone.CreatedOn.Value.Date);

        }

        [Test]
        public void Given_a_FRA_checklist_when_copied_then_significantFindings_are_not_copied()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraChecklist = new FireRiskAssessmentChecklist
            {
                Id = 123123
                ,
                Checklist = new Checklist { Id = 123123 }
                ,
                CreatedOn = DateTime.Now.AddDays(-123)
                ,
                CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }
                ,
                Answers = new List<FireAnswer>()
            };

            fraChecklist.SignificantFindings.Add(new SignificantFinding {Id = 3465});
            var fraToClone = fraChecklist.CopyWithYesAnswers(currentUser);

            //then
            Assert.IsFalse(fraToClone.SignificantFindings.Any());

        }
    }
}
