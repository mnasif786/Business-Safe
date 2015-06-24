using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Web;
using ActionMailer.Net.Standalone;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.ClientDocumentService;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.DocumentLibraryService;
using BusinessSafe.MessageHandlers.Emails.EmailPusherService;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using NHibernate.Linq;
using NHibernate.Mapping;
using NServiceBus;


namespace BusinessSafe.MessageHandlers.Emails.MessageHandlers
{
    public class DocumentConversionStructure
    {
        public string PhysicalPath { get; set; }
        public string DocType { get; set; }
    }

    public class SendSubmitChecklistEmailHandler : IHandleMessages<SendSubmitChecklistEmail>
    {
        private readonly IEmailSender _emailSender;
        private readonly ICheckListRepository _checklistRepository;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;
        private readonly IDocumentLibraryService _documentLibraryService;
        private readonly IClientDocumentService _clientDocumentService;
        private readonly IClientService _clientService;
       
        public SendSubmitChecklistEmailHandler(
            IEmailSender emailSender, ICheckListRepository checklistRepository, IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration,
            IDocumentLibraryService documentLibraryService, IClientDocumentService clientDocumentService, IClientService clientService)
           
        {
            _emailSender = emailSender;
            _checklistRepository = checklistRepository;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
            _documentLibraryService =  documentLibraryService;
            _clientDocumentService = clientDocumentService;
            _clientService = clientService;
        }

        public void Handle(SendSubmitChecklistEmail message)
        {
            var checklist = _checklistRepository.GetById(message.ChecklistId);
            var emailAddressToSendEmail = (checklist != null) ? GetEmailAddressList(checklist) : null;

            if (emailAddressToSendEmail != null && emailAddressToSendEmail.Any())
            {
                var sendersAddress = SenderEmailAddress(checklist.Jurisdiction);

                var companyDetails = (checklist.ClientId != null) ? _clientService.GetCompanyDetails(checklist.ClientId.Value) : null;

                var viewModel = new SendSubmitChecklistEmailViewModel()
                {
                    From = sendersAddress,
                    Cc = sendersAddress,
                    Subject = (companyDetails != null && companyDetails.CAN != null) ? String.Format("{0} Evaluation Report Uploaded into BusinessSafe Online", companyDetails.CAN) : "Evaluation Report Uploaded into BusinessSafe Online",
                    BusinessSafeOnlineLink = new HtmlString(_businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl())
                };

                //Add the emails addresses to the To List
                emailAddressToSendEmail.ForEach(emailAddress => viewModel.To.Add(emailAddress));

                var attachments = GetAttachment(checklist);

                if (attachments.Any())
                    Log4NetHelper.Log.Info(String.Format("EmailSenderHelper attachment: Old: {0} - New:{1}", attachments[0].OldFileName,attachments[0].NewFileName));

                var email = CreateRazorEmailResult(viewModel);
                _emailSender.SendWithDifferentAttachmentName(email, attachments);
            }
               
            Log4NetHelper.Log.Info("SendSubmitChecklistEmail Command Handled");
        }

        // Gets the attachment details associated with this checklist
        private AttachmentType[] GetAttachment(Checklist checklist)
        {
            Log4NetHelper.Log.Info("Get Attachment");
           
            if (checklist.ActionPlan == null)
            {
                Log4NetHelper.Log.Info("Action Plan is null");
                return new AttachmentType[0];
            }

            var savedAttachment = checklist.ActionPlan.ExecutiveSummaryDocumentLibraryId.HasValue
                   ? GetReportFileStructure(checklist.ActionPlan.ExecutiveSummaryDocumentLibraryId.Value)
                   :  null;

            if (savedAttachment == null)
            {
                Log4NetHelper.Log.Info("saved Attachment is null");
                return new AttachmentType[0];
            }

            var siteDetails = new SiteAddressDto();

            if (checklist.ClientId.HasValue && checklist.SiteId.HasValue)
                siteDetails = _clientService.GetSite(checklist.ClientId.Value, checklist.SiteId.Value);
            else
            {
                siteDetails.Postcode = "";
                siteDetails.AddressLine1 = "";
            }
            
            var visitDateString = checklist.VisitDate.HasValue ? checklist.VisitDate.Value.ToShortDateString() : null;

            var newAttachmentName = String.Format("Visit Report - {0} - {1} - {2}{3}", siteDetails.AddressLine1, siteDetails.Postcode, visitDateString, savedAttachment.DocType);
            
            Log4NetHelper.Log.Info("Return Attachment");
            return new [] { new AttachmentType() {NewFileName = newAttachmentName, OldFileName = savedAttachment.PhysicalPath } };
        }

        // gets the filePath, name and file type of attachment
        private DocumentConversionStructure GetReportFileStructure(long clientDocumentId)
        {
            Log4NetHelper.Log.Info("Report File structure");

            var doc = _clientDocumentService.GetById(clientDocumentId);
           

            if (doc == null || !doc.DocumentLibraryId.HasValue) return null;

            Log4NetHelper.Log.Info("client service docId: " + doc.DocumentLibraryId.Value);

            var request = new GetDocumentsByIdsRequest() {DocumentIds = new long[] {doc.DocumentLibraryId.Value}};
            
            var document = _documentLibraryService.GetDocumentsByIds(request);

            if (document == null) return null;

            Log4NetHelper.Log.Info("document service docId: " + document.First().PhysicalFilePath);

            return new DocumentConversionStructure()
            {
                PhysicalPath =
                    String.Format("{0}{1}", document.First().PhysicalFilePath, document.First().PhysicalFilename),
                DocType = document.First().Extension
            };
        }

        public static string SenderEmailAddress(string jurisdiction)
        {
            if(string.IsNullOrEmpty(jurisdiction))
            {
                return "business.safety@peninsula-uk.com";
            }

            string senderEmailAddress;
            switch (jurisdiction.ToUpper())
            {
                case "UK":
                    senderEmailAddress = "business.safety@peninsula-uk.com";
                    break;
                case "ROI":
                    senderEmailAddress = "business.safety@peninsula-ie.com";
                    break;
                case "NI":
                    senderEmailAddress = "business.safety@peninsula-ni.com";
                    break;
                default:
                    senderEmailAddress = "business.safety@peninsula-uk.com";
                    break;
            }
            return senderEmailAddress;
        }

        private List<string> GetEmailAddressList(Checklist checklit)
        {
            var emailAddressList = new List<string>();
            
            if (checklit.EmailReportToPerson && !string.IsNullOrEmpty(checklit.EmailAddress.Trim()))
            {
                emailAddressList.Add(checklit.EmailAddress.Trim());
            }

            if (checklit.EmailReportToOthers && checklit.OtherEmails.Any())
            {
                checklit.OtherEmails.ForEach(e =>
                        emailAddressList.Add(e.EmailAddress.Trim())
                    );
            }

            return emailAddressList;
        }
        

        protected virtual RazorEmailResult CreateRazorEmailResult(SendSubmitChecklistEmailViewModel viewModel)
        {
            var email = new MailerController().SubmitChecklist(viewModel);
            return email;
        }
    }
}
