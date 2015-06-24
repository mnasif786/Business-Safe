using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.WebSite.Areas.ActionPlans.Factories;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.ActionPlan.SearchAction
{
    [TestFixture]
    [Category("Unit")]

    public class SearchActionViewModelFactoryTests
    {

        private Mock<IActionService> _actionService;
        private Mock<IActionPlanService> _actionPlanService;
        private Mock<IEmployeeService> _employeeService;

        [SetUp]
        public void Setup()
        {
            _actionService = new Mock<IActionService>();
            _actionPlanService = new Mock<IActionPlanService>();
            _employeeService = new Mock<IEmployeeService>();
        }

        [Test]
        public void given_company_id_when_get_view_model_then_service_is_called_with_corect_parameters()
        {
            //given
            var actionPlanId = 1L;
            var companyId = 2L;
            var target = GetTarget();

            _actionPlanService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new ActionPlanDto() { Site = new SiteDto(){} });
            
            //when
            var result = target.WithCompanyId(companyId).WithActionPlanId(actionPlanId).GetViewModel();

            //then
            _actionPlanService.Verify(x => x.GetByIdAndCompanyId(actionPlanId,companyId), Times.Once());
        }

        [Test]
        public void given_factory_when_get_view_model_then_result_maps_ActionPlanDto_to_viewmodel()
        {
            //given
            var actionPlan = new ActionPlanDto()
                               {
                                   Id = 1234L,
                                   CompanyId = 1L,
                                   Title = "Action Plan1",
                                   Site = new SiteDto
                                              {
                                                  SiteId = 1L,
                                                  Name = "Site1"
                                              },
                                   DateOfVisit = DateTime.Now,
                                   VisitBy = "Consultant1",
                                   SubmittedOn = DateTime.Now.AddDays(-1)                                   
                               };
            var action = new ActionDto()
                             {
                                 Id = 123123,
                                 Title = "test title"
                                 ,
                                 AreaOfNonCompliance = "area not compliaint",
                                 ActionRequired = "action required test",
                                 Category = ActionCategory.Action,
                                 QuestionStatus = ActionQuestionStatus.Red

                             };
            
            var task = new ActionTaskDto
                           {
                               Id = 1L,
                               TaskAssignedTo = new EmployeeDto
                                                    {
                                                        Id = Guid.NewGuid()
                                                    },
                               TaskCompletionDueDate = DateTime.Now.ToShortDateString(),
                               
                           };

            action.ActionTasks = new List<ActionTaskDto>() {(task)};
            actionPlan.Actions = new List<ActionDto>() { action };

            var target = GetTarget();
            _actionPlanService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(actionPlan);

            //when
            var result = target.WithActionPlanId(actionPlan.Id).GetViewModel();

            //then
            Assert.That(result.SiteName, Is.EqualTo(actionPlan.Site.Name), "Site name not mapped");
            Assert.That(result.VisitDate, Is.EqualTo(actionPlan.DateOfVisit), "Visit Date name not mapped");
            Assert.That(result.Actions.FirstOrDefault().HasTask, Is.True);
            Assert.That(result.Actions.First().AssignedTo == task.TaskAssignedTo.Id);
            Assert.That(result.Actions.First().DueDate == DateTime.Parse(task.TaskCompletionDueDate));

        }

        [Test]
        public void given_factory_when_get_view_model_then_result_maps_dto_actions_to_viewmodel_actions()
        {
            //given
            var actionPlan = new ActionPlanDto()
                                 {
                                     Site = new SiteDto
                                                {
                                                    SiteId = 1L,
                                                    Name = "Site1"
                                                }
                                 };
            var action = new ActionDto()
                             {
                                 Id = 123123,
                                 Title = "test title"
                                 ,
                                 AreaOfNonCompliance = "area not compliant",
                                 ActionRequired = "action required test",
                                 GuidanceNote = "1-3",
                                 TargetTimescale = "do this now",
                                 AssignedTo = Guid.NewGuid(),
                                 DueDate = DateTime.Now.AddDays(10),
                                 Status = DerivedTaskStatusForDisplay.None,
                                 Category = ActionCategory.Action,
                                 QuestionStatus = ActionQuestionStatus.Red
                                 
                             };
            actionPlan.Actions = new List<ActionDto>() {action};

            var target = GetTarget();
            _actionPlanService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(actionPlan);

            //when
            var result = target.WithActionPlanId(actionPlan.Id).GetViewModel();

            //then
            Assert.That(result.Actions.First().Id, Is.EqualTo(action.Id));
            Assert.That(result.Actions.First().AreaOfNonCompliance, Is.EqualTo(action.AreaOfNonCompliance), "AreaOfNonCompliance not mapped");
            Assert.That(result.Actions.First().ActionRequired, Is.EqualTo(action.ActionRequired), "ActionRequired not mapped");
            Assert.That(result.Actions.First().GuidanceNote, Is.EqualTo(action.GuidanceNote), "GuidanceNote not mapped");
            Assert.That(result.Actions.First().TargetTimescale, Is.EqualTo(action.TargetTimescale), "TargetTimescale not mapped");
            Assert.That(result.Actions.First().AssignedTo, Is.EqualTo(action.AssignedTo), "AssignedTo not mapped");
            Assert.That(result.Actions.First().DueDate, Is.EqualTo(action.DueDate), "DueDate not mapped");

        }


        [Test]
        public void given_factory_when_get_view_model_then_result_maps_dto_IRN_actions_to_viewmodel_actions()
        {
            //given
            var actionPlan = new ActionPlanDto()
            {
                Site = new SiteDto
                {
                    SiteId = 1L,
                    Name = "Site1"
                }
            };

            var action = new ActionDto()
            {
                Id = 123123,
                Title = "test title",
                AreaOfNonCompliance = "area not compliant",
                ActionRequired = "action required test",
                GuidanceNote = "1-3",
                TargetTimescale = "do this now",
                AssignedTo = Guid.NewGuid(),
                DueDate = DateTime.Now.AddDays(10),
                Status = DerivedTaskStatusForDisplay.None,
                Reference = "The Reference",        
                Category = ActionCategory.ImmediateRiskNotification
                                            
            };

            actionPlan.Actions = new List<ActionDto>() { action };

            var target = GetTarget();
            _actionPlanService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(actionPlan);

            //when
            var result = target.WithActionPlanId(actionPlan.Id).GetViewModel();

            //then            
            Assert.That(result.ImmediateRiskNotification.Count(), Is.GreaterThan(0), "No entries in Immediate Risk Notifications");
            Assert.That(result.ImmediateRiskNotification.First().Id, Is.EqualTo(action.Id));
            Assert.That(result.ImmediateRiskNotification.First().Title, Is.EqualTo(action.Title), "Action title not mapped");
            Assert.That(result.ImmediateRiskNotification.First().Reference, Is.EqualTo(action.Reference), "Reference not mapped");            
            Assert.That(result.ImmediateRiskNotification.First().AssignedTo, Is.EqualTo(action.AssignedTo), "AssignedTo not mapped");
            Assert.That(result.ImmediateRiskNotification.First().DueDate, Is.EqualTo(action.DueDate), "DueDate not mapped");
            Assert.That(result.ImmediateRiskNotification.First().RecommendedImmediateAction, Is.EqualTo(action.ActionRequired), "RecommendedImmediateAction not mapped");
            Assert.That(result.ImmediateRiskNotification.First().SignificantHazardIdentified, Is.EqualTo(action.AreaOfNonCompliance), "SignificantHazardIdentified not mapped");
        }

        [Test]
        public void given_factory_when_get_view_model_then_result_maps_IRN_And_NonIRN_actions_to_correct_viewmodel_actions()
        {
            //given
            var actionPlan = new ActionPlanDto()
            {
                Site = new SiteDto
                {
                    SiteId = 1L,
                    Name = "Site1"
                }
            };

            var action = new ActionDto()
            {
                Id = 123123,
                Title = "test title",
                AreaOfNonCompliance = "area not compliant",
                ActionRequired = "action required test",
                GuidanceNote = "1-3",
                TargetTimescale = "do this now",
                AssignedTo = Guid.NewGuid(),
                DueDate = DateTime.Now.AddDays(10),
                Status = DerivedTaskStatusForDisplay.None,
                Reference = "The Reference",
                Category = ActionCategory.Action,
                QuestionStatus = ActionQuestionStatus.Red

            };

            var IRNaction = new ActionDto()
            {
                Id = 123123,
                Title = "IRN test title",
                AreaOfNonCompliance = "IRN area not compliant",
                ActionRequired = "IRN action required test",
                GuidanceNote = "IRN 1-3",
                TargetTimescale = "IRN do this now",
                AssignedTo = Guid.NewGuid(),
                DueDate = DateTime.Now.AddDays(3),
                Status = DerivedTaskStatusForDisplay.None,
                Reference = "IRN The Reference",
                Category = ActionCategory.ImmediateRiskNotification
            };

            actionPlan.Actions = new List<ActionDto>(){ action, IRNaction };

            var target = GetTarget();
            _actionPlanService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(actionPlan);

            //when
            var result = target.WithActionPlanId(actionPlan.Id).GetViewModel();

            //then      
            Assert.That(result.Actions.First().Id, Is.EqualTo(action.Id), "No entries in Actions");
            Assert.That(result.Actions.First().AreaOfNonCompliance, Is.EqualTo(action.AreaOfNonCompliance), "AreaOfNonCompliance not mapped");
            Assert.That(result.Actions.First().ActionRequired, Is.EqualTo(action.ActionRequired), "ActionRequired not mapped");
            Assert.That(result.Actions.First().GuidanceNote, Is.EqualTo(action.GuidanceNote), "GuidanceNote not mapped");
            Assert.That(result.Actions.First().TargetTimescale, Is.EqualTo(action.TargetTimescale), "TargetTimescale not mapped");
            Assert.That(result.Actions.First().AssignedTo, Is.EqualTo(action.AssignedTo), "AssignedTo not mapped");
            Assert.That(result.Actions.First().DueDate, Is.EqualTo(action.DueDate), "DueDate not mapped");

            Assert.That(result.ImmediateRiskNotification.Count(), Is.GreaterThan(0), "No entries in Immediate Risk Notifications");
            Assert.That(result.ImmediateRiskNotification.First().Id, Is.EqualTo(IRNaction.Id));
            Assert.That(result.ImmediateRiskNotification.First().Title, Is.EqualTo(IRNaction.Title), "Action title not mapped");
            Assert.That(result.ImmediateRiskNotification.First().Reference, Is.EqualTo(IRNaction.Reference), "Reference not mapped");
            Assert.That(result.ImmediateRiskNotification.First().AssignedTo, Is.EqualTo(IRNaction.AssignedTo), "AssignedTo not mapped");
            Assert.That(result.ImmediateRiskNotification.First().DueDate, Is.EqualTo(IRNaction.DueDate), "DueDate not mapped");
            Assert.That(result.ImmediateRiskNotification.First().RecommendedImmediateAction, Is.EqualTo(IRNaction.ActionRequired), "RecommendedImmediateAction not mapped");
            Assert.That(result.ImmediateRiskNotification.First().SignificantHazardIdentified, Is.EqualTo(IRNaction.AreaOfNonCompliance), "SignificantHazardIdentified not mapped");
        }

        [Test]
        public void given_site_is_null_when_get_view_model_then_action_site_is_null()
        {
            //given
            var actionPlan = new ActionPlanDto() {};

            var IRNaction = new ActionDto()
            {
                Id = 123123,
                Title = "IRN test title",
                AreaOfNonCompliance = "IRN area not compliant",
                ActionRequired = "IRN action required test",
                GuidanceNote = "IRN 1-3",
                TargetTimescale = "IRN do this now",
                AssignedTo = Guid.NewGuid(),
                DueDate = DateTime.Now.AddDays(3),
                Status = DerivedTaskStatusForDisplay.None,
                Reference = "IRN The Reference",
                Category = ActionCategory.ImmediateRiskNotification
            };

            actionPlan.Actions = new List<ActionDto>() { IRNaction };

            var target = GetTarget();
            _actionPlanService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(actionPlan);

            //when
            var result = target.WithActionPlanId(actionPlan.Id).GetViewModel();

            //then
            Assert.IsNull(result.SiteId);
            Assert.IsNull(result.SiteName);
            

        }
        public SearchActionViewModelFactory GetTarget()
        {
            return new SearchActionViewModelFactory(_actionService.Object, _actionPlanService.Object, _employeeService.Object);
        }
    }
}
