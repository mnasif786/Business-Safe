using System;
using System.Collections.Generic;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    public class FurtherControlMeasureTaskServiceTests
    {
        private HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService _target;
        private Mock<IHazardousSubstanceRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IDocumentTypeRepository> _documentTypeRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<ITaskCategoryRepository> _responsibilityTaskCategoryRepository;
        private Mock<IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskRepository> _hazardousSubstanceRiskAssessmentFurtherControlMeasureTaskRepository;
        private Mock<IDocumentParameterHelper> _documentParameterHelper;
        private Mock<IBus> _bus;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IHazardousSubstanceRiskAssessmentRepository>();

            _riskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new HazardousSubstanceRiskAssessment());
            
            _riskAssessmentRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<HazardousSubstanceRiskAssessment>()));
            
            _userRepository = new Mock<IUserForAuditingRepository>();
            
            _employeeRepository = new Mock<IEmployeeRepository>();
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new Employee());

            _documentTypeRepository = new Mock<IDocumentTypeRepository>();
            _documentTypeRepository
                .Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(new DocumentType());

            _responsibilityTaskCategoryRepository = new Mock<ITaskCategoryRepository>();
            _responsibilityTaskCategoryRepository
                .Setup(x => x.GetHazardousSubstanceRiskAssessmentTaskCategory()).Returns(new TaskCategory()
                                                                                         { Id = 6 });

            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _hazardousSubstanceRiskAssessmentFurtherControlMeasureTaskRepository = new Mock<IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskRepository>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _bus = new Mock<IBus>();

            _target = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService(
                _riskAssessmentRepository.Object, 
                _userRepository.Object,
                _employeeRepository.Object,
                _responsibilityTaskCategoryRepository.Object,
                _log.Object,
                _documentParameterHelper.Object
                
            );
        }

        [Test]
        public void When_AddFurtherControlMeasureTask_Then_associated_risk_assessment_saved()
        {
            // Given
            var creatingUserId = Guid.NewGuid();
            var assignedToEmployeeId = Guid.NewGuid();

            var saveRequest = new SaveFurtherControlMeasureTaskRequest()
                              {
                                  RiskAssessmentId = 1234,
                                  CompanyId = 5678,
                                  Title = "new title",
                                  Reference = "new reference",
                                  Description = "new description",
                                  UserId = creatingUserId,
                                  TaskAssignedToId = assignedToEmployeeId,
                                  TaskCompletionDueDate = DateTime.Now
                              };

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new UserForAuditing() { Id = creatingUserId });

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new Employee() { Id = assignedToEmployeeId });

            var returnedRiskAssessment = new Mock<HazardousSubstanceRiskAssessment>();
            returnedRiskAssessment.Setup(x => x.Id).Returns(1234);
            returnedRiskAssessment.Setup(x => x.Reviews).Returns(new List<RiskAssessmentReview>());

            _riskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(returnedRiskAssessment.Object);

            var passedFurtherControlMeasureTask = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask();

            returnedRiskAssessment
                .Setup(x => x.AddFurtherControlMeasureTask(It.IsAny<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>(), It.IsAny<UserForAuditing>()))
                .Callback<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, UserForAuditing>((task, user) => passedFurtherControlMeasureTask = task);

            // When
            _target.AddFurtherControlMeasureTask(saveRequest);

            // Then
            _riskAssessmentRepository.Verify(x => x.GetByIdAndCompanyId(saveRequest.RiskAssessmentId, saveRequest.CompanyId));
            _riskAssessmentRepository.Verify(x => x.SaveOrUpdate(It.IsAny<Domain.Entities.HazardousSubstanceRiskAssessment>()));
            _responsibilityTaskCategoryRepository.Verify(x => x.GetHazardousSubstanceRiskAssessmentTaskCategory(), Times.Once());
            _userRepository.Verify(x => x.GetByIdAndCompanyId(creatingUserId, saveRequest.CompanyId));
            returnedRiskAssessment.Verify(x => x.AddFurtherControlMeasureTask(It.IsAny<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>(), It.IsAny<UserForAuditing>()));

            Assert.That(passedFurtherControlMeasureTask.Title, Is.EqualTo(saveRequest.Title));
            Assert.That(passedFurtherControlMeasureTask.Reference, Is.EqualTo(saveRequest.Reference));
            Assert.That(passedFurtherControlMeasureTask.Description, Is.EqualTo(saveRequest.Description));
            Assert.That(passedFurtherControlMeasureTask.CreatedBy.Id, Is.EqualTo(saveRequest.UserId));
            Assert.That(passedFurtherControlMeasureTask.TaskAssignedTo.Id, Is.EqualTo(saveRequest.TaskAssignedToId));
            Assert.That(passedFurtherControlMeasureTask.TaskCompletionDueDate, Is.EqualTo(saveRequest.TaskCompletionDueDate));
            Assert.That(passedFurtherControlMeasureTask.Category.Id, Is.EqualTo(6));
        }

        [Test]
        public void When_AddFurtherControlMeasureTask_Then_log_request()
        {
            // Given
            var saveRequest = new SaveFurtherControlMeasureTaskRequest();

            // When
            _target.AddFurtherControlMeasureTask(saveRequest);

            // Then
            _log.Verify(x => x.Add(saveRequest));
        }

        [Test]
        public void When_AddFurtherControlMeasureTask_Then_Returned_DTO_Has_Correct_Values()
        {
            // Given
            var creatingUserId = Guid.NewGuid();
            var assignedToEmployeeId = Guid.NewGuid();

            var saveRequest = new SaveFurtherControlMeasureTaskRequest()
            {
                RiskAssessmentId = 1234,
                CompanyId = 5678,
                Title = "new title",
                Reference = "new reference",
                Description = "new description",
                UserId = creatingUserId,
                TaskAssignedToId = assignedToEmployeeId,
                TaskCompletionDueDate = DateTime.Now
            };

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new UserForAuditing() { Id = creatingUserId });

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new Employee() { Id = assignedToEmployeeId });

            var returnedRiskAssessment = new Mock<HazardousSubstanceRiskAssessment>();
            returnedRiskAssessment.Setup(x => x.Id).Returns(1234);
            returnedRiskAssessment.Setup(x => x.Reviews).Returns(new List<RiskAssessmentReview>());

            _riskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(returnedRiskAssessment.Object);

            var passedFurtherControlMeasureTask = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask();

            returnedRiskAssessment
                .Setup(x => x.AddFurtherControlMeasureTask(It.IsAny<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>(), It.IsAny<UserForAuditing>()))
                .Callback<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask, UserForAuditing>((task, user) => passedFurtherControlMeasureTask = task);

            // When
            var result = _target.AddFurtherControlMeasureTask(saveRequest);

            // Then
            Assert.That(DateTime.Parse(result.CreatedDate), Is.GreaterThan(DateTime.MinValue));
        }

     
    }
}