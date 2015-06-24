using System.Web.Mvc;
using FluentValidation;

namespace BusinessSafe.WebSite.Controllers.Helpers
{
    public static class UpdateModelState
    {
        public static void Update(this ModelStateDictionary modelStateDictionary, ValidationException fluentException)
        {
            foreach (var validationFailure in fluentException.Errors)
            {
                modelStateDictionary[validationFailure.PropertyName].Errors.Add(validationFailure.ErrorMessage);
            }
        }
    }
}