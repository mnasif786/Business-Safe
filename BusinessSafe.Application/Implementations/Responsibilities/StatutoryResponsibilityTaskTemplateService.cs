using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.Application.Implementations.Responsibilities
{
    public class StatutoryResponsibilityTaskTemplateService : IStatutoryResponsibilityTaskTemplateService
    {
        private readonly IResponsibilityRepository _responsibilityRepository;
        private readonly IPeninsulaLog _log;

        public StatutoryResponsibilityTaskTemplateService(
            IResponsibilityRepository responsibilityRepository,
            IPeninsulaLog log)
        {
            _responsibilityRepository = responsibilityRepository;
            _log = log;
        }

        public IEnumerable<StatutoryResponsibilityTaskTemplateDto> GetUncreatedByCompanyId(long companyId)
        {
            _log.Add(new object[] { companyId });
            var responsibilities = _responsibilityRepository.GetStatutoryByCompanyId(companyId).ToList();
            
            var uncreatedTaskTemplates =
                responsibilities.SelectMany(x => x.GetUncreatedStatutoryResponsibilityTaskTemplates()).ToList();

            return new StatutoryResponsibilityTaskTemplateDtoMapper().Map(uncreatedTaskTemplates);
        }

        
    }
}
