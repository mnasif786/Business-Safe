using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Implementations.ActionPlan;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Action = BusinessSafe.Domain.Entities.Action;

namespace BusinessSafe.Application.Tests.Implementations.ActionServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class AssignActionTaskTests
    {
        private Mock<IActionRepository> _actionRepository;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<ITaskCategoryRepository> _taskCategoryRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<IBus> _bus;
        private Mock<ITasksRepository> _tasksRepository;
        [SetUp]
        public void Setup()
        {
            _actionRepository = new Mock<IActionRepository>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _taskCategoryRepository = new Mock<ITaskCategoryRepository>();
            _log = new Mock<IPeninsulaLog>();
            _tasksRepository = new Mock<ITasksRepository>();
            _bus = new Mock<IBus>();
        }
      
        [Test]
        public void When_AssignAction_Task_Then_Calls_Correct_Methods()
        {
            //given
            var request = new AssignActionTaskRequest
                              {
                                  CompanyId = 1234L,
                                  UserId = Guid.NewGuid(),
                                  ActionId = 1L,
                                  AssignedTo = Guid.NewGuid(),
                                  DueDate = DateTime.Now
                              };

            var action = new Action
                             {
                                 Id = 1L,
                                 Title = "Action1",
                                 ActionPlan = new ActionPlan
                                                  {
                                                      Id = 1L,
                                                      Site = new Site
                                                                 {
                                                                     SiteId = 1L
                                                                 }
                                                  }
                             };

            _actionRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(action);
            _userForAuditingRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new UserForAuditing{Id = Guid.NewGuid()});
            _employeeRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Employee { Id = Guid.NewGuid() });
            _siteRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(new Site {Id = 1L});
            _taskCategoryRepository.Setup(x => x.GetActionTaskCategory()).Returns(new TaskCategory {Id = 8});

            var target = GetTarget();

            //when
            target.AssignActionTask(request);

            //then
            _actionRepository.Verify(x => x.GetById(It.Is<long>(y => y == request.ActionId)));
            _userForAuditingRepository.Verify(x => x.GetById(It.Is<Guid>(y => y == request.UserId)));
            _employeeRepository.Verify(x=> x.GetById(It.Is<Guid>(y=>y == request.AssignedTo)));
            _siteRepository.Verify(x=>x.GetById(It.IsAny<long>()));
            _taskCategoryRepository.Verify(x => x.GetActionTaskCategory());
        }


        [Test]
        public void When_AssignAction_Task_Then_Saved_Action_With_Task()
        {
            //given
            var request = new AssignActionTaskRequest
            {
                CompanyId = 1234L,
                UserId = Guid.NewGuid(),
                ActionId = 1L,
                AssignedTo = Guid.NewGuid(),
                DueDate = DateTime.Now
            };

            var action = new Action
            {
                Id = 1L,
                ActionPlan = new ActionPlan
                {
                    Id = 1L,
                    Site = new Site
                    {
                        SiteId = 1L
                    }
                }
            };

            _actionRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(action);

            var target = GetTarget();

            //when
            target.AssignActionTask(request);

            //then
            _actionRepository.Verify(x => x.SaveOrUpdate(It.Is<Action>(a => a.Id == request.ActionId)));

        }

        private IActionService GetTarget()
        {
            return new ActionService(_actionRepository.Object, _userForAuditingRepository.Object,
                                     _employeeRepository.Object, _siteRepository.Object, _taskCategoryRepository.Object,
                                     _log.Object, _bus.Object, _tasksRepository.Object, null);
        }
    }
}
