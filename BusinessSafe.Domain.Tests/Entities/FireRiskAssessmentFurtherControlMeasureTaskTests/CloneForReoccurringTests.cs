using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Domain.Entities;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentFurtherControlMeasureTaskTests
{
    [TestFixture]
    public class GetBasisForCloneTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_FireRiskAssessmentFurtherControlMeasureTask_When_GetBasisForClone_Then_new_task_has_original_significant_set()
        {
            // Given
            var user = new UserForAuditing();

            var significantFinding = new SignificantFinding()
            {
                Id = 1234L
            };

            var fraTask = new Mock<FireRiskAssessmentFurtherControlMeasureTask>() { CallBase = true };
            fraTask.Setup(x => x.SignificantFinding).Returns(significantFinding);

            // When
            var result = fraTask.Object.CloneForReoccurring(user, DateTime.Now) as FireRiskAssessmentFurtherControlMeasureTask;

            // Then
            Assert.That(result.SignificantFinding, Is.EqualTo(significantFinding));
        }
    }
}
