using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Responsibilities
{
    public class StatutoryResponsibilityTemplateService : IStatutoryResponsibilityTemplateService
    {
        private readonly IStatutoryResponsibilityTemplateRepository _statutoryResponsibilityTemplateRepository;

        private readonly IPeninsulaLog _log;

        public StatutoryResponsibilityTemplateService(
            IStatutoryResponsibilityTemplateRepository statutoryResponsibilityTemplateRepository,
            IPeninsulaLog log)
        {
            _statutoryResponsibilityTemplateRepository = statutoryResponsibilityTemplateRepository;
            _log = log;
        }


        public IEnumerable<StatutoryResponsibilityTemplateDto> GetStatutoryResponsibilityTemplates()
        {
            _log.Add();

            try
            {
                var templates = _statutoryResponsibilityTemplateRepository.GetAll();
                return new StatutoryResponsibilityTemplateDtoMapper().MapWithCategoryAndReason(templates);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<StatutoryResponsibilityTemplateDto> GetStatutoryResponsibilityTemplatesByIds(long[] selectedResponsibilityIds)
        {
            _log.Add();

            try
            {
                var templates = _statutoryResponsibilityTemplateRepository.GetByIds(new List<long>(selectedResponsibilityIds));
                return new StatutoryResponsibilityTemplateDtoMapper().MapWithCategoryAndReason(templates);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }
    }
}
