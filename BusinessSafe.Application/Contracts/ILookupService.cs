using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts
{
    public interface ILookupService
    {
        List<LookupDto> GetNationalities();
        List<LookupDto> GetEmploymentStatuses();
        List<CountryDto> GetCountries();
        List<LookupDto> GetDocumentTypes();
        List<OthersInvolvedAccidentDetailsDto> GetOthersInvolved();
    }
}