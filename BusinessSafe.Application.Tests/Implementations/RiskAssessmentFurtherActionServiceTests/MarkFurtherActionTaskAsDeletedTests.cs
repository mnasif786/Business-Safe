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
    public class MarkFurtherActionTaskAsDeletedTests
    {
        private Mock<ITasksRepository> _tasksRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new Mock<IUserForAuditingRepository>();
            _tasksRepository = new Mock<ITasksRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_a_valid_request_When_Mark_As_Deleted_Then_should_call_appropriate_methods()
        {
            // Given
            var target = CreateTaskService();

            var request = new MarkTaskAsDeletedRequest()
                              {
                                  CompanyId = 1,
                                  TaskId = 2,
                                  UserId = Guid.NewGuid()
                              };


            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            var mockFurtherControlMeasureTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();

            _tasksRepository
                .Setup(x => x.GetByIdAndCompanyId(request.TaskId, request.CompanyId))
                .Returns(mockFurtherControlMeasureTask.Object);

            // When
            target.MarkTaskAsDeleted(request);

            // Then
            _userRepository.VerifyAll();
            _tasksRepository.VerifyAll();
            mockFurtherControlMeasureTask.Verify(x => x.MarkForDelete(_user));
        }

        private TaskService CreateTaskService()
        {
            var target = new TaskService(
                null, 
                _log.Object, 
                _tasksRepository.Object, 
                _userRepository.Object, 
                null, 
                null);
            return target;
        }
    }
}