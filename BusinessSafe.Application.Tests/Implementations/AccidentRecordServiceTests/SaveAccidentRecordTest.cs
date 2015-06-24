using System;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Implementations.AccidentRecords;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using Moq;
using NUnit.Framework;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Tests.Implementations.AccidentRecordServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SaveAccidentRecordTest
    {
        private Mock<IAccidentRecordRepository> _accidentRecordRepository;
        private Mock<IJurisdictionRepository> _jurisdictionRepository;
        private Mock<IUserForAuditingRepository> _userRepository;

        private Mock<ICountriesRepository> _countriesRepository;
        private Mock<IEmployeeRepository> _employeeRepository;

        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;

        [SetUp]
        public void Setup()
        {
            _accidentRecordRepository = new Mock<IAccidentRecordRepository>();
            _jurisdictionRepository = new Mock<IJurisdictionRepository>();

            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing {Id = new Guid()};
            _log = new Mock<IPeninsulaLog>();

            _userRepository
                .Setup(curUserRepository => curUserRepository.GetById(It.IsAny<Guid>()))
                .Returns(_user);

            _jurisdictionRepository
                .Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(new Jurisdiction {Name = "Test Name"});

            _countriesRepository = new Mock<ICountriesRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
        }

        [Test]
        public void Given_valid_request_When_SaveRiskAssessment_Then_should_call_correct_methods()
        {

            //Given
            var accidentRecordService = GetTarget();

            //When
            var request = new SaveAccidentRecordSummaryRequest
                              {
                                  CompanyId = default(long),
                                  Reference = "Reference",
                                  Title = "Title"
                              };

            accidentRecordService.CreateAccidentRecord(request);

            //Then
            _accidentRecordRepository.Verify(x=>x.SaveOrUpdate(It.IsAny<AccidentRecord>()));
            _jurisdictionRepository.Verify(x => x.GetById(It.IsAny<long>()));
            _userRepository.Verify(x => x.GetByIdAndCompanyId(_user.Id, request.CompanyId), Times.Once());

        }

        private IAccidentRecordService GetTarget()
        {
            return new AccidentRecordService(_accidentRecordRepository.Object,
                null,
                null,
                _jurisdictionRepository.Object,
                _userRepository.Object,
                _countriesRepository.Object,
                _employeeRepository.Object,
                null,
                null,
                null,
                null,
                _log.Object,
                null);
        }
    }
}
