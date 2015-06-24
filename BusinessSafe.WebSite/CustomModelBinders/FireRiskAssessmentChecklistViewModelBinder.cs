using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Remotion.Linq.Utilities;
using StructureMap;

namespace BusinessSafe.WebSite.CustomModelBinders
{
    public class FireRiskAssessmentChecklistViewModelBinder : IModelBinder
    {
        public virtual IFireRiskAssessmentChecklistViewModelFactory FireRiskAssessmentChecklistViewModelFactory
        {
            get { return ObjectFactory.GetInstance<IFireRiskAssessmentChecklistViewModelFactory>(); }
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var formCollection = new FormCollection(controllerContext.HttpContext.Request.Form);
            return this.BindModel(formCollection);
        }

        public object BindModel(FormCollection formCollection)
        {
            long fireRiskAssessmentId;
            long companyId;
            long.TryParse(formCollection["RiskAssessmentId"], out fireRiskAssessmentId);
            long.TryParse(formCollection["CompanyId"], out companyId);

            if (fireRiskAssessmentId == default(long))
            {
                throw new ArgumentEmptyException("No FireRiskAssessment id present");
            }

            if (companyId == default(long))
            {
                throw new ArgumentEmptyException("No CompanyId id present");
            }

            var fireRiskAssessmentChecklistViewModel = FireRiskAssessmentChecklistViewModelFactory
                .WithRiskAssessmentId(fireRiskAssessmentId)
                .WithCompanyId(companyId)
                .GetViewModel();

            fireRiskAssessmentChecklistViewModel.Sections.SelectMany(x => x.Questions)
                .ToList()
                .ForEach(question =>
                {
                    if (question.Answer == null)
                    {
                        question.Answer = new FireAnswerViewModel();
                    }

                    var formItem = formCollection["YesNoNotApplicable_" + question.Id];

                    if (formItem != null)
                    {
                        switch (formItem)
                        {
                            case "Yes":
                                question.Answer.YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes;
                                break;
                            case "No":
                                question.Answer.YesNoNotApplicableResponse = YesNoNotApplicableEnum.No;
                                break;
                            case "NotApplicable":
                                question.Answer.YesNoNotApplicableResponse = YesNoNotApplicableEnum.NotApplicable;
                                break;
                            default:
                                question.Answer.YesNoNotApplicableResponse = null;
                                break;
                        }
                    }
                    else
                    {
                        question.Answer.YesNoNotApplicableResponse = null;
                    }

                    question.Answer.AdditionalInfo = formCollection["AdditionalInfo_" + question.Id];
                });

            return fireRiskAssessmentChecklistViewModel;
        }
    }
}