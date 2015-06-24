using System;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AddControlMeasureToRiskAssessmentTest
    {
        private Mock<IHazardousSubstanceRiskAssessmentRepository> _repository;
        private Mock<IHazardousSubstancesRepository> _hazardousSubstanceRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;
        private IHazardousSubstanceRiskAssessmentService _target;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IHazardousSubstanceRiskAssessmentRepository>();
            _hazardousSubstanceRepository = new Mock<IHazardousSubstancesRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
            _target = GetTarget();
        }

        [Test]
        public void Given_valid_request_When_AddControlMeasure_Then_should_call_correct_methods()
        {

            //Given
            var request = new AddControlMeasureRequest()
                              {
                                  CompanyId = 200,
                                  UserId = Guid.NewGuid(),
                                  RiskAssessmentId = 250,
                                  ControlMeasure = "Hello Control Measure"
                              };

            var mockRiskAssessment = new Mock<BusinessSafe.Domain.Entities.HazardousSubstanceRiskAssessment>();
            mockRiskAssessment
                .Setup( x => x.AddControlMeasure(It.Is<HazardousSubstanceRiskAssessmentControlMeasure>(r => r.ControlMeasure == request.ControlMeasure), It.Is<UserForAuditing>( r=> r.Id ==  _user.Id)));

            _repository
                .Setup(x => x.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId)).
                Returns(mockRiskAssessment.Object);
            
            _repository
                .Setup(x => x.SaveOrUpdate(mockRiskAssessment.Object));
           
            _repository
                .Setup(x => x.Flush());

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            //When
            _target.AddControlMeasureToRiskAssessment(request);

            //Then
            _repository.VerifyAll();
            _userRepository.VerifyAll();
            mockRiskAssessment.Verify();
        }

        private IHazardousSubstanceRiskAssessmentService GetTarget()
        {
            return new HazardousSubstanceRiskAssessmentService(
                _repository.Object,
                _userRepository.Object,
                _hazardousSubstanceRepository.Object,
                _log.Object,
                null,
                null,
                null,
                null
                );
        }
    }
}