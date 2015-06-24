using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateManyResponsibilityTaskFromTemplateTests
    {
        private Mock<IResponsibilityRepository> _responsibilityRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private Mock<IStatutoryResponsibilityTaskTemplateRepository> _statutoryResponsibilityTaskTemplateRepository;
        private Mock<ITaskCategoryRepository> _taskCategoryRepository;
        private Mock<IDocumentParameterHelper> _documentParameterHelper;
        private Mock<IPeninsulaLog> _log;
        private long _companyId;

        private Responsibility _responsibilityA;
        private Responsibility _responsibilityB;
        private Site _site;
        private UserForAuditing _creatingUser;
        private Employee _taskAssignedTo;
        private CreateResponsibilityTasksFromWizardRequest _task1ForResponsibilityA;
        private CreateResponsibilityTasksFromWizardRequest _task1ForResponsibilityB;
        private CreateResponsibilityTasksFromWizardRequest _task2ForResponsibilityA;
        private StatutoryResponsibilityTaskTemplate _taskTemplateA;
        private StatutoryResponsibilityTaskTemplate _taskTemplateB;
        private StatutoryResponsibilityTaskTemplate _taskTemplateC;
        private List<StatutoryResponsibilityTaskTemplate> _taskTemplates;
        private CreateManyResponsibilityTaskFromWizardRequest _manyTaskRequest;
        private TaskCategory _taskCategory;

        [SetUp]
        public void Setup()
        {
            _companyId = 234246L;
            
            _responsibilityA = new Responsibility { Id = 123L, CompanyId = _companyId };
            _responsibilityB = new Responsibility { Id = 456L, CompanyId = _companyId };

            _responsibilityRepository = new Mock<IResponsibilityRepository>();
            _responsibilityRepository.Setup(x => x.GetByIds(It.IsAny<List<long>>()));
            _responsibilityRepository.Setup(x => x.Save(It.IsAny<Responsibility>()));

            _taskAssignedTo = new Employee { Id = Guid.NewGuid(), CompanyId = _companyId };
            _employeeRepository = new Mock<IEmployeeRepository>();
            _employeeRepository
                .Setup(x => x.GetByIds(It.IsAny<List<Guid>>()))
                .Returns(new List<Employee>() {_taskAssignedTo });

            _site = new Site { Id = 1234L, ClientId = _companyId };
            _siteRepository = new Mock<ISiteRepository>();
            _siteRepository
                .Setup(x => x.GetByIds(It.IsAny<List<long>>()))
                .Returns(new List<Site> { _site });
            
            _statutoryResponsibilityTaskTemplateRepository = new Mock<IStatutoryResponsibilityTaskTemplateRepository>();
            _statutoryResponsibilityTaskTemplateRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(new StatutoryResponsibilityTaskTemplate());
            
            _creatingUser = new UserForAuditing { Id = Guid.NewGuid() };
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(_creatingUser.Id, _companyId))
                .Returns(_creatingUser);

            _taskCategory = new TaskCategory()
            {
                Id = 1L
            };

            _taskCategoryRepository = new Mock<ITaskCategoryRepository>();
            _taskCategoryRepository.Setup(x => x.GetResponsibilityTaskCategory())
                .Returns(_taskCategory);

            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _log = new Mock<IPeninsulaLog>();

            _taskTemplateA = new StatutoryResponsibilityTaskTemplate { Id = 12345464252L, Title = "task template A title", Description = "task template A description"};
            _taskTemplateB = new StatutoryResponsibilityTaskTemplate { Id = 154764252L, Title = "task template B title", Description = "task template B description" };
            _taskTemplateC = new StatutoryResponsibilityTaskTemplate { Id = 123453252L, Title = "task template C title", Description = "task template C description",  };

            _taskTemplates = new List<StatutoryResponsibilityTaskTemplate> { _taskTemplateA, _taskTemplateB, _taskTemplateC };

            _task1ForResponsibilityA = CreateResponsibilityTasksFromWizardRequest.Create(_companyId, _creatingUser.Id, _site.Id, _responsibilityA.Id, _taskTemplateA.Id,
                                                                            TaskReoccurringType.Weekly, _taskAssignedTo.Id,
                                                                            DateTime.Now.ToShortDateString(),
                                                                            DateTime.Now.AddDays(1).ToShortDateString(),
                                                                            Guid.NewGuid());

            _task2ForResponsibilityA = CreateResponsibilityTasksFromWizardRequest.Create(_companyId, _creatingUser.Id, _site.Id, _responsibilityA.Id, _taskTemplateB.Id,
                                                                            TaskReoccurringType.Weekly, _taskAssignedTo.Id,
                                                                            DateTime.Now.ToShortDateString(),
                                                                            DateTime.Now.AddDays(1).ToShortDateString(),
                                                                            Guid.NewGuid());

            _task1ForResponsibilityB = CreateResponsibilityTasksFromWizardRequest.Create(_companyId, _creatingUser.Id, _site.Id, _responsibilityB.Id, _taskTemplateC.Id,
                                                                            TaskReoccurringType.Weekly, _taskAssignedTo.Id,
                                                                            DateTime.Now.ToShortDateString(),
                                                                            DateTime.Now.AddDays(1).ToShortDateString(),
                                                                            Guid.NewGuid());

            _manyTaskRequest = new CreateManyResponsibilityTaskFromWizardRequest()
            {
                CompanyId = _companyId,
                CreatingUserId = _creatingUser.Id
            };

           
        }

        [Test]
        public void Given_valid_request_Then_save_associated_Responsibilities_to_repository()
        {
            // Given
            var taskDetails = new List<CreateResponsibilityTasksFromWizardRequest> { _task1ForResponsibilityA, _task2ForResponsibilityA, _task1ForResponsibilityB };
            var associatedResponsibilityIds = taskDetails.Select(y => y.ResponsibilityId).Distinct().ToList();
            _responsibilityRepository
                .Setup(x => x.GetByIds(associatedResponsibilityIds))
                .Returns(new List<Responsibility> { _responsibilityA, _responsibilityB });

            _manyTaskRequest.TaskDetails = taskDetails;

            // When
            GetTarget().CreateManyResponsibilityTaskFromWizard(_manyTaskRequest);

            // Then
            _responsibilityRepository.Verify(x => x.Save(_responsibilityA), Times.Once());
            _responsibilityRepository.Verify(x => x.Save(_responsibilityB), Times.Once());
        }

        [Test]
        public void Given_valid_request_Then_create_requested_tasks_on_associated_responsibility()
        {
            // Given
            var taskDetails = new List<CreateResponsibilityTasksFromWizardRequest> { _task1ForResponsibilityA, _task2ForResponsibilityA };

            Responsibility passedResponsibility = null;

            _responsibilityRepository
                .Setup(x => x.GetByIds(new List<long> { _task1ForResponsibilityA.ResponsibilityId }))
                .Returns(new List<Responsibility> { _responsibilityA });

            _responsibilityRepository
                .Setup(x => x.Save(It.IsAny<Responsibility>()))
                .Callback<Responsibility>(y => passedResponsibility = y);

            _manyTaskRequest.TaskDetails = taskDetails;

            // When
            GetTarget().CreateManyResponsibilityTaskFromWizard(_manyTaskRequest);

            // Then
            Assert.That(passedResponsibility.ResponsibilityTasks.Count, Is.EqualTo(2));
        }

        [Test]
        public void Given_valid_request_Then_map_task_details_from_associated_template()
        {
            // Given
            var taskDetails = new List<CreateResponsibilityTasksFromWizardRequest> { _task1ForResponsibilityA };

            Responsibility passedResponsibility = null;

            _responsibilityRepository
                .Setup(x => x.GetByIds(new List<long> { _task1ForResponsibilityA.ResponsibilityId }))
                .Returns(new List<Responsibility> { _responsibilityA });

            _responsibilityRepository
                .Setup(x => x.Save(It.IsAny<Responsibility>()))
                .Callback<Responsibility>(y => passedResponsibility = y);

            _statutoryResponsibilityTaskTemplateRepository
                .Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(_taskTemplates.Single(x => x.Id == _task1ForResponsibilityA.TaskTemplateId));

            _manyTaskRequest.TaskDetails = taskDetails;

            // When
            GetTarget().CreateManyResponsibilityTaskFromWizard(_manyTaskRequest);
            var task = passedResponsibility.ResponsibilityTasks.First();
            var requestedTaskTemplate = _taskTemplates.Single(x => x.Id == task.StatutoryResponsibilityTaskTemplateCreatedFrom.Id);

            // Then
            Assert.That(task.Title, Is.EqualTo(requestedTaskTemplate.Title));
            Assert.That(task.Description, Is.EqualTo(requestedTaskTemplate.Description));
            Assert.That(task.StatutoryResponsibilityTaskTemplateCreatedFrom, Is.EqualTo(requestedTaskTemplate));
        }

        [Test]
        public void Given_valid_request_Then_map_associated_objects_requested()
        {
            // Given
            var taskDetails = new List<CreateResponsibilityTasksFromWizardRequest> { _task1ForResponsibilityA };

            Responsibility passedResponsibility = null;

            _responsibilityRepository
                .Setup(x => x.GetByIds(new List<long> { _task1ForResponsibilityA.ResponsibilityId }))
                .Returns(new List<Responsibility> { _responsibilityA });

            _responsibilityRepository
                .Setup(x => x.Save(It.IsAny<Responsibility>()))
                .Callback<Responsibility>(y => passedResponsibility = y);

            _statutoryResponsibilityTaskTemplateRepository
                .Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(_taskTemplates.Single(x => x.Id == _task1ForResponsibilityA.TaskTemplateId));

            _manyTaskRequest.TaskDetails = taskDetails;

            // When
            GetTarget().CreateManyResponsibilityTaskFromWizard(_manyTaskRequest);
            var task = passedResponsibility.ResponsibilityTasks.First();

            // Then
            Assert.That(task.CreatedBy, Is.EqualTo(_creatingUser));
            Assert.That(task.TaskAssignedTo, Is.EqualTo(_taskAssignedTo));
            Assert.That(task.Category, Is.EqualTo(_taskCategory));
            Assert.That(task.Site, Is.EqualTo(_site));
        }

        [Test]
        public void Given_incorrect_assignees_in_request_Then_exception_thrown()
        {
            // Given
            _employeeRepository
                .Setup(x => x.GetByIds(It.IsAny<List<Guid>>()))
                .Returns(new List<Employee>() { });

            var task = CreateResponsibilityTasksFromWizardRequest.Create(_companyId, _creatingUser.Id, _site.Id, _responsibilityA.Id, _taskTemplateA.Id,
                                                                            TaskReoccurringType.Weekly, Guid.NewGuid(),
                                                                            DateTime.Now.ToShortDateString(),
                                                                            DateTime.Now.AddDays(1).ToShortDateString(),
                                                                            Guid.NewGuid());

            var taskDetails = new List<CreateResponsibilityTasksFromWizardRequest> { task };

            _manyTaskRequest.TaskDetails = taskDetails;

            // Then
            var e = Assert.Throws<ArgumentException>(() => GetTarget().CreateManyResponsibilityTaskFromWizard(_manyTaskRequest));
            Assert.That(e.Message, Is.EqualTo("Requested Employees for generating Statutory Tasks could not be retrieved"));
        }

        [Test]
        public void Given_requested_assignees_are_from_another_company_Then_exception_thrown()
        {
            // Given
            var assigneeEmployeeId = Guid.NewGuid();
            const long anotherCompanyId = 13244625522332L;
            _employeeRepository
                .Setup(x => x.GetByIds(new List<Guid>() { assigneeEmployeeId }))
                .Returns(new List<Employee>() { new Employee() { Id = assigneeEmployeeId, CompanyId = anotherCompanyId } });

            var task = CreateResponsibilityTasksFromWizardRequest.Create(_companyId, _creatingUser.Id, _site.Id, _responsibilityA.Id, _taskTemplateA.Id,
                                                                            TaskReoccurringType.Weekly, assigneeEmployeeId,
                                                                            DateTime.Now.ToShortDateString(),
                                                                            DateTime.Now.AddDays(1).ToShortDateString(),
                                                                            Guid.NewGuid());

            var taskDetails = new List<CreateResponsibilityTasksFromWizardRequest> { task };

            _manyTaskRequest.TaskDetails = taskDetails;

            // Then
            var e = Assert.Throws<ArgumentException>(() => GetTarget().CreateManyResponsibilityTaskFromWizard(_manyTaskRequest));
            Assert.That(e.Message, Is.EqualTo("Requested Employees are not from the requested Company"));
        }

        [Test]
        public void Given_requested_sites_not_found_Then_exception_thrown()
        {
            // Given
            _siteRepository
                .Setup(x => x.GetByIds(It.IsAny<List<long>>()))
                .Returns(new List<Site>());

            var task = _task1ForResponsibilityA;

            var taskDetails = new List<CreateResponsibilityTasksFromWizardRequest> { task };

            _manyTaskRequest.TaskDetails = taskDetails;

            // Then
            var e = Assert.Throws<ArgumentException>(() => GetTarget().CreateManyResponsibilityTaskFromWizard(_manyTaskRequest));
            Assert.That(e.Message, Is.EqualTo("Requested Sites for generating Statutory Tasks could not be retrieved"));
        }

        [Test]
        public void Given_requested_sites_are_from_another_company_Then_exception_thrown()
        {
            // Given
            const long siteId = 64756L;
            const long anotherCompanyId = 13244625522332L;
            _siteRepository
                .Setup(x => x.GetByIds(new List<long>() { siteId }))
                .Returns(new List<Site>() { new Site() { Id = siteId, ClientId = anotherCompanyId } });

            var task = _task1ForResponsibilityA;
            task.SiteId = siteId;

            var taskDetails = new List<CreateResponsibilityTasksFromWizardRequest> { task };

            _manyTaskRequest.TaskDetails = taskDetails;

            // Then
            var e = Assert.Throws<ArgumentException>(() => GetTarget().CreateManyResponsibilityTaskFromWizard(_manyTaskRequest));
            Assert.That(e.Message, Is.EqualTo("Requested Sites are not from the requested Company"));
        }

        private IResponsibilitiesService GetTarget()
        {
            return new ResponsibilitiesService(_responsibilityRepository.Object,
                                               null,
                                               null,
                                               _employeeRepository.Object,
                                               _siteRepository.Object,
                                               _userForAuditingRepository.Object,
                                               _taskCategoryRepository.Object,
                                               _documentParameterHelper.Object,
                                               null,
                                               _statutoryResponsibilityTaskTemplateRepository.Object,
                                               _log.Object);
        }
    }
}