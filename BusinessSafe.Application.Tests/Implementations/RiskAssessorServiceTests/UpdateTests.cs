using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class UpdateTests
    {
        private Mock<IRiskAssessorRepository> _riskAssessorRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<ISiteStructureElementRepository> _siteStructureElementRepository;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private Mock<IPeninsulaLog> _log;

        private Employee _employee;
        private Site _site;
        private UserForAuditing _userForAuditing;
        private RiskAssessor _riskAssessor;

        private long _siteId;
        private long _companyId;
        private long _riskAssessorId;
        private Guid _employeeId;
        private Guid _userForAuditingId;

        [SetUp]
        public void Setup()
        {
            _siteId = 456L;
            _companyId = 123L;
            _riskAssessorId = 789L;
            _employeeId = Guid.NewGuid();
            _userForAuditingId = Guid.NewGuid();

            new CreateEditRiskAssessorRequest()
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


            _employee = new Employee()
            {
                Id = _employeeId,
                CompanyId = _companyId
            };

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
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => _site);

            _userForAuditing = new UserForAuditing
            {
                Id = _userForAuditingId,
                CompanyId = _companyId
            };

            _riskAssessorRepository = new Mock<IRiskAssessorRepository>();

            _riskAssessorRepository
                .Setup(x => x.GetByIdAndCompanyId(_riskAssessorId, _companyId))
                .Returns(new RiskAssessor()
                             {
                                 Id = _riskAssessorId,
                                 Employee = _employee
                             });

            _riskAssessorRepository
                .Setup(x => x.Save(It.IsAny<RiskAssessor>()));

            _employeeRepository = new Mock<IEmployeeRepository>();
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(_employee.Id, _employee.CompanyId))
                .Returns(_employee);

            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(_userForAuditingId, _companyId))
                .Returns(_userForAuditing);

            new RiskAssessor
                {
                    Id = 1119L
                };

            _log = new Mock<IPeninsulaLog>();
            _log
                .Setup(x => x.Add(It.IsAny<object>()));
        }

        [Test]
        public void Given_request_has_site_When_Update_Then_saves_RiskAssessor()
        {
            // Given
            var request = new CreateEditRiskAssessorRequest
                              {
                                  CompanyId = _companyId,
                                  CreatingUserId = _userForAuditingId,
                                  SiteId = _siteId,
                                  RiskAssessorId = _riskAssessorId,
                                  DoNotSendReviewDueNotification = true,
                                  DoNotSendTaskOverdueNotifications = true,
                                  DoNotSendTaskCompletedNotifications = true,
                                  HasAccessToAllSites = false
                              };
            var target = GetTarget();

            RiskAssessor passedRiskAssessor = null;

            _riskAssessorRepository
                .Setup(x => x.Save(It.IsAny<RiskAssessor>()))
                .Callback < RiskAssessor>(y => passedRiskAssessor = y);

            // When
            target.Update(request);

            // Then
            _riskAssessorRepository
                .Verify(x => x.Save(It.IsAny<RiskAssessor>()));
            Assert.That(passedRiskAssessor.Employee.Id, Is.EqualTo(_employeeId));
            Assert.That(passedRiskAssessor.Site.Id, Is.EqualTo(_siteId));
            Assert.That(passedRiskAssessor.DoNotSendReviewDueNotification, Is.EqualTo(request.DoNotSendReviewDueNotification));
            Assert.That(passedRiskAssessor.DoNotSendTaskCompletedNotifications, Is.EqualTo(request.DoNotSendTaskCompletedNotifications));
            Assert.That(passedRiskAssessor.DoNotSendTaskOverdueNotifications, Is.EqualTo(request.DoNotSendTaskOverdueNotifications));
            Assert.That(passedRiskAssessor.LastModifiedBy.Id, Is.EqualTo(_userForAuditing.Id));
            Assert.That(passedRiskAssessor.Id, Is.EqualTo(request.RiskAssessorId));
        }

        private RiskAssessorService GetTarget()
        {
            return new RiskAssessorService(
                _riskAssessorRepository.Object,
                _employeeRepository.Object,
                _siteRepository.Object,
                _userForAuditingRepository.Object,
                null,
                _siteStructureElementRepository.Object,
                _log.Object
                );
        }
    }
}
