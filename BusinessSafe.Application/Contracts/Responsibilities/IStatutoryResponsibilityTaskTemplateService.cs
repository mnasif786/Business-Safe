using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.Responsibilities
{
    public interface IStatutoryResponsibilityTaskTemplateService
    {
        IEnumerable<StatutoryResponsibilityTaskTemplateDto> GetUncreatedByCompanyId(long companyId);
    }
}