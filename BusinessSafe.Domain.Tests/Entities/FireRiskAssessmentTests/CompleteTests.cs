using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class CompleteTests
    {
        private FireRiskAssessment _fireRiskAssessment;
        private UserForAuditing _currentUser;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _fireRiskAssessment = FireRiskAssessment.Create("Test Title", "REF01", 213123L, null, _currentUser);

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

            _fireRiskAssessment.CompleteFireRiskAssessmentChecklist(answerParameterClasses, _currentUser);
        }

        [Test]
        public void Given_two_no_answers_When_CompleteFireRiskAssessmentChecklist_called_Then_two_significant_findings_should_be_created()
        {
            Assert.That(_fireRiskAssessment.LatestFireRiskAssessmentChecklist.SignificantFindings.Count, Is.EqualTo(2));
        }

        [Test]
        public void Given_two_no_answers_When_CompleteFireRiskAssessmentChecklist_called_Then_a_significant_finding_relates_to_question()
        {
            var questionIds = _fireRiskAssessment
                .LatestFireRiskAssessmentChecklist
                .SignificantFindings
                .Select(significantFinding => significantFinding.FireAnswer)
                .Select(fireAnswer => fireAnswer.Question)
                .Select(question => question.Id)
                .ToList();

            Assert.That(questionIds.Contains(1002));
            Assert.That(questionIds.Contains(1003));
        }
    }
}
