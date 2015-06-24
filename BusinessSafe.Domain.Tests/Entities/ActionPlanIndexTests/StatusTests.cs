using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.ActionPlanIndexTests
{
    [TestFixture]
    public class StatusTests
    {
        [Test]
        public void given_action_plan_doesnt_have_any_tasks_then_status_equals_none()
        {
            //given
            var actionPlanIndex = new ActionPlanIndex();
            

            //THEN
            Assert.That(actionPlanIndex.Status, Is.EqualTo(DerivedTaskStatusForDisplay.None));
        }

        [Test]
        public void given_action_plan_has_any_overdue_tasks_then_status_equals_overdue()
        {
            //given
            var actionPlanIndex = new ActionPlanIndex();
            actionPlanIndex.AnyActionsOverdue = true;
            actionPlanIndex.AnyActionsCompleted = true;
            actionPlanIndex.AnyActionsOutstanding = true;
            actionPlanIndex.AnyActionsNoLongerRequired = true;

            //THEN
            Assert.That(actionPlanIndex.Status, Is.EqualTo(DerivedTaskStatusForDisplay.Overdue));
        }

        [Test]
        public void given_action_plan_has_any_completed_tasks_then_status_equals_Completed()
        {
            //given
            var actionPlanIndex = new ActionPlanIndex();
            actionPlanIndex.AnyActionsOverdue = false;
            actionPlanIndex.AnyActionsCompleted = true;
            actionPlanIndex.AnyActionsOutstanding = false;
            actionPlanIndex.AnyActionsNoLongerRequired = true;

            //THEN
            Assert.That(actionPlanIndex.Status, Is.EqualTo(DerivedTaskStatusForDisplay.Completed));
        }

        [Test]
        public void given_action_plan_has_any_outstanding_tasks_then_status_equals_Outstanding()
        {
            //given
            var actionPlanIndex = new ActionPlanIndex();
            actionPlanIndex.AnyActionsOverdue = false;
            actionPlanIndex.AnyActionsCompleted = false;
            actionPlanIndex.AnyActionsOutstanding = true;
            actionPlanIndex.AnyActionsNoLongerRequired = true;

            //THEN
            Assert.That(actionPlanIndex.Status, Is.EqualTo(DerivedTaskStatusForDisplay.Outstanding));
        }

        [Test]
        public void given_action_plan_has_any_no_longer_required_tasks_then_status_equals_NoLongerRequired()
        {
            //given
            var actionPlanIndex = new ActionPlanIndex();
            actionPlanIndex.AnyActionsOverdue = false;
            actionPlanIndex.AnyActionsCompleted = false;
            actionPlanIndex.AnyActionsOutstanding = false;
            actionPlanIndex.AnyActionsNoLongerRequired = true;

            //THEN
            Assert.That(actionPlanIndex.Status, Is.EqualTo(DerivedTaskStatusForDisplay.NoLongerRequired));
        }

        [Test]
        public void given_action_plan_is_no_longer_required_then_status_equals_overdue()
        {
            //given
            var actionPlanIndex = new ActionPlanIndex();
            actionPlanIndex.NoLongerRequired = true;
            actionPlanIndex.AnyActionsOverdue = true;
            actionPlanIndex.AnyActionsCompleted = true;
            actionPlanIndex.AnyActionsOutstanding = true;
            actionPlanIndex.AnyActionsNoLongerRequired = false;

            //THEN
            Assert.That(actionPlanIndex.Status, Is.EqualTo(DerivedTaskStatusForDisplay.NoLongerRequired));
        }
    }
}
