using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Implementations.AccidentRecords;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;


namespace BusinessSafe.Application.Tests.Implementations.AccidentRecordServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class AddDescriptionTests
    {
        private Mock<IAccidentRecordRepository> _accidentRecordRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user = new UserForAuditing() { Id = Guid.NewGuid(), CompanyId = 589 };
        private AccidentRecord _accidentRecord = new AccidentRecord() { Id = 123123 };
        private AccidentRecordOverviewRequest _request;
        
        [SetUp]
        public void Setup()
        {
            _accidentRecordRepository = new Mock<IAccidentRecordRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
         
            _userRepository
                .Setup(curUserRepository => curUserRepository.GetByIdAndCompanyId(It.IsAny<Guid>(),It.IsAny<long>()))
                .Returns(() => _user);

            _userRepository
                .Setup(curUserRepository => curUserRepository.GetById(It.IsAny<Guid>()))
                .Returns(() => _user);

            _accidentRecordRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => _accidentRecord);

            _accidentRecord = new AccidentRecord() { Id = 123123 };
        }

        [Test]
        public void Given_any_request_when_AddAccidentRecordDocument_then_user_retrieved()
        {
            _request = new AccidentRecordOverviewRequest()
                           {
                               AccidentRecordId = _accidentRecord.Id,
                               CompanyId = _user.CompanyId,
                               UserId = _user.Id,
                               Description = "description here"
                           };

            //when
            var target = GetTarget();
            target.SetAccidentRecordOverviewDetails(_request);

            //then
            _userRepository.Verify(x => x.GetByIdAndCompanyId(_user.Id, _user.CompanyId), Times.Once());
        }

        [Test]
        public void Given_any_request_when_AddAccidentRecordDocument_Accident_record_retrieved()
        {
            _request = new AccidentRecordOverviewRequest()
            {
                AccidentRecordId = _accidentRecord.Id,
                CompanyId = _user.CompanyId,
                UserId = _user.Id,
                Description = "description here"
            };

            //when
            var target = GetTarget();
            target.SetAccidentRecordOverviewDetails(_request);

            //then
            _accidentRecordRepository.Verify(x => x.GetByIdAndCompanyId(_request.AccidentRecordId,_request.CompanyId), Times.Once());
        }


        [Test]
        public void Given_any_request_when_AddAccidentRecordDocument_then_save_repo_called()
        {
            _request = new AccidentRecordOverviewRequest()
            {
                AccidentRecordId = _accidentRecord.Id,
                CompanyId = _user.CompanyId,
                UserId = _user.Id,
                Description = "description here"
            };

            //when
            var target = GetTarget();
            target.SetAccidentRecordOverviewDetails(_request);

            //then
            _accidentRecordRepository.Verify(x => x.Save(_accidentRecord), Times.Once());
        }


        private IAccidentRecordService GetTarget()
        {
            return new AccidentRecordService(_accidentRecordRepository.Object,
                null,
                null,
                null,
                _userRepository.Object,
                null,
                null,
                null,
                null,
                null,
                null,
                _log.Object,
                null);
        }
    }
}
