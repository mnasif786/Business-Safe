using System;
using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class CopyRiskAssessmentTests
    {
        private Mock<IGeneralRiskAssessmentRepository> _generalRiskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private const long RiskAssessmentId = 1;
        private const long CompanyId = 2;
        private UserForAuditing _user;

        [SetUp]
        public void Setup()
        {
            _generalRiskAssessmentRepository = new Mock<IGeneralRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
            _user = new UserForAuditing();
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
        }

        [Test]
        public void Given_valid_request_When_copy_riskassessment_Then_calls_the_correct_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            var request = new CopyRiskAssessmentRequest()
                              {
                                  CompanyId = CompanyId,
                                  RiskAssessmentToCopyId = RiskAssessmentId,
                                  UserId = Guid.NewGuid(),
                                  Reference = "Test Reference",
                                  Title = "new title"
                              };

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            mockRiskAssessment.Setup(x => x.Id).Returns(1);
            mockRiskAssessment
                .Setup(x => x.Copy(request.Title,request.Reference,  _user))
                .Returns(mockRiskAssessment.Object);

            _generalRiskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(RiskAssessmentId, CompanyId))
                .Returns(mockRiskAssessment.Object);

            _generalRiskAssessmentRepository
                .Setup(x => x.SaveOrUpdate(mockRiskAssessment.Object));

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<GeneralRiskAssessment> (It.IsAny<long>()
                                                                      , It.IsAny<string>()
                                                                      , It.IsAny<long?>()))
                .Returns(false);
            //When
            riskAssessmentService.CopyRiskAssessment(request);

            //Then
            mockRiskAssessment.VerifyAll();
            _generalRiskAssessmentRepository.VerifyAll();
            _userRepository.VerifyAll();
        }

        [Test]
        public void Given_valid_request_When_copy_riskassessment_Then_should_return_correct_result()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            var request = new CopyRiskAssessmentRequest()
            {
                CompanyId = CompanyId,
                RiskAssessmentToCopyId = RiskAssessmentId,
                UserId = Guid.NewGuid(),
                Reference = "Test Reference",
                Title = "new title"
            };

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            mockRiskAssessment
                .Setup(x => x.Id)
                .Returns(200);

            mockRiskAssessment
                .Setup(x => x.Copy(request.Title, request.Reference, _user))
                .Returns(mockRiskAssessment.Object);

            _generalRiskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(RiskAssessmentId, CompanyId))
                .Returns(mockRiskAssessment.Object);

            _generalRiskAssessmentRepository
                .Setup(x => x.SaveOrUpdate(mockRiskAssessment.Object));


            //When
            var result = riskAssessmentService.CopyRiskAssessment(request);

            //Then
            Assert.That(result, Is.EqualTo(mockRiskAssessment.Object.Id));
        }

        private GeneralRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new GeneralRiskAssessmentService(
                _generalRiskAssessmentRepository.Object, 
                _riskAssessmentRepository.Object, 
                _userRepository.Object, 
                null, 
                _log.Object, 
                null,
                null);

            return riskAssessmentService;
        }
    }
}