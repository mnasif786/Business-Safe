using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;

namespace BusinessSafe.WebSite.Areas.ActionPlans.Factories
{
    public class CompleteActionTaskViewModelFactory : ICompleteActionTaskViewModelFactory
    {
        private long _companyId;
        private long _actionTaskId;

        private readonly IActionTaskService _actionTaskService;
     
        private readonly IViewActionTaskViewModelFactory _viewActionTaskViewModelFactory;

        public CompleteActionTaskViewModelFactory(IActionTaskService actionTaskService, IViewActionTaskViewModelFactory viewActionTaskViewModelFactory)
        {
            _actionTaskService = actionTaskService;
            _viewActionTaskViewModelFactory = viewActionTaskViewModelFactory;
        }

        public ICompleteActionTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ICompleteActionTaskViewModelFactory WithActionTaskId(long actionTaskId)
        {
            _actionTaskId = actionTaskId;
            return this;
        }

        public ViewModels.CompleteActionTaskViewModel GetViewModel()
        {
            var task = _actionTaskService.GetByIdAndCompanyId(_actionTaskId, _companyId);

            return new CompleteActionTaskViewModel
            {
                CompanyId = _companyId,
                ActionTaskId = _actionTaskId,
                
                ActionTask = _viewActionTaskViewModelFactory
                     .WithCompanyId(_companyId)
                     .WithActionTaskId(_actionTaskId)
                     .GetViewModel()
            };
        }
    }
}