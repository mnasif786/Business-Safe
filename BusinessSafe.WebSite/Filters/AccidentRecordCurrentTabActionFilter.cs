using System.Web.Mvc;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using StructureMap;
using BusinessSafe.Application.Contracts.AccidentRecord;

namespace BusinessSafe.WebSite.Areas.Filters
{

    public class AccidentRecordCurrentTabActionFilter : ActionFilterAttribute
    {
        private readonly AccidentRecordTabs _accidentRecordTabs;
        private bool _nextStepsVisible;

        const string accidentRecordIdKey = "accidentRecordId";
        const string companyIdKey = "companyId";
        const string readonlyKey = "isReadonly";

        public AccidentRecordCurrentTabActionFilter(AccidentRecordTabs accidentRecordTabs)
        {
            _accidentRecordTabs = accidentRecordTabs;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            long accidentRecordVal = default(long);
            long companyIdVal;

            companyIdVal = GetCompanyId(filterContext);
            filterContext.HttpContext.Items[accidentRecordIdKey] = companyIdVal;
            
            if (filterContext.ActionParameters.ContainsKey(accidentRecordIdKey))
            {
                Log.Add("Found key.");
                Log.Add("Key value: " + filterContext.ActionParameters[accidentRecordIdKey]);
                long.TryParse(filterContext.ActionParameters[accidentRecordIdKey].ToString(), out accidentRecordVal);
                filterContext.HttpContext.Items[accidentRecordIdKey] = accidentRecordVal;
            }
            
            filterContext.HttpContext.Items[readonlyKey] = filterContext.RouteData.Values["action"].ToString() == "View";
            
            //moving this to the domain layer
            //var accidentRecord =
            //    ObjectFactory.GetInstance<IAccidentRecordService>().GetByIdAndCompanyId(_accidentRecordId, _companyId);

            //_nextStepsVisible = accidentRecord.SeverityOfInjury.HasValue
            //                    && accidentRecord.InjuredPersonWasTakenToHospital.HasValue
            //                    && accidentRecord.LengthOfTimeUnableToCarryOutWork.HasValue
            //                    && accidentRecord.InjuredPersonAbleToCarryOutWork.HasValue
            //                    && accidentRecord.InjuredPersonAbleToCarryOutWork.Value != YesNoUnknownEnum.Unknown;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            long accidentRecordVal = default(long);
            long companyIdVal = default(long);
            bool isReadOnly = false;

            if (filterContext.HttpContext.Items[companyIdKey]!=null)
            {
                long.TryParse(filterContext.HttpContext.Items[companyIdKey].ToString(), out companyIdVal);
            }

            if (filterContext.HttpContext.Items[accidentRecordIdKey] != null)
            {
                long.TryParse(filterContext.HttpContext.Items[accidentRecordIdKey].ToString(), out accidentRecordVal);
            }

            if (filterContext.HttpContext.Items[readonlyKey] != null)
            {
                bool.TryParse(filterContext.HttpContext.Items[readonlyKey].ToString(), out isReadOnly);
            }

            filterContext.Controller.ViewBag.TabViewModel = new AccidentRecordTabsViewModel
                                                                {
                                                                    CurrentTab = _accidentRecordTabs,
                                                                    CompanyId = companyIdVal,
                                                                    Id = accidentRecordVal,
                                                                    IsReadOnly = isReadOnly,
                                                                    NextStepsVisible = filterContext.Controller.ViewBag.NextStepsVisible ?? false
                                                                };
            base.OnActionExecuted(filterContext);
        }

        private long GetCompanyId(ControllerContext filterContext)
        {
            var user = filterContext.HttpContext.User as CustomPrincipal;

            return user.CompanyId;
        }

    }
}