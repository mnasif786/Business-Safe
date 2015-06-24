using System;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    public class CreateRiskAssessment
    {
        private Mock<IFireRiskAssessmentRepository> _fireRiskAssessmentRepo;
        private Mock<IUserForAuditingRepository> _userRepo;
        private Mock<IPeninsulaLog> _log;
        private Mock<IChecklistRepository> _checklistRepository;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;

        [SetUp]
        public void Setup()
        {
            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _userRepo = new Mock<IUserForAuditingRepository>();
            _userRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(new UserForAuditing());

            _fireRiskAssessmentRepo = new Mock<IFireRiskAssessmentRepository>();
            _fireRiskAssessmentRepo.Setup(x => x.Save(It.IsAny<FireRiskAssessment>()));

            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<FireRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()));

            new Mock<IEmployeeRepository>();
            _checklistRepository = new Mock<IChecklistRepository>();

            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
        }

        [Test]
        public void When_CreateRiskAssessment_Then_Log_Request()
        {
            // Given
            var target = GetTarget();
            var request = new CreateRiskAssessmentRequest()
            {
                Title = "Title",
                Reference = "Reference"
            };

            // When
            target.CreateRiskAssessment(request);

            // Then
            _log.Verify(x => x.Add(request), Times.Once());
        }

        [Test]
        public void When_CreateRiskAssessment_Then_Retrieve_Requesting_User_From_Repo()
        {
            // Given
            var target = GetTarget();
            var request = new CreateRiskAssessmentRequest()
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
            var request = new CreateRiskAssessmentRequest()
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

            var passedFireRiskAssessment = new FireRiskAssessment();

            _fireRiskAssessmentRepo
                .Setup(x => x.Save(It.IsAny<FireRiskAssessment>()))
                .Callback<FireRiskAssessment>(y => passedFireRiskAssessment = y);

            var target = GetTarget();

            // When
            target.CreateRiskAssessment(request);

            // Then
            _fireRiskAssessmentRepo.Verify(x => x.Save(It.IsAny<FireRiskAssessment>()));
            Assert.That(passedFireRiskAssessment.Title, Is.EqualTo(request.Title));
            Assert.That(passedFireRiskAssessment.Reference, Is.EqualTo(request.Reference));
            Assert.That(passedFireRiskAssessment.CompanyId, Is.EqualTo(request.CompanyId));
            Assert.That(passedFireRiskAssessment.CreatedBy.Id, Is.EqualTo(request.UserId));
        }

        [Test]
        public void When_CreateRiskAssessment_Then_New_RiskAssessment_Id_Is_Returned()
        {
            // Given
            var request = new CreateRiskAssessmentRequest()
            {
                UserId = Guid.NewGuid(),
                Title = "Title",
                Reference = "Reference",
                CompanyId = 100
            };

            var passedFireRiskAssessment = new FireRiskAssessment();

            _fireRiskAssessmentRepo
                .Setup(x => x.Save(It.IsAny<FireRiskAssessment>()))
                .Callback<FireRiskAssessment>(y => passedFireRiskAssessment = y);

            var target = GetTarget();

            // When
            var result = target.CreateRiskAssessment(request);

            // Then
            Assert.That(result, Is.EqualTo(passedFireRiskAssessment.Id));
        }
        

        [Test]
        public void When_CreateRiskAssessment_Then_Check_If_RiskAssessment_Reference_Has_Already_Been_Used()
        {
            // Given
            var request = new CreateRiskAssessmentRequest()
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
                .Verify(x => x.DoesAssessmentExistWithTheSameReference<FireRiskAssessment>(100, "Reference", 0));
        }

        [Test]
        public void When_CreateRiskAssessment_and_reference_is_empty_Then_do_not_Check_If_RiskAssessment_Reference_Has_Already_Been_Used()
        {
            // Given
            var request = new CreateRiskAssessmentRequest()
            {
                UserId = Guid.NewGuid(),
                Title = "Title",
                Reference = "",
                CompanyId = 100
            };

            var target = GetTarget();

            // When
            target.CreateRiskAssessment(request);

            // Then
            _riskAssessmentRepository
                .Verify(x => x.DoesAssessmentExistWithTheSameReference<FireRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()), Times.Never());
        }

        [Test]
        [Ignore("Logic no longer applies. PTD 05/09/13")]
        public void When_CreateRiskAssessment_Then_Check_If_RiskAssessment_title_Has_Already_Been_Used()
        {
            // Given
            var request = new CreateRiskAssessmentRequest()
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
            _riskAssessmentRepository.Verify(x => x.DoesAssessmentExistWithTheSameTitle<FireRiskAssessment>  (It.IsAny<long>(), "Title", 0),Times.Once());
            _riskAssessmentRepository.Verify(x => x.DoesAssessmentExistWithTheSameTitle<FireRiskAssessment>(100, It.IsAny<string>(), 0), Times.Once());
        }

        private FireRiskAssessmentService GetTarget()
        {
            return new FireRiskAssessmentService(
                _fireRiskAssessmentRepo.Object,
                _userRepo.Object, 
                _checklistRepository.Object,
                null, null, _log.Object, _riskAssessmentRepository.Object,null, null, null);
        }
    }
}
