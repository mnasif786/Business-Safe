using System;
using BusinessSafe.Application.Implementations.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentFurtherActionServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkAsNoLongerRequiredTest
    {
        private Mock<ITasksRepository> _tasksRepo;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;

        [SetUp]
        public void SetUp()
        {
            _tasksRepo = new Mock<ITasksRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_a_valid_request_When_Mark_As_No_Longer_Required_Then_should_call_appropriate_methods()
        {
            // Given
            var target = CreateRiskAssessmentFurtherActionService();

            var request = new MarkTaskAsNoLongerRequiredRequest()
                              {
                                  CompanyId = 1,
                                  TaskId = 4,
                                  UserId = Guid.NewGuid()
                              };


            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            var task = new Mock<Task>();

            _tasksRepo
                .Setup(x => x.GetById(request.TaskId))
                .Returns(task.Object);

            // When
            target.MarkTaskAsNoLongerRequired(request);

            //Then
            _userRepository.Verify(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId), Times.Once());
            _tasksRepo.Verify(x => x.SaveOrUpdate(task.Object));
            task.Verify(x => x.MarkAsNoLongerRequired(_user));
        }

        private TaskService CreateRiskAssessmentFurtherActionService()
        {
            var target = new TaskService(
                null, 
                _log.Object, 
                _tasksRepo.Object, 
                _userRepository.Object, 
                null, 
                null);
            return target;
        }
    }
}