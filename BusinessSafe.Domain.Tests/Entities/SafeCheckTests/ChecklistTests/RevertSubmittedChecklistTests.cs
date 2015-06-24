using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;
using NUnit.Framework;
using Action = BusinessSafe.Domain.Entities.Action;
using Checklist = BusinessSafe.Domain.Entities.SafeCheck.Checklist;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.ChecklistTests
{
    [TestFixture]
    internal class RevertSubmittedChecklistTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_Submitted_Checklist_When_No_Action_then_CanBeReverted_Returns_true()
        {
            var actionPlan = new ActionPlan() { Id = 1, Title = "My Action Plan", Deleted = false, };

            var actions = new List<Action>();
            actionPlan.Actions = actions;

            var checklist = new Checklist
            {
                Id = Guid.NewGuid(),
                ActionPlan = actionPlan,
                Status = "Submitted"
            };

            var target = checklist.CanBeReverted();

            Assert.IsTrue(target);
        }

        [Test]
        public void Given_Submitted_Checklist_When_Any_of_the_Action_is_Assigned_then_CanBeReverted_Returns_false()
        {
            var actionPlan = new ActionPlan() { Id = 1, Title = "My Action Plan", Deleted = false, };
            var action = new Action() { Id = 1, ActionPlanId = actionPlan.Id, ActionPlan = actionPlan, AssignedTo = new Employee() };

            var actionTasks = new List<ActionTask>();
            actionTasks.Add(new ActionTask() { Action = action} );

            action.ActionTasks = actionTasks;

            var actions = new List<Action>();
            actions.Add(action);
            actions.Add(new Action() { Id = 2, ActionPlanId = actionPlan.Id, ActionPlan = actionPlan, });
            actions.Add(new Action() { Id = 3, ActionPlanId = actionPlan.Id, ActionPlan = actionPlan, });

            actionPlan.Actions = actions;

            var checklist = new Checklist
            {
                Id = Guid.NewGuid(),
                ActionPlan = actionPlan,
                Status = "Submitted"
            };

            var target = checklist.CanBeReverted();

            Assert.IsFalse(target);
        }

        [Test]
        public void Given_Submitted_Checklist_When_No_Action_is_Assigned_then_CanBeReverted_Returns_true()
        {
            var actionPlan = new ActionPlan() { Id = 1, Title = "My Action Plan", Deleted = false, };

            var actions = new List<Action>();
            actions.Add(new Action() { Id = 1, ActionPlanId = actionPlan.Id });
            actions.Add(new Action() { Id = 2, ActionPlanId = actionPlan.Id });
            actions.Add(new Action() { Id = 3, ActionPlanId = actionPlan.Id });

            actionPlan.Actions = actions;

            var checklist = new Checklist
            {
                Id = Guid.NewGuid(),
                ActionPlan = actionPlan,
                Status = "Submitted"
            };

            var target = checklist.CanBeReverted();

            Assert.IsTrue(target);
        }

        [Test]
        public void Given_Submitted_Checklist_is_reverted_then_properties_are_set()
        {
            var actionPlan = new ActionPlan() { Id = 1, Title = "My Action Plan" };
            var actions = new List<Action>();
            actionPlan.Actions = actions;

            var user = new UserForAuditing();

            var checklist = new Checklist
            {
                Id = Guid.NewGuid(),
                Status = "Submitted",
                ActionPlan = actionPlan
            };

            checklist.Revert(user,"Test User");

            Assert.That(checklist.Status,Is.EqualTo("Assigned"));
            Assert.That(checklist.ExecutiveSummaryDocumentLibraryId, Is.Null);
            Assert.That(checklist.ChecklistSubmittedBy, Is.Null);
            Assert.That(checklist.ChecklistSubmittedOn, Is.Null);
            Assert.That(checklist.LastModifiedBy, Is.EqualTo(user));
            
        }

        //actions.Add(new Action() { Id = 1, ActionPlanId = actionPlan.Id });
        //actions.Add(new Action() { Id = 2, ActionPlanId = actionPlan.Id });
        //actions.Add(new Action() { Id = 3, ActionPlanId = actionPlan.Id });

        [Test]
        public void Given_Submitted_Checklist_is_reverted_then_Action_Plan_is_Removed()
        {
            var actionPlan = new ActionPlan() { Id = 1, Title = "My Action Plan" };
            var actions = new List<Action>();
            actionPlan.Actions = actions;

            var user = new UserForAuditing();

            var checklist = new Checklist
            {
                Id = Guid.NewGuid(),
                Status = "Submitted",
                ActionPlan = actionPlan
            };

            checklist.Revert(user,"Test User");

            Assert.That(checklist.ActionPlan, Is.EqualTo(null));
            
        }

        [Test]
        public void Given_Submitted_Checklist_Special_Report_when_is_reverted_then_properties_are_set()
        {
           var user = new UserForAuditing();

            var checklist = new Checklist
            {
                Id = Guid.NewGuid(),
                Status = "Submitted",
                SpecialReport = true
            };

            checklist.Revert(user, "Test User");

            Assert.That(checklist.Status, Is.EqualTo("Assigned"));
            Assert.That(checklist.ExecutiveSummaryDocumentLibraryId, Is.Null);
            Assert.That(checklist.ChecklistSubmittedBy, Is.Null);
            Assert.That(checklist.ChecklistSubmittedOn, Is.Null);
            Assert.That(checklist.LastModifiedBy, Is.EqualTo(user));

        }

    }
}
