using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class EmployeeChecklist : Entity<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual Checklist Checklist { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? CompletedDate { get; set; }
        public virtual DateTime? DueDateForCompletion { get; set; }
        public virtual string Password { get; set; }
        public virtual string CompletionNotificationEmailAddress { get; set; }
        public virtual bool? SendCompletedChecklistNotificationEmail { get; set; }
        public virtual IList<PersonalAnswer> Answers { get; set; }
        public virtual IList<EmployeeChecklistEmail> EmployeeChecklistEmails { get; set; }
        public virtual PersonalRiskAssessment PersonalRiskAssessment { get; set; }
        public virtual string ReferencePrefix { get; set; }
        public virtual long? ReferenceIncremental { get; set; }
        public virtual User CompletedOnEmployeesBehalfBy { get; set; }
        public virtual bool? IsFurtherActionRequired { get; set; }
        public virtual EmployeeForAuditing AssessedByEmployee { get; set; }
        public virtual DateTime? AssessmentDate { get; set; }

        public EmployeeChecklist()
        {
            Answers = new List<PersonalAnswer>();
        }

        public virtual string FriendlyReference
        {
            get
            {
                if (ReferencePrefix == null || !ReferenceIncremental.HasValue)
                {
                    return null;
                }

                return String.Format("{0}{1:0000}", ReferencePrefix, ReferenceIncremental);
            }
        }

        public virtual string LastRecipientEmail
        {
            get
            {
                if (EmployeeChecklistEmails == null || !EmployeeChecklistEmails.Any())
                {
                    return null;
                }

                return EmployeeChecklistEmails
                    .OrderBy(x => x.CreatedOn)
                    .Last()
                    .RecipientEmail;
            }
        }

        public virtual string LastMessage
        {
            get
            {
                if (EmployeeChecklistEmails == null || !EmployeeChecklistEmails.Any())
                {
                    return null;
                }

                return EmployeeChecklistEmails
                    .OrderBy(x => x.CreatedOn)
                    .Last()
                    .Message;
            }
        }

        public virtual void Submit(IList<SubmitPersonalAnswerParameters> answerParameterClasses, UserForAuditing submittingUser)
        {
            SetAnswers(answerParameterClasses, submittingUser);
            SetLastModifiedDetails(submittingUser);
        }

        public virtual void Complete(IList<SubmitPersonalAnswerParameters> answerParameterClasses, User completedOnEmployeesBehalfBy, UserForAuditing submittingUser, DateTime completedDate)
        {
            SetAnswers(answerParameterClasses, submittingUser);

            if(CompletedDate.HasValue)
            {
                throw new Exception("EmployeeChecklist was previously completed.");
            }

            if (AreAllQuestionsAnswered())
            {
                CompletedDate = completedDate;
            }
            else
            {
                throw new Exception("The questions have been not been answered");
            }

            CompletedOnEmployeesBehalfBy = completedOnEmployeesBehalfBy;
            SetLastModifiedDetails(submittingUser);
        }

        public virtual ValidationMessageCollection ValidateComplete()
        {
            var messages = new ValidationMessageCollection();

            if(CompletedDate.HasValue)
            {
                messages.AddError("This checklist has already been completed once and cannot be resubmitted.");
            }

            //PTD: Don't do this here for now but may do at some point. Will require IList<SubmitAnswerParameters> answerParameterClasses as a parameter.
            //if(!AreAllQuestionsAnswered())
            //{
            //    messages.AddError("Not all questions have been answered.");
            //}

            return messages;
        }

        private void SetAnswers(IList<SubmitPersonalAnswerParameters> answerParameterClasses, UserForAuditing submittingUser)
        {
            foreach (var answerParameters in answerParameterClasses)
            {
                if (Answers.Any(x => x.Question.Id == answerParameters.Question.Id))
                {
                    var answer = Answers.Single(x => x.Question.Id == answerParameters.Question.Id);
                    answer.Update(answerParameters.BooleanResponse, answerParameters.AdditionalInfo, submittingUser);
                }
                else
                {
                    var answer = PersonalAnswer.Create(this, answerParameters.Question, answerParameters.BooleanResponse,
                                               answerParameters.AdditionalInfo, submittingUser);

                    Answers.Add(answer);
                }
            }
        }

        public static EmployeeChecklist Generate(
            Employee employee, 
            Checklist checklist, 
            DateTime createdOn,
            UserForAuditing generatingUser, 
            PersonalRiskAssessment riskAssessment, 
            bool? sendCompletedChecklistNotificationEmail, 
            DateTime? completionDueDateForChecklists, 
            string completionNotificationEmailAddress,
            EmployeeChecklistEmail generatedFor,
            long referenceIncremental)
        {
            var employeeChecklist = new EmployeeChecklist
                                        {
                                            Id = Guid.NewGuid(),
                                            Employee = employee,
                                            Checklist = checklist,
                                            CreatedOn = createdOn,
                                            CreatedBy = generatingUser,
                                            PersonalRiskAssessment = riskAssessment,
                                            SendCompletedChecklistNotificationEmail = sendCompletedChecklistNotificationEmail,
                                            CompletionNotificationEmailAddress = completionNotificationEmailAddress,
                                            DueDateForCompletion = completionDueDateForChecklists,
                                            EmployeeChecklistEmails = new List<EmployeeChecklistEmail> { generatedFor },
                                            ReferencePrefix = employee.PrefixForEmployeeChecklists,
                                            ReferenceIncremental = referenceIncremental
                                        };

            return employeeChecklist;
        }

        public virtual bool AreAllQuestionsAnswered()
        {
            var allQuestions = Checklist.Sections.SelectMany(s => s.Questions);

            return allQuestions.ToList().All(q => DoesAnAnswerObjectExistForQuestion(q) && IsQuestionAnswered(q) );
        }

        public virtual bool IsQuestionAnswered(Question q)
        {
            switch (q.QuestionType)
            {
                case QuestionType.YesNo:
                case QuestionType.YesNoWithAdditionalInfo:
                    //get answer
                    return Answers.First(a => a.Question.Id == q.Id).BooleanResponse.HasValue;
                    break;
                case QuestionType.AdditionalInfo:
                    return true;
                    break;
                default:
                    throw new Exception("Question type not coded ");
            }
        }

        public virtual bool DoesAnAnswerObjectExistForQuestion(Question question)
        {
            return Answers.Any(a => a.Question.Id == question.Id);
        }

        public virtual void SetIsFurtherActionRequired(bool isRequired, UserForAuditing assessingUser)
        {
            IsFurtherActionRequired = isRequired;
            AssessmentDate = DateTime.Now;
            AssessedByEmployee = assessingUser.Employee;
            SetLastModifiedDetails(assessingUser);
        }

        public virtual  void SetPersonalRiskAssessment(PersonalRiskAssessment riskAssessment, UserForAuditing user)
        {
            this.PersonalRiskAssessment = riskAssessment;
            SetLastModifiedDetails(user);
        }
    }
}
