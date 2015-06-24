using System.Linq;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Messages.Commands;
using BusinessSafe.Messages.Events;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.CommandHandlers
{
    public class GenerateEmployeeChecklistEmailsHandler : IHandleMessages<GenerateEmployeeChecklistEmails>
    {
        private readonly IEmployeeChecklistEmailService _employeeChecklistEmailService;
        private readonly IBus _bus;

        public GenerateEmployeeChecklistEmailsHandler(IEmployeeChecklistEmailService employeeChecklistEmailService, IBus bus)
        {
            _employeeChecklistEmailService = employeeChecklistEmailService;
            _bus = bus;
        }

        public void Handle(GenerateEmployeeChecklistEmails message)
        {
            var generateEmployeeChecklistEmailRequest = new GenerateEmployeeChecklistEmailRequest
                                                            {
                                                                RequestEmployees =
                                                                    message.RequestEmployees.Select(
                                                                        x => new EmployeeWithNewEmailRequest
                                                                                 {
                                                                                     EmployeeId = x.EmployeeId,
                                                                                     NewEmail = x.NewEmail
                                                                                 }).ToList(),
                                                                ChecklistIds = message.ChecklistIds,
                                                                GeneratingUserId = message.GeneratingUserId,
                                                                Message = message.Message,
                                                                RiskAssessmentId = message.RiskAssessmentId,
                                                                SendCompletedChecklistNotificationEmail = message.SendCompletedChecklistNotificationEmail,
                                                                CompletionDueDateForChecklists = message.CompletionDueDateForChecklists,
                                                                CompletionNotificationEmailAddress = message.CompletionNotificationEmailAddress
                                                            };

            var employeeChecklistEmailIds = _employeeChecklistEmailService.Generate(generateEmployeeChecklistEmailRequest);
            var employeeChecklistEmails = _employeeChecklistEmailService.GetByIds(employeeChecklistEmailIds);

            foreach(var employeeChecklistEmail in employeeChecklistEmails)
            {
                var employeeChecklistEmailGenerated = new EmployeeChecklistEmailGenerated
                                                          {
                                                              EmployeeChecklistEmailId = employeeChecklistEmail.Id,
                                                              RecipientEmail = employeeChecklistEmail.RecipientEmail,
                                                              EmployeeChecklistIds =
                                                                  employeeChecklistEmail.EmployeeChecklists.Select(
                                                                      x => x.Id).ToList(),
                                                              Message = employeeChecklistEmail.Message,
                                                              CompletionDueDateForChecklists = message.CompletionDueDateForChecklists
                                                          };

                _bus.Publish(employeeChecklistEmailGenerated);
            }

            Log4NetHelper.Log.Info("GenerateEmployeeChecklistEmails Command Handled");
        }
    }
}
