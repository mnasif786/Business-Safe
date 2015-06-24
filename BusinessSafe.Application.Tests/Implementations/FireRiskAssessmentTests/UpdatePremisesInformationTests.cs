using System;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    public class UpdatePremisesInformationTests
    {
        private Mock<IFireRiskAssessmentRepository> _fireRiskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IPeninsulaLog> _log;
        private long _riskAssessmentId = 234L;
        private long _companyId = 425L;
        private Guid _userId;
        private UpdateFireRiskAssessmentPremisesInformationRequest _request;
        private Mock<FireRiskAssessment> _fireRiskAssessment;
        private Mock<IChecklistRepository> _checklistRepository;
        private UserForAuditing _user;
        
        [SetUp]
        public void Setup()
        {
            _userId = Guid.NewGuid();

            _request = new UpdateFireRiskAssessmentPremisesInformationRequest
                           {
                               CompanyId = _companyId,
                               FireRiskAssessmentId = _riskAssessmentId,
                               CurrentUserId = _userId,
                               PremisesProvidesSleepingAccommodation = true,
                               PremisesProvidesSleepingAccommodationConfirmed = true,
                               Location = "HERE MAN",
                               BuildingUse = "FOR CHILLING OUT HARD",
                               NumberOfFloors = 500,
                               NumberOfPeople = 5,
                               ElectricityEmergencyShutOff = "Elec shutoff",
                               WaterEmergencyShutOff = "Eater shutoff",
                               GasEmergencyShutOff = "Gas shutoff",
                               OtherEmergencyShutOff = "Other shutoff"
                           };

            _user = new UserForAuditing
                        {
                            Id = _userId
                        };

            _fireRiskAssessment = new Mock<FireRiskAssessment>();
            _fireRiskAssessmentRepository = new Mock<IFireRiskAssessmentRepository>();

            _fireRiskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(_riskAssessmentId, _companyId))
                .Returns(_fireRiskAssessment.Object);

            _userRepository = new Mock<IUserForAuditingRepository>();

            _userRepository
                .Setup(x => x.GetById(_userId))
                .Returns(_user);

            _employeeRepository = new Mock<IEmployeeRepository>();
            _log = new Mock<IPeninsulaLog>();

            _checklistRepository = new Mock<IChecklistRepository>();

        }

        [Test]
        public void When_UpdatePremisesInformation_called_Then_correct_methods_called()
        {
            GetTarget().UpdatePremisesInformation(_request);
            _fireRiskAssessmentRepository.Verify(x => x.GetByIdAndCompanyId(_riskAssessmentId, _companyId), Times.Once());
            _userRepository.Verify(x => x.GetById(_userId), Times.Once());
            _fireRiskAssessment.Verify(x => x.UpdatePremisesInformation(true, true, _request.Location, _request.BuildingUse, _request.NumberOfFloors, _request.NumberOfPeople, It.Is<EmergencyShutOffParameters>(y => y.ElectricityEmergencyShutOff == _request.ElectricityEmergencyShutOff && y.WaterEmergencyShutOff == _request.WaterEmergencyShutOff && y.GasEmergencyShutOff == _request.GasEmergencyShutOff && y.OtherEmergencyShutOff == _request.OtherEmergencyShutOff), _user));
        }

        private FireRiskAssessmentService GetTarget()
        {
            return new FireRiskAssessmentService(
                _fireRiskAssessmentRepository.Object,
                _userRepository.Object, 
                _checklistRepository.Object,
                null, null,_log.Object, null, null,null, null);
        }
    }
}
