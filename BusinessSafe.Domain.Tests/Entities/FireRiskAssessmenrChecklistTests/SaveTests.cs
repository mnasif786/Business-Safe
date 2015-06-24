using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmenrChecklistTests
{
    [TestFixture]
    public class SaveTests
    {
        private FireRiskAssessmentChecklist _fireRiskAssessmentChecklist;
        private UserForAuditing _user;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var answerParameterClasses = new List<SubmitFireAnswerParameters>
            {
                new SubmitFireAnswerParameters
                {
                    Question = new Question
                    {
                        Id = 101L          
                    },
                    YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes,
                    AdditionalInfo = "Test Addditional Info 1"
                },
                new SubmitFireAnswerParameters
                {
                    Question = new Question
                    {
                        Id = 102L          
                    },
                    YesNoNotApplicableResponse = YesNoNotApplicableEnum.No,
                },
                new SubmitFireAnswerParameters
                {
                    Question = new Question
                    {
                        Id = 103L          
                    },
                    YesNoNotApplicableResponse = YesNoNotApplicableEnum.NotApplicable,
                },
                new SubmitFireAnswerParameters
                {
                    Question = new Question
                    {
                        Id = 104L          
                    },
                }
            };

            _user = new UserForAuditing();
            _fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            _fireRiskAssessmentChecklist.Save(answerParameterClasses, _user);
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_correct_numer_of_answers()
        {
            Assert.That(_fireRiskAssessmentChecklist.Answers.Count, Is.EqualTo(4));
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_answer_1_has_correct_YesNoNaResponse()
        {
            Assert.That(_fireRiskAssessmentChecklist.Answers[0].YesNoNotApplicableResponse, Is.EqualTo(YesNoNotApplicableEnum.Yes));
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_answer_1_has_correct_AdditionalInfo()
        {
            Assert.That(_fireRiskAssessmentChecklist.Answers[0].AdditionalInfo, Is.EqualTo("Test Addditional Info 1"));
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_answer_2_has_correct_YesNoNaResponse()
        {
            Assert.That(_fireRiskAssessmentChecklist.Answers[1].YesNoNotApplicableResponse, Is.EqualTo(YesNoNotApplicableEnum.No));
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_answer_2_has_null_AdditionalInfo()
        {
            Assert.That(_fireRiskAssessmentChecklist.Answers[1].AdditionalInfo, Is.Null);
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_answer_3_has_correct_YesNoNaResponse()
        {
            Assert.That(_fireRiskAssessmentChecklist.Answers[2].YesNoNotApplicableResponse, Is.EqualTo(YesNoNotApplicableEnum.NotApplicable));
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_answer_3_has_null_AdditionalInfo()
        {
            Assert.That(_fireRiskAssessmentChecklist.Answers[2].AdditionalInfo, Is.Null);
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_answer_4_has_null_YesNoNaResponse()
        {
            Assert.That(_fireRiskAssessmentChecklist.Answers[3].YesNoNotApplicableResponse, Is.Null);
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_answer_4_has_null_AdditionalInfo()
        {
            Assert.That(_fireRiskAssessmentChecklist.Answers[3].AdditionalInfo, Is.Null);
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_correct_last_modified_by()
        {
            Assert.That(_fireRiskAssessmentChecklist.LastModifiedBy, Is.EqualTo(_user));
        }

        [Test]
        public void Given_valid_parameters_When_Save_called_Then_entity_has_correct_last_modified_on()
        {
            Assert.That(_fireRiskAssessmentChecklist.LastModifiedOn, Is.Not.Null);
            Assert.That(_fireRiskAssessmentChecklist.LastModifiedOn, Is.Not.EqualTo(default(DateTime)));
        }




    }
}
