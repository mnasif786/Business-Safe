using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class Question : BaseEntity<Guid>
    {
        public const string ACCEPTABLE_TITLE = "Acceptable";
        public const string UNACCEPTABLE_TITLE = "Unacceptable";
        public const string IMPROVEMENT_REQUIRED_TITLE = "Improvement Required";
        public const string NOT_APPLICABLE_TITLE = "Not Applicable";

        public virtual bool CustomQuestion { get; set; }
        public virtual string Title { get; set; }
        public virtual IList<QuestionResponse> PossibleResponses { get; set; }
        public virtual Category Category { get; set; }
        public virtual bool Mandatory { get; set; }
        public virtual long? SpecificToClientId { get; set; }
        public virtual int OrderNumber { get; set; }
        public virtual IList<ChecklistTemplateQuestion> Industries { get; set; }

        public Question()
        {
            PossibleResponses = new List<QuestionResponse>();
            Industries = new List<ChecklistTemplateQuestion>();
        }

        public static Question Create(Guid id, string title, Category category, bool isCustomQuestion,
                                      UserForAuditing systemUser)
        {
            var question = new Question() {Id = id, Title = title, Category = category};
            question.PossibleResponses = new List<QuestionResponse>();
            question.CreatedBy = systemUser;
            question.CreatedOn = DateTime.Now;
            question.LastModifiedBy = systemUser;
            question.LastModifiedOn = DateTime.Now;
            question.CustomQuestion = isCustomQuestion;
            return question;
        }

        public static Question Create(Guid id, string title, Category category, bool isCustomQuestion, long? specificToClientId,int orderNumber, UserForAuditing systemUser)
        {
            var question = Create(id, title, category, isCustomQuestion, systemUser);
            question.SpecificToClientId = specificToClientId;
            question.OrderNumber = orderNumber;
            return question;
        }

        public static Question Create(Guid id, string title, Category category, bool isCustomQuestion,
                                      long? specificToClientId, UserForAuditing systemUser,
                                      List<QuestionResponse> questionResponses)
        {
            var question = Create(id, title, category, isCustomQuestion, systemUser);

            return question;

        }

        public virtual void AddQuestionResponse(QuestionResponse questionResponse)
        {
            if (!PossibleResponses.Any(x => x.Id == questionResponse.Id))
            {
                questionResponse.Question = this;
                PossibleResponses.Add(questionResponse);
            }
            else
            {
                var respone = PossibleResponses.First(x => x.Id == questionResponse.Id);

                if (AreQuestionResponseDifferent(respone, questionResponse))
                {
                    respone.ActionRequired = questionResponse.ActionRequired;
                    respone.SupportingEvidence = questionResponse.SupportingEvidence;
                    respone.ReportLetterStatement = questionResponse.ReportLetterStatement;
                    respone.Deleted = questionResponse.Deleted;
                    respone.LastModifiedBy = questionResponse.LastModifiedBy;
                    respone.LastModifiedOn = DateTime.Now.Date;
                }
            }
        }

        public virtual void Update(string title, bool mandatory, Category category, bool deleted, UserForAuditing user)
        {
            Title = title;
            Mandatory = mandatory;
            Category = category;
            Deleted = Deleted;
            LastModifiedBy = user;
            LastModifiedOn = DateTime.Now;
        }

        private bool AreQuestionResponseDifferent(QuestionResponse questionResponse,
                                                  QuestionResponse updatedQuestionResponse)
        {
            return questionResponse.ActionRequired != updatedQuestionResponse.ActionRequired ||
                   questionResponse.SupportingEvidence != updatedQuestionResponse.SupportingEvidence ||
                   questionResponse.ReportLetterStatement != updatedQuestionResponse.ReportLetterStatement ||
                   questionResponse.Deleted != updatedQuestionResponse.Deleted;
        }

        public virtual bool IsAcceptableAnswerEnabled
        {
            get { return PossibleResponses.Any(x => x.Title == ACCEPTABLE_TITLE && !x.Deleted); }
            set { EnableResponse(value, ACCEPTABLE_TITLE); }
        }

        public virtual bool IsUnacceptableAnswerEnabled
        {
            get { return PossibleResponses.Any(x => x.Title == UNACCEPTABLE_TITLE && !x.Deleted); }
            set { EnableResponse(value, UNACCEPTABLE_TITLE); }
        }

        public virtual bool IsImprovementRequiredAnswerEnabled
        {
            get { return PossibleResponses.Any(x => x.Title == IMPROVEMENT_REQUIRED_TITLE && !x.Deleted); }
            set { EnableResponse(value, IMPROVEMENT_REQUIRED_TITLE); }
        }

        public virtual bool IsNotApplicableAnswerEnabled
        {
            get { return PossibleResponses.Any(x => x.Title == NOT_APPLICABLE_TITLE && !x.Deleted); }
            set { EnableResponse(value, NOT_APPLICABLE_TITLE); }
        }

        private void EnableResponse(bool enable, string title)
        {
            if (enable && !PossibleResponses.Any(x => x.Title == title))
            {
                var questionResponse = QuestionResponse.Create(Guid.NewGuid(), title, this.CreatedBy);
                questionResponse.ResponseType = GetResponseType(title);

                AddQuestionResponse(questionResponse);
            }

            if (!enable && PossibleResponses.Any(x => x.Title == title))
            {
                PossibleResponses.First(x => x.Title == title).Deleted = true;
            }

            if (enable && PossibleResponses.Any(x => x.Title == title))
            {
                PossibleResponses.First(x => x.Title == title).Deleted = false;
            }
        }

        private string GetResponseType(string title)
        {
            switch (title)
            {
                case ACCEPTABLE_TITLE:
                    return "Positive";
                    break;
                case UNACCEPTABLE_TITLE:
                    return "Negative";
                    break;
                default:
                case IMPROVEMENT_REQUIRED_TITLE:
                case NOT_APPLICABLE_TITLE:
                    return "Neutral";
                    break;
            }
        }

        public virtual string SupportingEvidence
        {
            get
            {
                return GetResponseByTitle(Question.ACCEPTABLE_TITLE) != null
                           ? GetResponseByTitle(Question.ACCEPTABLE_TITLE).SupportingEvidence
                           : null;
            }
            set
            {
                var title = ACCEPTABLE_TITLE;
                if (!PossibleResponses.Any(x => x.Title == title) && !string.IsNullOrEmpty(value))
                {
                    var questionResponse = QuestionResponse.Create(Guid.NewGuid(), title, this.CreatedBy);
                    questionResponse.ResponseType = GetResponseType(title);
                    questionResponse.SupportingEvidence = value;

                    AddQuestionResponse(questionResponse);
                }

                if (PossibleResponses.Any(x => x.Title == title))
                {
                    PossibleResponses.First(x => x.Title == title).SupportingEvidence = value;
                }
            }
        }

        public virtual string ActionRequired
        {
            get
            {
                return GetResponseByTitle(Question.UNACCEPTABLE_TITLE) != null
                           ? GetResponseByTitle(Question.UNACCEPTABLE_TITLE).ActionRequired
                           : null;
            }
            set
            {
                var title = UNACCEPTABLE_TITLE;
                if (!PossibleResponses.Any(x => x.Title == title) && !string.IsNullOrEmpty(value))
                {
                    var questionResponse = QuestionResponse.Create(Guid.NewGuid(), title, this.CreatedBy);
                    questionResponse.ResponseType = GetResponseType(title);
                    questionResponse.ActionRequired = value;

                    AddQuestionResponse(questionResponse);
                }

                if (PossibleResponses.Any(x => x.Title == title))
                {
                    PossibleResponses.First(x => x.Title == title).ActionRequired = value;
                }
            }
        }

        public virtual string ImprovementRequired
        {
            get
            {
                return GetResponseByTitle(Question.IMPROVEMENT_REQUIRED_TITLE) != null
                           ? GetResponseByTitle(Question.IMPROVEMENT_REQUIRED_TITLE).ActionRequired
                           : null;
            }
            set
            {
                var title = IMPROVEMENT_REQUIRED_TITLE;
                if (!PossibleResponses.Any(x => x.Title == title) && !string.IsNullOrEmpty(value))
                {
                    var questionResponse = QuestionResponse.Create(Guid.NewGuid(), title, this.CreatedBy);
                    questionResponse.ResponseType = GetResponseType(title);
                    questionResponse.ActionRequired = value;

                    AddQuestionResponse(questionResponse);
                }

                if (PossibleResponses.Any(x => x.Title == title))
                {
                    PossibleResponses.First(x => x.Title == title).ActionRequired = value;
                }
            }
        }

        public virtual QuestionResponse GetResponseByTitle(string title)
        {
            return PossibleResponses.FirstOrDefault(x => x.Title == title);
        }

        public virtual string GuidanceNotes
        {
            get
            {
                var firstOrDefault = PossibleResponses.FirstOrDefault(r => !string.IsNullOrEmpty(r.GuidanceNotes));
                return (firstOrDefault != null) ? firstOrDefault.GuidanceNotes : null;
            }
            set { PossibleResponses.ToList().ForEach(x => x.GuidanceNotes = value); }
        }

        public virtual string AreaOfNonCompliance
        {
            get
            {
                var firstOrDefault = PossibleResponses.FirstOrDefault(r => !string.IsNullOrEmpty(r.ReportLetterStatement));
                return (firstOrDefault != null) ? firstOrDefault.ReportLetterStatement : null;
            }

            set { PossibleResponses.ToList().ForEach(x => x.ReportLetterStatement = value); }
        }

        public virtual ReportLetterStatementCategory AreaOfNonComplianceHeading
        {
            get
            {
                var firstOrDefault = PossibleResponses.FirstOrDefault(r => r.ReportLetterStatementCategory != null );
                return (firstOrDefault != null) ? firstOrDefault.ReportLetterStatementCategory : null;
            }

            set { PossibleResponses.ToList().ForEach(x => x.ReportLetterStatementCategory = value); }
        }



        private void AddIndustryQuestion(ChecklistTemplateQuestion checklistTemplateQuestion, UserForAuditing user)
        {
            if (!Industries.Any(x => x.ChecklistTemplate.Id == checklistTemplateQuestion.ChecklistTemplate.Id))
            {
                checklistTemplateQuestion.Question = this;
                Industries.Add(checklistTemplateQuestion);
            }
            else
            {
                ChecklistTemplateQuestion checklistTemplate = Industries.FirstOrDefault(x => x.ChecklistTemplate.Id == checklistTemplateQuestion.ChecklistTemplate.Id && x.Deleted);

                if (checklistTemplate != null)
                {
                    checklistTemplate.ReinstateFromDelete(user);
                }
            }
        }

        public virtual void AddIndustry(ChecklistTemplate checklistTemplate, UserForAuditing user)
        {

            var industryQuestion = ChecklistTemplateQuestion.Create(checklistTemplate, this, user);
            industryQuestion.ChecklistTemplate = checklistTemplate;

            AddIndustryQuestion(industryQuestion, user);                      
        }

        private void RemoveIndustryQuestion(Guid industryId,UserForAuditing user)
        {
            var industryQuestion = Industries.FirstOrDefault(x => x.ChecklistTemplate.Id == industryId);

            if (industryQuestion != null)
            {
                industryQuestion.MarkForDelete(user);
            }
        }


        public virtual void RemoveIndustry(ChecklistTemplate checklistTemplate, UserForAuditing user)
        {
            RemoveIndustryQuestion(checklistTemplate.Id,user);
        }

        public virtual Question Copy()
        {
            var newQuestion = new Question();
            newQuestion.Id = Guid.NewGuid();
            newQuestion.Title = Title;
            newQuestion.Category = Category;
            newQuestion.OrderNumber = OrderNumber;
            newQuestion.SpecificToClientId = SpecificToClientId;
            newQuestion.CustomQuestion = CustomQuestion;
            newQuestion.CreatedBy = CreatedBy;
            newQuestion.CreatedOn = DateTime.Now;
            newQuestion.LastModifiedBy = LastModifiedBy;
            newQuestion.LastModifiedOn = DateTime.Now;
            newQuestion.Mandatory = Mandatory;
            
            PossibleResponses
                .Where(x => !x.Deleted)
                .ToList()
                .ForEach(r =>
                {
                    var newQuestionResponse = new QuestionResponse() {
                        Id = Guid.NewGuid(),
                        Title = r.Title,
                        ResponseType = r.ResponseType,
                        SupportingEvidence = r.SupportingEvidence,
                        ActionRequired = r.ActionRequired,
                        ReportLetterStatement = r.ReportLetterStatement,
                        ReportLetterStatementCategory = r.ReportLetterStatementCategory,
                        GuidanceNotes = r.GuidanceNotes,
                        CreatedBy = CreatedBy,
                        CreatedOn = DateTime.Now,
                        LastModifiedBy = LastModifiedBy,
                        LastModifiedOn = DateTime.Now,
                        Deleted = r.Deleted
                    };

                    newQuestion.AddQuestionResponse(newQuestionResponse);
                });

            return newQuestion;
        }
    }

}