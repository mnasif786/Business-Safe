using System;
using System.Linq;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.PersonalRiskAssessments
{
    [TestFixture]
    public class PremisesInformationViewModelFactoryTests
    {
        private const long _companyId = 2424L;
        private const long _riskAssessmentId = 322L;
        private Mock<IPersonalRiskAssessmentService> _riskAssessmentService;
        private Guid _currentUserId;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
            _currentUserId = Guid.NewGuid();
        }


        [Test]
        public void When_GetViewModel_Then_should_call_the_correct_methods()
        {
            // Given
            var target = GetTarget();

            var riskAssessment = new PersonalRiskAssessmentDto()
            {
                CompanyId = _companyId,
                Id = _riskAssessmentId,
                NonEmployees = new RiskAssessmentNonEmployeeDto[] { },
                Employees = new RiskAssessmentEmployeeDto[] { },
                RiskAssessmentSite = new SiteDto()
            };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(riskAssessment);
            
            // When
            target
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel();

            // Then
            _riskAssessmentService.VerifyAll();
        }

        [Test]
        public void When_GetViewModel_Then_should_call_the_correct_result()
        {
            // Given
            var target = GetTarget();

            var riskAssessment = new PersonalRiskAssessmentDto()
            {
                CompanyId = _companyId,
                Id = _riskAssessmentId,
                Employees = new[]
                                {
                                    new RiskAssessmentEmployeeDto{ Employee = new EmployeeDto() }
                                },
                NonEmployees = new[]
                                   {
                                       new RiskAssessmentNonEmployeeDto{ NonEmployee = new NonEmployeeDto() }
                                   },
                RiskAssessmentSite = new SiteDto() { Id = 200 },
                Location = "Test Location",
                TaskProcessDescription = "Test Task Process Description"
            };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(riskAssessment);

            // When
            var result = target
                                .WithCompanyId(_companyId)
                                .WithRiskAssessmentId(_riskAssessmentId)
                                .WithCurrentUserId(_currentUserId)
                                .GetViewModel();

            // Then
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(_riskAssessmentId));
            Assert.That(result.Employees.Count(), Is.EqualTo(riskAssessment.Employees.Count()));
            Assert.That(result.NonEmployees.Count(), Is.EqualTo(riskAssessment.NonEmployees.Count()));
            Assert.That(result.LocationAreaDepartment, Is.EqualTo(riskAssessment.Location));
            Assert.That(result.TaskProcessDescription, Is.EqualTo(riskAssessment.TaskProcessDescription));
        }

        [Test]
        public void Given_factory_takes_viewmodel_When_GetViewModel_Then_should_call_the_correct_methods()
        {
            // Given
            var target = GetTarget();

            var riskAssessment = new PersonalRiskAssessmentDto()
            {
                CompanyId = _companyId,
                Id = _riskAssessmentId,
                NonEmployees = new RiskAssessmentNonEmployeeDto[] { },
                Employees = new RiskAssessmentEmployeeDto[] { },
                RiskAssessmentSite = new SiteDto()
            };

            var viewModel = new PremisesInformationViewModel()
                                {
                                    RiskAssessmentId = _riskAssessmentId,
                                    CompanyId = _companyId
                                };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(viewModel.RiskAssessmentId, viewModel.CompanyId, _currentUserId))
                .Returns(riskAssessment);
            
            // When
            target
                .WithCompanyId(viewModel.CompanyId)
                .WithRiskAssessmentId(viewModel.RiskAssessmentId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel(viewModel);

            // Then
            _riskAssessmentService.VerifyAll();
        }

        [Test]
        public void Given_factory_takes_viewmodel_When_GetViewModel_Then_should_call_the_correct_result()
        {
            // Given
            var target = GetTarget();

            var riskAssessment = new PersonalRiskAssessmentDto()
            {
                CompanyId = _companyId,
                Id = _riskAssessmentId,
                Employees = new[]
                                {
                                    new RiskAssessmentEmployeeDto{ Employee = new EmployeeDto() }
                                },
                NonEmployees = new[]
                                   {
                                       new RiskAssessmentNonEmployeeDto{ NonEmployee = new NonEmployeeDto() }
                                   },
            };

            var viewModel = new PremisesInformationViewModel()
            {
                RiskAssessmentId = _riskAssessmentId,
                CompanyId = _companyId,
                TaskProcessDescription = "Test Task",
                LocationAreaDepartment = "Test Location"
            };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(viewModel.RiskAssessmentId, viewModel.CompanyId, _currentUserId))
                .Returns(riskAssessment);
            
            // When
            var result = target
                                .WithCompanyId(_companyId)
                                .WithRiskAssessmentId(_riskAssessmentId)
                                .WithCurrentUserId(_currentUserId)
                                .GetViewModel(viewModel);

            // Then
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(_riskAssessmentId));
            Assert.That(result.Employees.Count(), Is.EqualTo(riskAssessment.Employees.Count()));
            Assert.That(result.NonEmployees.Count(), Is.EqualTo(riskAssessment.NonEmployees.Count()));
            Assert.That(result.LocationAreaDepartment, Is.EqualTo(viewModel.LocationAreaDepartment));
            Assert.That(result.TaskProcessDescription, Is.EqualTo(viewModel.TaskProcessDescription));
        }

        private IPremisesInformationViewModelFactory GetTarget()
        {
            return new PremisesInformationViewModelFactory(_riskAssessmentService.Object);
        }
    }
}
