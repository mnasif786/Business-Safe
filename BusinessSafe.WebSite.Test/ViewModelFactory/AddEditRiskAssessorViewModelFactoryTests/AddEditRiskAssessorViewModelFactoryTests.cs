using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.AddEditRiskAssessorViewModelFactoryTests
{
    [TestFixture]
    public class AddEditRiskAssessorViewModelFactoryTests
    {
        private Mock<IRiskAssessorService> _riskAssessorService;

        private long _companyId;
        private long _riskAssessorId;
        private Guid _employeeId;
        private RiskAssessorDto _riskAssessor;

        [SetUp]
        public void Setup()
        {
            _companyId = 88888L;
            _riskAssessorId = 3L;
            _employeeId = Guid.NewGuid();

            _riskAssessor = new RiskAssessorDto
            {
                Id = _riskAssessorId,
                DoNotSendReviewDueNotification = true,
                DoNotSendTaskOverdueNotifications = true,
                DoNotSendTaskCompletedNotifications = true,
                Employee = new EmployeeDto()
                {
                    Id = _employeeId,
                    Forename = "John",
                    Surname = "Prescott"
                },
                Site = new SiteDto()
                {
                    Id = 2306L,
                    Name = "Hull"
                }
            };

            _riskAssessorService = new Mock<IRiskAssessorService>();
            _riskAssessorService
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_riskAssessor);
        }

        [Test]
        public void When_GetViewModel_Then_returns_empty_view_model()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.GetViewModel();

            // Then
            Assert.IsInstanceOf<AddEditRiskAssessorViewModel>(result);
            Assert.That(result.EmployeeId, Is.Null);
        }

        [Test]
        public void Given_company_id_and_risk_assessor_id_supplied_When_GetViewModel_retrieve_RiskAssessor_from_service()
        {
            // Given

            var target = GetTarget();

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithRiskAssessorId(_riskAssessorId)
                .GetViewModel();

            // Then
            _riskAssessorService.Verify(x => x.GetByIdAndCompanyId(_riskAssessorId, _companyId));
        }

        [Test]
        public void Given_company_id_and_risk_assessor_id_When_GetViewModel_Then_view_model_has_correct_employee_id()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithRiskAssessorId(_riskAssessorId)
                .GetViewModel();

            // Then
            Assert.That(result.EmployeeId, Is.EqualTo(_riskAssessor.Employee.Id));
        }

        [Test]
        public void Given_company_id_and_risk_assessor_id_When_GetViewModel_Then_view_model_has_correct_employee_name()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithRiskAssessorId(_riskAssessorId)
                .GetViewModel();

            // Then
            Assert.That(result.Employee, Is.EqualTo(_riskAssessor.Employee.Forename + " " + _riskAssessor.Employee.Surname));
        }

        [Test]
        public void Given_company_id_and_risk_assessor_id_When_GetViewModel_Then_view_model_has_correct_site_id()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithRiskAssessorId(_riskAssessorId)
                .GetViewModel();

            // Then
            Assert.That(result.SiteId, Is.EqualTo(_riskAssessor.Site.Id));
        }

        [Test]
        public void Given_company_id_and_risk_assessor_id_and_site_id_is_null_When_GetViewModel_Then_view_model_has_correct_all_sites()
        {
            // Given
            _riskAssessor.Site = null;
            _riskAssessor.HasAccessToAllSites = true;
            _riskAssessorService
               .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
               .Returns(_riskAssessor);

            var target = GetTarget();

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithRiskAssessorId(_riskAssessorId)
                .GetViewModel();

            // Then
            Assert.IsTrue(result.HasAccessToAllSites);
        }

        [Test]
        public void Given_company_id_and_risk_assessor_id_When_GetViewModel_Then_view_model_has_correct_site_name()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithRiskAssessorId(_riskAssessorId)
                .GetViewModel();

            // Then
            Assert.That(result.Site, Is.EqualTo(_riskAssessor.Site.Name));
        }

        [Test]
        public void Given_company_id_and_risk_assessor_id_When_GetViewModel_Then_view_model_has_correct_doNotSendReviewDueNotification()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithRiskAssessorId(_riskAssessorId)
                .GetViewModel();

            // Then
            Assert.That(result.DoNotSendReviewDueNotification, Is.EqualTo(_riskAssessor.DoNotSendReviewDueNotification));
        }

        [Test]
        public void Given_company_id_and_risk_assessor_id_When_GetViewModel_Then_view_model_has_correct_doNotSendTaskOverdueNotifications()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithRiskAssessorId(_riskAssessorId)
                .GetViewModel();

            // Then
            Assert.That(result.DoNotSendTaskOverdueNotifications, Is.EqualTo(_riskAssessor.DoNotSendTaskOverdueNotifications));
        }

        [Test]
        public void Given_company_id_and_risk_assessor_id_When_GetViewModel_Then_view_model_has_correct_doNotSendTaskCompletedNotifications()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithRiskAssessorId(_riskAssessorId)
                .GetViewModel();

            // Then
            Assert.That(result.DoNotSendTaskCompletedNotifications, Is.EqualTo(_riskAssessor.DoNotSendTaskCompletedNotifications));
        }

        private AddEditRiskAssessorViewModelFactory GetTarget()
        {
            var factory = new AddEditRiskAssessorViewModelFactory(_riskAssessorService.Object);

            return factory;
        }
    }
}
