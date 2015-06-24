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
    public class RemoveAccidentRecordDocumentTest
    {
        private Mock<IAccidentRecordRepository> _accidentRecordRepository;
        private Mock<IUserForAuditingRepository> _userRepository;

        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user = new UserForAuditing() {Id = Guid.NewGuid(), CompanyId = 589};
        private AccidentRecord _accidentRecord = new AccidentRecord(){Id =123123};

        private RemoveDocumentsFromAccidentRecordRequest _request;


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
          

            _request = new RemoveDocumentsFromAccidentRecordRequest()
                           {
                               AccidentRecordId = 123123,
                               CompanyId = 589,
                               DocumentLibraryIds = new List<long>() {56, 154, 234},
                               UserId = _user.Id
                           };
        }

        private AccidentRecordService GetTarget()
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

        [Test]
        public void Given_any_request_when_RemoveAccidentRecordDocument_then_accident_record_retrieved_from_repo()
        {
            //when
            var target = GetTarget();
            target.RemoveAccidentRecordDocuments(_request);

            //then
            _accidentRecordRepository.Verify(x => x.GetByIdAndCompanyId(_request.AccidentRecordId, _user.CompanyId));
        }

        [Test]
        public void Given_any_request_when_RemoveAccidentRecordDocument_then_user_retrieved_from_repo()
        {
            //when
            var target = GetTarget();
            target.RemoveAccidentRecordDocuments(_request);

            //then
            _userRepository.Verify(x => x.GetByIdAndCompanyId(_request.UserId,_request.CompanyId));
        }

        [Test]
        public void Given_any_request_when_RemoveAccidentRecordDocument_then_accident_record_is_saved()
        {
            //when
            var target = GetTarget();
            target.RemoveAccidentRecordDocuments(_request);

            //then
            _accidentRecordRepository.Verify(x => x.Save(_accidentRecord),Times.Once());
        }

        [Test]
        public void Given_any_request_when_RemovAccidentRecordDocument_then_document_added_to_accident_record()
        {
            //given
            _accidentRecord.AddAccidentDocumentRecord(new AccidentRecordDocument() { Id = 1231, DocumentLibraryId = 23 });
            _accidentRecord.AddAccidentDocumentRecord(new AccidentRecordDocument() { Id = 1231, DocumentLibraryId = 56 });
            _accidentRecord.AddAccidentDocumentRecord(new AccidentRecordDocument() { Id = 1231, DocumentLibraryId = 234 });

            _request.DocumentLibraryIds = new List<long>() { _accidentRecord.AccidentRecordDocuments.First().DocumentLibraryId };

            //when
            var target = GetTarget();
            target.RemoveAccidentRecordDocuments(_request);

            //then
            Assert.AreEqual(2, _accidentRecord.AccidentRecordDocuments.Count(x => x.Deleted == false));
        }
    }
}
