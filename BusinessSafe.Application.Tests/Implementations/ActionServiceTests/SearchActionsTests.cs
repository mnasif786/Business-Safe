using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Implementations.ActionPlan;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ActionServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SearchActionTests
    {
        private Mock<IActionRepository> _actionRepository;

        [SetUp]
        public void Setup()
        {
            _actionRepository = new Mock<IActionRepository>();
        }

        [Test]
        public void When_Search_Then_map_request_to_repository_call()
        {
            //given
            var target = GetTarget();

            //when
            SearchActionRequest request = new SearchActionRequest()
                                              {
                                                  ActionPlanId = 123L
                                              };
            target.Search(request);

            //then
            _actionRepository.Verify(x => x.GetAll());
        }

        [Test]
        public void When_Search_Then_map_returned_entities_to_dtos()
        {
            //given
            var actions = new List<BusinessSafe.Domain.Entities.Action>()
                                  {                      
                                        new BusinessSafe.Domain.Entities.Action()
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
                                        }
                                  };


            var target = GetTarget();

            _actionRepository.Setup(x => x.GetAll()).Returns(actions);

            //when
            SearchActionRequest request = new SearchActionRequest()
            {
                ActionPlanId = 123123L
            };
            var result = target.Search(request);

            //then
            Assert.That(result.FirstOrDefault().Id, Is.EqualTo(actions.FirstOrDefault().Id));
            Assert.That(result.FirstOrDefault().Title, Is.EqualTo(actions.FirstOrDefault().Title));                   
        }


        private IActionService GetTarget()
        {
            return new ActionService(_actionRepository.Object, null, null, null, null, null,null, null, null);
        }
    }
}
