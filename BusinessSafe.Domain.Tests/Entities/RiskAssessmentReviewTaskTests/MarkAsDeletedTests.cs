using System;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentReviewTaskTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkAsDeletedTests
    {
        
        [Test]
        public void When_MarkAsDeleted_review_task_already_completed_Then_should_throw_correct_exception()
        {
            //Given
            var task = new RiskAssessmentReviewTask();
            var user = new UserForAuditing();

            //When
            //Then
            Assert.Throws<AttemptingToDeleteRiskAssessmentReviewTaskException>(() => task.MarkForDelete(user));
        }

    }
}