using System.Linq;
using System.Web.Mvc;
using BusinessSafe.WebSite.ViewModels;
using FluentValidation;

namespace BusinessSafe.WebSite.Helpers
{
    public static class ModelStateErrorHelper
    {
        public static void AddValidationErrors(this ModelStateDictionary modelStateDictionary, ValidationException validationException )
        {
            var errorColumns = validationException.Errors.Select(error => new ErrorColumn(error.PropertyName, error.ErrorMessage));
            foreach (var errorColumn in errorColumns)
            {
                modelStateDictionary.AddModelError(errorColumn.Name, errorColumn.Message);
            }
        }
    }
}