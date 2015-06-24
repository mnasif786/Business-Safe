using System;
using System.Globalization;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.Messages.Events;
using NServiceBus;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.MessageHandlers.Emails.ViewModels;

namespace BusinessSafe.MessageHandlers.Emails.EventHandlers
{
    public class TaskAssignedHandler : IHandleMessages<TaskAssigned>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBusinessSafeEmailLinkBaseUrlConfiguration _businessSafeEmailLinkBaseUrlConfiguration;
        private readonly ITasksRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public TaskAssignedHandler(
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

        public void Handle(TaskAssigned taskAssigned)
        {
            try
            {
                var task = _taskRepository.GetByTaskGuid(taskAssigned.TaskGuid);

                var isActionTask = IsActionTask(task);

                bool isSendingNotifactionEmailRequired = IsSendingNotifactionEmailRequired(task);

                if (isSendingNotifactionEmailRequired)
                {
                    var viewModel = new TaskAssignedEmailViewModel();
                    viewModel.Subject = "New Risk Assessment Task";
                    viewModel.From = "BusinessSafeProject@peninsula-uk.com";
                    viewModel.To = task.TaskAssignedTo.ContactDetails[0].Email;
                    viewModel.TaskTitle = task.Title;
                    viewModel.TaskReference = task.Reference;
                    viewModel.TaskDescription = task.Description;

                    if ((task as FurtherControlMeasureTask) != null)
                    {
                        viewModel.EmailTitle = "New Risk Assessment Task";
                        viewModel.Subject = "New Risk Assessment Task";
                        viewModel.TaskType = "Further Control Measure Task";
                        viewModel.IncludeReference = true;
                    }
                    else if ((task as ResponsibilityTask) != null)
                    {
                        viewModel.EmailTitle = "New Responsibility Task";
                        viewModel.Subject = "New Responsibility Task";
                        viewModel.TaskType = "Responsibility Task";
                        viewModel.IncludeReference = false;
                    }
                    else if (isActionTask)
                    {
                        viewModel.IsIRN = ((ActionTask) task).Action.Category ==
                                          ActionCategory.ImmediateRiskNotification;

                        viewModel.EmailTitle = viewModel.IsIRN ? "New Immediate Action Task" : "New Action Task";
                        viewModel.Subject = viewModel.IsIRN ? "New Immediate Action Task" : "New Action Task";
                        viewModel.TaskType = viewModel.IsIRN ? "Immediate Action Task " : "Action Task";
                        viewModel.ReferenceTagTitle = viewModel.IsIRN ? "Reference" : "Number";
                        viewModel.ActionRequiredTagTitle = viewModel.IsIRN
                                                               ? "Immediate Action Required"
                                                               : "Action Required";
                        viewModel.IncludeReference = true;
                        viewModel.ActionRequired = ((ActionTask) task).Action.ActionRequired;
                    }
                    else
                    {
                        viewModel.EmailTitle = "";
                        viewModel.TaskType = "";
                    }

                    if (task.LastModifiedBy != null)
                    {
                        var user = _userRepository.GetById(task.LastModifiedBy.Id);

                        if (user.Employee != null)
                        {
                            viewModel.AssignedBy = user.Employee.FullName;
                        }
                    }
                    else
                    {
                        var user = _userRepository.GetById(task.CreatedBy.Id);
                        if (user.Employee != null)
                        {
                            viewModel.AssignedBy = user.Employee.FullName;
                        }
                    }

                    viewModel.TaskListUrl = _businessSafeEmailLinkBaseUrlConfiguration.GetBaseUrl();


                    viewModel.CompletionDueDate = task.TaskCompletionDueDate.HasValue
                                                      ? task.TaskCompletionDueDate.Value.ToString("d",
                                                                                                  new CultureInfo(
                                                                                                      "en-GB"))
                                                      : null;

                    SendTaskAssignedEmailHelper.SendTaskAssignedEmail(viewModel, _emailSender, isActionTask);

                    Log4NetHelper.Log.Info("TaskAssigned Command Handled");
                }

            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Error("Exception in TaskAssignedHandler - ", ex);
            }
        }
        
        private bool IsSendingNotifactionEmailRequired(Task task)
        {
            if (task.SendTaskNotification.GetValueOrDefault() == false)
            {
                return false;
            }

            var taskAssignedEmailAddress = GetTaskAssignedEmailAddress(task);

            return string.IsNullOrEmpty(taskAssignedEmailAddress) == false;
        }

        private string GetTaskAssignedEmailAddress(Task task)
        {
            var result = string.Empty;

            if (task.TaskAssignedTo == null || task.TaskAssignedTo.ContactDetails == null)
            {
                return result;
            }

            var contactDetail = task.TaskAssignedTo.ContactDetails.FirstOrDefault();

            if (contactDetail == null)
            {
                return result;
            }

            return contactDetail.Email;
        }

        private bool IsActionTask(Task task)
        {
            return ((task as ActionTask) != null);
        }

    }
}
