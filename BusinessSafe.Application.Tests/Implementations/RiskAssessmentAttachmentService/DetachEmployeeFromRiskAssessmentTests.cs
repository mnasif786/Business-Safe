using System;
using System.Collections.Generic;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentAttachmentService
{
    [TestFixture]
    [Category("Unit")]
    public class DetachEmployeeFromRiskAssessmentTests
    {
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_DetachEmployee_is_called_Then_should_call_appropiate_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var detachEmployeeRequest = new DetachEmployeeRequest
                                            {
                                                                     EmployeeIds = new List<Guid> { Guid.NewGuid() },
                                                                     RiskAssessmentId = 2
                                                                 };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(detachEmployeeRequest.RiskAssessmentId, detachEmployeeRequest.CompanyId))
                .Returns(mockRiskAssessment.Object);

            var employees = new[]{ new Employee()};
            _employeeRepository.Setup(x => x.GetByIds(detachEmployeeRequest.EmployeeIds)).Returns(employees);

            SetupValidUser(detachEmployeeRequest);

           
            //When
            riskAssessmentService.DetachEmployeeFromRiskAssessment(detachEmployeeRequest);

            //Then
            _riskAssessmentRepository.VerifyAll();
            mockRiskAssessment.Verify(x => x.DetachEmployeesFromRiskAssessment(employees, _user));
        }

        private Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService(_riskAssessmentRepository.Object, _userRepository.Object, null, null, _log.Object, _employeeRepository.Object);
            return riskAssessmentService;
        }

        private void SetupValidUser(DetachEmployeeRequest detachEmployeeRequest)
        {
            _userRepository.Setup(x => x.GetByIdAndCompanyId(detachEmployeeRequest.UserId,detachEmployeeRequest.CompanyId)).Returns(_user);
        }
    }
}