using System.Configuration;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.HtmlHelpers
{
    public static class FeatureSwitchChecker
    {
        private const string FeatureSwitchOverrideEmailsKey = "FeatureSwitchOverride_UserEmails";

        public static bool Enabled(FeatureSwitches featureSwitchToCheck, ICustomPrincipal customPrincipal)
        {
            if (IsOverrideFeatureSwitchSettingsUser(customPrincipal)) 
                return true;

            if (ConfigurationManager.AppSettings[featureSwitchToCheck.ToString()] == null)
                return true;

            if (ConfigurationManager.AppSettings[featureSwitchToCheck.ToString()] == "true")
                return true;

            if (ConfigurationManager.AppSettings[featureSwitchToCheck.ToString()] == "false")
                return false;

            return false;
        }

        private static bool IsOverrideFeatureSwitchSettingsUser(ICustomPrincipal customPrincipal)
        {
            return HasUserGotEmailAddress(customPrincipal) && IsEmailSpecifiedInWebConfig(customPrincipal);
        }

        private static bool HasUserGotEmailAddress(ICustomPrincipal customPrincipal)
        {
            return customPrincipal.Email.Length > 0;
        }

        private static bool IsEmailSpecifiedInWebConfig(ICustomPrincipal customPrincipal)
        {
            var emailsWithOverrideFeatureSwitchSettings = ConfigurationManager.AppSettings[FeatureSwitchOverrideEmailsKey];
            return !string.IsNullOrEmpty(emailsWithOverrideFeatureSwitchSettings) &&
                   emailsWithOverrideFeatureSwitchSettings.Contains(customPrincipal.Email.ToLower());
        }
    }
}