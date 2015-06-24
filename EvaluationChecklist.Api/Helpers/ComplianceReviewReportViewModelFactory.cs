using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using EvaluationChecklist.ClientDetails;
using EvaluationChecklist.Models;


namespace EvaluationChecklist.Helpers
{
    public class ComplianceReviewReportViewModelFactory : IComplianceReviewReportViewModelFactory
    {
        private long _siteId;
        private ISiteRepository _siteRepository;
        private ComplianceReviewReportViewModel _model;
        private IClientDetailsService _clientDetailsService;

        private IEmployeeRepository _employeeRepository;

        public ComplianceReviewReportViewModelFactory(IClientDetailsService clientDetailsService, IEmployeeRepository employeeRepository)
        {
            _clientDetailsService = clientDetailsService;
            _employeeRepository = employeeRepository;
        }

        public ComplianceReviewReportViewModel GetViewModel(ChecklistViewModel checklistViewModel)
        {   
            _model = new ComplianceReviewReportViewModel();

            AddSiteVisitDetails(checklistViewModel);

            AddSiteDetails(checklistViewModel);

            AddImmediateRiskNotifications(checklistViewModel);

            ParseAnswers(checklistViewModel);

            return _model;
        }
       
        private void AddSiteDetails(ChecklistViewModel checklistViewModel)
        {
            if (_model.Site == null)
            {
                _model.Site = new SiteViewModel();
            }

            if (checklistViewModel.SiteId != null && checklistViewModel.ClientId != null)
            {              
                _model.Site.Id = (int) checklistViewModel.SiteId;

                SiteAddressResponse response = _clientDetailsService.GetSite(checklistViewModel.ClientId.Value, checklistViewModel.SiteId.Value);
                
                if ( response != null)
                {                    
                    _model.Site.SiteName = response.SiteName;
                    _model.Site.Address1 = response.Address1;
                    _model.Site.Address2 = response.Address2;
                    _model.Site.Address3 = response.Address3;
                    _model.Site.Address4 = response.Address4;
                    _model.Site.Postcode = response.Postcode;

                }
            }        
        }

        private void AddImmediateRiskNotifications(ChecklistViewModel checklistViewModel)
        {            
            foreach( ImmediateRiskNotificationViewModel irn in checklistViewModel.ImmediateRiskNotifications)
            {
                ImmediateRiskNotificationPlanItem item = new ImmediateRiskNotificationPlanItem()
                                                             {                                                                                                                                 
                                                                 SignificantHazardIdentified = irn.SignificantHazard,
                                                                 RecommendedImmediateAction = irn.RecommendedImmediate,
                                                                 AllocatedTo = checklistViewModel.SiteVisit.PersonSeen == null ? 
                                                                            String.Empty : checklistViewModel.SiteVisit.PersonSeen.Name
                                                             };

                _model.ImmediateRiskNotifications.Add( item);   
            }            
        }

        private void ParseAnswers(ChecklistViewModel checklistViewModel)
        {            
             if (checklistViewModel.Questions != null)
             {
                 AddComplianceReviewItems(checklistViewModel);
                 AddActionPlanItems(checklistViewModel);
             }
        }
    
        private int GetTimescalePriority(TimescaleViewModel timescale)
        {
            // from response.js
            // var timescales = [
            //  { Id: "0", Name: "None" },
            //  { Id: "1", Name: "One Month" },
            //  { Id: "2", Name: "Three Months" },
            //  { Id: "3", Name: "Six Months" },
            //  { Id: "4", Name: "Urgent Action Required" }

            int priority = 0;

            if (timescale != null)
            {
                switch (timescale.Name)
                {
                    case "Urgent Action Required":
                        priority = 0;
                        break;
                    case "One Month":
                        priority = 1;
                        break;
                    case "Three Months":
                        priority = 2;
                        break;
                    case "Six Months":
                        priority = 3;
                        break;
                    case "None":
                        priority = 4;
                        break;                    
                }
            }
            else
            {
                priority = 5;             
            }
            return priority;            
        }

        private void AddActionPlanItems(ChecklistViewModel checklistViewModel)
        {
            int index = 0;

            index = AddQuestionsToActionPlan(checklistViewModel, "Unacceptable", index);
            AddQuestionsToActionPlan(checklistViewModel, "Improvement Required", index);
        }

        private int AddQuestionsToActionPlan(ChecklistViewModel checklistViewModel, string title, int index)
        {
            var questions = checklistViewModel.Questions
                .Where(     x => x.Answer != null                            
                            && GetSelectedResponse(x) != null 
                            && GetSelectedResponse(x).Title == title
                            )    
                .OrderBy( x => GetTimescalePriority(x.Answer.Timescale))
                .ThenBy(x => x.CategoryNumber)
                .ThenBy(x => x.QuestionNumber)
                .ToList();

            questions.ForEach(x =>{
                                  index++;
                                  _model.ActionPlanItems.Add(CreateActionPlanItem(x, index));
                                  });
            return index;
        }

        private bool IsQuestionAnswered( QuestionAnswerViewModel model)
        {
            return !(model.Answer == null || GetSelectedResponse(model) == null);
        }

