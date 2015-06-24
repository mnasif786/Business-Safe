using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class EmployeeChecklistEmail : Entity<Guid>
    {
        public virtual long? EmailPusherId { get; set; }
        public virtual string Message { get; set; }
        public virtual IList<EmployeeChecklist> EmployeeChecklists { get; set; }
        public virtual string RecipientEmail { get; set; }

        public static IList<EmployeeChecklistEmail> Generate(
            IList<EmployeesWithNewEmailsParameters> employeesParameters, 
            IList<Checklist> checklists, 
            string message,
            UserForAuditing generatingUser, 
            PersonalRiskAssessment riskAssessment, 
            bool? sendCompletedChecklistNotificationEmail, 
            DateTime? completionDueDateForChecklists, 
            string completionNotificationEmailAddress,
            IList<ExistingReferenceParameters> existingReferenceParameters)
        {
            var employeeChecklistEmails = new List<EmployeeChecklistEmail>();
            var now = DateTime.Now;

            foreach (var employeeParam in employeesParameters)
            {
                var employee = employeeParam.Employee;

                if (!employee.HasEmail && string.IsNullOrEmpty(employeeParam.NewEmail))
                {
                    throw new Exception("Employee does not have an email and no new NewEmail supplied");
                }

                if (!employee.HasEmail)
                {
                    employee.SetEmail(employeeParam.NewEmail, generatingUser);
                }

                var employeeChecklistEmail = new EmployeeChecklistEmail();
                employeeChecklistEmail.EmployeeChecklists = new List<EmployeeChecklist>();
                long referenceIncremental = 0;

                if (existingReferenceParameters.Any(x => x.Prefix == employee.PrefixForEmployeeChecklists))
                {
                    referenceIncremental =
                        existingReferenceParameters.Single(x => x.Prefix == employee.PrefixForEmployeeChecklists).
                            MaxIncremental;
                }

                foreach (var checklist in checklists)
                {
                    referenceIncremental++;

                    var employeeChecklist = EmployeeChecklist.Generate(
                        employee, 
                        checklist, 
                        now, 
                        generatingUser,
                        riskAssessment,
                        sendCompletedChecklistNotificationEmail,
                        completionDueDateForChecklists,
                        completionNotificationEmailAddress,
                        employeeChecklistEmail,
                        referenceIncremental);

                    employeeChecklistEmail.EmployeeChecklists.Add(employeeChecklist);
                }

                employeeChecklistEmail.Id = Guid.NewGuid();
                employeeChecklistEmail.Message = message;
                employeeChecklistEmail.RecipientEmail = employee.ContactDetails[0].Email;
                employeeChecklistEmail.CreatedBy = generatingUser;
                employeeChecklistEmail.CreatedOn = now;
                employeeChecklistEmails.Add(employeeChecklistEmail);
            }

            riskAssessment.PersonalRiskAssessementEmployeeChecklistStatus = PersonalRiskAssessementEmployeeChecklistStatusEnum.Generated;
            riskAssessment.SetLastModifiedDetails(generatingUser);
            return employeeChecklistEmails;
        }

        public static EmployeeChecklistEmail Generate(EmployeeChecklist employeeChecklist, UserForAuditing generatingUser)
        {
            if (employeeChecklist.EmployeeChecklistEmails.Any() == false)
            {
                throw new AttemptingToResendEmailForEmployeeChecklistThatDoesNotHaveExistingEmailsException(employeeChecklist.Id);
            }
            
            var regeneratingEmail = employeeChecklist.EmployeeChecklistEmails.OrderBy(x => x.CreatedOn).Last();

            var employeeChecklistEmail = new EmployeeChecklistEmail();
            employeeChecklistEmail.Id = Guid.NewGuid();
            employeeChecklistEmail.CreatedBy = generatingUser;
            employeeChecklistEmail.CreatedOn = DateTime.Now;
            employeeChecklistEmail.EmployeeChecklists = new List<EmployeeChecklist> {employeeChecklist};
            employeeChecklistEmail.Message = regeneratingEmail.Message;
            employeeChecklistEmail.RecipientEmail = employeeChecklist.Employee.ContactDetails[0].Email;
            employeeChecklist.EmployeeChecklistEmails.Add(employeeChecklistEmail);
            employeeChecklist.SetLastModifiedDetails(generatingUser);
            return employeeChecklistEmail;
        }
    }
}
