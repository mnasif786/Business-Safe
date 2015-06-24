using System.Collections.Generic;
using System.Linq;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Extensions
{
    public static class AutoCompleteViewModelExtensions
    {
        public static IEnumerable<AutoCompleteViewModel> AddDefaultOption(this IEnumerable<AutoCompleteViewModel> autoCompleteViewModels, string defaultOption = "")
        {
            var result = autoCompleteViewModels.ToList();
            result.Insert(0, new AutoCompleteViewModel("--Select Option--", defaultOption));
            return result;
        }


        public static IEnumerable<AutoCompleteViewModel> WithOtherOption(this IEnumerable<AutoCompleteViewModel> autoCompleteViewModels, AutoCompleteViewModel option)
        {
            var result = autoCompleteViewModels.ToList();
            result.Add(option);
            return result;
        }
    }
}