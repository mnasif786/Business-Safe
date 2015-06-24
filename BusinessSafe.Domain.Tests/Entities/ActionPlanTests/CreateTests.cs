using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;
using NUnit.Framework;
using Checklist = BusinessSafe.Domain.Entities.SafeCheck.Checklist;

namespace BusinessSafe.Domain.Tests.Entities.ActionPlanTests
{
    [TestFixture]
    public class CreateTests
    {
        [Test]
        public void Given_valid_checklist_When_Crete_called_Then_correct_action_plan_returned()
        {
            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                Id = Guid.NewGuid(),
                ClientId = 111,
                SiteAddress1 = "1 purple street",
                SiteAddressPostcode = "M1 1AA",
                VisitDate = "13/01/2000",
                Submit = true,
                Site = new Site() { Id = 11L}
            };

           var checklist = Checklist.Create(parameters); 
            
            // When
           // var actionPlan = ActionPlan.Create(checklist);

            // Then
            Assert.That(checklist.ActionPlan.Checklist , Is.EqualTo(checklist));
            Assert.That(checklist.ActionPlan.CompanyId, Is.EqualTo(checklist.ClientId));
            //Assert.That(actionPlan.Title, Is.EqualTo(checklist.));

            //todo: need to load site into checklist
            //Assert.That(actionPlan.Site.SiteId, Is.EqualTo(checklist.SiteId));

            Assert.That(checklist.ActionPlan.DateOfVisit, Is.EqualTo(checklist.VisitDate));
            Assert.That(checklist.ActionPlan.VisitBy, Is.EqualTo(checklist.VisitBy));
            Assert.That(checklist.ActionPlan.SubmittedOn, Is.Not.EqualTo(default(DateTime)));
            Assert.That(checklist.ActionPlan.AreasVisited, Is.EqualTo(checklist.AreasVisited));
            Assert.That(checklist.ActionPlan.AreasNotVisited, Is.EqualTo(checklist.AreasNotVisited));
            Assert.That(checklist.ActionPlan.Title, Is.EqualTo("Visit Report - 1 purple street - M1 1AA - 13/01/2000"));
            Assert.That(checklist.ActionPlan.Site, Is.EqualTo(parameters.Site));
        }

        [Test]
        public void Given_valid_checklist_When_Create_called_when_not_submitted_Then_action_plan_is_null()
        {

            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                Id = Guid.NewGuid(),
                ClientId = 111,
                SiteAddress1 = "1 purple street",
                SiteAddressPostcode = "M1 1AA",
                VisitDate = "13/01/2000"
            };

            var checklist = Checklist.Create(parameters);
            
            // Then
            Assert.That(checklist.ActionPlan, Is.Null);
        }

        [Test]
        public void Given_valid_checklist_When_CreateImmediateRiskNotifications_called_when_submitted_Then_Actions_Populated()
        {
            // Given
            var parameters = new CreateUpdateChecklistParameters()
            {
                Id = Guid.NewGuid(),
                ClientId = 111,
                SiteAddress1 = "1 purple street",
                SiteAddressPostcode = "M1 1AA",
                VisitDate = "13/01/2000",
                ImmediateRiskNotifications = new List<AddImmediateRiskNotificationParameters>(),
                CompletedBy = "CompletedBy",
                CompletedOn = DateTime.Now,
                Submit = true,
                MainPersonSeen = new Employee {Id = Guid.NewGuid()}
            };

            parameters.ImmediateRiskNotifications.Add(new AddImmediateRiskNotificationParameters()
            {
                Id = Guid.NewGuid(), 
                RecommendedImmediateAction = "Action 1",
                Reference = "Ref 1", 
                SignificantHazardIdentified = "Hazard 1", 
                Title = "Title 1",
            });

            parameters.ImmediateRiskNotifications.Add(new AddImmediateRiskNotificationParameters()
            {
                Id = Guid.NewGuid(),
                RecommendedImmediateAction = "Action 2",
                Reference = "Ref 2",
                SignificantHazardIdentified = "Hazard 2",
                Title = "Title 2"
            });

            var checklist = Checklist.Create(parameters);
            checklist.ActionPlan.CreateImmediateRiskNotifications();

            // Then
            Assert.That(checklist.ActionPlan, Is.Not.Null);
            Assert.That(checklist.ActionPlan.Actions.Count(), Is.EqualTo(2));
            Assert.That(checklist.ActionPlan.Actions.ElementAt(0).Title, Is.EqualTo(parameters.ImmediateRiskNotifications.ElementAt(0).Title));
            Assert.That(checklist.ActionPlan.Actions.ElementAt(1).Title, Is.EqualTo(parameters.ImmediateRiskNotifications.ElementAt(1).Title));
            Assert.That(checklist.ActionPlan.Actions.ElementAt(1).Reference, Is.EqualTo(parameters.ImmediateRiskNotifications.ElementAt(1).Reference));

            Assert.That(checklist.ActionPlan.Actions.ElementAt(0).AssignedTo.Id, Is.EqualTo(parameters.MainPersonSeen.Id));
            Assert.That(checklist.ActionPlan.Actions.ElementAt(1).AssignedTo.Id, Is.EqualTo(parameters.MainPersonSeen.Id));
        }
    }
}
