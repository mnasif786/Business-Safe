using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SignificantFindingTests
{
    [TestFixture]
    public class MarkSignificantFindingAsDeletedTests
    {
        
        [Test]
        public void When_mark_as_delete_Then_sets_correct_peorperties()
        {
            // Given
            var user = new UserForAuditing();
            var significantFinding = new SignificantFinding
            {
                Deleted = false
            };

            // When
            significantFinding.MarkForDelete(user);

            // Then
            Assert.That(significantFinding.Deleted, Is.EqualTo(true));
            Assert.That(significantFinding.LastModifiedBy, Is.EqualTo(user));
            Assert.That(significantFinding.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
        }

        [Test]
        public void Given_significant_finding_with_further_control_measure_tasks_When_mark_as_delete_Then_sets_correct_properties()
        {
            // Given
            var user = new UserForAuditing();
            var significantFinding = new SignificantFinding
            {
                FurtherControlMeasureTasks = new List<FireRiskAssessmentFurtherControlMeasureTask>
                                                 {
                                                     new FireRiskAssessmentFurtherControlMeasureTask
                                                         {
                                                             Deleted = false
                                                         }
                                                         ,
                                                         new FireRiskAssessmentFurtherControlMeasureTask
                                                         {
                                                             Deleted = false
                                                         }
                                                 }
            };

            // When
            significantFinding.MarkForDelete(user);

            // Then
            Assert.That(significantFinding.FurtherControlMeasureTasks.Count(x => x.Deleted), Is.EqualTo(2));
        }

        [Test]
        public void Given_completed_further_control_measure_task_When_mark_as_delete_Then_task_is_not_deleted()
        {
            // Given
            var user = new UserForAuditing();
            var significantFinding = new SignificantFinding
            {
                FurtherControlMeasureTasks = new List<FireRiskAssessmentFurtherControlMeasureTask>
                                                 {
                                                     new FireRiskAssessmentFurtherControlMeasureTask
                                                         {
                                                             Deleted = false,
                                                             TaskStatus = TaskStatus.Overdue
                                                         }
                                                         ,
                                                    new FireRiskAssessmentFurtherControlMeasureTask
                                                         {
                                                             Deleted = false,
                                                             TaskStatus = TaskStatus.Completed
                                                         }
                                                 }
            };

            // When
            significantFinding.MarkForDelete(user);

            // Then
            Assert.That(significantFinding.FurtherControlMeasureTasks.Count(x => x.Deleted), Is.EqualTo(1));            
        }
    }
}