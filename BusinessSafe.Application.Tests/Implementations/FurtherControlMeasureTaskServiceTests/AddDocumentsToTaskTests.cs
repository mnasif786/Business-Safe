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
    public class AddDocumentsToTaskTests
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
                null, _userRepo.Object, null, _documentTypeRepository.Object
                );
        }

        [Test]
        public void Given_further_control_measure_task__when_AddingDocuments_then_createdBy_and_createddate_are_the_same_as_the_request_object()
        {
            //given
            var fcmTask = new MultiHazardRiskAssessmentFurtherControlMeasureTask();
            fcmTask.CreatedBy = new UserForAuditing() {Id = Guid.NewGuid()};
            fcmTask.CreatedOn = DateTime.Now.AddDays(-5);
            fcmTask.Reference = "ref";
            fcmTask.MultiHazardRiskAssessmentHazard = MultiHazardRiskAssessmentHazard.Create(new GeneralRiskAssessment(){Reference = "test"}, new Hazard(), fcmTask.CreatedBy);

            _furtherControlMeasureTaskRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => fcmTask);

            var expectedCreatedBy = new UserForAuditing() {Id = Guid.NewGuid()};
            var expectedCreatedOn = DateTime.Now;

            _userForAuditingRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => expectedCreatedBy);

            _documentTypeRepository.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(() => new DocumentType());

            var request = new AddDocumentsToTaskRequest()
                              {
                                  CompanyId = 123,
                                  DocumentLibraryIds = new List<DocumentLibraryFile>() {new DocumentLibraryFile() {Id = 23, Description = "123", Filename = "image.png"}}
                                  ,
                                  TaskId = 123
                                  ,
                                  UserId = expectedCreatedBy.Id
                              };

            var target = GetTarget();

            //when
            var result = target.AddDocumentsToTask(request);

            //then
            Assert.AreEqual(expectedCreatedOn.Date, result.First().CreatedOn.Value.Date);
            Assert.AreEqual(expectedCreatedBy.Id, result.First().CreatedBy.Id);
            
        }


    }
}
