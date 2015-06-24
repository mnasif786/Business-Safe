using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using BusinessSafe.WebSite.Helpers;
using NHibernate.Engine;
using BusinessSafe.WebSite.Extensions;
using RestSharp.Extensions;
using BusinessSafe.WebSite.ViewModels;
using Action = BusinessSafe.Domain.Entities.Action;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    public class SearchActionViewModelFactory : ISearchActionViewModelFactory
    {
        public IActionService _actionService;
        public IActionPlanService _actionPlanService;
        public IEmployeeService _employeeService;

        private long _actionPlanId;
        private long _companyId;

        public SearchActionViewModelFactory(IActionService actionService, IActionPlanService actionPlanService, IEmployeeService employeeService)
        {
            _actionService = actionService;
            _actionPlanService = actionPlanService;
            _employeeService = employeeService;
        }
     
        public ISearchActionViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ISearchActionViewModelFactory WithActionPlanId(long actionPlanId)
        {
            _actionPlanId = actionPlanId;
            return this;
        }     

        public ImmediateRiskNotificationActionsIndexViewModel GetViewModel()
        {
            ActionPlanDto dto = _actionPlanService.GetByIdAndCompanyId(_actionPlanId, _companyId);
            IEnumerable<EmployeeDto> employees = _employeeService.GetAll(_companyId);

            var model = new ImmediateRiskNotificationActionsIndexViewModel()
                            {
                                ActionPlanId = dto.Id,
                                SiteId = (dto.Site != null) ? (long?)dto.Site.Id : null,
                                SiteName = (dto.Site != null) ? dto.Site.Name : null,
                                VisitDate = dto.DateOfVisit, 
                                PersonSeen = dto.VisitBy,
                                AreasVisited = dto.AreasVisited.HasValue() ? dto.AreasVisited : string.Empty,
                                AreasNotVisited = dto.AreasNotVisited.HasValue() ? dto.AreasNotVisited : string.Empty,
                                NoLongerRequired = dto.NoLongerRequired

                            };
                        
            model.Actions = dto.Actions
                            .Where(x => x.Category == ActionCategory.Action && 
                                (x.QuestionStatus == ActionQuestionStatus.Red || x.QuestionStatus == ActionQuestionStatus.Amber))
                            .OrderBy(x => x.QuestionStatus)
                            .Select(x=> new SearchActionResultViewModel()
                            {             
                                Id = x.Id,
                                AreaOfNonCompliance = x.AreaOfNonCompliance,
                                ActionRequired = x.ActionRequired,
                                GuidanceNote = x.GuidanceNote,
                                TargetTimescale = x.TargetTimescale,
                                AssignedTo = x.ActionTasks.Any() ? x.ActionTasks.First().TaskAssignedTo.Id : x.AssignedTo,
                                DueDate = x.ActionTasks.Any() ? DateTime.Parse(x.ActionTasks.First().TaskCompletionDueDate) : x.DueDate,
                                Status = EnumHelper.GetEnumDescription(x.Status) ,
                                QuestionStatus = x.QuestionStatus.ToString(),
                                HasTask = x.ActionTasks.Any()
                            })                          
                            .ToList();

            model.ImmediateRiskNotification = dto.Actions
                                .Where( x => x.Category == ActionCategory.ImmediateRiskNotification)
                                .OrderBy(x => x.QuestionStatus)
                                .Select(x => new SearchImmediateRiskNotificationResultViewModel()
                                {
                                    Id = x.Id,
                                    Title = x.Title,
                                    Reference = x.Reference,
                                    SignificantHazardIdentified = x.AreaOfNonCompliance,
                                    RecommendedImmediateAction = x.ActionRequired,
                                    AssignedTo = x.ActionTasks.Any() ? x.ActionTasks.First().TaskAssignedTo.Id : x.AssignedTo,
                                    DueDate = x.ActionTasks.Any() ? DateTime.Parse(x.ActionTasks.First().TaskCompletionDueDate) : x.DueDate,
                                    Status = EnumHelper.GetEnumDescription(x.Status),                                                                      
                                    QuestionStatus = x.QuestionStatus.ToString(),
                                    HasTask = x.ActionTasks.Any()
                                })                              
                                .ToList();

            model.Status = GetActionPlanStatus();

            model.AssignedTo =  employees.Select(AutoCompleteViewModel.ForEmployee).AddDefaultOption().ToList();
           
            return model;
        }

        private IList<AutoCompleteViewModel> GetActionPlanStatus()
        {
            var lstAutoCompleteViewModel = new List<AutoCompleteViewModel>();
            var actionPlanStatus = Enum.GetValues(typeof (ActionPlanStatus));

            foreach (var status in actionPlanStatus)
            {
                lstAutoCompleteViewModel.Add(
                        new AutoCompleteViewModel(Enum.GetName(typeof(ActionPlanStatus), status), status.ToString()) 
                    );
            }
            return lstAutoCompleteViewModel.AddDefaultOption("0").ToList();
        }
    }
}