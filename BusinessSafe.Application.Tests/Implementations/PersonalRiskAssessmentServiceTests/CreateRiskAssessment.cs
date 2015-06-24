using System;
using BusinessSafe.Application.Implementations.PersonalRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.PersonalRiskAssessmentServiceTests
{
    [TestFixture]
    public class CreateRiskAssessmentTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IUserForAuditingRepository> _userRepo;
        private Mock<IPersonalRiskAssessmentRepository> _personalRiskAssessmentRepo;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IChecklistRepository> _checklistRepository;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;

        [SetUp]
        public void Setup()
        {
            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _userRepo = new Mock<IUserForAuditingRepository>();
            _userRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(new UserForAuditing());

            _personalRiskAssessmentRepo = new Mock<IPersonalRiskAssessmentRepository>();
            _personalRiskAssessmentRepo.Setup(x => x.Save(It.IsAny<PersonalRiskAssessment>()));
            
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<PersonalRiskAssessment>  (It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()));

            _employeeRepository = new Mock<IEmployeeRepository>();
            _checklistRepository = new Mock<IChecklistRepository>();
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
        }

        [Test]
        public void When_CreateRiskAssessment_Then_Retrieve_Requesting_User_From_Repo()
        {
            // Given
            var target = GetTarget();
            var request = new CreatePersonalRiskAssessmentRequest()
            {
                UserId = Guid.NewGuid(),
                CompanyId = 100,
                Title = "title",
                Reference = "reference"
            };

            // When
            target.CreateRiskAssessment(request);

            // Then
            _userRepo.Verify(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId), Times.Once());
        }

        [Test]
        public void When_CreateRiskAssessment_Then_Pass_Populated_RiskAssessment_To_Repo_To_Save()
        {
            // Given
            var request = new CreatePersonalRiskAssessmentRequest()
            {
                UserId = Guid.NewGuid(),
                Title = "Title",
                Reference = "Reference",
                CompanyId = 100
            };

            _userRepo
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new UserForAuditing()
                {
                    Id = request.UserId
                });

            var passedPersonalRiskAssessment = new PersonalRiskAssessment();

            _personalRiskAssessmentRepo
                .Setup(x => x.Save(It.IsAny<PersonalRiskAssessment>()))
                .Callback<PersonalRiskAssessment>(y => passedPersonalRiskAssessment = y);

            var target = GetTarget();

            // When
            target.CreateRiskAssessment(request);

            // Then
            _personalRiskAssessmentRepo.Verify(x => x.Save(It.IsAny<PersonalRiskAssessment>()));
            Assert.That(passedPersonalRiskAssessment.Title, Is.EqualTo(request.Title));
            Assert.That(passedPersonalRiskAssessment.Reference, Is.EqualTo(request.Reference));
            Assert.That(passedPersonalRiskAssessment.CompanyId, Is.EqualTo(request.CompanyId));
            Assert.That(passedPersonalRiskAssessment.CreatedBy.Id, Is.EqualTo(request.UserId));
        }

        [Test]
        public void When_CreateRiskAssessment_Then_New_RiskAssessment_Id_Is_Returned()
        {
            // Given
            var request = new CreatePersonalRiskAssessmentRequest()
            {
                UserId = Guid.NewGuid(),
                Title = "Title",
                Reference = "Reference",
                CompanyId = 100
            };

            var passedPersonalRiskAssessment = new PersonalRiskAssessment();

            _personalRiskAssessmentRepo
                .Setup(x => x.Save(It.IsAny<PersonalRiskAssessment>()))
                .Callback<PersonalRiskAssessment>(y => passedPersonalRiskAssessment = y);

            var target = GetTarget();

            // When
            var result = target.CreateRiskAssessment(request);

            // Then
            Assert.That(result, Is.EqualTo(passedPersonalRiskAssessment.Id));
        }

        [Test]
        public void Given_CreateRiskAssessment_When_An_Exception_Is_Thrown_Then_Log_It()
        {
            // Given
            var request = new CreatePersonalRiskAssessmentRequest()
                          {
                              Title = "Title",
                              Reference = "Reference"
                          };

            
            var exception = new Exception();
            _personalRiskAssessmentRepo
                .Setup(x => x.Save(It.IsAny<PersonalRiskAssessment>()))
                .Throws(exception);

            // When
            var target = GetTarget();

            // Then
            Assert.Throws<Exception>(() => target.CreateRiskAssessment(request));
        }

        [Test]
        public void When_CreateRiskAssessment_Then_Check_If_RiskAssessment_Reference_Has_Already_Been_Used()
        {
            // Given
            var request = new CreatePersonalRiskAssessmentRequest()
            {
                UserId = Guid.NewGuid(),
                Title = "Title",
                Reference = "Reference",
                CompanyId = 100
            };

            var target = GetTarget();

            // When
            target.CreateRiskAssessment(request);

            // Then
            _riskAssessmentRepository
                .Verify(x => x.DoesAssessmentExistWithTheSameReference<PersonalRiskAssessment> (request.CompanyId, request.Reference, 0));
        }

        [Test]
        public void When_CreateRiskAssessment_Then_EmployeeChecklistStatus_is_equal_to_not_set()
        {
            // Given
            var target = GetTarget();
            var request = new CreatePersonalRiskAssessmentRequest()
            {
                UserId = Guid.NewGuid(),
                CompanyId = 100,
                Title = "title",
                Reference = "reference"
            };

   
            // When
            target.CreateRiskAssessment(request);

            // Then
            _personalRiskAssessmentRepo.Verify(x => x.Save(It.IsAny<PersonalRiskAssessment>()));
            _personalRiskAssessmentRepo.Verify(x => x.Save(It.Is<PersonalRiskAssessment>(pra => pra.PersonalRiskAssessementEmployeeChecklistStatus == PersonalRiskAssessementEmployeeChecklistStatusEnum.NotSet)));

        }

        private PersonalRiskAssessmentService GetTarget()
        {
            return new PersonalRiskAssessmentService(_personalRiskAssessmentRepo.Object, _userRepo.Object
                , _employeeRepository.Object
                , _checklistRepository.Object
                , _log.Object
                , _riskAssessmentRepository.Object
                , null
                , null, null);
        }
    }
}
