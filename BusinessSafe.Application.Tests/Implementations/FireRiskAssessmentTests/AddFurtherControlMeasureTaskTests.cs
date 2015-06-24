using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using NServiceBus;
using BusinessSafe.Messages.Events;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AddFurtherControlMeasureTaskTests
    {
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<ISignificantFindingRepository> _significantFindingRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IDocumentParameterHelper> _documentParameterHelper;
        private Mock<ITaskCategoryRepository> _taskCategoryRepository;
        private Mock<IBus> _bus;

        [SetUp]
        public void Setup()
        {
            _userRepository = new Mock<IUserForAuditingRepository>();
            _significantFindingRepository = new Mock<ISignificantFindingRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _taskCategoryRepository = new Mock<ITaskCategoryRepository>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void Given_valid_request_When_AddFurtherControlMeasureTask_is_called_Then_should_call_appropiate_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var request = new SaveFurtherControlMeasureTaskRequest
                              {
                                  Reference = "my reference",
                                  Title = "Title",
                                  Description = "my description",
                                  TaskAssignedToId = Guid.NewGuid(),
                                  CompanyId = 123L,
                                  UserId = Guid.NewGuid(),
                                  CreateDocumentRequests = new List<CreateDocumentRequest>(),
                              };

            
            var fireRiskAssessmentChecklist = new Mock<FireRiskAssessmentChecklist>();
            fireRiskAssessmentChecklist
                .Setup(x => x.FireRiskAssessment)
                .Returns(new FireRiskAssessment());

            var fireAnswer = new Mock<FireAnswer>();
            fireAnswer
                .Setup(x => x.Self)
                .Returns(fireAnswer.Object);
            fireAnswer
                .Setup(x => x.FireRiskAssessmentChecklist)
                .Returns(fireRiskAssessmentChecklist.Object);
            fireAnswer
                .Setup(x => x.Question)
                .Returns(new Question());

            var significantFinding = new Mock<SignificantFinding>();
            significantFinding
                .Setup(x => x.FireAnswer)
                .Returns(fireAnswer.Object);
            

            _significantFindingRepository
                .Setup(x => x.GetById(request.SignificantFindingId))
                .Returns(significantFinding.Object);


            var employee = new Employee();
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.TaskAssignedToId, request.CompanyId))
                .Returns(employee);

            var taskCategory = new TaskCategory();
            _taskCategoryRepository
                .Setup(x => x.GetFireRiskAssessmentTaskCategory())
                .Returns(taskCategory);

            var user = new UserForAuditing();
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user)
                ;

            significantFinding
                .Setup(x => x.AddFurtherControlMeasureTask(It.Is<FireRiskAssessmentFurtherControlMeasureTask>(y =>
                                                                                                              y.Reference == request.Reference &&
                                                                                                              y.Title == request.Title &&
                                                                                                              y.Description == request.Description &&
                                                                                                              y.CreatedBy == user &&
                                                                                                              y.TaskAssignedTo == employee &&
                                                                                                              y.Category == taskCategory
                                                               ), user));
            //When
            riskAssessmentService.AddFurtherControlMeasureTask(request);

            //Then
            _taskCategoryRepository.VerifyAll();
            _employeeRepository.VerifyAll();
            _userRepository.VerifyAll();
            significantFinding.VerifyAll();
            _significantFindingRepository.Verify(x => x.SaveOrUpdate(significantFinding.Object));

        }

        [Test]
        public void Given_valid_request_When_AddFurtherControlMeasureTask_is_called_Then_taskGuid_is_set_correctly()
        {
            //Given
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(
                    () => new UserForAuditing());

            var significantFinding = new Mock<SignificantFinding>() {CallBase = true};
            significantFinding.Object.FireAnswer = new FireAnswer
                                                       {
                                                           FireRiskAssessmentChecklist =
                                                               new FireRiskAssessmentChecklist
                                                                   {FireRiskAssessment = new FireRiskAssessment()},
                                                           Question = new Question()
                                                       };

        _significantFindingRepository
                .Setup(x => x.GetById(It.IsAny<long>())).Returns(() => significantFinding.Object);

            FireRiskAssessmentFurtherControlMeasureTask task = null;
            significantFinding
                .Setup(x => x.AddFurtherControlMeasureTask(It.IsAny<FireRiskAssessmentFurtherControlMeasureTask>(), It.IsAny<UserForAuditing>()))
                .Callback((FireRiskAssessmentFurtherControlMeasureTask taskValue, UserForAuditing userValue) => task = taskValue);

            _taskCategoryRepository
                .Setup(x => x.GetFireRiskAssessmentTaskCategory()).Returns(() => new TaskCategory());

            var riskAssessmentService = CreateRiskAssessmentServicePartialMock();
            //riskAssessmentService
            //    .Setup(x => x.Map(It.IsAny<FireRiskAssessmentFurtherControlMeasureTask>()))
            //    .Returns(() => new FireRiskAssessmentFurtherControlMeasureTaskDto());

            var taskGuid = Guid.NewGuid();
            var request = SaveFurtherControlMeasureTaskRequest.Create(
                "Title",
                "description test",
                "reference test",
                DateTime.Now.ToString(),
                1,
                1,
                1,
                1,
                Guid.NewGuid(),
                1,
                DateTime.Now.ToString(),
                DateTime.Now,
                Guid.NewGuid(),
                new List<CreateDocumentRequest>(),
                new List<long>(),
                false,
                false,
                false,
                false,
                taskGuid
                );
         
            //When
            riskAssessmentService.Object.AddFurtherControlMeasureTask(request);

            //Then
            Assert.AreEqual(taskGuid, task.TaskGuid);

        }

        private FireRiskAssessmentFurtherControlMeasureTaskService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new FireRiskAssessmentFurtherControlMeasureTaskService(
                _userRepository.Object,
                _significantFindingRepository.Object,
                _employeeRepository.Object,
                _documentParameterHelper.Object,
                _taskCategoryRepository.Object,
                null,
                _bus.Object);
            return riskAssessmentService;
        }

        private Mock<FireRiskAssessmentFurtherControlMeasureTaskService> CreateRiskAssessmentServicePartialMock()
        {
            var constructorParameters = new object[]
                                            {
                                                _userRepository.Object,
                                                _significantFindingRepository.Object,
                                                _employeeRepository.Object,
                                                _documentParameterHelper.Object,
                                                _taskCategoryRepository.Object,
                                                null,
                                                _bus.Object
                                            };

            return new Mock<FireRiskAssessmentFurtherControlMeasureTaskService>(constructorParameters){ CallBase = true};
        }
    }
}