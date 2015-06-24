using System.ComponentModel.DataAnnotations;

namespace BusinessSafe.WebSite.CustomValidators
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        //Copy from the advice project. Looks like it will accept ip addresses.
        public EmailAttribute()
            : base(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                   + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                   + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"                  
                   + @"([a-zA-Z0-9]+[\w-]*\.)+[a-zA-Z]{2,4})$")            
        {
        }
    }
}