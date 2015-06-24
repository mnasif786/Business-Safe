using System;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Domain.InfrastructureContracts.Logging;

namespace BusinessSafe.WebSite.Filters
{
    public class RiskAssessmentContextFilterHelper
    {
        private readonly ActionExecutingContext _filterContext;
        public string ActionParamterIdKey { get; set; }
        public string ViewModelPropertyNameKey { get; set; }

        public RiskAssessmentContextFilterHelper(ActionExecutingContext filterContext)
        {
            _filterContext = filterContext;
        }

        private static object GetViewModelPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public long GetRiskAssessmentId()
        {
            // Default Key
            const string defaultKey = "riskAssessmentId";

            if (_filterContext.ActionParameters.ContainsKey(defaultKey))
            {

                var actionParameters = _filterContext.ActionParameters;
                if (actionParameters[defaultKey] == null)
                {
                    throw new Exception("RiskAssessmentId value not found in URL");
                }
                    
                return long.Parse(actionParameters[defaultKey].ToString());
            }

            if (_filterContext.ActionParameters.ContainsKey(ActionParamterIdKey))
            {
                var actionParameters = _filterContext.ActionParameters;
                return long.Parse(actionParameters[ActionParamterIdKey].ToString());
            }

            if (_filterContext.ActionParameters.Values.Any())
            {
                var viewModel = _filterContext.ActionParameters.Values.First();
                var riskAssessmentId = RiskAssessmentContextFilterHelper.GetViewModelPropertyValue(viewModel, ViewModelPropertyNameKey);
                long result = 0;
                if (!long.TryParse(riskAssessmentId.ToString(), out result))
                {
                    throw new ArgumentException(
                        "Can not generate Risk Assessment Id for Summary. View Model Id can not be parsed.");
                }
                return result;
            }

            throw new ArgumentException("Can not generate Risk Assessment Id for Summary. View Model does not contain Id.");
        }
    }
}