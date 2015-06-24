using System;
using System.Linq;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using FluentValidation;
using Moq;
using NUnit.Framework;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateRiskAssessmentTest 
    {
        private Mock<IHazardousSubstanceRiskAssessmentRepository> _hazardousSubstanceAssessmentRepository;
        private Mock<IHazardousSubstancesRepository> _hazardousSubstanceRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private UserForAuditing _user;

        [SetUp]
        public void Setup()
        {
            _hazardousSubstanceAssessmentRepository = new Mock<IHazardousSubstanceRiskAssessmentRepository>();
            _hazardousSubstanceRepository = new Mock<IHazardousSubstancesRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
        }

        [Test]
        public void Given_valid_request_no_existing_risk_assessments_with_matching_references_When_CreateRiskAssessment_Then_should_call_correct_methods()
        {
        
            //Given
            var riskAssessmentService = GetTarget();
            var request = new SaveHazardousSubstanceRiskAssessmentRequestBuilder().WithTitle("Test").Build();
            request.Title = "the title";
            request.Reference = "the reference";

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<HazardousSubstanceRiskAssessment>(request.CompanyId, request.Reference, It.IsAny<long?>()) ).
                Returns(false);

            //No longer being used. PTD 05/09/13
            //_riskAssessmentRepository
            //    .Setup(x => x.DoesAssessmentExistWithTheSameTitle<HazardousSubstanceRiskAssessment>(request.CompanyId, request.Title, It.IsAny<long?>())).
            //    Returns(false);

            _hazardousSubstanceAssessmentRepository.Setup(rr => rr.SaveOrUpdate(It.IsAny<HazardousSubstanceRiskAssessment>()));

            //When
            riskAssessmentService.CreateRiskAssessment(request);
            
            //Then
            _riskAssessmentRepository.VerifyAll();
        }

        [Test]
        public void Given_valid_request_with_existing_risk_assessments_which_match_references_When_CreateRiskAssessment_Then_should_throw_correct_exception()
        {
            //Given
            var riskAssessmentService = GetTarget();
            var request = new SaveHazardousSubstanceRiskAssessmentRequestBuilder().WithTitle("Test").Build();
            request.Reference = "reference";

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<HazardousSubstanceRiskAssessment>(request.CompanyId, request.Reference, It.IsAny<long?>())).
                Returns(true);

            _riskAssessmentRepository
                .Setup(ra => ra.DoesAssessmentExistWithTheSameReference<HazardousSubstanceRiskAssessment>(request.CompanyId, request.Reference, It.IsAny<long?>()))
                .Returns(true);

            //When
            //Then
            Assert.Throws<ValidationException>(() => riskAssessmentService.CreateRiskAssessment(request));
        }

        [Test]
        public void Given_valid_request_When_CreateRiskAssessment_Then_should_return_value_greater_than_zero()
        {
            //Given
            var riskAssessmentService = GetTarget();

            _hazardousSubstanceAssessmentRepository.Setup(rr => rr.SaveOrUpdate(It.IsAny<HazardousSubstanceRiskAssessment>()))
                .Callback<HazardousSubstanceRiskAssessment>(par => par.Id = 1);

            SetupValidUser();

            //When
            var result = riskAssessmentService.CreateRiskAssessment(SaveHazardousSubstanceRiskAssessmentRequestBuilder.Create().Build());

            //Then
            Assert.That(result,Is.GreaterThan(0));
        }

        [Test]
        [Ignore("Logic no longer applies. PTD 05/09/13")]
        public void Given_invalid_request_Then_should_throw_validation_exception()
        {
            //Given
            const string reference = "abc";
            const long clientId = 20;
            _riskAssessmentRepository
                .Setup(ra => ra.DoesAssessmentExistWithTheSameReference<HazardousSubstanceRiskAssessment>(clientId, reference, It.IsAny<long?>()))
                .Returns(true);

            _riskAssessmentRepository
              .Setup(ra => ra.DoesAssessmentExistWithTheSameReference<HazardousSubstanceRiskAssessment>(clientId, reference, It.IsAny<long?>()))
              .Returns(true);

            var target = GetTarget();

            SetupValidUser();

            //When
            TestDelegate testDelegate = () => target.CreateRiskAssessment(new SaveHazardousSubstanceRiskAssessmentRequest { Reference = reference, Title = "needs to be supplied", CompanyId = clientId });

            //Then
            var exception = Assert.Throws<ValidationException>(testDelegate);
            Assert.That(exception.Errors.First().ErrorMessage, Is.EqualTo("Reference already exists"));
        }
     
        [Test]
        public void Given_valid_request_selected_hazardous_substance_id_is_passed_to_repo()
        {
            //Given
            var riskAssessmentService = GetTarget();
            var request = new SaveHazardousSubstanceRiskAssessmentRequestBuilder().WithTitle("Test").WithHazardousSubstanceId(1234).Build();
            var passedHazardousSubstanceRiskAssessment = new HazardousSubstanceRiskAssessment();

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<HazardousSubstanceRiskAssessment>(request.CompanyId, request.Reference, It.IsAny<long?>())).
                Returns(false);

            _hazardousSubstanceAssessmentRepository
                .Setup(rr => rr.SaveOrUpdate(It.IsAny<HazardousSubstanceRiskAssessment>()))
                .Callback<HazardousSubstanceRiskAssessment>(y => passedHazardousSubstanceRiskAssessment = y);

            _hazardousSubstanceRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new HazardousSubstance() { Id = 1234 });

            //When
            riskAssessmentService.CreateRiskAssessment(request);

            //Then
            Assert.That(passedHazardousSubstanceRiskAssessment.HazardousSubstance.Id, Is.EqualTo(request.HazardousSubstanceId));
        }
        
        private void SetupValidUser()
        {
            _userRepository
                .Setup(curUserRepository => curUserRepository.GetById(It.IsAny<Guid>()))
                .Returns(_user);
        }

        private IHazardousSubstanceRiskAssessmentService GetTarget()
        {
            return new HazardousSubstanceRiskAssessmentService(
                _hazardousSubstanceAssessmentRepository.Object, 
                _userRepository.Object,
                _hazardousSubstanceRepository.Object,
                _log.Object,
                _riskAssessmentRepository.Object,
                null,
                null,
                null
                );
        }
    }
}
