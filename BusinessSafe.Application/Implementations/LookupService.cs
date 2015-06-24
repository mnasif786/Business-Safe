using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations
{
    public class LookupService : ILookupService
    {
        private readonly IEmploymentStatusRepository _employmentStatusRepository;
        private readonly INationalityRepository _nationalityRepository;
        private readonly ICountriesRepository _countriesRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IOthersInvolvedAccidentDetailsRepository _involvedAccidentDetailsRepository;
        private readonly IPeninsulaLog _log;


        public LookupService(
            IEmploymentStatusRepository employmentStatusRepository,
            INationalityRepository nationalityRepository,
            ICountriesRepository countriesRepository,
            IDocumentTypeRepository documentTypeRepository,
            IOthersInvolvedAccidentDetailsRepository othersInvolvedAccidentDetailsRepository,
            IPeninsulaLog log)
        {
            _employmentStatusRepository = employmentStatusRepository;
            _nationalityRepository = nationalityRepository;
            _countriesRepository = countriesRepository;
            _documentTypeRepository = documentTypeRepository;
            _involvedAccidentDetailsRepository = othersInvolvedAccidentDetailsRepository;
            _log = log;
        }

        public List<LookupDto> GetNationalities()
        {
            _log.Add();

            try
            {
                var nationalities = _nationalityRepository.GetAll();
                return nationalities.Select(x => new LookupDto
                                                     {
                                                         Id = x.Id,
                                                         Name = x.Name
                                                     }).ToList();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public List<LookupDto> GetEmploymentStatuses()
        {
            _log.Add();

            try
            {
                var employmentStatuses = _employmentStatusRepository.GetAll();
                return employmentStatuses.Select(x => new LookupDto
                                                          {
                                                              Id = x.Id,
                                                              Name = x.Name
                                                          }).ToList();

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public List<CountryDto> GetCountries()
        {
            _log.Add();

            try
            {
                var countries = _countriesRepository.GetAll();
                return countries.Select(x => new CountryDto
                                                 {
                                                     Id = x.Id,
                                                     Name = x.Name
                                                 }).ToList();

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public List<LookupDto> GetDocumentTypes()
        {
            _log.Add();

            try
            {
                var documentTypes = _documentTypeRepository.GetAll();
                return documentTypes.Select(x => new LookupDto
                                                     {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public List<OthersInvolvedAccidentDetailsDto> GetOthersInvolved()
        {
            _log.Add();

            try
            {
                var othersInvolvedAccidentDetails = _involvedAccidentDetailsRepository.GetAll();
                return othersInvolvedAccidentDetails.Select(x => new OthersInvolvedAccidentDetailsDto()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }
    }
}