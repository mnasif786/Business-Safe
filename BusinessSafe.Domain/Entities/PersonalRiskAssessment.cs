using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class PersonalRiskAssessment : MultiHazardRiskAssessment
    {
        public virtual bool Sensitive { get; set; }
        public virtual bool? HasMultipleChecklistRecipients { get; set; }
        public virtual IList<ChecklistGeneratorEmployee> ChecklistGeneratorEmployees { get; set; }
        public virtual IList<PersonalRiskAssessmentChecklist> Checklists { get; set; }
        public virtual IList<EmployeeChecklist> EmployeeChecklists { get; set; }
        public virtual string ChecklistGeneratorMessage { get; set; }
        public virtual bool? SendCompletedChecklistNotificationEmail { get; set; }
        public virtual DateTime? CompletionDueDateForChecklists { get; set; }
        public virtual string CompletionNotificationEmailAddress { get; set; }
        public virtual PersonalRiskAssessementEmployeeChecklistStatusEnum PersonalRiskAssessementEmployeeChecklistStatus { get; set; }

        public PersonalRiskAssessment()
        {
            EmployeeChecklists = new List<EmployeeChecklist>();
        }

        public static PersonalRiskAssessment Create(string title, string reference, long clientId,
                                                    UserForAuditing currentUser)
        {
            return new PersonalRiskAssessment
                       {
                           CompanyId = clientId,
                           Reference = reference,
                           Title = title,
                           CreatedBy = currentUser,
                           CreatedOn = DateTime.Now,
                           Status = RiskAssessmentStatus.Draft
                           ,
                           PersonalRiskAssessementEmployeeChecklistStatus =
                               PersonalRiskAssessementEmployeeChecklistStatusEnum.NotSet
                       };
        }

        public static PersonalRiskAssessment Create(
            string title,
            string reference,
            long clientId,
            UserForAuditing currentUser,
            string location,
            string taskProcessDescription,
            Site site,
            DateTime? assessmentDate,
            RiskAssessor riskAssessor,
            bool IsSensitive
            )
        {
            var riskAssessment = new PersonalRiskAssessment
                                     {
                                         Title = title,
                                         Reference = reference,
                                         AssessmentDate = assessmentDate,
                                         RiskAssessmentSite = site,
                                         RiskAssessor = riskAssessor,
                                         Location = location,
                                         TaskProcessDescription = taskProcessDescription,
                                         CompanyId = clientId,
                                         CreatedBy = currentUser,
                                         CreatedOn = DateTime.Now,
                                         Status = RiskAssessmentStatus.Draft,
                                         Sensitive = IsSensitive
                                     };

            return riskAssessment;
        }

        public virtual void UpdateSummary(string title, string reference, DateTime? assessmentDate,
                                          RiskAssessor riskAssessor, bool sensitive, Site site,
                                          UserForAuditing currentUser)
        {
            if (IsDifferentRiskAssessor(riskAssessor))
            {
                if (AreThereAnyFurtherControlMeasureTasks())
                {
                    Hazards
                        .Where(h => h.FurtherControlMeasureTasks != null)
                        .SelectMany(h => h.FurtherControlMeasureTasks)
                        .ToList()
                        .ForEach(task =>
                                     {
                                         task.SendTaskCompletedNotification = riskAssessor == null
                                                                                  ? true
                                                                                  : !riskAssessor.
                                                                                         DoNotSendTaskCompletedNotifications;
                                         task.SendTaskOverdueNotification = riskAssessor == null
                                                                                ? true
                                                                                : !riskAssessor.
                                                                                       DoNotSendTaskOverdueNotifications;
                                         task.SetLastModifiedDetails(currentUser);
                                     });
                }
            }

            Title = title;
            Reference = reference;
            Sensitive = sensitive;
            RiskAssessor = riskAssessor;
            AssessmentDate = assessmentDate;
            RiskAssessmentSite = site;
            SetLastModifiedDetails(currentUser);
        }

        public override string PreFix
        {
            get { return "PRA"; }
        }

        public virtual void AddEmployeesToChecklistGenerator(IList<Employee> employees, UserForAuditing currentUser)
        {
            foreach (
                var employee in employees.Where(x => !ChecklistGeneratorEmployees.Select(y => y.Employee).Contains(x)))
            {
                ChecklistGeneratorEmployees.Add(new ChecklistGeneratorEmployee
                                                    {
                                                        Employee = employee,
                                                        PersonalRiskAssessment = this,
                                                        CreatedBy = currentUser,
                                                        CreatedOn = DateTime.Now,
                                                        Deleted = false
                                                    });
            }

            SetLastModifiedDetails(currentUser);
        }

        public virtual void SaveChecklistGenerator(
            bool? hasMultipleChecklistRecipients,
            IList<EmployeesWithNewEmailsParameters> employeesParameters,
            IList<Checklist> checklists,
            string message,
            UserForAuditing currentUser,
            bool? sendCompletedChecklistNotificationEmail,
            DateTime? completionDueDateForChecklists,
            string completionNotificationEmailAddress)
        {
            HasMultipleChecklistRecipients = hasMultipleChecklistRecipients;

            foreach (var employeeParam in employeesParameters)
            {
                var employee = employeeParam.Employee;

                if (!employee.HasEmail)
                {
                    employee.SetEmail(employeeParam.NewEmail, currentUser);
                }
            }

            //Add any new employees.
            foreach (var employee in employeesParameters.Select(x => x.Employee))
            {
                if (!ChecklistGeneratorEmployees.Select(x => x.Employee).Contains(employee))
                {
                    ChecklistGeneratorEmployees.Add(new ChecklistGeneratorEmployee
                                                        {
                                                            Employee = employee,
                                                            PersonalRiskAssessment = this,
                                                            CreatedBy = currentUser,
                                                            CreatedOn = DateTime.Now,
                                                            Deleted = false
                                                        });
                }
            }

            //Remove any employees no longer referenced.
            foreach (var checklistGeneratorEmployee in ChecklistGeneratorEmployees)
            {
                if (!employeesParameters.Select(x => x.Employee).Contains(checklistGeneratorEmployee.Employee))
                {
                    checklistGeneratorEmployee.Deleted = true;
                    checklistGeneratorEmployee.LastModifiedBy = currentUser;
                    checklistGeneratorEmployee.LastModifiedOn = DateTime.Now;
                }
            }

            //Add any new checklists
            foreach (var checklist in checklists)
            {
                if (!Checklists.Select(x => x.Checklist).Contains(checklist))
                {
                    Checklists.Add(new PersonalRiskAssessmentChecklist
                                       {
                                           Checklist = checklist,
                                           PersonalRiskAssessment = this,
                                           CreatedBy = currentUser,
                                           CreatedOn = DateTime.Now,
                                           Deleted = false
                                       });
                }
            }

            //Remove any checklists no longer referenced.
            foreach (var personalRiskAssessment in Checklists)
            {
                if (!checklists.Contains(personalRiskAssessment.Checklist))
                {
                    personalRiskAssessment.Deleted = true;
                    personalRiskAssessment.LastModifiedBy = currentUser;
                    personalRiskAssessment.LastModifiedOn = DateTime.Now;
                }
            }

            ChecklistGeneratorMessage = message;
            SendCompletedChecklistNotificationEmail = sendCompletedChecklistNotificationEmail;
            CompletionDueDateForChecklists = completionDueDateForChecklists;
            CompletionNotificationEmailAddress = completionNotificationEmailAddress;
            SetLastModifiedDetails(currentUser);
        }

        public virtual void ResetAfterGeneratingEmployeeChecklists(UserForAuditing user)
        {
            HasMultipleChecklistRecipients = null;
            SendCompletedChecklistNotificationEmail = null;
            CompletionDueDateForChecklists = null;
            CompletionNotificationEmailAddress = null;
            ChecklistGeneratorMessage = null;

            foreach (var checklistGeneratorEmployee in ChecklistGeneratorEmployees)
            {
                checklistGeneratorEmployee.MarkForDelete(user);
            }

            foreach (var checklist in Checklists)
            {
                checklist.MarkForDelete(user);
            }

            SetLastModifiedDetails(user);
        }

        public virtual Boolean CanUserAccess(Guid currentUserId)
        {
            return (
                       IsNotSensitive() ||
                       WasCreatedByCurrentUser(currentUserId) ||
                       CurrentUserIsAssignedRiskAssessor(currentUserId) ||
                       CurrentReviewIsAssignedToCurrentUser(currentUserId)
                   );
        }

        private bool CurrentReviewIsAssignedToCurrentUser(Guid currentUserId)
        {
            var latestReview = Reviews.OrderByDescending(x => x.CreatedOn).FirstOrDefault(z => z.Deleted == false);

            if (latestReview == null)
                return false;

            return latestReview.ReviewAssignedTo.User != null && latestReview.ReviewAssignedTo.User.Id == currentUserId;
        }

        private bool CurrentUserIsAssignedRiskAssessor(Guid currentUserId)
        {
            return (RiskAssessor != null && RiskAssessor.Employee != null && RiskAssessor.Employee.User != null &&
                    RiskAssessor.Employee.User.Id == currentUserId);
        }

        private bool WasCreatedByCurrentUser(Guid currentUserId)
        {
            return (CreatedBy != null && CreatedBy.Id == currentUserId);
        }

        private bool IsNotSensitive()
        {
            return Sensitive == false;
        }

        public virtual void AddChecklist(EmployeeChecklist checklist, UserForAuditing user)
        {
            checklist.PersonalRiskAssessment.EmployeeChecklists.Remove(checklist);
            checklist.PersonalRiskAssessment = this;
            checklist.PersonalRiskAssessment.PersonalRiskAssessementEmployeeChecklistStatus =
                PersonalRiskAssessementEmployeeChecklistStatusEnum.Generated;
            EmployeeChecklists.Add(checklist);
            checklist.SetLastModifiedDetails(user);
            SetLastModifiedDetails(user);
        }

        public virtual void RemoveCheckListGeneratorForEmployee(Guid employeeId, UserForAuditing user)
        {
            var checkList = ChecklistGeneratorEmployees.FirstOrDefault(x => x.Employee.Id == employeeId);
            if (checkList != null)
            {
                checkList.MarkForDelete(user);
                SetLastModifiedDetails(user);
            }
        }

        public override RiskAssessment Copy(string newTitle, string newReference, UserForAuditing user)
        {
            throw new NotImplementedException();
        }

        public override bool HasAnyReviews()
        {
            if (Reviews.Any(x => x.Deleted == false))
            {
                return true;
            }
            return false;
        }

        public override IEnumerable<FurtherControlMeasureTask> GetAllUncompleteFurtherControlMeasureTasks()
        {
            var furtherControlMeasureTasks = new List<FurtherControlMeasureTask>();
            foreach (var hazard in Hazards.Where(x => x.Deleted == false))
            {
                foreach (MultiHazardRiskAssessmentFurtherControlMeasureTask task in hazard.FurtherControlMeasureTasks.Where(x => x.TaskStatus != TaskStatus.Completed && x.Deleted == false))
                {
                    furtherControlMeasureTasks.Add(task);
                }
            }

            return furtherControlMeasureTasks;
        }
    }
}