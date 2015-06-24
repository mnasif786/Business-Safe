using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.ActionPlan;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using Action = BusinessSafe.Domain.Entities.Action;

namespace BusinessSafe.Application.Tests.Implementations.ActionPlanServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SearchActionPlanTests
    {
        private Mock<IActionPlanRepository> _actionPlanRepository;

        [SetUp]
        public void Setup()
        {
            _actionPlanRepository = new Mock<IActionPlanRepository>();
        }

        [Test]
        public void When_Search_Then_map_request_to_repository_call()
        {
            //given
            var target = GetTarget();
            var companyId = 1233L;
            var page = 1;
            var pageSize = 10;
            var orderyBy = ActionPlanOrderByColumn.Title;

            var request = new SearchActionPlanRequest
                              {
                                  CompanyId = companyId,
                                  Page = page,
                                  PageSize = pageSize,
                                  OrderBy = orderyBy,
                                  Ascending = true
                              };
            //when
            target.Search(request);
            //then
            _actionPlanRepository.Verify(x=>x.Search(null, It.Is<long>(c=>c ==companyId), null, null, null, null, false, 
                It.Is<int>(pg=>pg == page),
                It.Is<int>(pgz=>pgz == pageSize),
                It.Is<ActionPlanOrderByColumn>(o=> o == orderyBy),
                It.Is<bool>(asc=>asc)));
        }

        [Test]
        public void When_Search_Then_map_returned_entities_to_dtos()
        {
            //given
            var actionPlans = new List<ActionPlan>()
                                  {
                                      new ActionPlan()
                                          {
                                              Id = 1234L,
                                              CompanyId = 1L,
                                              Title = "Action Plan1",
                                              Site = new Site
                                                         {
                                                             SiteId = 1L
                                                         },
                                              DateOfVisit = DateTime.Now,
                                              VisitBy = "Consultant1",
                                              SubmittedOn = DateTime.Now.AddDays(-1),
                                              AreasVisited = "Area 1",
                                              AreasNotVisited = "Area 2"
                                          }
                                  };

            var target = GetTarget();

            _actionPlanRepository.Setup(
                x =>
                x.Search(It.IsAny<IList<long>>(), It.IsAny<long>(), It.IsAny<long?>(), It.IsAny<long?>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool>(), 
                        It.IsAny<int>(), It.IsAny<int>(),
                         It.IsAny<ActionPlanOrderByColumn>(),
                         It.IsAny<bool>()))
                         .Returns(actionPlans);

            //when
            var result = target.Search(new SearchActionPlanRequest());
            
            //then
            Assert.That(result.FirstOrDefault().Id, Is.EqualTo(actionPlans.FirstOrDefault().Id));
            Assert.That(result.FirstOrDefault().CompanyId, Is.EqualTo(actionPlans.FirstOrDefault().CompanyId));
            Assert.That(result.FirstOrDefault().Title, Is.EqualTo(actionPlans.FirstOrDefault().Title));
            Assert.That(result.FirstOrDefault().Site.Id, Is.EqualTo(actionPlans.FirstOrDefault().Site.Id));
            Assert.That(result.FirstOrDefault().DateOfVisit, Is.EqualTo(actionPlans.FirstOrDefault().DateOfVisit));
            Assert.That(result.FirstOrDefault().VisitBy, Is.EqualTo(actionPlans.FirstOrDefault().VisitBy));
            Assert.That(result.FirstOrDefault().SubmittedOn, Is.EqualTo(actionPlans.FirstOrDefault().SubmittedOn));
            Assert.That(result.FirstOrDefault().AreasVisited, Is.EqualTo(actionPlans.FirstOrDefault().AreasVisited));
            Assert.That(result.FirstOrDefault().AreasNotVisited, Is.EqualTo(actionPlans.FirstOrDefault().AreasNotVisited));
        }

        [Test]
        public void When_Search_Then_map_returned_entities_with_actions_to_dtos()
        {
            //given

            var actionPlan =
                new ActionPlan()
                    {
                        Id = 1234L,
                        CompanyId = 1L,
                        Title = "Action Plan1",
                        Site = new Site
                                   {
                                       SiteId = 1L
                                   },
                        DateOfVisit = DateTime.Now,
                        VisitBy = "Consultant1",
                        SubmittedOn = DateTime.Now.AddDays(-1),                       
                    };


            var action =
                new Action()
                    {
                        Id = 123123,
                        Title = "test title",
                        AreaOfNonCompliance = "area not compliant",
                        ActionRequired = "action required test",                        
                        TargetTimescale = "do this now",
                        AssignedTo = new Employee(){Id = Guid.NewGuid(), Forename = "Fred", Surname = "Flintstone"},
                        DueDate = DateTime.Now.AddDays(10),
                        Reference = "The Reference",
                        Category = ActionCategory.Action
                    };

            actionPlan.Actions = new List<Action>() {action};

            var target = GetTarget();

            _actionPlanRepository.Setup(
                x =>
                x.Search(It.IsAny<IList<long>>(),It.IsAny<long>(), It.IsAny<long?>(), It.IsAny<long?>(), It.IsAny<DateTime?>(),
                         It.IsAny<DateTime?>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ActionPlanOrderByColumn>(),
                         It.IsAny<bool>())).Returns(new List<ActionPlan>() {actionPlan});

            //when
            var result = target.Search(new SearchActionPlanRequest());

            //then
            Assert.That(result.First().Actions.Count(), Is.GreaterThan(0));
            Assert.That(result.First().Actions.First().Id, Is.EqualTo(action.Id));  
            Assert.That(result.First().Actions.First().Title, Is.EqualTo(action.Title));  
            Assert.That(result.First().Actions.First().AreaOfNonCompliance, Is.EqualTo(action.AreaOfNonCompliance));  
            Assert.That(result.First().Actions.First().ActionRequired, Is.EqualTo(action.ActionRequired));       
            Assert.That(result.First().Actions.First().TargetTimescale, Is.EqualTo(action.TargetTimescale));              
            Assert.That(result.First().Actions.First().AssignedTo, Is.EqualTo(action.AssignedTo.Id));  
            Assert.That(result.First().Actions.First().DueDate, Is.EqualTo(action.DueDate));  
            Assert.That(result.First().Actions.First().Reference, Is.EqualTo(action.Reference));
            Assert.That(result.First().Actions.First().Category, Is.EqualTo(action.Category));   
        }

        private IActionPlanService GetTarget()
        {
            return new ActionPlanService(_actionPlanRepository.Object);
        }
    }
}
