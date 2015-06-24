using System.Linq;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Messages.Commands;
using BusinessSafe.Messages.Events;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.CommandHandlers
{
    public class ResendEmployeeChecklistEmailHandler : IHandleMessages<ResendEmployeeChecklistEmail>
    {
        private readonly IEmployeeChecklistEmailService _employeeChecklistEmailService;
        private readonly IBus _bus;

        public ResendEmployeeChecklistEmailHandler(IEmployeeChecklistEmailService employeeChecklistEmailService, IBus bus)
        {
            _employeeChecklistEmailService = employeeChecklistEmailService;
            _bus = bus;
        }

        public void Handle(ResendEmployeeChecklistEmail message)
        {
            var regeneratedEmployeeChecklistEmail = _employeeChecklistEmailService.Regenerate(new ResendEmployeeChecklistEmailRequest()
                                                    {
                                                        EmployeeChecklistId = message.EmployeeChecklistId,
                                                        ResendUserId = message.ResendUserId,
                                                        RiskAssessmentId = message.RiskAssessmentId
                                                    });

            var employeeChecklistEmailGenerated = new EmployeeChecklistEmailGenerated
                                                      {
                                                          EmployeeChecklistEmailId = regeneratedEmployeeChecklistEmail.Id,
                                                          RecipientEmail = regeneratedEmployeeChecklistEmail.RecipientEmail,
                                                          EmployeeChecklistIds = regeneratedEmployeeChecklistEmail.EmployeeChecklists.Select(x => x.Id).ToList(),
                                                          Message = regeneratedEmployeeChecklistEmail.Message
                                                      };

            _bus.Publish(employeeChecklistEmailGenerated);
            

            Log4NetHelper.Log.Info("ResendEmployeeChecklistEmail Command Handled");
        }

    }
}
