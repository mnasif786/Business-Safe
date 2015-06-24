using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkAsArchivedTest
    {
        [Test]
        public void Given_riskassessment_mark_as_archived_Then_should_set_correct_properties()
        {
            //Given
            var riskAssessment = GeneralRiskAssessment.Create("", "", default(long), null);
            var user = new UserForAuditing();


            //When
            riskAssessment.MarkAsArchived(user);

            //Then
            Assert.That(riskAssessment.Status, Is.EqualTo(RiskAssessmentStatus.Archived));
            Assert.That(riskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void When_Mark_As_Archived_Then_associated_outstanding_FCM_Tasks_are_set_as_TaskStatus_NoLongerRequired()
        {
            // Given
            var user = new UserForAuditing();
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            var dummyHazard = new Mock<MultiHazardRiskAssessmentHazard>();

            var tasks = new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();

            var outstandingTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            outstandingTask.Setup(x => x.TaskStatus).Returns(TaskStatus.Outstanding);
            outstandingTask.Setup(x => x.MarkAsNoLongerRequired(It.IsAny<UserForAuditing>()));

            var completedTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            completedTask.Setup(x => x.TaskStatus).Returns(TaskStatus.Completed);
            completedTask.Setup(x => x.MarkAsNoLongerRequired(It.IsAny<UserForAuditing>()));

            var deletedTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            deletedTask.Setup(x => x.Deleted).Returns(true);
            deletedTask.Setup(x => x.MarkAsNoLongerRequired(It.IsAny<UserForAuditing>()));

            var noLongerRequiredTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            noLongerRequiredTask.Setup(x => x.TaskStatus).Returns(TaskStatus.NoLongerRequired);
            noLongerRequiredTask.Setup(x => x.MarkAsNoLongerRequired(It.IsAny<UserForAuditing>()));

            tasks.Add(outstandingTask.Object);
            tasks.Add(completedTask.Object);
            tasks.Add(deletedTask.Object);
            tasks.Add(noLongerRequiredTask.Object);

            dummyHazard.Setup(x => x.FurtherControlMeasureTasks).Returns(tasks);


            target.Hazards.Add(dummyHazard.Object);

            // When
            target.MarkAsArchived(user);

            // Then
            outstandingTask.Verify(x => x.MarkAsNoLongerRequired(user), Times.Once());
            completedTask.Verify(x => x.MarkAsNoLongerRequired(user), Times.Never());
            deletedTask.Verify(x => x.MarkAsNoLongerRequired(user), Times.Never());
            noLongerRequiredTask.Verify(x => x.MarkAsNoLongerRequired(user), Times.Never());
        }

        [Test]
        public void When_Mark_As_Archived_Then_associated_outstanding_FCM_Tasks_within_deleted_Hazards_are_not_set_as_TaskStatus_NoLongerRequired()
        {
            // Given
            var user = new UserForAuditing();
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            var dummyHazard = new Mock<MultiHazardRiskAssessmentHazard>();
            dummyHazard.Setup(x => x.Deleted).Returns(true);

            var tasks = new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>();

            var outstandingTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            outstandingTask.Setup(x => x.TaskStatus).Returns(TaskStatus.Outstanding);
            outstandingTask.Setup(x => x.MarkAsNoLongerRequired(It.IsAny<UserForAuditing>()));

            var completedTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            completedTask.Setup(x => x.TaskStatus).Returns(TaskStatus.Completed);
            completedTask.Setup(x => x.MarkAsNoLongerRequired(It.IsAny<UserForAuditing>()));

            var deletedTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            deletedTask.Setup(x => x.TaskStatus).Returns(TaskStatus.Outstanding);
            deletedTask.Setup(x => x.MarkAsNoLongerRequired(It.IsAny<UserForAuditing>()));

            var noLongerRequiredTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            noLongerRequiredTask.Setup(x => x.TaskStatus).Returns(TaskStatus.NoLongerRequired);
            noLongerRequiredTask.Setup(x => x.MarkAsNoLongerRequired(It.IsAny<UserForAuditing>()));

            tasks.Add(outstandingTask.Object);
            tasks.Add(completedTask.Object);
            tasks.Add(deletedTask.Object);
            tasks.Add(noLongerRequiredTask.Object);

            dummyHazard.Setup(x => x.FurtherControlMeasureTasks).Returns(tasks);


            target.Hazards.Add(dummyHazard.Object);

            // When
            target.MarkAsArchived(user);

            // Then
            outstandingTask.Verify(x => x.MarkAsNoLongerRequired(user), Times.Never());
            completedTask.Verify(x => x.MarkAsNoLongerRequired(user), Times.Never());
            deletedTask.Verify(x => x.MarkAsNoLongerRequired(user), Times.Never());
            noLongerRequiredTask.Verify(x => x.MarkAsNoLongerRequired(user), Times.Never());
        }

        [Test]
        [TestCase(TaskStatus.NoLongerRequired, false)]
        [TestCase(TaskStatus.Completed, false)]
        [TestCase(TaskStatus.Outstanding, true)]
        public void When_Mark_As_Archived_Then_associated_outstanding_Review_Tasks_are_set_as_TaskStatus_NoLongerRequired(TaskStatus initialStatus, bool shouldCallMarkAsNoLongerRequired)
        {
            // Given
            var user = new UserForAuditing();
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            var review = new Mock<RiskAssessmentReview>();
            var reviewTask = new Mock<RiskAssessmentReviewTask>();
            reviewTask.Setup(x => x.TaskStatus).Returns(initialStatus);
            reviewTask.Setup(x => x.MarkAsNoLongerRequired(It.IsAny<UserForAuditing>()));
            review.Setup(x => x.RiskAssessmentReviewTask).Returns(reviewTask.Object);

            target.Reviews.Add(review.Object);

            // When
            target.MarkAsArchived(user);

            // Then
            if(shouldCallMarkAsNoLongerRequired)
            {
                reviewTask.Verify(x => x.MarkAsNoLongerRequired(user), Times.Once());
            }else
            {
                reviewTask.Verify(x => x.MarkAsNoLongerRequired(user), Times.Never());
            }
        }
    }
}