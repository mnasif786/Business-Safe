using System;
using System.Linq;

using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.GeneralRiskAssessmentFurtherActionTasksTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetReoccurringScheduleTests
    {
        [Test]
        public void Given_not_reoccurring_task_When_get_schedule_Then_returns_correct_schedule()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask {TaskReoccurringType = TaskReoccurringType.None};
            
            //When
            var result = task.GetReoccurringSchedule();

            
            //Then
            Assert.That(result.ScheduledDates.Count(), Is.EqualTo(0));    
        }

        [Test]
        public void Given_reoccurring_task_that_has_gone_past_last_end_date_When_get_schedule_Then_returns_correct_schedule()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask { TaskReoccurringType = TaskReoccurringType.Weekly, TaskReoccurringEndDate = DateTime.Today.AddDays(-1)};

            //When
            var result = task.GetReoccurringSchedule();


            //Then
            Assert.That(result.ScheduledDates.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Given_weekly_reoccurring_task_that_has_no_last_end_date_When_get_schedule_Then_returns_correct_schedule_with_five_dates()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask { TaskReoccurringType = TaskReoccurringType.Weekly, TaskCompletionDueDate = DateTime.Today};

            //When
            var result = task.GetReoccurringSchedule();


            //Then
            Assert.That(result.ScheduledDates.Count(), Is.EqualTo(5));
            Assert.That(result.ScheduledDates.First(), Is.EqualTo(DateTime.Today.ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(1).Take(1).First(), Is.EqualTo(DateTime.Today.AddDays(7).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(2).Take(1).First(), Is.EqualTo(DateTime.Today.AddDays(14).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(3).Take(1).First(), Is.EqualTo(DateTime.Today.AddDays(21).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(4).Take(1).First(), Is.EqualTo(DateTime.Today.AddDays(28).ToShortDateString()));
        }

        [Test]
        public void Given_monthly_reoccurring_task_that_has_no_last_end_date_When_get_schedule_Then_returns_correct_schedule_with_five_dates()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask { TaskReoccurringType = TaskReoccurringType.Monthly, TaskCompletionDueDate = DateTime.Today };

            //When
            var result = task.GetReoccurringSchedule();


            //Then
            Assert.That(result.ScheduledDates.Count(), Is.EqualTo(5));
            Assert.That(result.ScheduledDates.First(), Is.EqualTo(DateTime.Today.ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(1).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(1).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(2).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(2).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(3).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(3).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(4).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(4).ToShortDateString()));
        }

        [Test]
        public void Given_3monthly_reoccurring_task_that_has_no_last_end_date_When_get_schedule_Then_returns_correct_schedule_with_five_dates()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask { TaskReoccurringType = TaskReoccurringType.ThreeMonthly, TaskCompletionDueDate = DateTime.Today };

            //When
            var result = task.GetReoccurringSchedule();


            //Then
            Assert.That(result.ScheduledDates.Count(), Is.EqualTo(5));
            Assert.That(result.ScheduledDates.First(), Is.EqualTo(DateTime.Today.ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(1).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(1 * 3).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(2).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(2 * 3).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(3).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(3* 3).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(4).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(4* 3).ToShortDateString()));
        }

        [Test]
        public void Given_6monthly_reoccurring_task_that_has_no_last_end_date_When_get_schedule_Then_returns_correct_schedule_with_five_dates()
        {
            //Given
            var task = new MultiHazardRiskAssessmentFurtherControlMeasureTask { TaskReoccurringType = TaskReoccurringType.SixMonthly, TaskCompletionDueDate = DateTime.Today };

            //When
            var result = task.GetReoccurringSchedule();


            //Then
            Assert.That(result.ScheduledDates.Count(), Is.EqualTo(5));
            Assert.That(result.ScheduledDates.First(), Is.EqualTo(DateTime.Today.ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(1).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(1 * 6).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(2).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(2 * 6).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(3).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(3* 6).ToShortDateString()));
            Assert.That(result.ScheduledDates.Skip(4).Take(1).First(), Is.EqualTo(DateTime.Today.AddMonths(4* 6).ToShortDateString()));
        }
    }
}