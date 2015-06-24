using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.PersonalRiskAssessments.EditPersonalRiskAssessmentSummaryViewModelFactoryTests
{
    [TestFixture]
    public class GetViewModelTests
    {
        private EditPersonalRiskAssessmentSummaryViewModelFactory _target;
        private Mock<IPersonalRiskAssessmentService> _riskAssessmentService;
        private Mock<IEmployeeService> _employeeService;
        private Mock<ISiteService> _siteService;

        private long _companyId = 0;
        private long _riskAssessmentId = 0;
        private Guid _currentUserId;

        private PersonalRiskAssessmentDto _riskAssessment;


        [SetUp]
        public void Setup()
        {
            _currentUserId = Guid.NewGuid();
            _riskAssessment = new PersonalRiskAssessmentDto()
                              {
                                  Id = _riskAssessmentId,
                                  CompanyId = _companyId,
                                  CreatedBy = new AuditedUserDto() { Id = _currentUserId },
                                  Title = "title",
                                  Sensitive = true,
                              };

            _riskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
            _riskAssessmentService
                .Setup(x => x.GetWithReviews(_riskAssessmentId, _companyId, It.IsAny<Guid>()))
                .Returns(_riskAssessment);

            _employeeService = new Mock<IEmployeeService>();
            _employeeService
                .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Returns(new List<EmployeeDto>());

           

            _siteService = new Mock<ISiteService>();

            _target = GetTarget();
        }

        [Test]
        public void Given_logged_in_user_is_risk_assessments_creating_user_When_GetViewModel_Then_Is_ReadOnly_is_false()
        {
            // Given

            // When
            var result = _target
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel();

            // Then
            Assert.False(result.IsReadOnly);
        }

        [Test]
        public void Given_logged_in_user_is_risk_assessments_creating_user_When_GetViewModel_Then_Is_ReadOnly_is_true()
        {
            // Given

            // When
            var result = _target
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCurrentUserId(new Guid())
                .GetViewModel();

            // Then
            Assert.IsTrue(result.IsReadOnly);
        }


        [Test]
        public void Given_logged_in_user_is_not_risk_assessments_assessor_When_GetViewModel_Then_LoggedInUserIsAssessor_is_false()
        {
            // Given

            // When
            var result = _target
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel();

            // Then
            Assert.IsFalse(result.IsSensitiveReadonly);
        }

        [Test]
        public void Given_risk_assessor_is_not_a_user_When_GetViewModel_Then_IsSensitiveReadonly_is_true()
        {
            // Given
            _riskAssessment.CreatedBy = new AuditedUserDto() {Id = Guid.NewGuid()};
            _riskAssessment.RiskAssessor = new RiskAssessorDto()
            {
                Employee = new EmployeeDto() { Id = Guid.NewGuid(), User = null}
            };

            var employee = new EmployeeDto();
            _employeeService.Setup(x => x.GetEmployee(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => employee);
            
            // When
            var result = _target
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel();

            // Then
            Assert.IsTrue(result.IsSensitiveReadonly);
        }

        private EditPersonalRiskAssessmentSummaryViewModelFactory GetTarget()
        {
            return new EditPersonalRiskAssessmentSummaryViewModelFactory(_riskAssessmentService.Object, _employeeService.Object, _siteService.Object);
        }
    }
}
