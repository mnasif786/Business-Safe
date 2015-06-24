using System;
using System.Linq;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using FluentValidation;
using Moq;
using NUnit.Framework;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SaveRiskAssessmentTest 
    {
        private Mock<IGeneralRiskAssessmentRepository> _generalRiskAssessmentRepository;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;

        [SetUp]
        public void Setup()
        {
            _generalRiskAssessmentRepository = new Mock<IGeneralRiskAssessmentRepository>();
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_SaveRiskAssessment_Then_should_call_correct_methods()
        {
        
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            _generalRiskAssessmentRepository.Setup(rr => rr.SaveOrUpdate(It.IsAny<GeneralRiskAssessment>()))
                .Callback<GeneralRiskAssessment>(par => par.Id = 1);
            
            SetupValidUser();

            //When
            riskAssessmentService.CreateRiskAssessment(new SaveRiskAssessmentRequestBuilder().WithTitle("Test").Build());
            
            //Then
            _generalRiskAssessmentRepository.Verify(rr => rr.SaveOrUpdate(It.IsAny<GeneralRiskAssessment>()), Times.Once());
        }

        [Test]
        public void Given_valid_request_When_SaveRiskAssessment_Then_should_return_value_greater_than_zero()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            _generalRiskAssessmentRepository.Setup(rr => rr.SaveOrUpdate(It.IsAny<GeneralRiskAssessment>()))
                .Callback<GeneralRiskAssessment>(par => par.Id = 1);

            SetupValidUser();

            //When
            var result = riskAssessmentService.CreateRiskAssessment(SaveRiskAssessmentRequestBuilder.Create().Build());

            //Then
            Assert.That(result,Is.GreaterThan(0));
        }
        
        [Test]
        public void Given_invalid_request_Then_should_throw_validation_exception()
        {
            //Given
            const string reference = "abc";
            const long clientId = 20;
            _riskAssessmentRepository.Setup(ra => ra.DoesAssessmentExistWithTheSameReference<GeneralRiskAssessment> (It.IsAny<long>(),It.IsAny<string>(),It.IsAny<long?>())).Returns(true);
            var target = GetTarget();

            SetupValidUser();

            //When
            TestDelegate testDelegate = () => target.CreateRiskAssessment(new CreateRiskAssessmentRequest { Reference = reference, Title = "needs to be supplied", CompanyId = clientId});

            //Then
            var exception = Assert.Throws<ValidationException>(testDelegate);
            Assert.That(exception.Errors.First().ErrorMessage, Is.EqualTo("Reference already exists"));
        }
     

        private GeneralRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new GeneralRiskAssessmentService(
                _generalRiskAssessmentRepository.Object, 
                _riskAssessmentRepository.Object, 
                _userRepository.Object, null, _log.Object, null, null);
            return riskAssessmentService;
        }

        private void SetupValidUser()
        {
            _userRepository
                .Setup(curUserRepository => curUserRepository.GetById(It.IsAny<Guid>()))
                .Returns(_user);
        }

        private IGeneralRiskAssessmentService GetTarget()
        {
            return new GeneralRiskAssessmentService(_generalRiskAssessmentRepository.Object
                , _riskAssessmentRepository.Object
                , _userRepository.Object, null, _log.Object, null, null);
        }
    }
}
