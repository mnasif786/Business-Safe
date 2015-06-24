
using System.Collections.Generic;

using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.Responsibilities.Wizard
{
    [TestFixture]
    public class RecommendedFrequencyTests
    {
        private StatutoryResponsibilityViewModel _target;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_multiple_Tasks_When_RecommendedFrequency_Then_get_longest_one()
        {
            // Given
            _target = GetTarget();
            _target.Tasks = new List<StatutoryResponsibilityTaskViewModel>
                            {
                                new StatutoryResponsibilityTaskViewModel() { InitialFrequency = TaskReoccurringType.None },
                                new StatutoryResponsibilityTaskViewModel() { InitialFrequency = TaskReoccurringType.SixMonthly },
                                new StatutoryResponsibilityTaskViewModel() { InitialFrequency = TaskReoccurringType.TwentyFourMonthly },
                                new StatutoryResponsibilityTaskViewModel() { InitialFrequency = TaskReoccurringType.Annually }
                            };

            // When
            var result = _target.RecommendedFrequency;

            // Then
            Assert.That(result, Is.EqualTo(TaskReoccurringType.TwentyFourMonthly));
        }

        [Test]
        public void Given_no_Tasks_When_RecommendedFrequency_Then_return_none()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.RecommendedFrequency;

            // Then
            Assert.That(result, Is.EqualTo(TaskReoccurringType.None));
        }

        private StatutoryResponsibilityViewModel GetTarget()
        {
            return new StatutoryResponsibilityViewModel();
        }
    }
}
