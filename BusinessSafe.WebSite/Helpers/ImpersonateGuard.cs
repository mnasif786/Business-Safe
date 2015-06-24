using System;

namespace BusinessSafe.WebSite.Helpers
{
    public class ImpersonateGuard
    {
        private readonly string _isImpersonateOn;
        private readonly string _environment;
        private readonly string _allowedUrlReferrerHost;


        public ImpersonateGuard(ImpersonateGuardSettings settings)
        {
            _isImpersonateOn = settings.IsImpersonateOn;
            _environment = settings.Environment;
            _allowedUrlReferrerHost =  settings.AllowedUrlReferrerHost;
        }

        public bool IsAllowed(Uri currentRequestUrlReferrer)
        {
            if (Convert.ToBoolean(_isImpersonateOn) == false)
                return false;    
            
            if(_environment.ToLower() == "live")
            {
                if (currentRequestUrlReferrer == null)
                    return false;

                if(_allowedUrlReferrerHost.ToLower().Equals(currentRequestUrlReferrer.Host.ToLower()) == false)
                    return false;
            }

            return true;
        }
    }

    public class ImpersonateGuardSettings
    {
        public string IsImpersonateOn { get; set; }
        public string Environment { get; set; }
        public string AllowedUrlReferrerHost { get; set; }
    }
}