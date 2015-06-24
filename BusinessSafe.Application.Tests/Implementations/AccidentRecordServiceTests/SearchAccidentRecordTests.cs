using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using Moq;
using NUnit.Framework;
using BusinessSafe.Application.Implementations.AccidentRecords;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Domain.Constants;

namespace BusinessSafe.Application.Tests.Implementations.AccidentRecordServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SearchAccidentRecordTests
    {
        private Mock<IAccidentRecordRepository> _accidentRecordRepository;
        private Mock<IJurisdictionRepository> _jurisdictionRepository;
        private Mock<IUserForAuditingRepository> _userRepository;

        private Mock<ICountriesRepository> _countriesRepository;
        private Mock<IEmployeeRepository> _employeeRepository;

        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;

        private SearchAccidentRecordsRequest _request;

        private IEnumerable<AccidentRecord>  _returnedAccidentRecords;

        [SetUp]
        public void Setup()
        {
            // set up repositiories
             _accidentRecordRepository = new Mock<IAccidentRecordRepository>();
            _jurisdictionRepository = new Mock<IJurisdictionRepository>();

            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing {Id = new Guid()};
            _log = new Mock<IPeninsulaLog>();

            _userRepository
                .Setup(curUserRepository => curUserRepository.GetById(It.IsAny<Guid>()))
                .Returns(_user);

            _countriesRepository = new Mock<ICountriesRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();


            _returnedAccidentRecords = new List<BusinessSafe.Domain.Entities.AccidentRecord>()
                                           {
                                               new BusinessSafe.Domain.Entities.AccidentRecord()
                                                   {
                                                       CompanyId = 12345L,
                                                       Title = "Some Title 111",
                                                       Reference = "",

                                                       EmployeeInjured = new Employee() {EmployeeReference = "Test"},
                                                       CreatedBy = new UserForAuditing() {Id = Guid.NewGuid()},
                                                       SiteWhereHappened = new Site() {Id = 1346624L},
                                                       Jurisdiction =
                                                           new Jurisdiction {Id = 1, Name = JurisdictionNames.GB}
                                                   },

                                               new BusinessSafe.Domain.Entities.AccidentRecord()
                                                   {
                                                       CompanyId = 12345L,
                                                       Title = "Some Title 222",
                                                       Reference = "",

                                                       EmployeeInjured = new Employee() {EmployeeReference = "Test"},
                                                       CreatedBy = new UserForAuditing() {Id = Guid.NewGuid()},
                                                       SiteWhereHappened = new Site() {Id = 1346624L},
                                                       Jurisdiction =
                                                           new Jurisdiction {Id = 1, Name = JurisdictionNames.GB}
                                                   },

                                               new BusinessSafe.Domain.Entities.AccidentRecord()
                                                   {
                                                       CompanyId = 12345L,
                                                       Title = "Some Title 333",
                                                       Reference = "",

                                                       EmployeeInjured = new Employee() {EmployeeReference = "Test"},
                                                       CreatedBy = new UserForAuditing() {Id = Guid.NewGuid()},

                                                       SiteWhereHappened = new Site() {Id = 1346624L},
                                                       Jurisdiction =
                                                           new Jurisdiction {Id = 1, Name = JurisdictionNames.GB}
                                                   }
                                           };


            _accidentRecordRepository
                .Setup(x => x.Search(
                    It.IsAny<IList<long>>(),        // allowedSiteIds
                    It.IsAny<long>(),               // companyId
                    It.IsAny<long?>(),              // siteId
                    It.IsAny<string>(),             // title
                    It.IsAny<DateTime?>(),          // createdFrom
                    It.IsAny<DateTime?>(),          // createdTo
                    It.IsAny<bool>(),               // showDeleted
                    It.IsAny<string>(),             // injuredPersonForename
                    It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<AccidentRecordstOrderByColumn>(), It.IsAny<bool>()))
                .Returns(_returnedAccidentRecords);

            // set up request
            _request = new SearchAccidentRecordsRequest()
                           {
                                CompanyId = 12345L,
                                Title = "",
                                CreatedFrom =  DateTime.Now.AddDays(-10),
                                CreatedTo =  DateTime.Now.AddDays(-2),
                                SiteId = 123L,
                                ShowDeleted  = false,
                                ShowArchived = false,
                                InjuredPersonForename = "",
                                InjuredPersonSurname = ""
                           };
        }
       
        [Test]
        public void When_Search_Then_map_returned_entities_to_dtos()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Search(_request);

            // Then
            Assert.That(result, Is.InstanceOf<IEnumerable<AccidentRecordDto>>());
            Assert.That(result.Count(), Is.EqualTo(_returnedAccidentRecords.Count()));
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
