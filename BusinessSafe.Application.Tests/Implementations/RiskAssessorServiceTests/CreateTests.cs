using System;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessorServiceTests
{
    [TestFixture]
    public class CreateTests
    {
        private Mock<IRiskAssessorRepository> _riskAssessorRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<ISiteStructureElementRepository> _siteStructureElementRepository;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private Mock<IPeninsulaLog> _log;

        private CreateEditRiskAssessorRequest _request;
        private Employee _employee;
        private RiskAssessor _riskAssessor;
        private Site _site;
        private UserForAuditing _userForAuditing;

        private long _siteId;
        private long _companyId;
        private Guid _employeeId;
        private Guid _userForAuditingId;


        [SetUp]
        public void Setup()
        {
            _siteId = 456L;
            _companyId = 123L;
            _employeeId = Guid.NewGuid();
            _userForAuditingId = Guid.NewGuid();

            _request = new CreateEditRiskAssessorRequest()
            {
                CompanyId = _companyId,
                CreatingUserId = _userForAuditingId,
                DoNotSendReviewDueNotification = true,
                DoNotSendTaskCompletedNotifications = false,
                DoNotSendTaskOverdueNotifications = true,
                EmployeeId = _employeeId,
                SiteId = _siteId,
                HasAccessToAllSites = false
            };

            _riskAssessorRepository = new Mock<IRiskAssessorRepository>();

            _riskAssessorRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new RiskAssessor());

            _riskAssessorRepository
                .Setup(x => x.Save(It.IsAny<RiskAssessor>()));


            _employee = new Employee()
                           {
                               Id = _employeeId,
                               CompanyId = _companyId
                           };

            _employeeRepository = new Mock<IEmployeeRepository>();
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(_employee.Id, _employee.CompanyId))
                .Returns(_employee);

            _site = new Site
                       {
                           Id = _siteId,
                           ClientId = _companyId,
                           Name = "Middle Earth"
                       };

            _siteRepository = new Mock<ISiteRepository>();
            _siteRepository
                .Setup(x => x.GetByIdAndCompanyId(_siteId, _companyId))
                .Returns(_site);

            _siteStructureElementRepository = new Mock<ISiteStructureElementRepository>();
            _siteStructureElementRepository
                .Setup(x => x.GetByIdAndCompanyId(_siteId, _companyId))
                .Returns(_site);

            _userForAuditing = new UserForAuditing
                                  {
                                      Id = _userForAuditingId,
                                      CompanyId = _companyId
                                  };

            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(_userForAuditingId, _companyId))
                .Returns(_userForAuditing);

            _riskAssessor = new RiskAssessor
                            {
                                Id = 1119L
                            };

            _log = new Mock<IPeninsulaLog>();
            _log
                .Setup(x => x.Add(It.IsAny<object>()));
        }

        [Test]
        public void Given_request_has_site_When_Create_Then_saves_new_RiskAssessor()
        {
            // Given
            var target = GetTarget();

            RiskAssessor savedRiskAssessor = null;

            _riskAssessorRepository.Setup(x => x.Save(It.IsAny<RiskAssessor>()))
                .Callback<RiskAssessor>(parameter => savedRiskAssessor = parameter);

            // When
            target.Create(_request);

            // Then
            Assert.That(savedRiskAssessor.Employee.Id, Is.EqualTo(_employee.Id));
            Assert.That(savedRiskAssessor.Site.Id, Is.EqualTo(_request.SiteId));
            Assert.That(savedRiskAssessor.HasAccessToAllSites, Is.EqualTo(_request.HasAccessToAllSites));
            Assert.That(savedRiskAssessor.DoNotSendReviewDueNotification, Is.EqualTo(_request.DoNotSendReviewDueNotification));
            Assert.That(savedRiskAssessor.DoNotSendTaskCompletedNotifications, Is.EqualTo(_request.DoNotSendTaskCompletedNotifications));
            Assert.That(savedRiskAssessor.DoNotSendTaskOverdueNotifications, Is.EqualTo(_request.DoNotSendTaskOverdueNotifications));
            Assert.That(savedRiskAssessor.CreatedBy.Id, Is.EqualTo(_userForAuditing.Id));

        }

        [Test]
        public void Given_request_does_not_have_site_When_Create_Then_saves_new_RiskAssessor()
        {
            // Given
            var target = GetTarget();
            _request.SiteId = (long?)null;
            _request.HasAccessToAllSites = true;

            RiskAssessor savedRiskAssessor = null;

            _riskAssessorRepository.Setup(x => x.Save(It.IsAny<RiskAssessor>()))
                .Callback<RiskAssessor>(parameter => savedRiskAssessor = parameter);

            // When
            target.Create(_request);

            // Then
            Assert.That(savedRiskAssessor.Employee.Id,Is.EqualTo(_employee.Id));
            Assert.That(savedRiskAssessor.Site, Is.EqualTo(null));
            Assert.That(savedRiskAssessor.HasAccessToAllSites, Is.EqualTo(_request.HasAccessToAllSites));
            Assert.That(savedRiskAssessor.DoNotSendReviewDueNotification, Is.EqualTo(_request.DoNotSendReviewDueNotification));
            Assert.That(savedRiskAssessor.DoNotSendTaskCompletedNotifications, Is.EqualTo(_request.DoNotSendTaskCompletedNotifications));
            Assert.That(savedRiskAssessor.DoNotSendTaskOverdueNotifications, Is.EqualTo(_request.DoNotSendTaskOverdueNotifications));
            Assert.That(savedRiskAssessor.CreatedBy.Id, Is.EqualTo(_userForAuditing.Id));

        }
        
        private RiskAssessorService GetTarget()
        {
            return new RiskAssessorService(
                _riskAssessorRepository.Object,
                _employeeRepository.Object,
                _siteRepository.Object,
                _userForAuditingRepository.Object,
                null,
                _siteStructureElementRepository.Object, _log.Object);
        }
    }
}
