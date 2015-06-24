using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace BusinessSafe.WebSite.Extensions
{
    public static class ModelStateExtensions
    {
        public static string[] GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return (from val in dictionary.Values from error in val.Errors where !string.IsNullOrEmpty(error.ErrorMessage) select error.ErrorMessage).ToArray();
        }

        public static string[] GetErrorMessagesWithPropertyName(this ModelStateDictionary dictionary)
        {
            var keys = dictionary.Keys.ToList();
            var values = dictionary.Values.ToList();
            var errors = new List<String>() {};

            for (int x=0 ;x< keys.Count; x++ )
            {
                var propertyName = keys[x];
                var value = values[x];

                if (value.Errors.Count > 0)
                {
                    errors.Add(propertyName + " " + value );
                }
            }

            return (from val in dictionary.Values from error in val.Errors where !string.IsNullOrEmpty(error.ErrorMessage) select  error.ErrorMessage).ToArray();
        }
    }

    
}