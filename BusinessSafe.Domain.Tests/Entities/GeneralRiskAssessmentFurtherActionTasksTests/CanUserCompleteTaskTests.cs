using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.GeneralRiskAssessmentFurtherActionTasksTests
{
     public class FutherControlMeasuerTaskForTesting: FurtherControlMeasureTask
     {
         private RiskAssessment _riskAssessment;

         protected override Task GetBasisForClone()
         {
             throw new NotImplementedException();
         }

         public override RiskAssessment RiskAssessment
         {
             get { return _riskAssessment; }
         }

         public void SetRiskAssessment(RiskAssessment riskAssessment)
         {
             _riskAssessment = riskAssessment;
         }
     }

    [TestFixture]
    public class CanUserCompleteTests
    {


        public FutherControlMeasuerTaskForTesting GetTarget()
        {
            return new FutherControlMeasuerTaskForTesting();
        }

        [Test]
        public void Given_the_task_is_assigned_to_an_employee_who_belongs_to_a_site_the_user_can_access_when_CanUserComplete_then_return_true()
        {
            //Given
            var siteId = 12345;
            var user = new User();
            user.Site = new Site() { Id = siteId };
            
            var target = GetTarget();
            target.TaskAssignedTo = new Employee() {Site = new Site() {Id = siteId}};

            //when
            var result = target.CanUserComplete(user);

            //then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_the_task_is_assigned_to_an_employee_who_belongs_to_a_child_site_the_user_can_access_when_CanUserComplete_then_return_true()
        {
            //given
            var childSiteId = 187123478;
            var user = new User();
            user.Site = new Site() { Id = 123456 };
            user.Site.Children.Add(new Site() { Id = childSiteId });

            var target = GetTarget();
            target.TaskAssignedTo = new Employee() { Site = new Site() { Id = childSiteId } };

            //when
            var result = target.CanUserComplete(user);

            //then
            Assert.IsTrue(result);
        }

         [Test]
        public void Given_the_task_is_assigned_to_an_employee_who_doesnt_belongs_to_a_site_CanUserComplete_then_return_true()
         {
             //given
             var user = new User();
             user.Site = new Site() {Id = 123456};
             var target = GetTarget();
             target.TaskAssignedTo = new Employee() {Site = null};

             //when
             var result = target.CanUserComplete(user);

             //then
             Assert.IsTrue(result);
         }

        [Test]
        public void Given_the_task_is_assigned_to_an_employee_who_belongs_to_a_site_the_user_cant_access_when_CanUserComplete_then_return_false()
        {
            //given
            var user = new User();
            user.Site = new Site() { Id = 123456 };
            var target = GetTarget();
            target.TaskAssignedTo = new Employee() {Site = new Site() { Id = 354629738 }};
            target.SetRiskAssessment(new GeneralRiskAssessment() { RiskAssessmentSite = new Site() { Id = 98765L }});

            //when
            var result = target.CanUserComplete(user);

            //then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_the_task_is_part_of_an_assessment_that_is_for_a_site_the_user_can_access_When_CanUserComplete_then_return_true()
        {
            //given
            const long siteId = 123456L;
            var user = new User {Site = new Site() {Id = siteId}};

            var target = GetTarget();
            target.TaskAssignedTo = new Employee() { Site = new Site() { Id = 354629738 } };
            target.SetRiskAssessment(new GeneralRiskAssessment() { RiskAssessmentSite = new Site() { Id = siteId } });

            //when
            var result = target.CanUserComplete(user);

            //then
            Assert.IsTrue(result);
        }
    }
}
