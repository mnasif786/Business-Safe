using System;
using System.Collections.Generic;

using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireAnswerTests
{
    [TestFixture]
    public class IsValidateForCompleteChecklistTests
    {
        [Test]
        public void Given_no_answer_and_no_significant_finding_When_IsValidateForCompleteChecklist_Then_returns_false()
        {
            // Given
            var fireAnswer = new Domain.Entities.FireAnswer()
                             {
                                 YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
                             };

            // When
            var result = fireAnswer.IsValidateForCompleteChecklist();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_no_further_control_measure_tasks_on_significant_finding_When_IsValidateForCompleteChecklist_Then_returns_false()
        {
            // Given
            var fireAnswer = new Domain.Entities.FireAnswer
                                 {
                                     YesNoNotApplicableResponse = YesNoNotApplicableEnum.No,
                                     SignificantFinding = new SignificantFinding()
                                 };

            // When
            var result = fireAnswer.IsValidateForCompleteChecklist();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_noone_assigned_to_further_control_measure_tasks_on_significant_finding_When_IsValidateForCompleteChecklist_Then_returns_false()
        {
            // Given
            var fireAnswer = new Domain.Entities.FireAnswer
            {
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.No,
                SignificantFinding = new SignificantFinding
                                         {
                                             FurtherControlMeasureTasks = new List<FireRiskAssessmentFurtherControlMeasureTask>
                                                                              {
                                                                                  new FireRiskAssessmentFurtherControlMeasureTask()
                                                                              }
                                         }
            };

            // When
            var result = fireAnswer.IsValidateForCompleteChecklist();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_someone_assigned_to_further_control_measure_tasks_on_significant_finding_When_IsValidateForCompleteChecklist_Then_returns_true()
        {
            // Given
            var fireAnswer = new Domain.Entities.FireAnswer
            {
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.No,
                SignificantFinding = new SignificantFinding
                {
                    FurtherControlMeasureTasks = new List<FireRiskAssessmentFurtherControlMeasureTask>
                                                                              {
                                                                                  new FireRiskAssessmentFurtherControlMeasureTask
                                                                                      {
                                                                                          TaskAssignedTo=new Employee
                                                                                                             {
                                                                                                                 Id = Guid.NewGuid()
                                                                                                             }
                                                                                      }
                                                                              }
                }
            };

            // When
            var result = fireAnswer.IsValidateForCompleteChecklist();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_yes_answer_with_additional_info_set_When_IsValidateForCompleteChecklist_Then_returns_true()
        {
            // Given
            var fireAnswer = new Domain.Entities.FireAnswer
            {
                AdditionalInfo = "additional info",
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
            };

            // When
            var result = fireAnswer.IsValidateForCompleteChecklist();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_yes_answer_with_no_additional_info_set_When_IsValidateForCompleteChecklist_Then_returns_false()
        {
            // Given
            var fireAnswer = new Domain.Entities.FireAnswer
            {
                AdditionalInfo = null,
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
            };

            // When
            var result = fireAnswer.IsValidateForCompleteChecklist();

            // Then
            Assert.IsFalse(result);
        }
    }
}