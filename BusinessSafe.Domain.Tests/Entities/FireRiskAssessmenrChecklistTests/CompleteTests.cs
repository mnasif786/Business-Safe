using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmenrChecklistTests
{
    [TestFixture]
    public class CompleteTests
    {
        private FireRiskAssessmentChecklist _fireRiskAssessmentChecklist;
        private UserForAuditing _currentUser;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();

            var answerParameterClasses = new List<SubmitFireAnswerParameters>
                                             {
                                                 new SubmitFireAnswerParameters
                                                     {
                                                         Question = new Question
                                                                        {
                                                                            Id = 1001L
                                                                        },
                                                         YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes,
                                                         AdditionalInfo = "some more stuff"
                                                     },
                                                 new SubmitFireAnswerParameters
                                                     {
                                                         Question = new Question
                                                                        {
                                                                            Id = 1002L
                                                                        },
                                                         YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
                                                     },
                                                 new SubmitFireAnswerParameters
                                                     {
                                                         Question = new Question
                                                                        {
                                                                            Id = 1003L
                                                                        },
                                                         YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
                                                     },
                                                 new SubmitFireAnswerParameters
                                                     {
                                                         Question = new Question
                                                                        {
                                                                            Id = 1004L
                                                                        },
                                                         YesNoNotApplicableResponse =
                                                             YesNoNotApplicableEnum.NotApplicable
                                                     }
                                             };

            _fireRiskAssessmentChecklist.Complete(answerParameterClasses, _currentUser);
        }

        [Test]
        public void When_CompleteFireRiskAssessmentChecklist_called_Then_HasCompleteFailureAttempt_should_be_marked_as_false()
        {
            Assert.That(_fireRiskAssessmentChecklist.HasCompleteFailureAttempt, Is.False);
        }

        [Test]
        public void Given_two_no_answers_When_CompleteFireRiskAssessmentChecklist_called_Then_two_significant_findings_should_be_created()
        {
            Assert.That(_fireRiskAssessmentChecklist.SignificantFindings.Count, Is.EqualTo(2));
        }

        [Test]
        public void Given_two_no_answers_When_CompleteFireRiskAssessmentChecklist_called_Then_a_significant_finding_relates_to_question()
        {
            var questionIds = _fireRiskAssessmentChecklist
                .SignificantFindings
                .Select(significantFinding => significantFinding.FireAnswer)
                .Select(fireAnswer => fireAnswer.Question)
                .Select(question => question.Id)
                .ToList();

            Assert.That(questionIds.Contains(1002));
            Assert.That(questionIds.Contains(1003));
        }

        [Test]
        public void When_CompleteFireRiskAssessmentChecklist_called_Then_completed_date_is_set()
        {
            Assert.That(_fireRiskAssessmentChecklist.CompletedDate, Is.Not.Null);
            Assert.That(_fireRiskAssessmentChecklist.CompletedDate, Is.Not.EqualTo(default(DateTime)));
        }
    }
}
