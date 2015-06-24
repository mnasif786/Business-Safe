using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;
//using EvaluationChecklist.Models;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.ChecklistTests
{
    [TestFixture]
    internal class UpdateChecklistTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Update_Checklist_Details()
        {
            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                SiteId = 11,
                ClientId = 22,
                CoveringLetterContent = "coveringLetterContent",
                Status = "status"
            };

            var checklist = Checklist.Create(parameters);

            var parametersNew = new CreateUpdateChecklistParameters()
            {
                SiteId = 111,
                ClientId = 222,
                CoveringLetterContent = "Content1",
                Status = "Status1"
            };

            // When
            checklist.UpdateChecklistDetails(parametersNew);

            Assert.That(checklist.SiteId, Is.EqualTo(111));
            Assert.That(checklist.ClientId, Is.EqualTo(222));
            Assert.That(checklist.CoveringLetterContent, Is.EqualTo("Content1"));
            Assert.That(checklist.Status, Is.EqualTo("Status1"));
        }

        [Test]
        public void Given_Exisiting_Checklist_When_Completed_Status_Then_Correct_Properties_Are_Set()
        {
            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                SiteId = 11,
                ClientId = 22,
                CoveringLetterContent = "coveringLetterContent",
                Status = null
            };

            var checklist = Checklist.Create(parameters);

            var parametersNew = new CreateUpdateChecklistParameters()
            {
                SiteId = 111,
                ClientId = 222,
                CoveringLetterContent = "Content1",
                Status = "Completed",
                CompletedBy = "fullname",
                CompletedOn = DateTime.Now
            };

            // When
            checklist.UpdateChecklistDetails(parametersNew);

            Assert.That(checklist.Status, Is.EqualTo(parametersNew.Status));
            Assert.That(checklist.ChecklistCompletedBy, Is.EqualTo(parametersNew.CompletedBy));
            Assert.That(checklist.ChecklistCompletedOn, Is.EqualTo(parametersNew.CompletedOn));
        }

        [Test]
        public void Given_Completed_Checklist_When_Updated_Properties_Are_Not_Set()
        {
            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                SiteId = 11,
                ClientId = 22,
                CoveringLetterContent = "coveringLetterContent",
                Status = "Completed",
                CompletedBy = "fullname",
                CompletedOn = DateTime.Now
            };

            var checklist = Checklist.Create(parameters);

            var parametersNew = new CreateUpdateChecklistParameters()
            {
                SiteId = 111,
                ClientId = 222,
                CoveringLetterContent = "Content1",
                Status = "Completed",
                CompletedBy = "another name",
                CompletedOn = DateTime.Now
            };

            // When
            checklist.UpdateChecklistDetails(parametersNew);

            Assert.That(checklist.Status, Is.EqualTo(parametersNew.Status));
            Assert.That(checklist.ChecklistCompletedBy, Is.EqualTo(parameters.CompletedBy));
            Assert.That(checklist.ChecklistCompletedOn, Is.EqualTo(parameters.CompletedOn));
        }

        [Test]
        public void Given_A_Checklist_is_not_submitted_then_action_plan_is_not_created()
        {
            var Id = Guid.NewGuid();

            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                Id = Id,
                ClientId = 11,
                SiteId = 22,
                CoveringLetterContent = "Letter content",
                Status = "Status",
                VisitBy = "VisitBy",
                VisitDate = "10/10/2013",
                VisitType = "Visit Type",
                MainPersonSeenName = "Person Seen Name",
                AreasVisited = "Areas Visited",
                AreasNotVisited = "Areas Not Visited",
                EmailAddress = "email",
                ImpressionType = new ImpressionType() { Comments = "comments" },
                ImmediateRiskNotifications = new List<AddImmediateRiskNotificationParameters>(),
                Submit = false
            };

            // When
            var checklist = Checklist.Create(parameters);

            Assert.That(checklist.ActionPlan, Is.Null);
        }

        [Test]
        public void Given_A_Checklist_is_submitted_then_action_plan_is_created()
        {
            var Id = Guid.NewGuid();

            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                Id = Id,
                ClientId = 11,
                SiteId = 22,
                CoveringLetterContent = "Letter content",
                Status = "Status",
                VisitBy = "VisitBy",
                VisitDate = "10/10/2013",
                VisitType = "Visit Type",
                MainPersonSeenName = "Person Seen Name",
                AreasVisited = "Areas Visited",
                AreasNotVisited = "Areas Not Visited",
                EmailAddress = "email",
                ImpressionType = new ImpressionType() { Comments = "comments" },
                ImmediateRiskNotifications = new List<AddImmediateRiskNotificationParameters>(),
                Submit = true
            };

            // When
            var checklist = Checklist.Create(parameters);

            Assert.That(checklist.ActionPlan, Is.Not.Null);
        }

        [Test]
        public void Given_A_Checklist_is_submitted_then_exception_thrown_if_submitted_again()
        {
            var Id = Guid.NewGuid();

            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                Id = Id,
                ClientId = 11,
                SiteId = 22,
                CoveringLetterContent = "Letter content",
                Status = "Status",
                VisitBy = "VisitBy",
                VisitDate = "10/10/2013",
                VisitType = "Visit Type",
                MainPersonSeenName = "Person Seen Name",
                AreasVisited = "Areas Visited",
                AreasNotVisited = "Areas Not Visited",
                EmailAddress = "email",
                ImpressionType = new ImpressionType() { Comments = "comments" },
                ImmediateRiskNotifications = new List<AddImmediateRiskNotificationParameters>(),
                Submit = true
            };

            var checklist = Checklist.Create(parameters);
            
            var ex = Assert.Throws<SafeCheckChecklistAlreadySubmittedException>(() => checklist.UpdateChecklistDetails(parameters));
        }

        [Test]
        public void Given_A_Checklist_is_submitted_then_submitted_properties_are_set()
        {
            var Id = Guid.NewGuid();

            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                Id = Id,
                ClientId = 11,
                SiteId = 22,
                CoveringLetterContent = "Letter content",
                Status = "Status",
                VisitBy = "VisitBy",
                VisitDate = "10/10/2013",
                VisitType = "Visit Type",
                MainPersonSeenName = "Person Seen Name",
                AreasVisited = "Areas Visited",
                AreasNotVisited = "Areas Not Visited",
                EmailAddress = "email",
                ImpressionType = new ImpressionType() { Comments = "comments" },
                ImmediateRiskNotifications = new List<AddImmediateRiskNotificationParameters>(),
                SubmittedBy = "username",
                SubmittedOn = DateTime.Now,
                Submit = true
            };

            // When
            var checklist = Checklist.Create(parameters);

            Assert.That(checklist.ChecklistSubmittedBy, Is.EqualTo(parameters.SubmittedBy));
            Assert.That(checklist.ChecklistSubmittedOn, Is.EqualTo(parameters.SubmittedOn));
        }

        [Test]
        public void Given_a_checklist_is_assigned_and_a_consultant_completes_checklist_then_status_stays_assigned()
        {
            var Id = Guid.NewGuid();

            // Given
            var checklist = new Checklist();
            checklist.Status = "Assigned";

            var parameters = new CreateUpdateChecklistParameters()
            {
                Id = Id,
                ClientId = 11,
                SiteId = 22,
                CoveringLetterContent = "Letter content",
                Status = "Completed",
                VisitBy = "VisitBy",
                VisitDate = "10/10/2013",
                VisitType = "Visit Type",
                MainPersonSeenName = "Person Seen Name",
                AreasVisited = "Areas Visited",
                AreasNotVisited = "Areas Not Visited",
                EmailAddress = "email",
                ImpressionType = new ImpressionType() { Comments = "comments" },
                ImmediateRiskNotifications = new List<AddImmediateRiskNotificationParameters>(),
                SubmittedBy = "username",
                SubmittedOn = DateTime.Now,
                Submit = true
            };
          
            checklist.UpdateChecklistDetails(parameters);

            Assert.That(checklist.Status,Is.EqualTo("Assigned"));
        }

        [Test]
        public void Given_a_checklist_is_assigned_when_Delete_then_deleted_by_recorded()
        {
            var username = "test.user";

            // Given
            var checklist = new Checklist();
            checklist.Status = "Assigned";


            checklist.MarkForDelete(null, username);

            Assert.That(checklist.Deleted, Is.True);
            Assert.That(checklist.DeletedBy, Is.EqualTo(username));
            Assert.That(checklist.DeletedBy, Is.Not.Null);
        }
    }
}
