using System;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using ActionMailer.Net;
using ActionMailer.Net.Standalone;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.ClientDocumentService;
using BusinessSafe.MessageHandlers.Emails.DocumentLibraryService;
using BusinessSafe.MessageHandlers.Emails.EmailPusherService;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NUnit.Framework;
using Checklist = BusinessSafe.Domain.Entities.SafeCheck.Checklist;
using DocumentDto = BusinessSafe.MessageHandlers.Emails.DocumentLibraryService.DocumentDto;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class SendSubmitChecklistEmailHandlerTest
    {
        private Mock<IEmailSender> _emailSender;
        private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _urlConfiguration;
        private Mock<ICheckListRepository> _checklistRepository;
        private Mock<IDocumentLibraryService> _docLibraryService;
        private Mock<IClientDocumentService> _clientDocumentService;
        private Mock<IClientService> _clientService;
        

        [SetUp]
        public void SetUp()
        {
            _emailSender = new Mock<IEmailSender>();
            _checklistRepository = new Mock<ICheckListRepository>();
            _docLibraryService = new Mock<IDocumentLibraryService>();
            _clientDocumentService = new Mock<IClientDocumentService>();
            _clientService = new Mock<IClientService>();

            _urlConfiguration = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();
            _urlConfiguration.Setup(u => u.GetBaseUrl()).Returns(string.Empty);
        }

        [Test]
        public void given_submitted_checklists_has_a_client_document_when_Handle_then_attachment_added_to_email()
        {
            //GIVEN
            var handler = GetTarget();
            var message = new SendSubmitChecklistEmail() {ChecklistId = Guid.NewGuid()};
            var clientDocumentId = 12345;
            var docLibraryId = 123456;
            var checklist = new Checklist()
                                {
                                    Id = Guid.NewGuid(),
                                    MainPersonSeenName = "Test User",
                EmailReportToPerson = true,
                                    EmailAddress = "test@test.com",
                                    Jurisdiction = "UK",
                                    ExecutiveSummaryDocumentLibraryId = clientDocumentId,
                                    ClientId = 123123,
                                    ActionPlan = new ActionPlan() {ExecutiveSummaryDocumentLibraryId = clientDocumentId},
                                    VisitDate = new DateTime(2014, 05, 01),
                                    SiteId = 1
                                };

            var docLibDocument = new DocumentDto()
                                     {
                                         PhysicalFilePath = @"C:\docLib\2014\1\1\",
                                         PhysicalFilename = "thefilename.pdf",
                                         Extension = ".pdf"
                                     };

            var siteAddress = new SiteAddressDto() {AddressLine1 = "Address", Postcode = "M1 1AA"};

            _checklistRepository.Setup(c => c.GetById(It.IsAny<Guid>()))
                .Returns(() => checklist);

            _clientDocumentService.Setup(x => x.GetById(clientDocumentId))
                .Returns(() => new ClientDocumentDto() {Id = clientDocumentId, DocumentLibraryId = docLibraryId});

            _docLibraryService.Setup((x => x.GetDocumentsByIds(It.IsAny<GetDocumentsByIdsRequest>())))
                .Returns(() => new[] {docLibDocument});

            _clientService.Setup(x => x.GetSite(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => siteAddress);

            AttachmentType[] emailedAttachments = new AttachmentType[0];

            _emailSender.Setup(x => x.SendWithDifferentAttachmentName(It.IsAny<RazorEmailResult>(), It.IsAny<AttachmentType[]>()))
                .Callback<RazorEmailResult, AttachmentType[]>(
                    (emailResult, attachments) => emailedAttachments = attachments);

            //WHEN
            handler.Handle(message);

            //THEN
            Assert.That(emailedAttachments.Length, Is.GreaterThan(0));
            Assert.That(emailedAttachments[0].NewFileName,
                        Is.EqualTo(String.Format("Visit Report - {0} - {1} - {2}{3}", siteAddress.AddressLine1,
                                                 siteAddress.Postcode, checklist.VisitDate.Value.ToShortDateString(), docLibDocument.Extension)));
            Assert.That(emailedAttachments[0].OldFileName, Is.EqualTo(docLibDocument.PhysicalFilePath + docLibDocument.PhysicalFilename));
        }
        
       [Test]
       public void given_submitted_checklist_has_no_document_Id_then_send_email_without_attachment_details()
       {
           //GIVEN
           var handler = GetTarget();
           var message = new SendSubmitChecklistEmail() { ChecklistId = Guid.NewGuid() };
           var clientDocumentId = 12345;
           var docLibraryId = 123456;
           var checklist = new Checklist()
           {
               Id = Guid.NewGuid(),
               MainPersonSeenName = "Test User",
               EmailReportToPerson = true,
               EmailAddress = "test@test.com",
               Jurisdiction = "UK",
               ExecutiveSummaryDocumentLibraryId = null,
               ClientId = 123123,
               ActionPlan = new ActionPlan() { ExecutiveSummaryDocumentLibraryId = null },
               VisitDate = new DateTime(2014, 05, 01),
               SiteId = 1
           };

           var docLibDocument = new DocumentDto()
           {
               PhysicalFilePath = @"C:\docLib\2014\1\1\",
               PhysicalFilename = "thefilename.pdf",
               Extension = ".pdf"
           };

           var companyDetails = new CompanyDetailsDto(1212, "long legs", "lon050", "1 London road", "", "", "", 1, "lon50 67", "", "", "");
           var siteAddress = new SiteAddressDto() { AddressLine1 = "Address", Postcode = "M1 1AA" };

           _checklistRepository.Setup(c => c.GetById(It.IsAny<Guid>()))
               .Returns(() => checklist);

           _clientDocumentService.Setup(x => x.GetById(clientDocumentId))
               .Returns(() => new ClientDocumentDto() { Id = clientDocumentId, DocumentLibraryId = docLibraryId });

           _docLibraryService.Setup((x => x.GetDocumentsByIds(It.IsAny<GetDocumentsByIdsRequest>())))
               .Returns(() => new[] { docLibDocument });

           _clientService.Setup(x => x.GetSite(It.IsAny<long>(), It.IsAny<long>()))
             .Returns(() => siteAddress);

           AttachmentType[] emailedAttachments = new AttachmentType[0];

           _emailSender.Setup(x => x.SendWithDifferentAttachmentName(It.IsAny<RazorEmailResult>(), It.IsAny<AttachmentType[]>()))
               .Callback<RazorEmailResult, AttachmentType[]>(
                   (emailResult, attachments) => emailedAttachments = attachments);

           //WHEN
           handler.Handle(message);

           //THEN
           Assert.That(emailedAttachments.Length, Is.EqualTo(0));
       }
        
       [Test]
       public void given_submitted_checklist_has_no_visit_date_then_send_attachment_name_is_without_date()
       {
           //GIVEN
           var handler = GetTarget();
           var message = new SendSubmitChecklistEmail() { ChecklistId = Guid.NewGuid() };
           var clientDocumentId = 12345;
           var docLibraryId = 123456;
           var checklist = new Checklist()
           {
               Id = Guid.NewGuid(),
               MainPersonSeenName = "Test User",
               EmailReportToPerson = true,
               EmailAddress = "test@test.com",
               Jurisdiction = "UK",
               ExecutiveSummaryDocumentLibraryId = clientDocumentId,
               ClientId = 123123,
               ActionPlan = new ActionPlan() { ExecutiveSummaryDocumentLibraryId = clientDocumentId },
               SiteId = 1
           };

           var docLibDocument = new DocumentDto()
           {
               PhysicalFilePath = @"C:\docLib\2014\1\1\",
               PhysicalFilename = "thefilename.pdf",
               Extension = ".pdf"
           };
           
           var siteAddress = new SiteAddressDto() { AddressLine1 = "Address", Postcode = "M1 1AA" };

           _checklistRepository.Setup(c => c.GetById(It.IsAny<Guid>()))
               .Returns(() => checklist);

           _clientDocumentService.Setup(x => x.GetById(clientDocumentId))
               .Returns(() => new ClientDocumentDto() { Id = clientDocumentId, DocumentLibraryId = docLibraryId });

           _docLibraryService.Setup((x => x.GetDocumentsByIds(It.IsAny<GetDocumentsByIdsRequest>())))
               .Returns(() => new[] { docLibDocument });

           _clientService.Setup(x => x.GetSite(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => siteAddress);

            AttachmentType[] emailedAttachments = new AttachmentType[0];

            _emailSender.Setup(x => x.SendWithDifferentAttachmentName(It.IsAny<RazorEmailResult>(), It.IsAny<AttachmentType[]>()))
                .Callback<RazorEmailResult, AttachmentType[]>(
                    (emailResult, attachments) => emailedAttachments = attachments);

            //WHEN
            handler.Handle(message);

            //THEN
            Assert.That(emailedAttachments.Length, Is.GreaterThan(0));
            Assert.That(emailedAttachments[0].NewFileName,
                        Is.EqualTo(String.Format("Visit Report - {0} - {1} - {2}", siteAddress.AddressLine1, siteAddress.Postcode, docLibDocument.Extension)));
            Assert.That(emailedAttachments[0].OldFileName, Is.EqualTo(docLibDocument.PhysicalFilePath + docLibDocument.PhysicalFilename));
        }


       //when document not found in document library
       [Test]
       public void given_submitted_checklist_has_no_document_returned_by_document_library_then_send_email_without_attachment_details()
       {
           //GIVEN
           var handler = GetTarget();
           var message = new SendSubmitChecklistEmail() { ChecklistId = Guid.NewGuid() };
           var clientDocumentId = 12345;
           var docLibraryId = 123456;
           var checklist = new Checklist()
           {
               Id = Guid.NewGuid(),
               MainPersonSeenName = "Test User",
               EmailReportToPerson = true,
               EmailAddress = "test@test.com",
               Jurisdiction = "UK",
               ExecutiveSummaryDocumentLibraryId = docLibraryId,
               ClientId = 123123,
               ActionPlan = new ActionPlan() { ExecutiveSummaryDocumentLibraryId = clientDocumentId },
               VisitDate = new DateTime(2014, 05, 01),
               SiteId = 1
           };
           
           var siteAddress = new SiteAddressDto() { AddressLine1 = "Address", Postcode = "M1 1AA" };
           
           _checklistRepository.Setup(c => c.GetById(It.IsAny<Guid>()))
               .Returns(() => checklist);

           _clientDocumentService.Setup(x => x.GetById(clientDocumentId))
               .Returns(() => new ClientDocumentDto() { Id = clientDocumentId, DocumentLibraryId = docLibraryId });

           _docLibraryService.Setup((x => x.GetDocumentsByIds(It.IsAny<GetDocumentsByIdsRequest>())))
               .Returns(() => null);

           _clientService.Setup(x => x.GetSite(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => siteAddress);

            AttachmentType[] emailedAttachments = new AttachmentType[0];

            _emailSender.Setup(x => x.SendWithDifferentAttachmentName(It.IsAny<RazorEmailResult>(), It.IsAny<AttachmentType[]>()))
                .Callback<RazorEmailResult, AttachmentType[]>(
                    (emailResult, attachments) => emailedAttachments = attachments);

            //WHEN
            handler.Handle(message);

            //THEN
            Assert.That(emailedAttachments.Length, Is.EqualTo(0));
        }

        //when document not found in client documentation
        [Test]
        public void given_submitted_checklist_has_no_document_returned_by_client_document_library_then_send_email_without_attachment_details()
        {
            //GIVEN
            var handler = GetTarget();
            var message = new SendSubmitChecklistEmail() {ChecklistId = Guid.NewGuid()};
            var clientDocumentId = 12345;
            var docLibraryId = 123456;
            var checklist = new Checklist()
                                {
                                    Id = Guid.NewGuid(),
                                    MainPersonSeenName = "Test User",
                                    EmailAddress = "test@test.com",
                                    Jurisdiction = "UK",
                                    ExecutiveSummaryDocumentLibraryId = docLibraryId,
                                    ClientId = 123123,
                                    ActionPlan = new ActionPlan() {ExecutiveSummaryDocumentLibraryId = clientDocumentId},
                                    VisitDate = new DateTime(2014, 05, 01),
                                    SiteId = 1
                                };

            var docLibDocument = new DocumentDto()
                                     {
                                         PhysicalFilePath = @"C:\docLib\2014\1\1\",
                                         PhysicalFilename = "thefilename.pdf",
                                         Extension = ".pdf"
                                     };

            var siteAddress = new SiteAddressDto() {AddressLine1 = "Address", Postcode = "M1 1AA"};

            _checklistRepository.Setup(c => c.GetById(It.IsAny<Guid>()))
                .Returns(() => checklist);

            _clientDocumentService.Setup(x => x.GetById(clientDocumentId))
                .Returns(() => null);

            _docLibraryService.Setup((x => x.GetDocumentsByIds(It.IsAny<GetDocumentsByIdsRequest>())))
                .Returns(() => new[] {docLibDocument});

            _clientService.Setup(x => x.GetSite(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => siteAddress);

            AttachmentType[] emailedAttachments = new AttachmentType[0];

            _emailSender.Setup(x => x.SendWithDifferentAttachmentName(It.IsAny<RazorEmailResult>(), It.IsAny<AttachmentType[]>()))
                .Callback<RazorEmailResult, AttachmentType[]>(
                    (emailResult, attachments) => emailedAttachments = attachments);

            //WHEN
            handler.Handle(message);

            //THEN
            Assert.That(emailedAttachments.Length, Is.EqualTo(0));
        }


        [TestCase("Uk", "business.safety@peninsula-uk.com")]
        [TestCase("ROI", "business.safety@peninsula-ie.com")]
        [TestCase("NI", "business.safety@peninsula-ni.com")]
        [TestCase("", "business.safety@peninsula-uk.com")]
        [TestCase(null, "business.safety@peninsula-uk.com")]
        public void given_UK_jurisdiction_when_SenderEmailAddress_then_expected_email_address(string jurisdiction, string emailAddress)
        {
            //GIVEN
            var result = SendSubmitChecklistEmailHandler.SenderEmailAddress(jurisdiction);

            //THEN
            Assert.That(result, Is.EqualTo(emailAddress));
        }

        [Ignore] //need to refactor the mailer controller to get this to work
        [Test]
        public void given_UK_jurisdiction_when_sent_then_CCRecipent()
        {
            //GIVEN
           // var handler = GetTarget();
            var handler = new SendSubmitChecklistEmailHandler(_emailSender.Object, _checklistRepository.Object, _urlConfiguration.Object, _docLibraryService.Object, _clientDocumentService.Object, _clientService.Object);
            var message = new SendSubmitChecklistEmail() { ChecklistId = Guid.NewGuid() };
            var qaAdvisor = new QaAdvisor() {Id = Guid.NewGuid(), Email = "qaAdvisorTest@test.com"};

            var checklist = new Checklist()
            {
                Id = message.ChecklistId,
                MainPersonSeenName = "Test User",
                EmailAddress = "test@test.com",
                Jurisdiction = "UK",
                ClientId = 123123,
                VisitDate = new DateTime(2014, 05, 01),
                SiteId = 1,
                QaAdvisor = qaAdvisor
            };

            var siteAddress = new SiteAddressDto() { AddressLine1 = "Address", Postcode = "M1 1AA" };

            _checklistRepository.Setup(c => c.GetById(It.IsAny<Guid>()))
                .Returns(() => checklist);

            _clientService.Setup(x => x.GetSite(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => siteAddress);

            RazorEmailResult sentEmail = null;

            _emailSender.Setup(x => x.SendWithDifferentAttachmentName(It.IsAny<RazorEmailResult>(), It.IsAny<AttachmentType[]>()))
                .Callback<RazorEmailResult, AttachmentType[]>(
                    (emailResult, attachments) => sentEmail = emailResult);

            //WHEN
            handler.Handle(message);

            //THEN
            Assert.That(sentEmail.Mail.CC.Count, Is.GreaterThan(0));
          
        }

        [Ignore]
        [Test]
        public void given_qaAdvisor_has_no_email_address_when_sent_then_no_CCRecipents()
        {
            //GIVEN
            // var handler = GetTarget();
            var handler = new SendSubmitChecklistEmailHandler(_emailSender.Object, _checklistRepository.Object, _urlConfiguration.Object, _docLibraryService.Object, _clientDocumentService.Object, _clientService.Object);
            var message = new SendSubmitChecklistEmail() { ChecklistId = Guid.NewGuid() };
            var qaAdvisor = new QaAdvisor() { Id = Guid.NewGuid(), Email = "" };

            var checklist = new Checklist()
            {
                Id = message.ChecklistId,
                MainPersonSeenName = "Test User",
                EmailAddress = "test@test.com",
                Jurisdiction = "UK",
                ClientId = 123123,
                VisitDate = new DateTime(2014, 05, 01),
                SiteId = 1,
                QaAdvisor = qaAdvisor
            };

            var siteAddress = new SiteAddressDto() { AddressLine1 = "Address", Postcode = "M1 1AA" };

            _checklistRepository.Setup(c => c.GetById(It.IsAny<Guid>()))
                .Returns(() => checklist);

            _clientService.Setup(x => x.GetSite(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => siteAddress);

            RazorEmailResult sentEmail = null;

            _emailSender.Setup(x => x.SendWithDifferentAttachmentName(It.IsAny<RazorEmailResult>(), It.IsAny<AttachmentType[]>()))
                .Callback<RazorEmailResult, AttachmentType[]>(
                    (emailResult, attachments) => sentEmail = emailResult);

            //WHEN
            handler.Handle(message);

            //THEN
            Assert.That(sentEmail.Mail.CC.Count, Is.EqualTo(0));
        }

        private FakeSendChecklistSubmitEmailHandler GetTarget()
        {
            var handler = new FakeSendChecklistSubmitEmailHandler(_emailSender.Object, _checklistRepository.Object, _urlConfiguration.Object, _docLibraryService.Object, _clientDocumentService.Object, _clientService.Object);
            return handler;
        }
    }

    public class FakeSendChecklistSubmitEmailHandler : SendSubmitChecklistEmailHandler
    {
        public FakeSendChecklistSubmitEmailHandler(
            IEmailSender emailSender, ICheckListRepository checklistRepository,
            IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration, IDocumentLibraryService docLibraryService
            , IClientDocumentService clientDocumentService, IClientService clientService)
            : base(emailSender, checklistRepository, businessSafeEmailLinkBaseUrlConfiguration, docLibraryService, clientDocumentService, clientService)
        {
        }

        protected override RazorEmailResult CreateRazorEmailResult(SendSubmitChecklistEmailViewModel viewModel)
        {

            var mailMessage = new MailMessage();
            return new RazorEmailResult(new Mock<IMailInterceptor>().Object,
                                        new Mock<IMailSender>().Object,
                                        new Mock<MailMessage>().Object,
                                        "ViewName",
                                        Encoding.ASCII,
                                        "ViewPath");
        }
    }

   
}
