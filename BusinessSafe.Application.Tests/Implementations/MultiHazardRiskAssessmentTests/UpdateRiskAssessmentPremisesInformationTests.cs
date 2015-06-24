using System;
using BusinessSafe.Application.Implementations.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.MultiHazardRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateRiskAssessmentPremisesInformationTests
    {
        private Mock<IMultiHazardRiskAssessmentRepository> _multiHazardRiskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private const long CompanyId = 1;
        private const long RiskAssessmentId = 2;
        
        [SetUp]
        public void Setup()
        {
            _multiHazardRiskAssessmentRepository = new Mock<IMultiHazardRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_UpdateRiskAssessmentPremisesInformation_Then_calls_the_correct_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            var request = new SaveRiskAssessmentPremisesInformationRequest()
                              {
                                  CompanyId = CompanyId,
                                  Id = RiskAssessmentId,
                                  UserId = Guid.NewGuid(),
                                  LocationAreaDepartment = "Test Location",
                                  TaskProcessDescription = "Test Task"
                              };
            
          
            var mockRiskAssessment = new Mock<MultiHazardRiskAssessment>();
            _multiHazardRiskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(RiskAssessmentId, CompanyId))
                .Returns(mockRiskAssessment.Object);

            var user = new UserForAuditing();
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);
            
            
            //When
            riskAssessmentService.UpdateRiskAssessmentPremisesInformation(request);

            //Then
            mockRiskAssessment.Verify(x => x.UpdatePremisesInformation(request.LocationAreaDepartment,request.TaskProcessDescription, user));
            _multiHazardRiskAssessmentRepository.Verify(x => x.SaveOrUpdate(mockRiskAssessment.Object));
            _userRepository.VerifyAll();
        }

        private MultiHazardRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new MultiHazardRiskAssessmentService(_multiHazardRiskAssessmentRepository.Object, null, _userRepository.Object, _log.Object);
            return riskAssessmentService;
        }
    }
}