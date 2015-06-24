using System;
using System.Globalization;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Events;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.Emails.EventHandlers
{
    public class ReviewAssignedHandler : IHandleMessages<ReviewAssigned>
    {
        private readonly ITasksRepository _taskRepository;
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;
        private readonly IUserRepository _userRepository;

        public ReviewAssignedHandler(
            ITasksRepository taskRepository,
            IEmailSender emailSender,
            IBusinessSafeEmailLinkBaseUrlConfiguration businessSafeEmailLinkBaseUrlConfiguration,
            IUserRepository userRepository)
        {
            _emailSender = emailSender;
            _businessSafeEmailLinkBaseUrlConfiguration = businessSafeEmailLinkBaseUrlConfiguration;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
        }

        public void Handle(ReviewAssigned reviewAssigned)
        {
            RiskAssessmentReviewTask reviewTask = null;

            try
            {            
                reviewTask = (RiskAssessmentReviewTask)_taskRepository.GetByTaskGuid(reviewAssigned.TaskGuid);
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Error("Exception in ReviewAssignedHandler - Could Not Retrieve Review Task ", ex);
            }

            if (reviewTask != null &&
                reviewTask.TaskAssignedTo.ContactDetails != null
                && reviewTask.TaskAssignedTo.ContactDetails.Any()
                && !string.IsNullOrEmpty(reviewTask.TaskAssignedTo.ContactDetails[0].Email))
            {
                var viewModel = new ReviewAssignedEmailViewModel();
                viewModel.Subject = "New Risk Assessment Review";
                viewModel.From = "BusinessSafeProject@peninsula-uk.com";
                viewModel.To = reviewTask.TaskAssignedTo.ContactDetails[0].Email;
                viewModel.TaskTitle = reviewTask.RiskAssessmentReview.RiskAssessment.Title;
                viewModel.TaskReference = reviewTask.RiskAssessmentReview.RiskAssessment.Reference;
                viewModel.TaskDescription = reviewTask.Description;

                if (reviewTask.LastModifiedBy != null)
                {
                    var user = _userRepository.GetById(reviewTask.LastModifiedBy.Id);

                    if (user.Employee != null)
                    {
                        viewModel.AssignedBy = user.Employee.FullName;
                    }
                }
                else
                {
                    var user = _userRepository.GetById(reviewTask.CreatedBy.Id);
                    if (user.Employee != null)
                    {
                        viewModel.AssignedBy = user.Employee.FullName;
                    }
                }

                viewModel.TaskListUrl = _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl();

                viewModel.CompletionDueDate = reviewTask.TaskCompletionDueDate.HasValue
                                                  ? reviewTask.TaskCompletionDueDate.Value.ToString("d",
                                                                                                    new CultureInfo(
                                                                                                        "en-GB"))
                                                  : null;

                SendEmail(viewModel);
            }

            Log4NetHelper.Log.Info("ReviewAssigned Command Handled");
        }

        public virtual void SendEmail(ReviewAssignedEmailViewModel viewModel)
        {
            SendReviewAssignedEmailHelper.SendReviewAssignedEmail(viewModel, _emailSender);
        }
    }
}
