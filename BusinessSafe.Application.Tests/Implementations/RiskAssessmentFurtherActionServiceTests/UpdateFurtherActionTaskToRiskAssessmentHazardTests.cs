using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentFurtherActionServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateFurtherActionTaskToRiskAssessmentHazardTests
    {
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IDocumentTypeRepository> _documentTypeRepository;
        private Mock<IFurtherControlMeasureTasksRepository> _furtherControlMeasureTaskRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;
        private Mock<IDocumentParameterHelper> _documentParameterHelper;
        private Mock<IUserRepository> _userRepo;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new Mock<IUserForAuditingRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _documentTypeRepository = new Mock<IDocumentTypeRepository>();
            _furtherControlMeasureTaskRepository = new Mock<IFurtherControlMeasureTasksRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _userRepo = new Mock<IUserRepository>();
        }

        [Test]
        public void Given_that_update_further_action_task_to_risk_assessment_is_called_with_no_task_assigned_to_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateRiskAssessmentFurtherActionService();

            var request = new UpdateFurtherControlMeasureTaskRequest()
            {
                TaskAssignedToId = Guid.Empty
            };

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            var mockFurtherActionTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();

            _furtherControlMeasureTaskRepository
                .Setup(x => x.GetById(request.Id))
                .Returns(mockFurtherActionTask.Object);

            //When
            target.Update(request);

            //Then
            _furtherControlMeasureTaskRepository.Verify(x => x.SaveOrUpdate(mockFurtherActionTask.Object));
            _userRepository.VerifyAll();
            _employeeRepository.Verify(x => x.GetByIdAndCompanyId(request.TaskAssignedToId, request.CompanyId), Times.Never());
            mockFurtherActionTask.Verify(x => x.Update(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime?>(),
                It.IsAny<TaskStatus>(),
                It.IsAny<List<CreateDocumentParameters>>(),
                It.IsAny<List<long>>(),
                It.IsAny<int>(),
                It.IsAny<DateTime?>(),
                It.IsAny<Employee>(),
                It.IsAny<UserForAuditing>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()
                ));
        }

        [Test]
        public void When_Update_Then_fields_mapped_to_Task_Update()
        {
            //Given
            var target = CreateRiskAssessmentFurtherActionService();

            var request = new UpdateFurtherControlMeasureTaskRequest()
            {
                CompanyId = 1234L,
                CompletedComments = "completed comments",
                CreateDocumentRequests = new List<CreateDocumentRequest>(),
                Description = "description",
                DocumentLibraryIdsToDelete = new List<long>(),
                Id = 5678L,
                Reference = "reference",
                SendTaskCompletedNotification = true,
                SendTaskNotification = true,
                SendTaskOverdueNotification = true,
                TaskAssignedToId = Guid.NewGuid(),
                TaskCompletionDueDate = DateTime.Now,
                TaskReoccurringEndDate = DateTime.Now,
                TaskReoccurringTypeId = 123,
                TaskStatus = TaskStatus.Completed,
                Title = "title",
                UserId = Guid.NewGuid()
            };

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            var mockFurtherActionTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();

            _furtherControlMeasureTaskRepository
                .Setup(x => x.GetById(request.Id))
                .Returns(mockFurtherActionTask.Object);

            //When
            target.Update(request);

            //Then
            _furtherControlMeasureTaskRepository.Verify(x => x.SaveOrUpdate(mockFurtherActionTask.Object));
            _userRepository.VerifyAll();
            mockFurtherActionTask.Verify(x => x.Update(
                request.Reference,
                request.Title,
                request.Description,
                request.TaskCompletionDueDate,
                request.TaskStatus,
                It.IsAny<List<CreateDocumentParameters>>(),
                request.DocumentLibraryIdsToDelete,
                request.TaskReoccurringTypeId,
                request.TaskReoccurringEndDate,
                It.IsAny<Employee>(),
                It.IsAny<UserForAuditing>(),
                request.SendTaskCompletedNotification,
                request.SendTaskNotification,
                request.SendTaskOverdueNotification,
                request.SendTaskDueTomorrowNotification
            ));
        }

        [Test]
        public void Given_that_update_further_action_task_to_risk_assessment_is_called_with_task_assigned_to_Then_should_call_employee_get()
        {
            //Given
            var target = CreateRiskAssessmentFurtherActionService();

            var request = new UpdateFurtherControlMeasureTaskRequest()
            {
                TaskAssignedToId = Guid.NewGuid()
            };

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            var mockFurtherActionTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            _furtherControlMeasureTaskRepository
                .Setup(x => x.GetById(request.Id))
                .Returns(mockFurtherActionTask.Object);

            //When
            target.Update(request);

            //Then
            _employeeRepository.Verify(x => x.GetByIdAndCompanyId(request.TaskAssignedToId, request.CompanyId), Times.Once());

        }

        [Test]
        public void Given_that_update_further_action_task_to_risk_assessment_is_called_with_task_assigned_to_Then_flush_task_repo()
        {
            //Given
            var target = CreateRiskAssessmentFurtherActionService();

            var request = new UpdateFurtherControlMeasureTaskRequest()
            {
                TaskAssignedToId = Guid.NewGuid()
            };

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            var mockFurtherActionTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            _furtherControlMeasureTaskRepository
                .Setup(x => x.GetById(request.Id))
                .Returns(mockFurtherActionTask.Object);

            //When
            target.Update(request);

            //Then
            _furtherControlMeasureTaskRepository.Verify(x => x.Flush(), Times.Once());

        }

        private FurtherControlMeasureTaskService CreateRiskAssessmentFurtherActionService()
        {
            var target = new FurtherControlMeasureTaskService(
                _userRepository.Object, 
                _furtherControlMeasureTaskRepository.Object, 
                _employeeRepository.Object, 
                _log.Object,
                _documentParameterHelper.Object,
                null,
                null,
                _userRepo.Object
                , null, null
                );
            return target;
        }
    }
}