using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.Implementations.AccidentRecords;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.AccidentRecordServiceTests
{
    [TestFixture]
    public class DeleteAccidentRecordTests
    {
        private Mock<IAccidentRecordRepository> _accidentRecordRepository;
        private Mock< IAccidentTypeRepository> _accidentTypeRepository;
        private Mock< ICauseOfAccidentRepository> _causeOfAccidentRepository;
        private Mock< IJurisdictionRepository> _jurisdictionRepository;
        private Mock< IUserForAuditingRepository> _userForAuditingRepository;
        private Mock< ICountriesRepository> _countriesRepository;
        private Mock< IEmployeeRepository> _employeeRepository;
        private Mock< ISiteRepository> _siteRepository;
        private Mock< IPeninsulaLog> _log;
        private UserForAuditing _user;
        private long _companyId;
        private Mock<AccidentRecord> _accidentRecord;


        [SetUp]
        public void Setup()
        {
            _accidentRecordRepository = new Mock<IAccidentRecordRepository>();
            _accidentTypeRepository = new Mock<IAccidentTypeRepository>();
            _causeOfAccidentRepository = new Mock<ICauseOfAccidentRepository>();
            _jurisdictionRepository = new Mock<IJurisdictionRepository>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _countriesRepository = new Mock<ICountriesRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _log = new Mock<IPeninsulaLog>();

            _companyId = 1234L;

            _user = new UserForAuditing {Id = Guid.NewGuid(), CompanyId = _companyId};

            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(
                _user);

            _accidentRecord = new Mock<AccidentRecord>();

            _accidentRecordRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_accidentRecord.Object);
        }

        [Test]
        public void when_delete_then_get_accident_record_from_repository()
        {
            //given

            long accidentRecordId = 1L;
            var target = GetTarget();
            
            //when

            target.Delete(accidentRecordId, _companyId, _user.Id);

            //then

            _accidentRecordRepository.Verify(x => x.GetByIdAndCompanyId(accidentRecordId, _companyId));

        }

        [Test]
        public void when_accident_record_found_then_mark_for_delete()
        {
            //given
            long accidentRecordId = 1L;
            var target = GetTarget();

            //when
            target.Delete(accidentRecordId, _companyId, _user.Id);

            //then

            _accidentRecord.Verify(x => x.MarkForDelete(_user));
            
        }


        [Test]
        [ExpectedException(typeof(AccidentRecordNotFoundException))]
        public void when_accident_record_not_found_then_exception_thrown()
        {
            //given
            var accidentRecordId = 1;
            _accidentRecordRepository.Setup(x => x.GetByIdAndCompanyId(accidentRecordId, _companyId));
            
            var target = GetTarget();

            //when
            target.Delete(accidentRecordId, _companyId, _user.Id);

            //then
            
        }

        private IAccidentRecordService GetTarget()
        {
            var accidentRecordService = new AccidentRecordService(_accidentRecordRepository.Object,
                                                                  _accidentTypeRepository.Object,
                                                                  _causeOfAccidentRepository.Object,
                                                                  _jurisdictionRepository.Object,
                                                                  _userForAuditingRepository.Object,
                                                                  _countriesRepository.Object,
                                                                  _employeeRepository.Object,
                                                                  _siteRepository.Object,
                                                                  null,
                                                                  null,
                                                                  null,
                                                                  _log.Object,
                                                                  null);
            return accidentRecordService;
        }
    }
}
