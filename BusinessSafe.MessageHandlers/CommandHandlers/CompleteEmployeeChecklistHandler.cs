using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Messages.Commands;
using BusinessSafe.Messages.Events;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.CommandHandlers
{
    public class CompleteEmployeeChecklistHandler :IHandleMessages<CompleteEmployeeChecklist>
    {
       private readonly IEmployeeChecklistService _employeeChecklistService;
        private readonly IBus _bus;

        public CompleteEmployeeChecklistHandler(IEmployeeChecklistService employeeChecklistService, IBus bus)
        {
            _employeeChecklistService = employeeChecklistService;
            _bus = bus;
        }

        public void Handle(CompleteEmployeeChecklist message)
        {
            Log4NetHelper.Log.Info("CompleteEmployeeChecklist Command Handled Start");

            var request = CreateCompleteEmployeeChecklistRequest(message);

            var validationMessage = _employeeChecklistService.ValidateComplete(request);
            if(validationMessage.Count == 0)
            {
                _employeeChecklistService.Complete(request);
                _bus.Publish(new EmployeeChecklistCompleted { CompletedDate = message.CompletedDate, EmployeeChecklistId = message.EmployeeChecklistId });    
            }
            
            Log4NetHelper.Log.Info("CompleteEmployeeChecklist Command Handled End");
        }

        private static CompleteEmployeeChecklistRequest CreateCompleteEmployeeChecklistRequest(CompleteEmployeeChecklist request)
        {
            var completeCommand = new CompleteEmployeeChecklistRequest
            {
                Answers =
                    request.Answers.Select(
                        a =>
                        new SubmitAnswerRequest
                        {
                            AdditionalInfo = a.AdditionalInfo,
                            BooleanResponse = a.BooleanResponse,
                            QuestionId = a.QuestionId
                        }).ToList()
                ,
                CompletedOnBehalf = request.CompletedOnBehalf,
                CompletedOnEmployeesBehalfBy = request.CompletedOnEmployeesBehalfBy,
                EmployeeChecklistId = request.EmployeeChecklistId,
                CompletedDate = request.CompletedDate
            };
            return completeCommand;
        }
    }
}