        private void AddComplianceReviewItems(ChecklistViewModel checklistViewModel)
        {
            checklistViewModel.Questions.Where(
                x =>
                ((x.Answer != null && GetSelectedResponse(x) != null &&
                    GetSelectedResponse(x).Title != "Not Applicable") || !IsQuestionAnswered(x))
                 )
                 .OrderBy(x=>x.CategoryNumber)
                 .ThenBy(x=>x.QuestionNumber)
                .ToList()
                .ForEach(questionAnswer =>
                             {
                                 ComplianceReviewItem reviewItem = new ComplianceReviewItem()
                                                                       {
                                                                           QuestionText = questionAnswer.Question.Text,
                                                                           CategoryName = questionAnswer.Question.Category == null
                                                                                            ? String.Empty : questionAnswer.Question.Category.Title,
                                                                           CategoryNumber = questionAnswer.CategoryNumber,
                                                                           QuestionNumber = questionAnswer.QuestionNumber,
                                                                       };

                                 if (questionAnswer.Answer != null && GetSelectedResponse(questionAnswer) != null)
                                 {
                                     reviewItem.SupportingEvidence = questionAnswer.Answer.SupportingEvidence;
                                     reviewItem.ActionRequired = questionAnswer.Answer.ActionRequired;
                                     reviewItem.Status = ComplianceReviewItemStatus(questionAnswer);

                                 }
                                 
                                 _model.ComplianceReviewItems.Add(reviewItem);
                             });
        }

        private static QuestionResponseViewModel GetSelectedResponse(QuestionAnswerViewModel question)
        {
            QuestionResponseViewModel response = null;

            if (question.Answer != null && question.Answer.SelectedResponseId != null)
            {
                response = question.Question.PossibleResponses.FirstOrDefault(x => x.Id == question.Answer.SelectedResponseId);
            }

            return response;
        }


        private ComplianceReviewItemStatus? ComplianceReviewItemStatus(QuestionAnswerViewModel questionAnswer)
        {
            var response = GetSelectedResponse(questionAnswer);

            return ComplianceReviewItemStatus(response);
        }

        private ComplianceReviewItemStatus? ComplianceReviewItemStatus(QuestionResponseViewModel response)
        {
            if (response == null)
            {
                return null;
            }
            switch (response.Title)
            {
                default:
                case "Acceptable":
                case "Not Applicable":
                    return Models.ComplianceReviewItemStatus.Satisfactory;
                    break;

                case "Unacceptable":
                    return Models.ComplianceReviewItemStatus.ImmediateActionRequired;                   
                    break;

                case "Improvement Required":
                    return Models.ComplianceReviewItemStatus.FurtherActionRequired;                  
                    break;
            }
        }

        private string GetAssignedToEmployeeName( QuestionAnswerViewModel qaViewModel )
        {
            string employeeName = String.Empty;

            if (qaViewModel.Answer.AssignedTo != null && qaViewModel.Answer.AssignedTo.Id.HasValue)
            {
                Employee assignedToEmployee =
                    _employeeRepository.GetById( qaViewModel.Answer.AssignedTo.Id.GetValueOrDefault() );                            

                if (assignedToEmployee == null)
                {
                    employeeName = qaViewModel.Answer.AssignedTo.NonEmployeeName;
                }
                else
                {
                    employeeName = assignedToEmployee.FullName;
                }
            }
            
            return employeeName;
        }

        private ActionPlanItem CreateActionPlanItem(QuestionAnswerViewModel question, int index)
        {
            var selectedResponse = GetSelectedResponse(question);
            
            ActionPlanItem actionPlanItem = new ActionPlanItem()
                                                {
                                                    AreaOfNonCompliance = !string.IsNullOrEmpty(selectedResponse.ReportLetterStatement) ? selectedResponse.ReportLetterStatement : question.Question.Text , 
                                                    ActionRequired = question.Answer.ActionRequired,
                                                    GuidanceNumber = question.Answer.GuidanceNotes,
                                                    TargetTimescale = question.Answer.Timescale == null ? String.Empty : question.Answer.Timescale.Name,
                                                    AllocatedTo =  GetAssignedToEmployeeName(question),   

                                                    QuestionNumber = question.QuestionNumber,
                                                    CategoryNumber = question.CategoryNumber
                                                };

       

            return actionPlanItem;
        }

        private void AddSiteVisitDetails(ChecklistViewModel checklistViewModel)
        {            
            if (checklistViewModel.SiteVisit != null)
            {
                if (checklistViewModel.SiteVisit.PersonSeen != null)
                {
                    _model.PersonSeen = checklistViewModel.SiteVisit.PersonSeen.Name;
                }

                if (!string.IsNullOrEmpty(checklistViewModel.SiteVisit.VisitDate))
                {
                    _model.VisitDate = DateTime.Parse(checklistViewModel.SiteVisit.VisitDate);
                }

                _model.AreasVisited = checklistViewModel.SiteVisit.AreasVisited;
                _model.AreasNotVisited = checklistViewModel.SiteVisit.AreasNotVisited;
            }
        }
    }
}