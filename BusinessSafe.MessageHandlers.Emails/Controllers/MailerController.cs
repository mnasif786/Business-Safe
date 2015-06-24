using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;

using ActionMailer.Net;
using ActionMailer.Net.Standalone;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;

namespace BusinessSafe.MessageHandlers.Emails.Controllers
{
    public class MailerController : RazorMailerBase
    {
        private const string DefaultFromAddress = "donotreply@peninsula-online.com";
        const string InternalEmailAddress = "BusinessSafeProject@peninsula-uk.com";

        public override string ViewPath
        {
            get
            {
                var assemblyFile = new FileInfo(Assembly.GetAssembly(typeof(MailerController)).Location);
                var viewFolder = assemblyFile.Directory.GetDirectories("Views")[0];
                var viewPath = viewFolder.FullName;
                Log4NetHelper.Log.Debug(string.Format("MailerController ViewPath : {0}", viewPath));
                return viewPath;
            }
        }

        public RazorEmailResult CompanyDetailsUpdated(SendCompanyDetailsUpdatedEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("SendCompanyDetailsUpdatedEmail", viewModel);
        }

        public RazorEmailResult SiteDetailsUpdated(SendSiteDetailsUpdatedEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;
            
            return Email("SendSiteDetailsUpdatedEmail", viewModel);
        }

        public RazorEmailResult EmployeeChecklistEmailGenerated(EmployeeChecklistEmailGeneratedViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = "BusinessSafe Online";
            From = DefaultFromAddress;

            return Email("EmployeeChecklistEmailGenerated", viewModel);
        }

        public RazorEmailResult EmployeeChecklistCompleted(SendEmployeeChecklistCompletedEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("SendEmployeeChecklistCompletedEmail", viewModel);
        }

        public RazorEmailResult TaskAssigned(TaskAssignedEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("TaskAssignedEmail", viewModel);
        }

        public RazorEmailResult ActionTaskAssigned(TaskAssignedEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("ActionTaskAssignedEmail", viewModel);
        }

        public RazorEmailResult TaskCompleted(TaskCompletedViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("TaskCompletedEmail", viewModel);
        }

        public RazorEmailResult ActionTaskCompleted(ActionTaskCompletedViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("ActionTaskCompletedEmail", viewModel);
        }

        public RazorEmailResult TaskOverdueUser(TaskOverdueViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("TaskOverdueUserEmail", viewModel);
        }

        public RazorEmailResult ActionTaskOverdueUser(TaskOverdueViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("ActionTaskOverdueUserEmail", viewModel);
        }

        public RazorEmailResult TaskOverdueRiskAssessor(TaskOverdueViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("TaskOverdueRiskAssessorEmail", viewModel);
        }

        public RazorEmailResult ReviewOverdueRiskAssessor(ReviewOverdueViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("ReviewOverdueRiskAssessorEmail", viewModel);
        }

        public RazorEmailResult ReviewCompleted(ReviewCompletedEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("ReviewCompletedEmail", viewModel);
        }

        public RazorEmailResult ReviewAssigned(ReviewAssignedEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("ReviewAssignedEmail", viewModel);
        }

        private string GetToAddress(string originalToAddress)
        {
            var sendExternalEmailSwitch = Convert.ToBoolean(ConfigurationManager.AppSettings["SendExternalEmail"]);
            Log4NetHelper.Log.Debug(string.Format("MailerController SendExternalEmailSwitch : {0}", sendExternalEmailSwitch));
            if(sendExternalEmailSwitch)
            {
                return originalToAddress;
            }

            Log4NetHelper.Log.Debug(string.Format("MailerController InternalEmailAddress : {0}", InternalEmailAddress));
            return InternalEmailAddress;
        }

        private List<string> GetToAddressList(List<string> originalToAddresses)
        {
            var sendExternalEmailSwitch = Convert.ToBoolean(ConfigurationManager.AppSettings["SendExternalEmail"]);
            Log4NetHelper.Log.Debug(string.Format("MailerController SendExternalEmailSwitch : {0}", sendExternalEmailSwitch));
            if (sendExternalEmailSwitch)
            {
                return originalToAddresses;
            }

            Log4NetHelper.Log.Debug(string.Format("MailerController InternalEmailAddress : {0}", InternalEmailAddress));
            return new List<string>() {InternalEmailAddress};
        }

        public RazorEmailResult ResponsibilityTaskCompleted(ResponsibilityTaskCompletedViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("ResponsibilityTaskCompletedEmail", viewModel);
        }

        public RazorEmailResult OffWorkReminder(OffWorkReminderViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("OffWorkReminderEmail", viewModel);
        }

        public RazorEmailResult ChecklistAssigned(SendChecklistAssignedEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("SendChecklistAssignedEmail", viewModel);
        }

        public RazorEmailResult UpdateRequired(SendUpdateRequiredEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("SendUpdateRequiredEmail", viewModel);
        }

        public RazorEmailResult SubmitChecklist(SendSubmitChecklistEmailViewModel viewModel)
        {
            var toAddress = GetToAddressList(viewModel.To);
            toAddress.ForEach(e => To.Add(e));
            CC.Add(GetToAddress(viewModel.Cc));
            Subject = viewModel.Subject;
            From = string.IsNullOrEmpty(viewModel.From) ? DefaultFromAddress : viewModel.From;
          
            return Email("SendSubmitChecklistEmail", viewModel);            
        }

        public RazorEmailResult IRNNotification(SendIRNNotificationEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("SendIRNNotificationEmail", viewModel);
        }
       
        public RazorEmailResult CompletedNotification(SafeCheckChecklistCompletedViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("SendSafeCheckChecklistCompletedEmail", viewModel);
        }

        public RazorEmailResult HardCopyToOfficeEmail(SendHardCopyToOfficeEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("SendHardCopyToOfficeEmail", viewModel);
        }

        public RazorEmailResult SendAccidentRecordEmail(AccidentRecordEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("AccidentRecordEmail", viewModel);
        }

        public RazorEmailResult SendTechnicalSupportEmail(SendTechnicalSupportEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = viewModel.From;

            return Email("SendTechnicalSupportEmail", viewModel);
        }

        public RazorEmailResult SendEmployeeDigestEmail(SendEmployeeDigestEmailViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("SendEmployeeDigestEmail", viewModel);
        }

 		public RazorEmailResult SendTaskDueTomorrowEmail(TaskDueTomorrowViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;

            return Email("SendTaskDueTomorrowEmail", viewModel);
        }

        public RazorEmailResult SendVisitRequestedEmail(VisitRequestedViewModel viewModel)
        {
            var toAddress = GetToAddress(viewModel.To);
            To.Add(toAddress);
            Subject = viewModel.Subject;
            From = DefaultFromAddress;
            
            return Email("SendVisitRequestedEmail", viewModel);
        }
    }
}
