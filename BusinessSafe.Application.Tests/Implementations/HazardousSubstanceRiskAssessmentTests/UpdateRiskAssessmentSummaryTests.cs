using System;
using BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    public class UpdateRiskAssessmentSummaryTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IHazardousSubstanceRiskAssessmentRepository> _hazardousSubstanceRiskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IHazardousSubstancesRepository> _hazardousSubstanceRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IRiskAssessorRepository> _riskAssessorRepository;
        private HazardousSubstanceRiskAssessmentService _target;
        private SaveHazardousSubstanceRiskAssessmentRequest _request;

        [SetUp]
        public void Setup()
        {
            _log = new Mock<IPeninsulaLog>();
            _siteRepository = new Mock<ISiteRepository>();
            _hazardousSubstanceRiskAssessmentRepository = new Mock<IHazardousSubstanceRiskAssessmentRepository>();
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _hazardousSubstanceRepository = new Mock<IHazardousSubstancesRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _riskAssessorRepository = new Mock<IRiskAssessorRepository>();

            _request = new SaveHazardousSubstanceRiskAssessmentRequest()
                       {
                           CompanyId = 100,
                           Reference = "Reference",
                           Title = "Title",
                           UserId = Guid.NewGuid(),
                           Id = 200,
                           AssessmentDate = DateTime.Now,
                           RiskAssessorId = 959L,
                           HazardousSubstanceId = 300,
                           SiteId = 1
                       };

            

            _target = CreateRiskAssessmentService();

            _riskAssessmentRepository
               .Setup(x => x.DoesAssessmentExistWithTheSameReference<HazardousSubstanceRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()))
               .Returns(false);

        }
        
        [Test]
        public void Given_site_and_risk_assessor_When_UpdateRiskAssessmentSummaryTests_Then_should_call_correct_methods()
        {
            // Given
            var returnedHazardousSubstance = new HazardousSubstance() { Id = 300 };
            _hazardousSubstanceRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.HazardousSubstanceId, _request.CompanyId))
                .Returns(returnedHazardousSubstance);

            //var employee = new Employee();
            //_employeeRepository
            //    .Setup(x => x.GetByIdAndCompanyId(_request.RiskAssessorEmployeeId.Value, _request.CompanyId))
            //    .Returns(employee);

            var riskAssessor = new RiskAssessor();
            _riskAssessorRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.RiskAssessorId.Value, _request.CompanyId))
                .Returns(riskAssessor);
               
            var user = new UserForAuditing();
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.UserId, _request.CompanyId))
                .Returns(user);

            var site = new Site();
            _siteRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.SiteId.Value, _request.CompanyId))
                .Returns(site);

            var riskAssessment = new Mock<HazardousSubstanceRiskAssessment>();
            _hazardousSubstanceRiskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.Id,_request.CompanyId))
                .Returns(riskAssessment.Object);

            // When
            _target.UpdateRiskAssessmentSummary(_request);

            // Then
            _employeeRepository.VerifyAll();
            _siteRepository.VerifyAll();
            _hazardousSubstanceRiskAssessmentRepository.Verify(x => x.SaveOrUpdate(riskAssessment.Object));
            riskAssessment.Verify(x => x.UpdateSummary(_request.Title, _request.Reference, _request.AssessmentDate, riskAssessor, returnedHazardousSubstance, site, user));
        }
        
        [Test]
        public void Given_HSRA_With_Reference_X_When_Updating_Another_GRA_To_Set_Reference_To_X_Then_Throw_Error()
        {
            // Given
            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<HazardousSubstanceRiskAssessment>   (It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()))
                .Returns(true);

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameTitle<HazardousSubstanceRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()))
                .Returns(false);

            // When
            // Then
            Assert.Throws<ValidationException>(() => _target.UpdateRiskAssessmentSummary(_request));
            
        }
        
        private HazardousSubstanceRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new HazardousSubstanceRiskAssessmentService(
                _hazardousSubstanceRiskAssessmentRepository.Object, 
                _userRepository.Object, 
                _hazardousSubstanceRepository.Object,
                _log.Object,
                _riskAssessmentRepository.Object,
                null,
                _siteRepository.Object,
                _riskAssessorRepository.Object
                );

            return riskAssessmentService;
        }
    }
}
