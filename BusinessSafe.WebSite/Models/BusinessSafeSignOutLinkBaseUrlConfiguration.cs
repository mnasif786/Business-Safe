using System;
using System.Collections.Generic;

namespace BusinessSafe.WebSite.Models
{
    public class BusinessSafeSignOutLinkBaseUrlConfiguration
    {
        public readonly IDictionary<string, string> BusinessSafeSignOutLinkBaseUrlConfigurations;

        public BusinessSafeSignOutLinkBaseUrlConfiguration() { }

        public BusinessSafeSignOutLinkBaseUrlConfiguration(IDictionary<string, string> businessSafeSignOutLinkBaseUrlConfigurations)
        {
            BusinessSafeSignOutLinkBaseUrlConfigurations = businessSafeSignOutLinkBaseUrlConfigurations;
        }

        public virtual string GetBaseUrl()
        {
            if (BusinessSafeSignOutLinkBaseUrlConfigurations.ContainsKey(System.Environment.MachineName))
            {
                return BusinessSafeSignOutLinkBaseUrlConfigurations[System.Environment.MachineName];
            }
            
            throw new Exception(string.Format("Could not find URL in BusinessSafeSignOutLinkBaseUrlConfigurations for machine {0}. You will need to add a value to the structuremap.config.", System.Environment.MachineName));
        }
    }
}