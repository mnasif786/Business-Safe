using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class Checklist : BaseEntity<Guid>
    {
        public const string STATUS_DRAFT = "Draft";
        public const string STATUS_COMPLETED = "Completed";
        public const string STATUS_ASSIGNED = "Assigned";
        public const string STATUS_SUBMITTED = "Submitted";

        public virtual int? ClientId { get; set; }

        /// <summary>
        /// this is the id of a record in Peninsula.dbo.tblSiteAddresses
        /// </summary>
        public virtual int? SiteId { get; set; }

        public virtual IList<ChecklistQuestion> Questions { get; protected set; }
        public virtual IList<ChecklistAnswer> Answers { get; protected set; }
        public virtual string CoveringLetterContent { get; set; }
        public virtual DateTime? VisitDate { get; set; }
        public virtual string VisitBy { get; set; }
        public virtual string VisitType { get; set; }
        public virtual string Status { get; set; }
        public virtual string MainPersonSeenName { get; set; }
        public virtual Employee MainPersonSeen { get; set; }
        public virtual string AreasVisited { get; set; }
        public virtual string AreasNotVisited { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Jurisdiction { get; set; }
        protected IList<ImmediateRiskNotification> _immediateRiskNotifications;
        public virtual IList<ImmediateRiskNotification> ImmediateRiskNotifications
        {
            get
            {
                return _immediateRiskNotifications
                    .Where(x => !x.Deleted)
                    .ToList();
            }
        }

        public virtual ActionPlan ActionPlan { get; set; }
        public virtual ImpressionType ImpressionType { get; set; }
        public virtual string ChecklistCreatedBy { get; set; }
        public virtual DateTime? ChecklistCreatedOn { get; set; }
        public virtual string ChecklistCompletedBy { get; set; }
        public virtual DateTime? ChecklistCompletedOn { get; set; }
        public virtual string ChecklistSubmittedBy { get; set; }
        public virtual DateTime? ChecklistSubmittedOn { get; set; }
        public virtual string Title { get; set; }
        public virtual string ChecklistLastModifiedBy { get; set; }
        public virtual QaAdvisor QaAdvisor { get; set; }
        public virtual ChecklistTemplate ChecklistTemplate { get; set; }
        public virtual string QAComments { get; set; }
        public virtual bool UpdatesRequired { get; set; }

        public virtual bool ExecutiveSummaryUpdateRequired { get; set; }  
        public virtual bool ExecutiveSummaryQACommentsResolved { get; set; }  
        public virtual string ExecutiveSummaryQACommentsSignedOffBy{ get; set; }
        public virtual DateTime? ExecutiveSummaryQACommentsSignedOffDate { get; set; }  

        public virtual bool EmailReportToPerson { get; set; }
        public virtual bool EmailReportToOthers { get; set; }
        public virtual bool PostReport { get; set; }
        public virtual string OtherEmailAddresses { get; set; }
        public virtual long? ExecutiveSummaryDocumentLibraryId { get; set; }
        public virtual SummaryReportHeaderType ReportHeaderType { get; set; }
        public virtual DateTime? QaAdvisorAssignedOn { get; set; }
        public virtual IList<ChecklistUpdatesRequired> UpdatesRequiredLog { get; set; }
        public virtual IList<ChecklistPersonSeen> PersonsSeen { get; set; }
        public virtual IList<ChecklistOtherEmail> OtherEmails { get; set; }

        public virtual bool IncludeActionPlan { get; set; }
        public virtual bool IncludeComplianceReview { get; set; }
        public virtual DateTime? DeletedOn { get; set; }
        public virtual string DeletedBy { get; set; }
        public virtual string ClientLogoFilename { get; set; }
        public virtual string ClientSiteGeneralNotes { get; set; }
        public virtual bool SpecialReport { get; set; }
        
        public Checklist()
        {
            Questions = new List<ChecklistQuestion>();
            Answers = new List<ChecklistAnswer>();
            _immediateRiskNotifications = new List<ImmediateRiskNotification>();
            UpdatesRequiredLog = new List<ChecklistUpdatesRequired>();
            PersonsSeen = new List<ChecklistPersonSeen>();
            OtherEmails = new List<ChecklistOtherEmail>();
        }

        public virtual void UpdateAnswers(IList<ChecklistAnswer> updatedAnswers, UserForAuditing user)
        {
            foreach (var updatedAnswer in updatedAnswers)
            {                

                var checkListAnswer = Answers.FirstOrDefault(a => a.Question.Id == updatedAnswer.Question.Id);
                if (checkListAnswer != null)
                {                   
                    //using this method because we want to reduce the number of unnecessary database updates and audits. Also prevent unanswered questions from updating anwsered questions
                    if (AreAnswersDifferent(checkListAnswer, updatedAnswer) && updatedAnswer.Response != null)
                    {
                        if (updatedAnswer.Response != null)
                        {
                            
                        }
                        checkListAnswer.Response = updatedAnswer.Response;
                        checkListAnswer.SupportingEvidence = updatedAnswer.SupportingEvidence;
                        checkListAnswer.ActionRequired = updatedAnswer.ActionRequired;
                        checkListAnswer.AssignedTo = updatedAnswer.AssignedTo;
                        checkListAnswer.EmployeeNotListed = updatedAnswer.EmployeeNotListed;
                        checkListAnswer.GuidanceNotes = updatedAnswer.GuidanceNotes;
                        checkListAnswer.Timescale = updatedAnswer.Timescale;
                        checkListAnswer.LastModifiedBy = user;
                        checkListAnswer.LastModifiedOn = DateTime.Now;
                        checkListAnswer.QaComments = updatedAnswer.QaComments;
                        checkListAnswer.QaSignedOffBy = updatedAnswer.QaSignedOffBy;
                        checkListAnswer.QaSignedOffDate = updatedAnswer.QaSignedOffDate;
                        checkListAnswer.QaCommentsResolved = updatedAnswer.QaCommentsResolved;
                        checkListAnswer.AreaOfNonCompliance = updatedAnswer.AreaOfNonCompliance;
                        checkListAnswer.SupportingDocumentationStatus = updatedAnswer.SupportingDocumentationStatus;
                        checkListAnswer.SupportingDocumentationDate = updatedAnswer.SupportingDocumentationDate;
                    }
                }
                else
                {
                    updatedAnswer.Id = Guid.NewGuid();
                    updatedAnswer.Checklist = this;
                    updatedAnswer.LastModifiedBy = user;
                    updatedAnswer.LastModifiedOn = DateTime.Now;
                    Answers.Add(updatedAnswer);
                }
            }
        }

        private static bool AreAnswersDifferent(ChecklistAnswer checklistAnswer, ChecklistAnswer updatedAnswer)
        {
            return checklistAnswer.Response != updatedAnswer.Response
                   || checklistAnswer.ActionRequired != updatedAnswer.ActionRequired
                   || checklistAnswer.AssignedTo != updatedAnswer.AssignedTo
                   || checklistAnswer.EmployeeNotListed != updatedAnswer.EmployeeNotListed
                   || checklistAnswer.GuidanceNotes != updatedAnswer.GuidanceNotes
                   || checklistAnswer.SupportingEvidence != updatedAnswer.SupportingEvidence
                   || checklistAnswer.Timescale != updatedAnswer.Timescale
                   || checklistAnswer.QaComments != updatedAnswer.QaComments
                   || checklistAnswer.QaSignedOffBy != updatedAnswer.QaSignedOffBy
                   || checklistAnswer.QaCommentsResolved != updatedAnswer.QaCommentsResolved
                   || IsQaSignedOffDateModified(checklistAnswer.QaSignedOffDate, updatedAnswer.QaSignedOffDate)
                   || checklistAnswer.AreaOfNonCompliance != updatedAnswer.AreaOfNonCompliance
                   || checklistAnswer.SupportingDocumentationStatus != updatedAnswer.SupportingDocumentationStatus
                   || checklistAnswer.SupportingDocumentationDate != updatedAnswer.SupportingDocumentationDate;
        }

        protected virtual void AddImmediateRiskNotifications(IList<AddImmediateRiskNotificationParameters> parameterSets, UserForAuditing user)
        {
            var newImmediateRiskNotifications =
                parameterSets.Where(x => !_immediateRiskNotifications.Select(y => y.Id).Contains(x.Id));

            var missingImmediateRiskNotifications =
                _immediateRiskNotifications.Where(x => !parameterSets.Select(y => y.Id).Contains(x.Id));

            foreach (var parameterSet in newImmediateRiskNotifications)
            {
                var immediateRiskNotification = ImmediateRiskNotification.Create(
                    parameterSet.Id,
                    parameterSet.Reference,
                    parameterSet.Title,
                    parameterSet.SignificantHazardIdentified,
                    parameterSet.RecommendedImmediateAction,
                    this, 
                    user);

                _immediateRiskNotifications.Add(immediateRiskNotification);
            }

            foreach (var immediateRiskNotification in missingImmediateRiskNotifications)
            {
                immediateRiskNotification.MarkForDelete(user);
            }
        }

        public virtual void AddImmediateRiskNotification(ImmediateRiskNotification immediateRiskNotification)
        {
            if(!_immediateRiskNotifications.Any(x=> x.Id == immediateRiskNotification.Id ))
            {
                _immediateRiskNotifications.Add(immediateRiskNotification);
            }
        }

        public static Checklist Create(Guid id)
        {
            var checklist = new Checklist() {Id = id};
            checklist.CreatedOn = DateTime.Now;
            return checklist;
        }

        public static Checklist Create(CreateUpdateChecklistParameters parameters)
        {
            var checklist = new Checklist()
            {
                Id = parameters.Id,
                ChecklistCreatedBy = parameters.CreatedBy,
                ChecklistCreatedOn = parameters.CreatedOn.HasValue ? parameters.CreatedOn.Value.ToLocalTime() : (DateTime?)null
            };

            checklist.SetValues(parameters);
            return checklist;
        }

        public virtual void UpdateChecklistDetails(CreateUpdateChecklistParameters parameters)
        {
            SetValues(parameters);
        }

        protected virtual void SetValues(CreateUpdateChecklistParameters parameters)
        {
            if (parameters.Submit)
            {
                if (ActionPlan != null)
                {
                    throw new SafeCheckChecklistAlreadySubmittedException(this.ActionPlan.Id);
                }
            }

            SiteId = parameters.SiteId;
            ClientId = parameters.ClientId;
            CoveringLetterContent = parameters.CoveringLetterContent;
            VisitBy = parameters.VisitBy;
            VisitType = parameters.VisitType;
            MainPersonSeenName = parameters.MainPersonSeenName;
            MainPersonSeen = parameters.MainPersonSeen;
            AreasVisited = parameters.AreasVisited;
            AreasNotVisited = parameters.AreasNotVisited;
            EmailAddress = parameters.EmailAddress;
            ImpressionType = parameters.ImpressionType;
            EmailReportToPerson = parameters.EmailReportToPerson;
            EmailReportToOthers = parameters.EmailReportToOthers;
            PostReport = parameters.PostReport;
            OtherEmailAddresses = parameters.OtherEmailAddresses;
            Jurisdiction = parameters.Jurisdiction; 

            CreatedBy = parameters.User;
            CreatedOn = DateTime.Now;
            LastModifiedBy = parameters.User;
            LastModifiedOn = parameters.LastModifiedOn.HasValue ? parameters.LastModifiedOn.Value : DateTime.Now;

            ChecklistTemplate = parameters.ChecklistTemplate;
            ChecklistLastModifiedBy = parameters.PostedBy + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            QAComments = parameters.QAComments;
            UpdatesRequired = parameters.UpdatesRequired;
            ReportHeaderType = parameters.ReportHeader;
            IncludeActionPlan = parameters.IncludeActionPlan;
            IncludeComplianceReview = parameters.IncludeComplianceReview;

            ExecutiveSummaryUpdateRequired = parameters.ExecutiveSummaryUpdateRequired;
            ExecutiveSummaryQACommentsResolved = parameters.ExecutiveSummaryQACommentsResolved;
            ExecutiveSummaryQACommentsSignedOffBy = parameters.ExecutiveSummaryQACommentsSignedOffBy;
            ExecutiveSummaryQACommentsSignedOffDate = parameters.ExecutiveSummaryQACommentsSignedOffDate;

            ClientLogoFilename = parameters.ClientLogoFilename;
            ClientSiteGeneralNotes = parameters.ClientSiteGeneralNotes;
            SpecialReport = parameters.SpecialReport;

            switch (parameters.Status)
            {
                case STATUS_DRAFT:
                    if (string.IsNullOrEmpty(Status))
                    {
                        Status = STATUS_DRAFT;
                    }
                    break;
                case STATUS_COMPLETED:
                    CompleteChecklist(parameters.CompletedOn, parameters.CompletedBy);
                    break;
                case STATUS_ASSIGNED:
                    if (string.IsNullOrEmpty(Status) && Status != STATUS_SUBMITTED)
                    {
                        Status = STATUS_ASSIGNED;
                    }
                    break;
                default:
                    Status = parameters.Status;
                    break;
            }


            DateTime _visitdate;
            DateTime? dVisitdate = null;
            bool isValidDate = DateTime.TryParse(parameters.VisitDate, out _visitdate);
            if (isValidDate) dVisitdate = _visitdate;
            VisitDate = dVisitdate;

            AddImmediateRiskNotifications(parameters.ImmediateRiskNotifications, parameters.User);
            Title = string.Format("Visit Report - {0} - {1} - {2:dd/MM/yyyy}", parameters.SiteAddress1, parameters.SiteAddressPostcode, VisitDate);
            if (parameters.Submit)
            {
                if (ActionPlan != null)
                {
                    throw new SafeCheckChecklistAlreadySubmittedException(this.ActionPlan.Id);
                }

                ChecklistSubmittedBy = parameters.SubmittedBy;
                ChecklistSubmittedOn = parameters.SubmittedOn.HasValue ? parameters.SubmittedOn.Value.ToLocalTime() : (DateTime?)null;

                if (!parameters.SpecialReport)
                {
                    ActionPlan = ActionPlan.Create(this, Title, parameters.Site);    
                }
                
            }
        }

        private void CompleteChecklist(DateTime? completedOn,string completedBy)
        {
            if (!ChecklistCompletedOn.HasValue)
            {
                ChecklistCompletedOn = completedOn.HasValue
                   ? completedOn.Value.ToLocalTime()
                   : (DateTime?)null;
                ChecklistCompletedBy = completedBy;
            }

            if (string.IsNullOrEmpty(Status) || Status == STATUS_DRAFT)
            {
                Status = STATUS_COMPLETED;
            }
        }

        public virtual void UpdateQuestion(ChecklistQuestion updatedQuestion, UserForAuditing user)
        {
            var checkListQuestion = Questions.FirstOrDefault(a => a.Question.Id == updatedQuestion.Question.Id);

            if (checkListQuestion != null)
            {
                //using this method because we want to reduce the number of unnecessary database updates and audits
                if (AreQuestionsDifferent(checkListQuestion, updatedQuestion))
                {
                    checkListQuestion.Deleted = false;
                    checkListQuestion.CategoryNumber = updatedQuestion.CategoryNumber;
                    checkListQuestion.QuestionNumber = updatedQuestion.QuestionNumber;
                    checkListQuestion.LastModifiedBy = user;
                    checkListQuestion.LastModifiedOn = DateTime.Now;
                }

            }
            else
            {
                updatedQuestion.Id = Guid.NewGuid();
                updatedQuestion.Checklist = this;
                updatedQuestion.LastModifiedBy = user;
                updatedQuestion.LastModifiedOn = DateTime.Now;
                updatedQuestion.CreatedBy = user;
                updatedQuestion.CreatedOn = DateTime.Now;
                Questions.Add(updatedQuestion);
            }
        }

        public virtual void RemoveQuestion(ChecklistQuestion questionToRemove, UserForAuditing user)
        {
            var checkListQuestion = Questions.FirstOrDefault(a => a.Question.Id == questionToRemove.Question.Id && questionToRemove.Deleted == false);
            if (checkListQuestion != null)
            {
                questionToRemove.MarkForDelete(user);
            }
        }

        private static bool AreQuestionsDifferent(ChecklistQuestion checklistQuestion, ChecklistQuestion updatedChecklistQuestion)
        {
            return checklistQuestion.CategoryNumber != updatedChecklistQuestion.CategoryNumber
                 || checklistQuestion.QuestionNumber != updatedChecklistQuestion.QuestionNumber
                 || checklistQuestion.Deleted != updatedChecklistQuestion.Deleted;
        }

        private static bool IsQaSignedOffDateModified (DateTime? value1 , DateTime? value2)
        {
            if(!value1.HasValue  && !value2.HasValue)
            {
                return false;
            }

            if (value1.HasValue && value2.HasValue && (value1.Value.Date == value2.Value.Date))
            {
                return false;
            }

            return true;
        }

        public virtual void AddPersonSeen(ChecklistPersonSeen personSeen)
        {
            if (IsPersonSeenInPersonsSeenList(personSeen))
            {
                var checklistPersonSeen = GetPersonSeenFromPersonsSeenList(personSeen);
                checklistPersonSeen.FullName = personSeen.FullName;
                checklistPersonSeen.EmailAddress = personSeen.EmailAddress;
                checklistPersonSeen.Employee = personSeen.Employee;
            }
            else
            {
                personSeen.Checklist = this;
                PersonsSeen.Add(personSeen);
            }
        }

        private bool IsPersonSeenInPersonsSeenList(ChecklistPersonSeen personSeen)
        {
            return (personSeen.Employee == null && PersonsSeen.Any(x => x.Id == personSeen.Id))
                   || (personSeen.Employee != null && PersonsSeen.Any(x => x.Employee != null && x.Employee.Id == personSeen.Employee.Id));
        }

        public virtual void RemovePersonSeen(ChecklistPersonSeen personSeen)
        {
            var personSeenFromList = GetPersonSeenFromPersonsSeenList(personSeen);

            if (personSeenFromList != null)
            {
                PersonsSeen.Remove(personSeenFromList);
            }
        }

        public virtual void RemovePersonsSeenNotInList(List<ChecklistPersonSeen> personsSeenNotToRemove)
        {
            //remove from checklist
            var employeeIds = personsSeenNotToRemove
                .Where(x => x.Employee != null && x.Employee.Id != Guid.Empty)
                .Select(x => x.Employee.Id)
                .ToList();

            var idsToKeep = personsSeenNotToRemove
                .Where(x => x.Employee == null)
                .Select(x => x.Id).ToList();

            var employeePersonSeenToRemove = PersonsSeen
                .Where(x => x.Employee != null && x.Employee.Id != Guid.Empty && !employeeIds.Contains(x.Employee.Id))
                .ToList();

            var nonEmployeePersonSeenToRemove = PersonsSeen
                .Where(x => x.Employee == null && !idsToKeep.Contains(x.Id))
                .ToList();

            employeePersonSeenToRemove
                .ForEach(RemovePersonSeen);

            nonEmployeePersonSeenToRemove
               .ForEach(RemovePersonSeen);
        }

        private ChecklistPersonSeen GetPersonSeenFromPersonsSeenList(ChecklistPersonSeen personSeen)
        {
            return PersonsSeen.FirstOrDefault(x =>
                                              (
                                                  (personSeen.Employee != null &&
                                                  x.Employee != null && x.Employee.Id == personSeen.Employee.Id)
                                                  ||
                                                  (personSeen.Employee == null &&
                                                   x.Id == personSeen.Id)
                                              )
                );
        }

        public virtual void MarkForDelete(UserForAuditing user, string username)
        {
            if (!Deleted)
            {
                DeletedBy = username;
                DeletedOn = DateTime.Now;
            }

            base.MarkForDelete(user);
        }

        public virtual void AddOtherEmailAddresses(ChecklistOtherEmail checklistOtherEmail)
        {
            var existingOtherEmail = GetOtherEmailFromOtherEmailList(checklistOtherEmail);

            if (existingOtherEmail != null)
            {
                existingOtherEmail.EmailAddress = checklistOtherEmail.EmailAddress;
                existingOtherEmail.Name = checklistOtherEmail.Name;
            }
            else
            {
                checklistOtherEmail.Checklist = this;
                OtherEmails.Add(checklistOtherEmail);    
            }
        }

        private void RemoveOtherEmail(ChecklistOtherEmail checklistOtherEmail)
        {
            var existingOtherEmail = GetOtherEmailFromOtherEmailList(checklistOtherEmail);

            if (existingOtherEmail != null)
            {
                OtherEmails.Remove(existingOtherEmail);
            }
        }

        private ChecklistOtherEmail GetOtherEmailFromOtherEmailList(ChecklistOtherEmail checklistOtherEmail)
        {
            return OtherEmails.Any(e => e.Id == checklistOtherEmail.Id)
                                   ? OtherEmails.FirstOrDefault(e => e.Id == checklistOtherEmail.Id)
                                   : null;
        }

        public virtual void UpdateOtherEmailsList(List<ChecklistOtherEmail> otherEmailsNotToBeRemoved)
        {
            var otherEmailsNotTobeRemovedIds = otherEmailsNotToBeRemoved.Select(e => e.Id).ToList();
            var otherEmailsToBeRemoved = OtherEmails.Where(e => !otherEmailsNotTobeRemovedIds.Contains(e.Id)).ToList();
            otherEmailsToBeRemoved.ForEach(RemoveOtherEmail);
        }

        private Checklist Create(int siteId, UserForAuditing user, String copiedByUsername)
        {
            var checklist = Create(Guid.NewGuid());
            checklist.CreatedOn = DateTime.Now;
            checklist.CreatedBy = user;
            checklist.ChecklistCreatedBy = copiedByUsername;
            checklist.ChecklistCreatedOn = DateTime.Now;
            checklist.CreatedOn = DateTime.Now;
            checklist.SiteId = siteId;
            checklist.ClientId = ClientId;
            checklist.Status = "Draft";
            checklist.Jurisdiction = Jurisdiction;
            checklist.VisitDate = VisitDate;
            checklist.ChecklistTemplate = ChecklistTemplate;

            return checklist;
        }

        public virtual Checklist CopyToSiteWithoutResponses(int siteId, int clientId, UserForAuditing user, String copiedByUsername, bool isClone)
        {
            Checklist newChecklist = Create(siteId, user, copiedByUsername);
            newChecklist.ClientId = clientId;
            newChecklist.ClientSiteGeneralNotes = isClone == false ? ClientSiteGeneralNotes : null;
            newChecklist.SpecialReport = SpecialReport;

            // Mandatory questions & not deleted
            Questions.Where(q => !q.Question.CustomQuestion && !q.Question.Deleted).ToList()
                .ForEach(x=> newChecklist.UpdateQuestion(new ChecklistQuestion()
                {
                    Question = x.Question, CategoryNumber = x.CategoryNumber, QuestionNumber = x.QuestionNumber,
                    
                }, user));

           
            // Bespoke or non mandatory/custom questions
            Questions.Where(q => q.Question.CustomQuestion && !q.Question.Deleted).ToList()
                .ForEach(x => newChecklist.UpdateQuestion(new ChecklistQuestion()
                {
                    Question = x.Question.Copy(), CategoryNumber = x.CategoryNumber, QuestionNumber = x.QuestionNumber
                }, user));
          
            return newChecklist;
        }


        public virtual Checklist CopyToSiteWithResponses(int siteId, int clientId, UserForAuditing user, string copiedByUsername, bool isClone)
        {
            var newChecklist = Create(siteId, user, copiedByUsername);
            newChecklist.ClientId = clientId;
            newChecklist.ClientSiteGeneralNotes = isClone == false ? ClientSiteGeneralNotes : null;
            newChecklist.SpecialReport = SpecialReport;
            
            var mandatoryQuestionAnswers = new List<ChecklistAnswer>();
            var customQuestionAnswers = new List<ChecklistAnswer>();

            //Mandatory questions & not deleted
            Questions.Where(q => !q.Question.CustomQuestion && !q.Question.Deleted).ToList()
                .ForEach(x =>
                {
                    var answer = Answers.First(a => a.Question.Id == x.Question.Id).Copy();
                    newChecklist.UpdateQuestion(new ChecklistQuestion()
                    {
                        Question = x.Question,
                        CategoryNumber = x.CategoryNumber,
                        QuestionNumber = x.QuestionNumber
                    }, user);

                    mandatoryQuestionAnswers.Add(answer);
                });

            //Bespoke or non mandatory/Custom questions
            Questions.Where(q => q.Question.CustomQuestion && !q.Question.Deleted).ToList()
                .ForEach(x =>
                {
                    var customQuestion = x.Question.Copy();
                    var answer = Answers.First(a => a.Question.Id == x.Question.Id).Copy();
                    answer.UpdateQuestion(customQuestion);
                    
                    newChecklist.UpdateQuestion(new ChecklistQuestion()
                    {
                        Question = customQuestion,
                        CategoryNumber = x.CategoryNumber,
                        QuestionNumber = x.QuestionNumber
                    }, user);

                    customQuestionAnswers.Add(answer);
                });
            
            // IRNs
            ImmediateRiskNotifications
                .Where( i => !i.Deleted)
                .ToList()
                .ForEach( x =>
                              {
                                  var newIRN = x.Copy(user, newChecklist);
                                  
                                  newChecklist.AddImmediateRiskNotification(newIRN);
                              });


            newChecklist.UpdateAnswers(mandatoryQuestionAnswers, user);

            newChecklist.UpdateAnswers(customQuestionAnswers, user);

            // Clear AssignedTo, Target Timescales and Document date
            newChecklist.Answers.ToList().ForEach(x => {    x.AssignedTo = null;
                                                            x.Timescale = null;
                                                            x.SupportingDocumentationDate = null; });
            
            
            

            return newChecklist;
        }

        public virtual bool CanBeReverted()
        {
            return

                (SpecialReport && Status == "Submitted") 
                ||
                (SpecialReport == false && Status == "Submitted" && ActionPlan != null &&  !ActionPlan.HasAnyActionAssigned());
        }

        public virtual void Revert(UserForAuditing user, string postedBy)
        {
            if (CanBeReverted())
            {
                Status = "Assigned";
                ChecklistSubmittedBy = null;
                ChecklistSubmittedOn = null;
                LastModifiedBy = user;
                LastModifiedOn = DateTime.Now;
                ChecklistLastModifiedBy = postedBy + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                ExecutiveSummaryDocumentLibraryId = null;

                //There must be no Action Plan for Special Report. So nothing to be removed.
                if (!SpecialReport)
                {
                    RemoveActionPlan(user);
                }
            }
            
        }

        private void RemoveActionPlan(UserForAuditing user)
        {
            ActionPlan.MarkForDelete(user);
            ActionPlan.Actions.ToList().ForEach(a => a.MarkForDelete(user));
            ActionPlan = null;
        }
    }
}
