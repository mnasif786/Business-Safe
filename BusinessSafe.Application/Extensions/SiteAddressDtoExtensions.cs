using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.RestAPI.Responses;

namespace BusinessSafe.Application.Extensions
{
    public static class SiteAddressDtoExtensions
    {
        public static IEnumerable<SiteAddressResponse> ExcludeArchivedHealthAndSafetySites(this IEnumerable<SiteAddressResponse> sites)
        {
            return sites.Where(x => x.IsArchivedHealthAndSafetySite == false);
        }

        public static IEnumerable<SiteAddressResponse> InculdeContractualHealthAndSafetySites(this IEnumerable<SiteAddressResponse> sites)
        {
            return sites.Where(x => x.IsAdditionalHealthAndSafetySite || x.IsPrincipalHealthAndSafetySite);
        }

        public static IEnumerable<SiteAddressResponse> InculdeMainAndAllHealthAndSafetySitesOnly(this IEnumerable<SiteAddressResponse> sites)
        {
            return sites.Where(x => x.IsMainSite || x.IsAdditionalHealthAndSafetySite || x.IsPrincipalHealthAndSafetySite || x.IsArchivedHealthAndSafetySite);
        }
    }
}
