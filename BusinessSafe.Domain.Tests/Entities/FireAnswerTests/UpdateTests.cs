using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireAnswerTests
{
    [TestFixture]
    public class UpdateTests
    {
        [Test]
        public void Given_a_existing_no_answer_but_update_with_yes_When_answer_updated_Then_should_set_properties_as_expected()
        {
            // Given
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            var question = new Question();

            var target = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);

            // When
            target.Update(YesNoNotApplicableEnum.Yes, "Additional Info", user);

            // Then
            Assert.That(target.SignificantFinding, Is.Not.Null);
            Assert.That(target.SignificantFinding.Deleted, Is.True);
            Assert.That(target.FireRiskAssessmentChecklist, Is.EqualTo(fireRiskAssessmentChecklist));
            Assert.That(target.YesNoNotApplicableResponse, Is.EqualTo(YesNoNotApplicableEnum.Yes));
            Assert.That(target.AdditionalInfo, Is.EqualTo("Additional Info"));
            Assert.That(target.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
        }


        [Test]
        public void Given_a_existing_yes_answer_but_update_no_When_answer_updated_Then_should_set_properties_as_expected()
        {
            // Given
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            var question = new Question();

            var target = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.Yes, "Additional Info", user);

            // When
            target.Update(YesNoNotApplicableEnum.No, "Additional Info", user);

            // Then
            Assert.That(target.SignificantFinding, Is.Not.Null);
            Assert.That(target.SignificantFinding.Deleted, Is.False);
            Assert.That(target.FireRiskAssessmentChecklist, Is.EqualTo(fireRiskAssessmentChecklist));
            Assert.That(target.YesNoNotApplicableResponse, Is.EqualTo(YesNoNotApplicableEnum.No));
            Assert.That(target.AdditionalInfo, Is.EqualTo("Additional Info"));
            Assert.That(target.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_a_existing_yes_answer_but_update_no_and_have_a_deleted_significant_finding_When_answer_updated_Then_should_set_properties_as_expected()
        {
            // Given
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            var question = new Question();

            var target = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.Yes, "Additional Info", user);
            target.SignificantFinding = new SignificantFinding()
                                            {
                                                Deleted = true
                                            };
            
            // When
            target.Update(YesNoNotApplicableEnum.No, "Additional Info", user);

            // Then
            Assert.That(target.SignificantFinding, Is.Not.Null);
            Assert.That(target.SignificantFinding.Deleted, Is.False);
            Assert.That(target.FireRiskAssessmentChecklist, Is.EqualTo(fireRiskAssessmentChecklist));
            Assert.That(target.YesNoNotApplicableResponse, Is.EqualTo(YesNoNotApplicableEnum.No));
            Assert.That(target.AdditionalInfo, Is.EqualTo("Additional Info"));
            Assert.That(target.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_answer_has_not_changed_when_Update_Then_last_modified_not_updated()
        {
            // Given
            var expectedAnswer = YesNoNotApplicableEnum.Yes;
            var expectedAdditionalInfo = "Test additional";
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            var question = new Question();

            var target = FireAnswer.Create(fireRiskAssessmentChecklist, question, expectedAnswer, expectedAdditionalInfo, user);

            // When
            target.Update(expectedAnswer, expectedAdditionalInfo, user);

            //THEN
            Assert.That(target.LastModifiedOn,Is.Null);
            Assert.That(target.LastModifiedBy, Is.Null);
        }

        [Test]
        public void Given_answer_is_null_and_has_not_changed_when_Update_Then_last_modified_not_updated()
        {
            // Given
            YesNoNotApplicableEnum? expectedAnswer = null;
            var expectedAdditionalInfo = "Test additional";
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            var question = new Question();

            var target = FireAnswer.Create(fireRiskAssessmentChecklist, question, expectedAnswer, expectedAdditionalInfo, user);

            // When
            target.Update(expectedAnswer, expectedAdditionalInfo, user);

            //THEN
            Assert.That(target.LastModifiedOn, Is.Null);
            Assert.That(target.LastModifiedBy, Is.Null);
        }

        [Test]
        public void Given_answer_has_changed_when_Update_Then_last_modified_updated()
        {
            // Given
            var expectedAnswer = YesNoNotApplicableEnum.Yes;
            var expectedAdditionalInfo = "Test additional";
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            var question = new Question();

            var target = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.NotApplicable, expectedAdditionalInfo, user);

            // When
            target.Update(expectedAnswer, expectedAdditionalInfo, user);

            //THEN
            Assert.That(target.LastModifiedOn, Is.Not.Null);
            Assert.That(target.LastModifiedBy, Is.Not.Null);
        }

        [Test]
        public void Given_answer_additional_info_has_changed_when_Update_Then_last_modified_updated()
        {
            // Given
            var expectedAnswer = YesNoNotApplicableEnum.Yes;
            var expectedAdditionalInfo = "Test additional";
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            var question = new Question();

            var target = FireAnswer.Create(fireRiskAssessmentChecklist, question, expectedAnswer, "test thththt", user);

            // When
            target.Update(expectedAnswer, expectedAdditionalInfo, user);

            //THEN
            Assert.That(target.LastModifiedOn, Is.Not.Null);
            Assert.That(target.LastModifiedBy, Is.Not.Null);
        }


    }
}