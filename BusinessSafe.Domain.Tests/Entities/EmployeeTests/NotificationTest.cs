using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmployeeTests
{
    [TestFixture]
    public class NotificationTest
    {
        [Test]
        public void Given_notification_to_be_sent_daily_then_return_true()
        {
            //Given
            var employee = new Employee();

            employee.NotificationType = NotificationType.Daily;

            //Then
            Assert.IsTrue(employee.DoesWantToBeNotifiedOn(DateTime.Now));
        }

        [Test]
        public void Given_notification_to_be_sent_weekly_on_wednesday_and_is_wednesday_then_return_true()
        {
            //Given
            var employee = new Employee();

            employee.NotificationType = NotificationType.Weekly;
            employee.NotificationFrequecy = (short?)System.DayOfWeek.Wednesday;

            var dateTime = new DateTime(2014, 5, 14); //Wednesday

            employee.DoesWantToBeNotifiedOn(dateTime);

            //Then
            Assert.IsTrue(employee.DoesWantToBeNotifiedOn(dateTime));
        }


        [Test]
        public void Given_notification_to_be_sent_weekly_on_tuesday_and_is_wednesday_then_return_false()
        {
            //Given
            var employee = new Employee();

            employee.NotificationType = NotificationType.Weekly;
            employee.NotificationFrequecy = (int?)System.DayOfWeek.Tuesday;

            var dateTime = new DateTime(2014, 5, 14); //Wednesday

            employee.DoesWantToBeNotifiedOn(dateTime);

            //Then
            Assert.IsFalse(employee.DoesWantToBeNotifiedOn(dateTime));
        }

        [Test]
        public void Given_notification_to_be_sent_monthly_on_26th_and_is_26th_then_return_true()
        {
            //Given
            var employee = new Employee();

            employee.NotificationType = NotificationType.Monthly;
            employee.NotificationFrequecy = 26;

            var dateTime = new DateTime(2014, 5, 26); //26th of the month

            employee.DoesWantToBeNotifiedOn(dateTime);

            //Then
            Assert.IsTrue(employee.DoesWantToBeNotifiedOn(dateTime));
        }

        [Test]
        public void Given_notification_to_be_sent_monthly_on_26th_and_is_25th_then_return_false()
        {
            //Given
            var employee = new Employee();

            employee.NotificationType = NotificationType.Monthly;
            employee.NotificationFrequecy = 26;

            var dateTime = new DateTime(2014, 5, 25); //25th of the month

            employee.DoesWantToBeNotifiedOn(dateTime);

            //Then
            Assert.IsFalse(employee.DoesWantToBeNotifiedOn(dateTime));
        }
    }
}
