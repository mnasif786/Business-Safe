using System;
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
    public class AttachEmployeeToRiskAssessmentTests
    {
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
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
        public void Given_valid_request_When_AttachEmployee_is_called_Then_should_call_appropiate_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var attachEmployeeRequest = new AttachEmployeeRequest()
                                                               {
                                                                   EmployeeId = Guid.NewGuid(),
                                                                   RiskAssessmentId = 2
                                                               };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                    .Setup(rr => rr.GetByIdAndCompanyId(attachEmployeeRequest.RiskAssessmentId, attachEmployeeRequest.CompanyId))
                    .Returns(mockRiskAssessment.Object);

            var employee = new Employee();
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(attachEmployeeRequest.EmployeeId, attachEmployeeRequest.CompanyId))
                .Returns(employee);

            SetupValidUser(attachEmployeeRequest);

            //When
            riskAssessmentService.AttachEmployeeToRiskAssessment(attachEmployeeRequest);

            //Then
            _riskAssessmentRepository.VerifyAll();
            mockRiskAssessment.Verify(x => x.AttachEmployeeToRiskAssessment(employee, _user));
        }

        private void SetupValidUser(AttachEmployeeRequest attachEmployeeRequest)
        {
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(attachEmployeeRequest.UserId, attachEmployeeRequest.CompanyId))
                .Returns(_user);
        }


      
        private Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService(_riskAssessmentRepository.Object, _userRepository.Object, null, null, _log.Object, _employeeRepository.Object);
            return riskAssessmentService;
        }
    }
}