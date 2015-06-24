using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Contracts.Responsibilities
{
    public interface IStatutoryResponsibilityTemplateService
    {
        IEnumerable<StatutoryResponsibilityTemplateDto> GetStatutoryResponsibilityTemplates();
        IEnumerable<StatutoryResponsibilityTemplateDto>  GetStatutoryResponsibilityTemplatesByIds(long[] selectedResponsibilityIds);
    }
}