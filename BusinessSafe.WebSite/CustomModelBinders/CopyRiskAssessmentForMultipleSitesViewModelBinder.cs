using System.Web.Mvc;
using BusinessSafe.WebSite.ViewModels;
using System;

namespace BusinessSafe.WebSite.CustomModelBinders
{
    public class CopyRiskAssessmentForMultipleSitesViewModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = (CopyRiskAssessmentForMultipleSitesViewModel)base.BindModel(controllerContext, bindingContext);
            var formCollection = new FormCollection(controllerContext.HttpContext.Request.Form);
            var siteIds = formCollection["SiteIdForCopy"];
            if (siteIds != null)
            {
                foreach (var siteIdString in siteIds.Split(','))
                {
                    model.SiteIds.Add(Convert.ToInt64(siteIdString));
                }
            }

            return model;
        }
    }
}