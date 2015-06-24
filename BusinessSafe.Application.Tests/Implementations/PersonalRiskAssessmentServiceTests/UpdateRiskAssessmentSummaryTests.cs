using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.PersonalRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.PersonalRiskAssessmentServiceTests
{
    [TestFixture]
    public class UpdateRiskAssessmentSummaryTests
    {

        private Mock<IPeninsulaLog> _log;
        private Mock<IPersonalRiskAssessmentRepository> _personalRiskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IChecklistRepository> _checklistRepository;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IRiskAssessorRepository> _riskAssessorRepository;
        private UserForAuditing _user;
        private Employee _employee = new Employee();
        private RiskAssessor _riskAssessor = new RiskAssessor();
        private UpdatePersonalRiskAssessmentSummaryRequest _request;

        private Guid _currentUserId;


        [SetUp]
        public void Setup()
        {
            _currentUserId = Guid.NewGuid();

            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<Object>()));


            _personalRiskAssessmentRepository = new Mock<IPersonalRiskAssessmentRepository>();

            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(_user);

            _employeeRepository = new Mock<IEmployeeRepository>();
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(_employee);

            _riskAssessorRepository = new Mock<IRiskAssessorRepository>();
            _riskAssessorRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_riskAssessor);

            _request = new UpdatePersonalRiskAssessmentSummaryRequest()
            {
                CompanyId = 100,
                Reference = "Reference",
                Title = "Title",
                UserId = _currentUserId,
                Id = 200,
                AssessmentDate = DateTime.Now,
                RiskAssessorId = 266L,

            };

            _checklistRepository = new Mock<IChecklistRepository>();
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();

        }

        [Test]
        public void Given_trying_to_update_reference_to_one_that_already_exists_When_Update_Summary_Then_should_throw_correct_error()
        {
            // Given
            var riskAssessment = new PersonalRiskAssessment();
            _personalRiskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.Id, _request.CompanyId,_currentUserId))
                .Returns(riskAssessment);

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<PersonalRiskAssessment> (It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()))
                .Returns(true);


            var target = CreateRiskAssessmentService();
            // When
              Assert.Throws<ValidationException>(() =>target.UpdateRiskAssessmentSummary(_request));
      
        }

        [Test]
        public void Given_When_Update_Summary_Then_the_new_reference_value_should_be_used_when_checking_to_see_if_it_exists()
        {
            // Given
            var riskAssessment = new PersonalRiskAssessment();
            riskAssessment.Id = _request.Id;
            riskAssessment.CompanyId = _request.CompanyId;
            
            _personalRiskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.Id, _request.CompanyId, _currentUserId))
                .Returns(riskAssessment);

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<PersonalRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()))
                .Returns(false);

            var target = CreateRiskAssessmentService();

            // When
            target.UpdateRiskAssessmentSummary(_request);

            _riskAssessmentRepository.Verify(x => x.DoesAssessmentExistWithTheSameReference<PersonalRiskAssessment>(_request.CompanyId, It.IsAny<string>(), It.IsAny<long?>()));
            _riskAssessmentRepository.Verify(x => x.DoesAssessmentExistWithTheSameReference<PersonalRiskAssessment>(It.IsAny<long>(), _request.Reference, It.IsAny<long?>()));
            _riskAssessmentRepository.Verify(x => x.DoesAssessmentExistWithTheSameReference<PersonalRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), _request.Id));
        }

        private PersonalRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new PersonalRiskAssessmentService(
                _personalRiskAssessmentRepository.Object,
                _userRepository.Object,
                _employeeRepository.Object,
                _checklistRepository.Object,
                _log.Object,
                _riskAssessmentRepository.Object,
                null,
                _riskAssessorRepository.Object,null);
            return riskAssessmentService;
        }
    }
}
