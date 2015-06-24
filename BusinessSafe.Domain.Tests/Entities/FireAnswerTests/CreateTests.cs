using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireAnswerTests
{
    [TestFixture]
    public class CreateTests
    {
        [Test]
        public void Given_a_no_answer_When_answer_created_Then_should_create_as_expected()
        {
            // Given
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            var question = new Question();

            // When
            var result = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);

            // Then
            Assert.That(result.SignificantFinding, Is.Not.Null);
            Assert.That(result.FireRiskAssessmentChecklist, Is.EqualTo(fireRiskAssessmentChecklist));
            Assert.That(result.Question, Is.EqualTo(question));
            Assert.That(result.YesNoNotApplicableResponse, Is.EqualTo(YesNoNotApplicableEnum.No));
            Assert.That(result.AdditionalInfo, Is.EqualTo("Additional Info"));
            Assert.That(result.CreatedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
        }


        [Test]
        public void Given_a_yes_answer_When_answer_created_Then_should_create_as_expected()
        {
            // Given
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            var question = new Question();

            // When
            var result = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.Yes, "Additional Info", user);

            // Then
            Assert.That(result.SignificantFinding, Is.Null);
            Assert.That(result.FireRiskAssessmentChecklist, Is.EqualTo(fireRiskAssessmentChecklist));
            Assert.That(result.Question, Is.EqualTo(question));
            Assert.That(result.YesNoNotApplicableResponse, Is.EqualTo(YesNoNotApplicableEnum.Yes));
            Assert.That(result.AdditionalInfo, Is.EqualTo("Additional Info"));
            Assert.That(result.CreatedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
        }
    }
}