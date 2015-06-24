using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using ActionMailer.Net.Standalone;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.Controllers;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Events;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.EventHandlers
{
    public class EmployeeChecklistCompletedHandler : IHandleMessages<EmployeeChecklistCompleted>
    {
        private readonly IEmployeeChecklistRepository _employeeChecklistRepository;
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;

        public EmployeeChecklistCompletedHandler(IEmployeeChecklistRepository employeeChecklistRepository,
            IEmailSender emailSender,
            IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration)
        {
            _employeeChecklistRepository = employeeChecklistRepository;
            _emailSender = emailSender;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
        }

       
        public void Handle(EmployeeChecklistCompleted message)
        {
            try
            {
                Log4NetHelper.Log.Info("SendEmployeeChecklistCompletedEmail Command Started");

                var employeeChecklist = _employeeChecklistRepository.GetById(message.EmployeeChecklistId);
                if (employeeChecklist == null)
                {
                    throw new NullReferenceException("Employee Checklist Not Found");
                }


                var emailLink = _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl() +
                                "/PersonalRiskAssessments/ChecklistManager?riskAssessmentId=" +
                                employeeChecklist.PersonalRiskAssessment.Id.ToString() +
                                "&CompanyId=" + employeeChecklist.PersonalRiskAssessment.CompanyId.ToString();

                Log4NetHelper.Log.Debug(string.Format("Email link is {0}", emailLink));

                var employeeWhoCompleted = employeeChecklist.Employee.FullName;
                Log4NetHelper.Log.Debug(string.Format("Employee Who Completed is {0}", employeeWhoCompleted));

                var checklistName = employeeChecklist.Checklist.Title;
                Log4NetHelper.Log.Debug(string.Format("Checklist Name is {0}", checklistName));

                var viewModel = new SendEmployeeChecklistCompletedEmailViewModel
                {
                    From = "BusinessSafeProject@peninsula-uk.com",
                    Subject = "BusinessSafeOnline Completed Checklist",
                    To = employeeChecklist.CompletionNotificationEmailAddress,
                    ChecklistName = checklistName,
                    EmployeeWhoCompleted = employeeWhoCompleted,
                    CompletedOn = message.CompletedDate.ToString("dd/MM/yyyy HH:mm:ss"),
                    BusinessSafeOnlineLink = new HtmlString(emailLink)
                };

                var email = CreateRazorEmailResult(viewModel);
                Log4NetHelper.Log.Debug("Created Razor Email Result");

                _emailSender.Send(email);
                Log4NetHelper.Log.Info("SendEmployeeChecklistCompletedEmail Command Handled");
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Debug("Exception in EmployeeChecklistCompletedHandler - ", ex);
            }            
        }

        protected virtual RazorEmailResult CreateRazorEmailResult(SendEmployeeChecklistCompletedEmailViewModel viewModel)
        {
            var email = new MailerController().EmployeeChecklistCompleted(viewModel);
            return email;
        }

      
    }
}
