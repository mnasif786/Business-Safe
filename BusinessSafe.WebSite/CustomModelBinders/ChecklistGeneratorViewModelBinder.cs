using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

namespace BusinessSafe.WebSite.CustomModelBinders
{
    public class ChecklistGeneratorViewModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = (EmployeeChecklistGeneratorViewModel)base.BindModel(controllerContext, bindingContext);
            
            var employeeWithNewEmailRequestList = new List<EmployeeWithNewEmailRequest>();

            var checklistsToGenerate = new ChecklistsToGenerateViewModel
                              {
                                  RequestEmployees = employeeWithNewEmailRequestList,
                                  ChecklistIds = new List<long>(),
                                  Message = model.Message
                              };

            if (model.IsForMultipleEmployees == "single")
            {
                checklistsToGenerate.HasMultipleChecklistRecipients = false;

                if (model.EmployeeId != null)
                {
                    employeeWithNewEmailRequestList.Add(new EmployeeWithNewEmailRequest
                                                            {
                                                                EmployeeId = model.EmployeeId.Value
                                                            });

                    if (model.NewEmployeeEmailVisible)
                    {
                        checklistsToGenerate.RequestEmployees[0].NewEmail = model.NewEmployeeEmail;
                    }
                }
            }

            var formCollection = new FormCollection(controllerContext.HttpContext.Request.Form);

            if (model.IsForMultipleEmployees == "multiple")
            {
                checklistsToGenerate.HasMultipleChecklistRecipients = true;

                foreach (var key in formCollection.Keys.Cast<string>().Where(currentKey => currentKey.StartsWith("MultiSelectedEmployeeId_")))
                {
                    var employeeId = new Guid(key.Substring(24));
                    var newEmail = formCollection["MultiSelectedEmployeeEmail_" + employeeId.ToString()];

                    employeeWithNewEmailRequestList.Add(new EmployeeWithNewEmailRequest
                    {
                        EmployeeId = employeeId,
                        NewEmail = newEmail
                    });
                }
            }
            
            foreach (var key in formCollection.Keys.Cast<string>().Where(currentKey => currentKey.StartsWith("IncludeChecklist_")))
            {
                if (formCollection[key].Contains("true"))
                {
                    checklistsToGenerate.ChecklistIds.Add(Convert.ToInt64(key.Replace("IncludeChecklist_", "")));
                }
            }

            model.ChecklistsToGenerate = checklistsToGenerate;
            return model;
        }

    }
}
