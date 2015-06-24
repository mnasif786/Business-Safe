using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Company
{
    public class CompanyDefaultService : ICompanyDefaultService
    {
        private readonly IHazardRepository _hazardRepository;
        private readonly IPeopleAtRiskRepository _peopleAtRiskRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPeninsulaLog _log;
        private readonly ISourceOfFuelRepository _sourceOfFuelRepository;
        private readonly IHazardTypeRepository _hazardTypeRepository;
        private readonly IMultiHazardRiskAssessmentRepository _multiHazardRiskAssessmentRepository;
        private readonly IFireSafetyControlMeasureRepository _fireSafetyControlMeasureRepository;
        private readonly ISourceOfIgnitionRepository _sourceOfIgnitionRepository;
        private IInjuryRepository _injuryRepository;
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;

        public CompanyDefaultService(IHazardRepository hazardRepository,
                                     IPeopleAtRiskRepository peopleAtRiskRepository,
                                     IUserForAuditingRepository userForAuditingRepository,
                                     IHazardTypeRepository hazardTypeRepository,
                                     IMultiHazardRiskAssessmentRepository multiHazardRiskAssessmentRepository,
                                     IFireSafetyControlMeasureRepository fireSafetyControlMeasureRepository,
                                     ISourceOfIgnitionRepository sourceOfIgnitionRepository,
                                     ISourceOfFuelRepository sourceOfFuelRepository,
                                     IInjuryRepository injuryRepository,
                                     IRiskAssessmentRepository riskAssessmentRepository,
                                     IPeninsulaLog log)
        {
            _hazardRepository = hazardRepository;
            _peopleAtRiskRepository = peopleAtRiskRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _log = log;
            _sourceOfFuelRepository = sourceOfFuelRepository;
            _hazardTypeRepository = hazardTypeRepository;
            _multiHazardRiskAssessmentRepository = multiHazardRiskAssessmentRepository;
            _fireSafetyControlMeasureRepository = fireSafetyControlMeasureRepository; 
            _sourceOfIgnitionRepository = sourceOfIgnitionRepository;
            _injuryRepository = injuryRepository;
            _riskAssessmentRepository = riskAssessmentRepository;
        }

        public IEnumerable<CompanyDefaultDto> GetAllFireSafetyControlMeasuresForRiskAssessments(long companyId, long riskAssessmentId)
        {
            _log.Add(companyId);

            try
            {
                var fireSafetyControlMeasures = _fireSafetyControlMeasureRepository.GetAllFireSafetyControlMeasureForRiskAssessments(companyId, riskAssessmentId);
                return CompanyDefaultDto.CreateFrom(fireSafetyControlMeasures);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<CompanyDefaultDto> GetAllHazardsForCompany(long companyId)
        {
            _log.Add(companyId);

            try
            {
                var hazards = _hazardRepository.GetByCompanyId(companyId);
                return CompanyDefaultDto.CreateFrom(hazards);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<CompanyDefaultDto> GetAllSourceOfIgnitionForRiskAssessment(long companyId, long riskAssessmentId)
        {
            _log.Add(companyId);

            try
            {
                var sourceOfIgnitions = _sourceOfIgnitionRepository.GetAllSourceOfIgnitionForRiskAssessments(companyId, riskAssessmentId);
                return CompanyDefaultDto.CreateFrom(sourceOfIgnitions);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<CompanyDefaultDto> GetAllSourceOfFuelForRiskAssessment(long companyId, long riskAssessmentId)
        {
            _log.Add(companyId);

            try
            {
                var sourceOfFuels = _sourceOfFuelRepository.GetAllSourceOfFuelForRiskAssessments(companyId, riskAssessmentId);
                return CompanyDefaultDto.CreateFrom(sourceOfFuels);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public CompanyHazardDto GetHazardForCompany(long companyId, long hazardId)
        {
            _log.Add(hazardId);

            try
            {
                var hazard = _hazardRepository.GetByIdAndCompanyId(hazardId, companyId);
                return CompanyHazardDto.CreateFrom(hazard);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public long SaveSourceOfIgnition(SaveCompanyDefaultRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

                SourceOfIgnition sourceOfIgnition = SourceOfIgnition.Create(
                                                                                    request.Name,
                                                                                    request.CompanyId,
                                                                                    request.RiskAssessmentId,
                                                                                    user);


                _sourceOfIgnitionRepository.SaveOrUpdate(sourceOfIgnition);
                return sourceOfIgnition.Id;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public long SaveHazard(SaveCompanyHazardDefaultRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessmentHazardTypes = GetRiskAssessmentHazardTypes(request);

                var riskAssessment = request.RiskAssessmentId.HasValue
                                         ? _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId.Value,
                                                                                  request.CompanyId)
                                         : null;

                Hazard hazard;
                if (request.Id == 0)
                {
                    hazard = Hazard.Create(
                                            request.Name,
                                            request.CompanyId,
                                            user,
                                            riskAssessmentHazardTypes,
                                            riskAssessment
                                            );
                }
                else
                {
                    hazard = _hazardRepository.GetById(request.Id);

                    hazard.Update(
                                    request.Name,
                                    request.CompanyId,
                                    user,
                                    riskAssessmentHazardTypes
                                    );
                }


                _hazardRepository.SaveOrUpdate(hazard);
                return hazard.Id;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }


        }

        private IList<HazardType> GetRiskAssessmentHazardTypes(SaveCompanyHazardDefaultRequest request)
        {
            IList<HazardType> riskAssessmentHazardTypes = request
                .RiskAssessmentTypeApplicable
                .Select(
                    riskAssessmentTypeApplicable => _hazardTypeRepository.LoadById(riskAssessmentTypeApplicable))
                .ToList();
            return riskAssessmentHazardTypes;
        }

        public IEnumerable<CompanyDefaultDto> GetAllPeopleAtRiskForCompany(long companyId)
        {
            _log.Add(companyId);
            try
            {
                var peopleAtRisk = _peopleAtRiskRepository.GetByCompanyId(companyId);
                return CompanyDefaultDto.CreateFrom(peopleAtRisk);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }

        }

        public IEnumerable<CompanyDefaultDto> GetAllPeopleAtRiskForRiskAssessments(long companyId, long riskAssessmentId)
        {
            _log.Add(companyId);
            try
            {
                var peopleAtRisk = _peopleAtRiskRepository.GetAllPeopleAtRiskForRiskAssessments(companyId, riskAssessmentId);
                return CompanyDefaultDto.CreateFrom(peopleAtRisk);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }

        }

        public long SavePeopleAtRisk(SaveCompanyDefaultRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);


                PeopleAtRisk peopleAtRisk;

                if (request.Id == 0)
                {
                    peopleAtRisk = PeopleAtRisk.Create(
                                                        request.Name,
                                                        request.CompanyId,
                                                        request.RiskAssessmentId,
                                                        user);
                }
                else
                {
                    peopleAtRisk = _peopleAtRiskRepository.GetById(request.Id);
                    peopleAtRisk.Update(
                                            request.Name,
                                            request.CompanyId,
                                            request.RiskAssessmentId,
                                            user);
                }

                _peopleAtRiskRepository.SaveOrUpdate(peopleAtRisk);
                return peopleAtRisk.Id;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }

        }

        public long SaveFireSafetyControlMeasure(SaveCompanyDefaultRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

                FireSafetyControlMeasure fireSafetyControlMeasure = FireSafetyControlMeasure.Create(
                                                                                    request.Name,
                                                                                    request.CompanyId,
                                                                                    request.RiskAssessmentId,
                                                                                    user);
                

                _fireSafetyControlMeasureRepository.SaveOrUpdate(fireSafetyControlMeasure);
                return fireSafetyControlMeasure.Id;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public long SaveSourceOfFuel(SaveCompanyDefaultRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

                SourceOfFuel sourceOfFuel = SourceOfFuel.Create(
                                                                                    request.Name,
                                                                                    request.CompanyId,
                                                                                    request.RiskAssessmentId,
                                                                                    user);


                _sourceOfFuelRepository.SaveOrUpdate(sourceOfFuel);
                return sourceOfFuel.Id;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public long SaveInjury(SaveInjuryRequest request)
        {
            _log.Add(request);
            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                Injury injury = Injury.Create(request.Name,
                                                    request.CompanyId,
                                                    request.AccidentRecordId,
                                                    user);
                _injuryRepository.SaveOrUpdate(injury);
                return injury.Id;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void MarkHazardAsDeleted(MarkCompanyDefaultAsDeletedRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var hazard = _hazardRepository.GetByIdAndCompanyId(request.CompanyDefaultId, request.CompanyId);
                hazard.MarkForDelete(user);
                _hazardRepository.SaveOrUpdate(hazard);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }

        }

        public void MarkPersonAtRiskAsDeleted(MarkCompanyDefaultAsDeletedRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var personAtRisk = _peopleAtRiskRepository.GetByIdAndCompanyId(request.CompanyDefaultId, request.CompanyId);

                personAtRisk.MarkForDelete(user);
                _peopleAtRiskRepository.SaveOrUpdate(personAtRisk);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }


        }

        public IEnumerable<CompanyDefaultDto> GetAllMultiHazardRiskAssessmentHazardsForCompany(long companyId, HazardTypeEnum hazardType, long riskAssessmentId)
        {
            _log.Add(companyId);

            try
            {
                var hazards = _hazardRepository.GetByCompanyIdAndHazardTypeId(companyId, hazardType, riskAssessmentId);
                return CompanyDefaultDto.CreateFrom(hazards);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public bool CanDeleteHazard(long hazardId, long companyId)
        {
            _log.Add(new object[] { hazardId, companyId });
            try
            {
                return _multiHazardRiskAssessmentRepository.IsHazardAttachedToRiskAssessments(hazardId, companyId) == false;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IList<AccidentRecordNotificationMember> GetAccidentRecordNotificationMembers(long siteId)
        {
            throw new NotImplementedException();
        }
    }
}
