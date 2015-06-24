using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities.SafeCheck;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.QuestionTests
{
    [TestFixture]
    public class IsNotApplicableAnswerEnabledTest
    {
        [Test]
        public void Given_when_IsNotApplicableAnswerEnabled_True_Then_IsNotApplicable_should_return_true()
        {
            //given
            var question = new Question();

            //when
            question.IsNotApplicableAnswerEnabled = true;

            //then
            Assert.That(question.IsNotApplicableAnswerEnabled,Is.EqualTo(true));
        }

        [Test]
        public void Given_not_applicable_is_true_when_IsNotApplicableAnswerEnabled_set_to_false_Then_IsNotApplicableAnswerEnabled_should_return_false()
        {
            //given
            var question = new Question();
            question.IsNotApplicableAnswerEnabled = true;

            //when
            question.IsNotApplicableAnswerEnabled = false;

            //then
            Assert.That(question.IsNotApplicableAnswerEnabled, Is.EqualTo(false));
        }

        [Test]
        public void Given_not_applicable_is_set_to_true_then_to_false_when_IsNotApplicableAnswerEnabled_set_to_true_Then_IsNotApplicableAnswerEnabled_should_return_true()
        {
            //given
            var question = new Question();
            question.IsNotApplicableAnswerEnabled = true;
            question.IsNotApplicableAnswerEnabled = false;

            //when
            question.IsNotApplicableAnswerEnabled = true;

            //then
            Assert.That(question.IsNotApplicableAnswerEnabled, Is.EqualTo(true));
        }
    }
}
