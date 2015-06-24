using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmenrChecklistTests
{
    [TestFixture]
    public class UpdateAnswersTest
    {
        private FireRiskAssessmentChecklist _fireRiskAssessmentChecklist;
        private UserForAuditing _user;


        [SetUp]
        public void Setup()
        {
        }


        //[Test]
        //public void Given_not_all_questions_update_When_Save_called_Then_only_updated_answers_are_changed()
        //{
        //    var answerParameterClasses = new List<SubmitFireAnswerParameters>
        //    {
        //        new SubmitFireAnswerParameters
        //        {
        //            Question = new Question
        //            {
        //                Id = 101L          
        //            },
        //            YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes,
        //            AdditionalInfo = "Test Addditional Info 1"
        //        },
        //        new SubmitFireAnswerParameters
        //        {
        //            Question = new Question
        //            {
        //                Id = 102L          
        //            },
        //            YesNoNotApplicableResponse = YesNoNotApplicableEnum.No,
        //        },
        //        new SubmitFireAnswerParameters
        //        {
        //            Question = new Question
        //            {
        //                Id = 103L          
        //            },
        //            YesNoNotApplicableResponse = YesNoNotApplicableEnum.NotApplicable,
        //        },
        //        new SubmitFireAnswerParameters
        //        {
        //            Question = new Question
        //            {
        //                Id = 104L          
        //            },
        //        }
        //    };

        //    _user = new UserForAuditing();
        //    _fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
        //    _fireRiskAssessmentChecklist.Save(answerParameterClasses, _user);
        //}
    }
}
