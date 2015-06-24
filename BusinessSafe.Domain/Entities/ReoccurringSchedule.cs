using System;
using System.Collections.Generic;

namespace BusinessSafe.Domain.Entities
{
    public class ReoccurringSchedule
    {
        
        public IEnumerable<string> ScheduledDates { get; private set; }
        
        public ReoccurringSchedule(Task task)
        {
            ScheduledDates = new List<string>();

            if (!IsReoccurring(task))
                return;

            if (HasReoccurringPeriodFinished(task))
                return;

            if (IsTaskStatusNoLongerRequired(task))
                return;

            ScheduledDates = GetReoccurringScheduleDates(task);
        }

        private IEnumerable<string> GetReoccurringScheduleDates(Task task)
        {
            var scheduleStrategy = GetReoccurrencyStrategy(task.TaskReoccurringType);
             var result = new List<string>();
             for (int i = 0; i <= 4; i++)
             {
                var scheduleDate = scheduleStrategy.GetDate(task.TaskCompletionDueDate.GetValueOrDefault(), i);
                if (scheduleDate <= task.TaskReoccurringEndDate || task.TaskReoccurringEndDate == null)
                    result.Add(scheduleDate.ToShortDateString());
            }
            ScheduledDates = result;
            return result;
         }

        private ReoccurrencyScheduleStrategy GetReoccurrencyStrategy(TaskReoccurringType taskReoccurringType)
        {
            if (taskReoccurringType == TaskReoccurringType.Weekly)
                return new WeeklyScheduleStrategy();

            if (taskReoccurringType == TaskReoccurringType.Monthly)
                return new MonthlyScheduleStrategy();

            if (taskReoccurringType == TaskReoccurringType.ThreeMonthly)
                return new ThreeMonthlyScheduleStrategy();

            if (taskReoccurringType == TaskReoccurringType.SixMonthly)
                return new SixMonthlyScheduleStrategy();

            if (taskReoccurringType == TaskReoccurringType.Annually)
                return new AnnuallyMonthlyScheduleStrategy();

            throw new ArgumentException("Task Reoccurrency Type Not Known For Schedule");
        }

        private static bool IsTaskStatusNoLongerRequired(Task task)
        {
            return task.TaskStatus == TaskStatus.NoLongerRequired;
        }

        private bool IsReoccurring(Task task)
        {
            return task.TaskReoccurringType != TaskReoccurringType.None;
        }

        private static bool HasReoccurringPeriodFinished(Task task)
        {
            return DateTime.Now > task.TaskReoccurringEndDate;
        }

        private abstract class ReoccurrencyScheduleStrategy
        {
            public abstract DateTime GetDate(DateTime date, int times);
        }

        private class WeeklyScheduleStrategy: ReoccurrencyScheduleStrategy
        {
            public override DateTime GetDate(DateTime date, int times)
            {
                var daysToAdd = 7 * times;
                return date.AddDays(daysToAdd);
            }
        }

        private class MonthlyScheduleStrategy : ReoccurrencyScheduleStrategy
        {
            public override DateTime GetDate(DateTime date, int times)
            {
                return date.AddMonths(times);
            }
        }

        private class ThreeMonthlyScheduleStrategy : ReoccurrencyScheduleStrategy
        {
            public override DateTime GetDate(DateTime date, int times)
            {
                return date.AddMonths(times * 3);
            }
        }

        private class SixMonthlyScheduleStrategy : ReoccurrencyScheduleStrategy
        {
            public override DateTime GetDate(DateTime date, int times)
            {
                return date.AddMonths(times * 6);
            }
        }

        private class AnnuallyMonthlyScheduleStrategy : ReoccurrencyScheduleStrategy
        {
            public override DateTime GetDate(DateTime date, int times)
            {
                return date.AddYears(times);
            }
        }
        
    }


}