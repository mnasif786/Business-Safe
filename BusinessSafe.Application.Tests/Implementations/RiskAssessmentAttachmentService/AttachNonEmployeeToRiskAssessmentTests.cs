using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations
{
    [TestFixture]
    [Category("Unit")]
    public class AttachNonEmployeeToRiskAssessmentTests
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
        public void Given_valid_request_When_AttachNonEmployee_is_called_Then_should_call_appropiate_methods()
        {

            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var attachNonEmployeeToRiskAssessmentRequest = new AttachNonEmployeeToRiskAssessmentRequest()
                                                               {
                                                                   NonEmployeeToAttachId = 1,
                                                                   RiskAssessmentId = 2
                                                               };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                    .Setup(rr => rr.GetByIdAndCompanyId(attachNonEmployeeToRiskAssessmentRequest.RiskAssessmentId, attachNonEmployeeToRiskAssessmentRequest.CompanyId))
                    .Returns(mockRiskAssessment.Object);

            var mockNonEmployee = new Mock<NonEmployee>();
            _nonEmployeeRepository
                    .Setup(x => x.GetByIdAndCompanyId(attachNonEmployeeToRiskAssessmentRequest.NonEmployeeToAttachId, attachNonEmployeeToRiskAssessmentRequest.CompanyId))
                    .Returns(mockNonEmployee.Object);

            SetupValidUser(attachNonEmployeeToRiskAssessmentRequest);

            //When
            riskAssessmentService.AttachNonEmployeeToRiskAssessment(attachNonEmployeeToRiskAssessmentRequest);

            //Then
            _riskAssessmentRepository.VerifyAll();
            _nonEmployeeRepository.VerifyAll(); 
            mockRiskAssessment.Verify(x => x.AttachNonEmployeeToRiskAssessment(mockNonEmployee.Object, _user));
        }



        private Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService(_riskAssessmentRepository.Object, _userRepository.Object, null, _nonEmployeeRepository.Object, _log.Object, null);
            return riskAssessmentService;
        }

        private void SetupValidUser(AttachNonEmployeeToRiskAssessmentRequest attachNonEmployeeToRiskAssessmentRequest)
        {
            _userRepository.Setup(x => x.GetByIdAndCompanyId(attachNonEmployeeToRiskAssessmentRequest.UserId, attachNonEmployeeToRiskAssessmentRequest.CompanyId)).Returns(_user);
        }
    }
}