using System;
using System.Web.Mvc;

using BusinessSafe.Checklists.ViewModelFactories;
using BusinessSafe.Checklists.ViewModels;
using BusinessSafe.Domain.Entities;

using Remotion.Linq.Utilities;

using StructureMap;
using System.Linq;

namespace BusinessSafe.Checklists.CustomModelBinders
{
    public class EmployeeChecklistViewModelBinder : IModelBinder
    {
        public virtual IEmployeeChecklistViewModelFactory EmployeeChecklistViewModelFactory
        {
            get { return ObjectFactory.GetInstance<IEmployeeChecklistViewModelFactory>(); }
        }
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var formCollection = new FormCollection(controllerContext.HttpContext.Request.Form);
            //get the check list id
            Guid checklistID;
            Guid.TryParse(formCollection["EmployeeChecklistId"], out checklistID);

            if (checklistID == null)
            {
                throw new ArgumentEmptyException("No EmployeeChecklist id present");
            }

            var employeeChecklistViewModel = EmployeeChecklistViewModelFactory.WithEmployeeChecklistId(checklistID).GetViewModel();

            employeeChecklistViewModel.Sections.SelectMany(x => x.Questions)
                .ToList()
                .ForEach(x =>
                {
                    if (x.Answer == null)
                    {
                        x.Answer = new AnswerViewModel();
                    }

                    switch (x.QuestionType)
                    {
                        case QuestionType.YesNo:
                            SetBooleanResponse(formCollection, x);
                            break;
                        case QuestionType.YesNoWithAdditionalInfo:
                            SetBooleanResponse(formCollection, x);
                            SetAdditionalInfo(formCollection, x);
                            break;
                        case QuestionType.AdditionalInfo:
                            SetAdditionalInfo(formCollection, x);
                            break;
                        default:
                            throw new Exception("Question type not mapped!!!!");
                    }
                });

            return employeeChecklistViewModel;
        }

        public virtual void SetAdditionalInfo(FormCollection formCollection, QuestionViewModel questionViewModel)
        {
            var formItem = formCollection["AdditionalInfo_" + questionViewModel.Id.ToString()];
            questionViewModel.Answer.AdditionalInfo = (formItem);
        }

        public virtual void SetBooleanResponse(FormCollection formCollection, QuestionViewModel questionViewModel)
        {
            var formItem = formCollection["YesNo_" + questionViewModel.Id.ToString()];

            if (formItem != null)
            {
                questionViewModel.Answer.BooleanResponse = (formItem.ToLower() == "yes" ? true : false);
            }
            else
            {
                questionViewModel.Answer.BooleanResponse = null;
            }
        }

    }
}