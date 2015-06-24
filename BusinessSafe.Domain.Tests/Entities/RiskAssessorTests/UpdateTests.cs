using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessorTests
{
    [TestFixture]
    public class UpdateTests
    {
        [Test]
        public void When_Update_Then_new_values_set()
        {
            var site = new Site();
            var hasAccessToAllSites = true;
            var doNotSendReviewDueNotification = true;
            var doNotSendTaskOverdueNotifications = true;
            var doNotSendTaskCompletedNotifications = true;
            var user = new UserForAuditing();

            // Given
            var riskAssessor = new RiskAssessor();

            // When
            riskAssessor.Update(
                site,
                hasAccessToAllSites,
                doNotSendReviewDueNotification,
                doNotSendTaskOverdueNotifications,
                doNotSendTaskCompletedNotifications,
                user
                );

            // Then
            Assert.That(riskAssessor.Site == site);
            Assert.That(riskAssessor.HasAccessToAllSites == hasAccessToAllSites);
            Assert.That(riskAssessor.DoNotSendReviewDueNotification == doNotSendReviewDueNotification);
            Assert.That(riskAssessor.DoNotSendTaskOverdueNotifications == doNotSendTaskOverdueNotifications);
            Assert.That(riskAssessor.DoNotSendTaskCompletedNotifications == doNotSendTaskCompletedNotifications);
            Assert.That(riskAssessor.LastModifiedBy == user);
            Assert.That(riskAssessor.LastModifiedOn.Value.ToShortDateString() == DateTime.Today.ToShortDateString());
        }
    }
}
