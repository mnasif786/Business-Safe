using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.FurtherControlMeasureTaskServiceTests
{
    public class CompleteFurtherControlMeasureTaskTests
    {
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
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
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _documentTypeRepository = new Mock<IDocumentTypeRepository>();
            _furtherControlMeasureTaskRepository = new Mock<IFurtherControlMeasureTasksRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _userRepo = new Mock<IUserRepository>();
        }

        private FurtherControlMeasureTaskService GetTarget()
        {
            return new FurtherControlMeasureTaskService(
                _userForAuditingRepository.Object,
                _furtherControlMeasureTaskRepository.Object,
                _employeeRepository.Object,
                _log.Object,
                _documentParameterHelper.Object,
                null,
                null, _userRepo.Object, null, null
                );
        }

        private Mock<FurtherControlMeasureTaskService> GetTargetMocked()
        {
            return new Mock<FurtherControlMeasureTaskService>(
                _userForAuditingRepository.Object,
                _furtherControlMeasureTaskRepository.Object,
                _employeeRepository.Object,
                _log.Object,
                _documentParameterHelper.Object,
                null,
                null, _userRepo.Object
                );
        }

        [Test]
        public void Given_further_control_measure_task_doesnt_exists_when_Completed_then_throw_task_not_found_exception()
        {
            //given
            _furtherControlMeasureTaskRepository.Setup(x => x.GetById(It.IsAny<long>())).Throws(new TaskNotFoundException(123));
            _furtherControlMeasureTaskRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Throws(new TaskNotFoundException(123));

            var target = GetTarget();
            
            Assert.Throws<TaskNotFoundException>(() => target.CompleteFurtherControlMeasureTask(new CompleteTaskRequest()));
        }

        [Test]
        public void Given_further_control_measure_task_exists_when_Completed_then_correct_values_are_passed_to_the_complete_task_method()
        {
            //given
            var request = new CompleteTaskRequest()
                              {
                                  CompanyId = 123435,
                                  CompletedComments = "complete task comments test",
                                  CreateDocumentRequests = new List<CreateDocumentRequest>()
                                  ,
                                  DocumentLibraryIdsToDelete = new List<long>()
                                  ,
                                  FurtherControlMeasureTaskId = 34983
                                  ,
                                  UserId = Guid.NewGuid()
                                  ,CompletedDate = DateTimeOffset.Now.AddDays(-12)
                              };

            var auditUser = new UserForAuditing() {Id = Guid.NewGuid()};
            var bsoUser = new User() {Id = Guid.NewGuid()};

            _userForAuditingRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(),It.IsAny<long>())).Returns(() => auditUser);
            _userRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(),It.IsAny<long>())).Returns(() => bsoUser);

            var task = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            task.Setup(x => x.Complete(It.IsAny<string>()
                                       , It.IsAny<IEnumerable<CreateDocumentParameters>>()
                                       , It.IsAny<IEnumerable<long>>()
                                       , It.IsAny<UserForAuditing>()
                                       , It.IsAny<User>()
                                       , It.IsAny<DateTimeOffset>()))
                .Callback<string, IEnumerable<CreateDocumentParameters>, IEnumerable<long>, UserForAuditing, User, DateTimeOffset >(
                    (comments, createDocumentParameters, docLibIdsToRemove, userForAuditing, user, completedDate)  =>
                        {
                            Assert.AreEqual(request.CompletedComments, comments);
                            Assert.AreEqual(request.CreateDocumentRequests, createDocumentParameters);
                            Assert.AreEqual(request.DocumentLibraryIdsToDelete, docLibIdsToRemove);
                            Assert.AreEqual(auditUser.Id, userForAuditing.Id);
                            Assert.AreEqual(request.CompletedDate, completedDate);
                            Assert.AreEqual(bsoUser.Id, user.Id);
                        });

            _furtherControlMeasureTaskRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => task.Object);

            var target = GetTarget();

            target.CompleteFurtherControlMeasureTask(request);

        }

        [Test]
        public void Given_further_control_measure_task_is_not_required_when_Completed_then_throw_exception()
        {
            //given
            var task = new Mock<FurtherControlMeasureTask>() {CallBase = true}; // new MultiHazardRiskAssessmentFurtherControlMeasureTask();}
            task.Object.TaskStatus = TaskStatus.NoLongerRequired;
            _furtherControlMeasureTaskRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(() => task.Object);

            var target = GetTarget();

            Assert.Throws<AttemptingToCompleteFurtherControlMeasureTaskThatIsNotRequiredException>(() => target.CompleteFurtherControlMeasureTask(new CompleteTaskRequest()));
        }
    }
}
