using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateResponsibilityTaskFromTemplateTests
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

        private Responsibility _responsibility;
        private Site _site;
        private StatutoryResponsibilityTaskTemplate _statutoryResponsibilityTaskTemplate;
        private UserForAuditing _testUser;

        [SetUp]
        public void Setup()
        {
            _companyId = 234246L;
            _responsibilityRepository = new Mock<IResponsibilityRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _statutoryResponsibilityTaskTemplateRepository = new Mock<IStatutoryResponsibilityTaskTemplateRepository>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _taskCategoryRepository = new Mock<ITaskCategoryRepository>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _log = new Mock<IPeninsulaLog>();

            _responsibility = new Responsibility
                                  {
                                      Id = 1L
                                  };
            _site = new Site
                        {
                            Id = 1L
                        };
            _statutoryResponsibilityTaskTemplate = new StatutoryResponsibilityTaskTemplate
                                                       {
                                                           Id = 1L
                                                       };

            _testUser = new UserForAuditing() {CompanyId = _companyId, Id = Guid.NewGuid()};

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new Employee() {CompanyId = 1L, Id = Guid.NewGuid()});

            _responsibilityRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_responsibility);

            _siteRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_site);

            _statutoryResponsibilityTaskTemplateRepository.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(_statutoryResponsibilityTaskTemplate);

            _userForAuditingRepository.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(_testUser);

            _taskCategoryRepository.Setup(x => x.GetResponsibilityTaskCategory()).Returns(new TaskCategory());

            _documentParameterHelper.Setup(
                x => x.GetCreateDocumentParameters(It.IsAny<IEnumerable<CreateDocumentRequest>>(), It.IsAny<long>()))
                .Returns(new List<CreateDocumentParameters>());
        }

        [Test]
        public void given_valid_request_when_CreateResponsibilityTaskFromWizard_called_then_correct_methods()
        {
            //given
            var target = GetTarget();

            var request = CreateResponsibilityTasksFromWizardRequest.Create(12L, Guid.NewGuid(), 1L, 1L, 1L,
                                                                            TaskReoccurringType.Weekly, Guid.NewGuid(),
                                                                            DateTime.Now.ToShortDateString(),
                                                                            DateTime.Now.AddDays(1).ToShortDateString(),
                                                                            Guid.NewGuid());

            //when
            target.CreateResponsibilityTaskFromWizard(request);

            //then
            _statutoryResponsibilityTaskTemplateRepository.Verify(x => x.GetById(It.IsAny<long>()));
            _employeeRepository.Verify(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()));
            _responsibilityRepository.Verify(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()));
            _siteRepository.Verify(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()));
            _userForAuditingRepository.Verify(x => x.GetById(It.IsAny<Guid>()));
            _responsibilityRepository.Verify(x => x.SaveOrUpdate(It.IsAny<Responsibility>()));
        }

        [Test]
        public void given_valid_request_when_CreateResponsibilityTaskFromWizard_then_map_save_request()
        {
            //given
            var target = GetTarget();

            var request = CreateResponsibilityTasksFromWizardRequest.Create(12L, Guid.NewGuid(), 1L, 1L, 1L,
                                                                            TaskReoccurringType.Weekly, Guid.NewGuid(),
                                                                            DateTime.Now.ToShortDateString(),
                                                                            DateTime.Now.AddDays(1).ToShortDateString(),
                                                                            Guid.NewGuid());

            //when
            target.CreateResponsibilityTaskFromWizard(request);

            //then
            _responsibilityRepository.Verify(x => x.GetByIdAndCompanyId(request.ResponsibilityId, request.CompanyId));
            _employeeRepository.Verify(x => x.GetByIdAndCompanyId(It.Is<Guid>(y => y == request.AssigneeId), It.Is<long>(y => y == request.CompanyId)));
            _siteRepository.Verify(x => x.GetByIdAndCompanyId(It.Is<long>(y => y == request.SiteId), It.Is<long>(y => y == request.CompanyId)));
            _userForAuditingRepository.Verify(x => x.GetById(It.Is<Guid>(y=>y==request.UserId)));
            _statutoryResponsibilityTaskTemplateRepository.Verify(x => x.GetById(It.Is<long>(y=>y==request.TaskTemplateId)));

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