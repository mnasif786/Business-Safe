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
    public class DetachNonEmployeeToRiskAssessmentTests
    {
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<INonEmployeeRepository> _nonEmployeeRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _nonEmployeeRepository = new Mock<INonEmployeeRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_DetachNonEmployee_is_called_Then_should_call_appropiate_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var detachNonEmployeeFromRiskAssessmentRequest = new DetachNonEmployeeFromRiskAssessmentRequest
                                                                 {
                                                                     NonEmployeesToDetachIds = new List<long> { 1 },
                                                                     RiskAssessmentId = 2
                                                                 };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(detachNonEmployeeFromRiskAssessmentRequest.RiskAssessmentId, detachNonEmployeeFromRiskAssessmentRequest.CompanyId))
                .Returns(mockRiskAssessment.Object);

            var mockNonEmployee = new Mock<NonEmployee>();
            _nonEmployeeRepository
                .Setup(x => x.GetByIdAndCompanyId(detachNonEmployeeFromRiskAssessmentRequest.NonEmployeesToDetachIds[0], detachNonEmployeeFromRiskAssessmentRequest.CompanyId))
                .Returns(mockNonEmployee.Object);


            SetupValidUser(detachNonEmployeeFromRiskAssessmentRequest);

            //When
            riskAssessmentService.DetachNonEmployeeFromRiskAssessment(detachNonEmployeeFromRiskAssessmentRequest);

            //Then
            _riskAssessmentRepository.VerifyAll();
            _nonEmployeeRepository.VerifyAll();
            mockRiskAssessment.Verify(x => x.DetachNonEmployeeFromRiskAssessment(mockNonEmployee.Object, _user));
        }

        [Test]
        public void Given_valid_request_with_multiple_non_employees_to_detach_When_DetachNonEmployee_is_called_Then_should_call_appropiate_methods_correct_amount_of_times()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var detachNonEmployeeFromRiskAssessmentRequest = new DetachNonEmployeeFromRiskAssessmentRequest
                                                                 {
                NonEmployeesToDetachIds = new List<long> { 1, 2 },
                RiskAssessmentId = 2
            };

            SetupValidUser(detachNonEmployeeFromRiskAssessmentRequest);

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(detachNonEmployeeFromRiskAssessmentRequest.RiskAssessmentId,detachNonEmployeeFromRiskAssessmentRequest.CompanyId))
                .Returns(mockRiskAssessment.Object);

            var mockNonEmployee = new Mock<NonEmployee>();
            _nonEmployeeRepository
                .Setup(x => x.GetByIdAndCompanyId(detachNonEmployeeFromRiskAssessmentRequest.NonEmployeesToDetachIds[0], detachNonEmployeeFromRiskAssessmentRequest.CompanyId))
                .Returns(mockNonEmployee.Object);
            _nonEmployeeRepository
                .Setup(x => x.GetByIdAndCompanyId(detachNonEmployeeFromRiskAssessmentRequest.NonEmployeesToDetachIds[1],detachNonEmployeeFromRiskAssessmentRequest.CompanyId))
                .Returns(mockNonEmployee.Object);

            //When
            riskAssessmentService.DetachNonEmployeeFromRiskAssessment(detachNonEmployeeFromRiskAssessmentRequest);

            //Then
            _riskAssessmentRepository.VerifyAll();
            _nonEmployeeRepository.VerifyAll();

            mockRiskAssessment.Verify(x => x.DetachNonEmployeeFromRiskAssessment(mockNonEmployee.Object, _user));
        }


        private Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService(_riskAssessmentRepository.Object, _userRepository.Object, null, _nonEmployeeRepository.Object, _log.Object, null);
            return riskAssessmentService;
        }

        private void SetupValidUser(DetachNonEmployeeFromRiskAssessmentRequest detachNonEmployeeFromRiskAssessmentRequest)
        {
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(detachNonEmployeeFromRiskAssessmentRequest.UserId,detachNonEmployeeFromRiskAssessmentRequest.CompanyId))
                .Returns(_user);
        }
    }
}