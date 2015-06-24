using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations
{
    [TestFixture]
    [Category("Unit")]
    public class AttachDocumentsToRiskAssessmentTests
    {
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;
        private Mock<IDocumentTypeRepository> _documentTypeRepository;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
            _documentTypeRepository = new Mock<IDocumentTypeRepository>();
            _documentTypeRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(new DocumentType());
        }

        [Test]
        public void Given_valid_request_When_AttachDocuments_is_called_Then_should_call_appropiate_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var attachDocumentsToRiskAssessmentRequest = new AttachDocumentsToRiskAssessmentRequest()
                                                             {
                                                                 DocumentsToAttach = new List<CreateDocumentRequest>()
                                                                                         {
                                                                                             new CreateDocumentRequest(), 
                                                                                             new CreateDocumentRequest()
                                                                                         },
                                                                 RiskAssessmentId = 2,
                                                                 CompanyId = 1,
                                                                 UserId = Guid.NewGuid()
                                                             };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            mockRiskAssessment.Setup(x => x.Documents)
                .Returns(() => new List<RiskAssessmentDocument>());

            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(attachDocumentsToRiskAssessmentRequest.RiskAssessmentId, attachDocumentsToRiskAssessmentRequest.CompanyId))
                .Returns(mockRiskAssessment.Object);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(attachDocumentsToRiskAssessmentRequest.UserId, attachDocumentsToRiskAssessmentRequest.CompanyId))
                .Returns(_user);

            //When
            riskAssessmentService.AttachDocumentsToRiskAssessment(attachDocumentsToRiskAssessmentRequest);

            //Then
            _riskAssessmentRepository.Verify(x => x.SaveOrUpdate(mockRiskAssessment.Object));
            _userRepository.Verify(x => x.GetByIdAndCompanyId(attachDocumentsToRiskAssessmentRequest.UserId, attachDocumentsToRiskAssessmentRequest.CompanyId));
            mockRiskAssessment.Verify(x => x.AttachDocumentToRiskAssessment(It.Is<IEnumerable<RiskAssessmentDocument>>(y => y.Count() == attachDocumentsToRiskAssessmentRequest.DocumentsToAttach.Count), _user), Times.Once());
        }



        [Test]
        public void Given_valid_request_When_AttachDocuments_parameters_are_set_correctly()
        {
            //Given
            _documentTypeRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(new DocumentType() { Id = 1 });

            var createDocReq = new CreateDocumentRequest()
                                   {
                                       Title = "title",
                                       DocumentLibraryId = 456,
                                       Filename = "filename",
                                       Extension = ".ext",
                                       FilesizeByte = 1000,
                                       Description = "description",
                                       DocumentType = DocumentTypeEnum.GRADocumentType,
                                       DocumentOriginType = DocumentOriginType.TaskCreated
                                   };
            var riskAssessmentService = CreateRiskAssessmentService();
            var attachDocumentsToRiskAssessmentRequest = new AttachDocumentsToRiskAssessmentRequest()
            {
                DocumentsToAttach = new List<CreateDocumentRequest>()
                                        {
                                            createDocReq
                                        },
                RiskAssessmentId = 2,
                CompanyId = 1,
                UserId = Guid.NewGuid()
            };

            var passedRiskAssessmentDocuments = new List<RiskAssessmentDocument>();

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            mockRiskAssessment.Setup(x => x.Documents)
               .Returns(() => new List<RiskAssessmentDocument>());

            mockRiskAssessment
                .Setup(x => x.AttachDocumentToRiskAssessment(It.IsAny<IEnumerable<RiskAssessmentDocument>>(), It.IsAny<UserForAuditing>()))
                .Callback<IEnumerable<RiskAssessmentDocument>, UserForAuditing>((IEnumerable<RiskAssessmentDocument> y, UserForAuditing z) => passedRiskAssessmentDocuments = y.ToList());

            var site = new SiteGroup();
            site.Name = "site name";
            mockRiskAssessment
                .Setup(x => x.RiskAssessmentSite).Returns(site);

            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(attachDocumentsToRiskAssessmentRequest.RiskAssessmentId, attachDocumentsToRiskAssessmentRequest.CompanyId))
                .Returns(mockRiskAssessment.Object);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(attachDocumentsToRiskAssessmentRequest.UserId, attachDocumentsToRiskAssessmentRequest.CompanyId))
                .Returns(_user);

            //When
            riskAssessmentService.AttachDocumentsToRiskAssessment(attachDocumentsToRiskAssessmentRequest);

            //Then
            mockRiskAssessment.Verify(x => x.AttachDocumentToRiskAssessment(It.IsAny<List<RiskAssessmentDocument>>(), _user), Times.Once());
            Assert.That(passedRiskAssessmentDocuments.ElementAt(0).ClientId, Is.EqualTo(attachDocumentsToRiskAssessmentRequest.CompanyId));
            Assert.That(passedRiskAssessmentDocuments.ElementAt(0).CreatedBy, Is.EqualTo(_user));
            Assert.That(passedRiskAssessmentDocuments.ElementAt(0).CreatedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
            Assert.That(passedRiskAssessmentDocuments.ElementAt(0).Deleted, Is.EqualTo(false));
            Assert.That(passedRiskAssessmentDocuments.ElementAt(0).Description, Is.EqualTo(createDocReq.Description));
            Assert.That(passedRiskAssessmentDocuments.ElementAt(0).DocumentType.Id, Is.EqualTo((long)createDocReq.DocumentType));
            Assert.That(passedRiskAssessmentDocuments.ElementAt(0).Extension, Is.EqualTo(createDocReq.Extension));
            Assert.That(passedRiskAssessmentDocuments.ElementAt(0).Filename, Is.EqualTo(createDocReq.Filename));
            Assert.That(passedRiskAssessmentDocuments.ElementAt(0).FilesizeByte, Is.EqualTo(createDocReq.FilesizeByte));
            Assert.That(passedRiskAssessmentDocuments.ElementAt(0).Title, Is.EqualTo(createDocReq.Filename));
        }

        private Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new Application.Implementations.RiskAssessments.RiskAssessmentAttachmentService(_riskAssessmentRepository.Object, _userRepository.Object,  _documentTypeRepository.Object, null, _log.Object, null);
            return riskAssessmentService;
        }
    }
}