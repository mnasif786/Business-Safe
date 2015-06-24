using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Implementations.HazardousSubstanceInventory
{
    public class HazardousSubstancesService : IHazardousSubstancesService
    {
        private readonly IHazardousSubstanceRiskAssessmentRepository _hazardousSubstanceRiskAssessmentRepository;
        private readonly IHazardousSubstancesRepository _hazardousSubstancesRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPictogramRepository _pictogramRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IRiskPhraseRepository _riskPhraseRepository;
        private readonly ISafetyPhraseRepository _safetyPhraseRepository;
        private readonly IPeninsulaLog _log;

        public HazardousSubstancesService(
            IHazardousSubstancesRepository hazardousSubstancesRepository,
            IUserForAuditingRepository userForAuditingRepository, 
            ISupplierRepository supplierRepository, 
            IPictogramRepository pictogramRepository, 
            IRiskPhraseRepository riskPhraseRepository, 
            ISafetyPhraseRepository safetyPhraseRepository, 
            IHazardousSubstanceRiskAssessmentRepository hazardousSubstanceRiskAssessmentRepository, 
            IPeninsulaLog log)
        {
            _hazardousSubstancesRepository = hazardousSubstancesRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _supplierRepository = supplierRepository;
            _pictogramRepository = pictogramRepository;
            _riskPhraseRepository = riskPhraseRepository;
            _safetyPhraseRepository = safetyPhraseRepository;
            _log = log;
            _hazardousSubstanceRiskAssessmentRepository = hazardousSubstanceRiskAssessmentRepository;
        }

        public IEnumerable<HazardousSubstanceDto> GetForCompany(long companyId)
        {
            var items = _hazardousSubstancesRepository.GetForCompany(companyId);
            return new HazardousSubstanceDtoMapper().Map(items);
        }

        public IEnumerable<HazardousSubstanceDto> Search(SearchHazardousSubstancesRequest request)
        {
            var hazardousSubstances = _hazardousSubstancesRepository.Search(
                request.CompanyId,
                request.SupplierId,
                request.SubstanceNameLike,
                request.ShowDeleted);

            return new HazardousSubstanceDtoMapper().Map(hazardousSubstances); 
        }

        public long Add(AddHazardousSubstanceRequest request)
        {
            _log.Add(request);
            try
            {

                var currentUser = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var supplier = request.SupplierId != default(long) ? _supplierRepository.GetByIdAndCompanyId(request.SupplierId, request.CompanyId) : null;
                var riskPhrases = request.RiskPhraseIds != null ? _riskPhraseRepository.GetByIds(request.RiskPhraseIds).ToList() : null;
                var pictograms = request.PictogramIds != null ? _pictogramRepository.GetByIds(request.PictogramIds).ToList() : null;
                var safetyPhrases = request.SafetyPhraseIds != null ? _safetyPhraseRepository.GetByIds(request.SafetyPhraseIds).ToList() : null;
                var safetyPhrasesParameters = CreateSafetyPhraseParameters(request, safetyPhrases);


                var hazardousSubstance = HazardousSubstance.Add(
                    request.CompanyId, 
                    currentUser, 
                    request.Name, 
                    request.Reference, 
                    supplier, 
                    request.SdsDate,
                    pictograms,
                    riskPhrases,
                    safetyPhrasesParameters,
                    request.HazardousSubstanceStandard,
                    request.DetailsOfUse,
                    request.AssessmentRequired.GetValueOrDefault()
                );
                _hazardousSubstancesRepository.SaveOrUpdate(hazardousSubstance);
                return hazardousSubstance.Id;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void Update(AddHazardousSubstanceRequest request)
        {
            _log.Add(request);

            try
            {
                var hazardousSubstance = _hazardousSubstancesRepository.GetByIdAndCompanyId(request.Id, request.CompanyId);
                var currentUser = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var supplier = request.SupplierId != default(long) ? _supplierRepository.GetByIdAndCompanyId(request.SupplierId, request.CompanyId) : null;
                var riskPhrases = request.RiskPhraseIds != null ? _riskPhraseRepository.GetByIds(request.RiskPhraseIds).ToList() : null;
                var pictograms = request.PictogramIds != null ? _pictogramRepository.GetByIds(request.PictogramIds).ToList() : null;
                var safetyPhrases = request.SafetyPhraseIds != null ? _safetyPhraseRepository.GetByIds(request.SafetyPhraseIds).ToList() : null;
                var safetyPhrasesParameters = CreateSafetyPhraseParameters(request, safetyPhrases);

                // We currently return back all the selected hazards from the ui, both the global and the european, this is a ui limitation where we pass backa ll selected hazards across both the standards
                // We just want to save the hazards relating to the currently selected standard
                if (pictograms != null)
                {
                    pictograms = pictograms.Where(pictogram => pictogram.HazardousSubstanceStandard == request.HazardousSubstanceStandard).ToList();
                }

                hazardousSubstance.Update(currentUser, request.Name, request.Reference, supplier, request.SdsDate, pictograms, riskPhrases, safetyPhrasesParameters, request.HazardousSubstanceStandard, request.DetailsOfUse, request.AssessmentRequired.GetValueOrDefault());
                _hazardousSubstancesRepository.SaveOrUpdate(hazardousSubstance);
            }
            catch (Exception ex)
            {
                _log.Add(ex);

                throw;
            }
        }


        public HazardousSubstanceDto GetByIdAndCompanyId(long hazardousSubstanceId, long companyId)
        {
            _log.Add(new object[] { companyId, hazardousSubstanceId });

            var hazardousSubstance = _hazardousSubstancesRepository.GetByIdAndCompanyId(hazardousSubstanceId, companyId);
            var hazardousSubstanceDto = new HazardousSubstanceDtoMapper().Map(hazardousSubstance);

            return hazardousSubstanceDto;
        }

        public IEnumerable<HazardousSubstanceDto> GetHazardousSubstancesForSearchTerm(string term, long companyId, int pageLimit)
        {
           var hazardousSubstances = _hazardousSubstancesRepository.GetByTermSearch(term, companyId, pageLimit);
           return hazardousSubstances
                        .Select(x => new HazardousSubstanceDtoMapper().Map(x))
                        .ToList();
        }

        public void MarkForDelete(MarkHazardousSubstanceAsDeleteRequest request)
        {

            bool riskAssessmentsExist = HasHazardousSubstanceGotRiskAssessments(request.HazardousSubstanceId, request.CompanyId);
            if (riskAssessmentsExist)
            {
                throw new TryingToDeleteHazardousSubstanceThatUsedByRiskAssessmentsException(request.HazardousSubstanceId);
            }

            var hazardousSubstance = _hazardousSubstancesRepository.GetByIdAndCompanyId(request.HazardousSubstanceId, request.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            hazardousSubstance.MarkForDelete(user);
            _hazardousSubstancesRepository.SaveOrUpdate(hazardousSubstance);
            
        }

        public void Reinstate(ReinstateHazardousSubstanceRequest request)
        {
            var hazardousSubstance = _hazardousSubstancesRepository.GetDeletedByIdAndCompanyId(request.HazardousSubstanceId, request.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            hazardousSubstance.ReinstateFromDelete(user);
            _hazardousSubstancesRepository.SaveOrUpdate(hazardousSubstance);
        }

        public bool HasSupplierGotHazardousSubstances(long supplierId, long companyId)
        {
            return _hazardousSubstancesRepository.DoesHazardousSubstancesExistForSupplier(supplierId, companyId);
        }

        public bool HasHazardousSubstanceGotRiskAssessments(long hazardousSubstanceId, long companyId)
        {
            return
                _hazardousSubstanceRiskAssessmentRepository.DoesRiskAssessmentsExistForHazardousSubstance(
                    hazardousSubstanceId, companyId);
        }

        private static IEnumerable<SafetyPhraseParameters> CreateSafetyPhraseParameters(AddHazardousSubstanceRequest request, IEnumerable<SafetyPhrase> safetyPhrases)
        {
            var safetyPhrasesParameters = new List<SafetyPhraseParameters>();
            if (safetyPhrases != null)
            {
                safetyPhrasesParameters.AddRange(safetyPhrases.Select(safetyPhrase => new SafetyPhraseParameters()
                                                                                          {
                                                                                              Phrase = safetyPhrase,
                                                                                              Information = GetSafetyPhraseAdditionalInformation(request, safetyPhrase)
                                                                                          }));
            }
            return safetyPhrasesParameters;
        }

        private static string GetSafetyPhraseAdditionalInformation(AddHazardousSubstanceRequest request, SafetyPhrase safetyPhrase)
        {
            return request.AdditionalInformation.Where(x =>x.SafetyPhaseId ==safetyPhrase.Id).Select(x => x.AdditionalInformation).FirstOrDefault();
        }
    }
}