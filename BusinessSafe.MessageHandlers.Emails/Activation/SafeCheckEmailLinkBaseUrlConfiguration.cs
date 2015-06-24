using System;
using System.Collections.Generic;

namespace BusinessSafe.MessageHandlers.Emails.Activation
{
    public class SafeCheckEmailLinkBaseUrlConfiguration : ISafeCheckEmailLinkBaseUrlConfiguration
    {
        public readonly IDictionary<string, string> EmailLinkBaseConfigurations;

        public SafeCheckEmailLinkBaseUrlConfiguration()
        { }

        public SafeCheckEmailLinkBaseUrlConfiguration(IDictionary<string, string> emailLinkBaseConfigurations)
        {
            EmailLinkBaseConfigurations = emailLinkBaseConfigurations;
        }

        public virtual string GetBaseUrl()
        {
            if (!EmailLinkBaseConfigurations.ContainsKey(Environment.MachineName))
            {
                throw new Exception("No key for machine with name " + Environment.MachineName);
            }

            return EmailLinkBaseConfigurations[Environment.MachineName];
        }
    }
}
