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
    public class AddAccidentRecordDocumentTest
    {
        private Mock<IAccidentRecordRepository> _accidentRecordRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IDocumentTypeRepository> _documentTypeRepository;

        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user = new UserForAuditing() {Id = Guid.NewGuid(), CompanyId = 589};
        private AccidentRecord _accidentRecord = new AccidentRecord(){Id =123123};
        private AddDocumentToAccidentReportRequest _request;

        [SetUp]
        public void Setup()
        {
            _accidentRecordRepository = new Mock<IAccidentRecordRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
            _documentTypeRepository = new Mock<IDocumentTypeRepository>();

            _userRepository
                .Setup(curUserRepository => curUserRepository.GetByIdAndCompanyId(It.IsAny<Guid>(),It.IsAny<long>()))
                .Returns(() => _user);

            _userRepository
                .Setup(curUserRepository => curUserRepository.GetById(It.IsAny<Guid>()))
                .Returns(() => _user);

            _accidentRecordRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => _accidentRecord);
            
            _accidentRecord = new AccidentRecord() { Id = 123123 };

            _documentTypeRepository.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(new DocumentType() {Id = 17, Name = "Accident Record"});

            _request = new AddDocumentToAccidentReportRequest()
                           {
                               AccidentRecordId = 56456,
                               CompanyId = _user.CompanyId,
                               DocumentLibraryIds =
                                   new List<DocumentLibraryFile>()
                                       {
                                           new DocumentLibraryFile()
                                               {Id = 123, Description = "descaf", Filename = "test.txt"}
                                       }
                                       ,
                               UserId = _user.Id

                           };
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
                                             _documentTypeRepository.Object,
                                             null,
                                             null,
                                             _log.Object,
                                             null);
        }

        [Test]
        public void Given_any_request_when_AddAccidentRecordDocument_then_accident_record_retrieved_from_repo()
        {
            
            //when
            var target = GetTarget();
            target.AddAccidentRecordDocument(_request);

            //then
            _accidentRecordRepository.Verify(x=> x.GetByIdAndCompanyId(_request.AccidentRecordId,_request.CompanyId));

        }

        [Test]
        public void Given_any_request_when_AddAccidentRecordDocument_then_user_retrieved_from_repo()
        {

            //when
            var target = GetTarget();
            target.AddAccidentRecordDocument(_request);

            //then
            _userRepository.Verify(x => x.GetByIdAndCompanyId(_request.UserId, _request.CompanyId));

        }

        [Test]
        public void Given_any_request_when_AddAccidentRecordDocument_then_accident_record_is_saved()
        {

            //when
            var target = GetTarget();
            target.AddAccidentRecordDocument(_request);

            //then
            _accidentRecordRepository.Verify(x => x.Save(_accidentRecord),Times.Once());

        }

        [Test]
        public void Given_any_request_when_AddAccidentRecordDocument_then_document_added_to_accident_record()
        {
            //when
            var target = GetTarget();
            target.AddAccidentRecordDocument(_request);

            //then
            Assert.AreEqual(_request.DocumentLibraryIds.Count, _accidentRecord.AccidentRecordDocuments.Count());

        }
    }
}
