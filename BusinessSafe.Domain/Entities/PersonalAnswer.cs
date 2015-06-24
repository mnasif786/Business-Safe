using System;

namespace BusinessSafe.Domain.Entities
{
    public class PersonalAnswer : Answer
    {
        public virtual bool? BooleanResponse { get; set; }
        public virtual EmployeeChecklist EmployeeChecklist { get; set; }

        public static PersonalAnswer Create(EmployeeChecklist employeeChecklist, Question question, bool? booleanResponse, string additionalInfo, UserForAuditing getNsbSystemUser)
        {
            return new PersonalAnswer
                       {
                           EmployeeChecklist = employeeChecklist,
                           Question = question,
                           BooleanResponse = booleanResponse,
                           AdditionalInfo = additionalInfo,
                           CreatedOn = DateTime.Now,
                           CreatedBy = getNsbSystemUser
                       };
        }

        public virtual void Update(bool? booleanResponse, string additionalInfo, UserForAuditing submittingUser)
        {
            BooleanResponse = booleanResponse;
            AdditionalInfo = additionalInfo;
            SetLastModifiedDetails(submittingUser);
        }
    }
}
