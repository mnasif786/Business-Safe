using System;
using System.Collections.Generic;

namespace EvaluationChecklist.Models
{
    public class ChecklistViewModel
    {
        public Guid Id { get; set; }
        public int? ClientId { get; set; }
        public int? SiteId { get; set; }
        public SiteViewModel Site { get; set; }
        public string CoveringLetterContent { get; set; }
        public List<CategoryQuestionAnswerViewModel> Categories { get; set; }
        public List<QuestionAnswerViewModel> Questions { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public string SubmittedBy { get; set; }
        public string Status { get; set; }
        public SiteVisitViewModel SiteVisit { get; set; }
        public List<ImmediateRiskNotificationViewModel> ImmediateRiskNotifications { get; set; }
        public bool Submit { get; set; }
        public string PostedBy { get; set; }
        public string ContentPath { get; set; }
        public Guid? IndustryId { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public string QAComments { get; set; }
        public bool? EmailReportToPerson { get; set; }
        public bool? EmailReportToOthers { get; set; }
        public bool? PostReport { get; set; }
        public string OtherEmailAddresses { get; set; }
		public bool UpdatesRequired { get; set; }

        public string ImagePath { get; set; }

        public ChecklistViewModel()
        {
            Categories = new List<CategoryQuestionAnswerViewModel>();
            Questions = new List<QuestionAnswerViewModel>();
            ImmediateRiskNotifications = new List<ImmediateRiskNotificationViewModel>();
            SiteVisit = new SiteVisitViewModel();
        }
    }

    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public int OrderNumber { get; set; }
        public string TabTitle { get; set; }

        public CategoryViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }
    }



    /// <summary>
    /// create this class so we don't get a infinite loop
    /// </summary>

   // public class QuestionWithOutCategoryViewModel
   // {
   //     public virtual Guid Id { get; set; }
   //     public virtual string Text { get; set; }
   //     public List<QuestionResponseViewModel> PossibleResponses { get; set; }
   //     public virtual Guid CategoryId { get; set; }
   //}


    public class CategoryQuestionAnswerViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TabTitle { get; set; }
        public List<QuestionAnswerViewModel> Questions { get; set; }

        public CategoryQuestionAnswerViewModel()
        {
            Questions = new List<QuestionAnswerViewModel>();
        }
    }

    public class SiteViewModel
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Postcode { get; set; }
    }

    public class ChecklistIndexViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string VisitBy { get; set; }
        public string Status { get; set; }
        public DateTime? VisitDate { get; set; }
        public SiteViewModel Site { get; set; }
        public string ClientName { get; set; }
        //public List<ImmediateRiskNotificationViewModel> ImmediateRiskNotifications { get; set; }
        public string CAN { get; set; }
        public QaAdvisorViewModel QaAdvisor { get; set; }

        public bool HasQaComments { get; set; }
        public bool Deleted { get; set; }
    }
}