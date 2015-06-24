using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Messages.Emails.Commands;
using BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.AccidentDetails;
using NServiceBus;

namespace BusinessSafe.Application.Implementations.AccidentRecords
{
    public class AccidentRecordService : IAccidentRecordService
    {
        private readonly IAccidentRecordRepository _accidentRecordRepository;
        private readonly IAccidentTypeRepository _accidentTypeRepository;
        private readonly ICauseOfAccidentRepository _causeOfAccidentRepository;
        private readonly IJurisdictionRepository _jurisdictionRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly ICountriesRepository _countriesRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly IInjuryRepository _injuryRepository;
        private readonly IBodyPartRepository _bodyPartRepository;
        private readonly IPeninsulaLog _log;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IBus _bus; 
     
        public AccidentRecordService(IAccidentRecordRepository accidentRecordRepository,
                                     IAccidentTypeRepository accidentTypeRepository,
                                     ICauseOfAccidentRepository causeOfAccidentRepository,
                                     IJurisdictionRepository jurisdictionRepository,
                                     IUserForAuditingRepository userForAuditingRepository,
                                     ICountriesRepository countriesRepository,
                                     IEmployeeRepository employeeRepository,
                                     ISiteRepository siteRepository,
                                     IDocumentTypeRepository documentTypeRepository,
                                     IInjuryRepository injuryRepository,
                                     IBodyPartRepository bodyPartRepository,
                                     IPeninsulaLog log,
                                     IBus bus)
        {
            _accidentRecordRepository = accidentRecordRepository;
            _accidentTypeRepository = accidentTypeRepository;
            _causeOfAccidentRepository = causeOfAccidentRepository;
            _jurisdictionRepository = jurisdictionRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _countriesRepository = countriesRepository;
            _employeeRepository = employeeRepository;
            _siteRepository = siteRepository;
            _documentTypeRepository = documentTypeRepository;
            _log = log;
            _injuryRepository = injuryRepository;
            _bodyPartRepository = bodyPartRepository;
            _bus = bus;
        }

        public AccidentRecordDto GetByIdAndCompanyId(long accidentRecordId, long companyId)
        {
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(accidentRecordId, companyId);
            return new AccidentRecordDtoMapper().MapWithEverything(accidentRecord);
        }

        public AccidentRecordDto GetByIdAndCompanyIdWithAccidentRecordDocuments(long accidentRecordId, long companyId)
        {
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(accidentRecordId, companyId);
            return new AccidentRecordDtoMapper().MapWithSiteAndAccidentRecordDocuments(accidentRecord);
        }

        public AccidentRecordDto GetByIdAndCompanyIdWithEmployeeInjured(long accidentRecordId, long companyId)
        {
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(accidentRecordId, companyId);
            return new AccidentRecordDtoMapper().MapWithEmployeeInjuredAndCountry(accidentRecord);
        }

        public AccidentRecordDto GetByIdAndCompanyIdWithSite(long accidentRecordId, long companyId)
        {
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(accidentRecordId, companyId);
            return new AccidentRecordDtoMapper().MapWithSite(accidentRecord);
        }

        public AccidentRecordDto GetByIdAndCompanyIdWithNextStepSections(long accidentRecordId, long companyId)
        {
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(accidentRecordId, companyId);
            return new AccidentRecordDtoMapper().MapWithNextStepSections(accidentRecord);
        }

        public AccidentRecordDto GetByIdAndCompanyIdWithEverything(long accidentRecordId, long companyId)
        {
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(accidentRecordId, companyId);
            return new AccidentRecordDtoMapper().MapWithEverything(accidentRecord);
        }

        public long CreateAccidentRecord(SaveAccidentRecordSummaryRequest request)
        {
            _log.Add(request);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var jurisdiction = _jurisdictionRepository.GetById(request.JurisdictionId);

            var accidentRecord = AccidentRecord.Create(request.Title, request.Reference, jurisdiction, request.CompanyId, user);
            _accidentRecordRepository.SaveOrUpdate(accidentRecord);

            return accidentRecord.Id;
        }

        public void SaveAccidentRecordSummary(SaveAccidentRecordSummaryRequest request)
        {
            _log.Add(request);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var jurisdiction = _jurisdictionRepository.GetById(request.JurisdictionId);

            var accidentRecord = _accidentRecordRepository.GetById(request.AccidentRecordId);
            accidentRecord.SetSummaryDetails(request.Title, request.Reference, jurisdiction, user);
            _accidentRecordRepository.SaveOrUpdate(accidentRecord);
        }


        public void UpdateInjuredPerson(UpdateInjuredPersonRequest request)
        {
            _log.Add(request);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.CurrentUserId, request.CompanyId);

            //TODO: get by company id and userid
            //TODO: handle accident record being null
            var accidentRecord = _accidentRecordRepository.GetById(request.AccidentRecordId);

            var country = request.NonEmployeeInjuredCountry.HasValue
                              ? _countriesRepository.GetById(request.NonEmployeeInjuredCountry.Value)
                              : null;

            var employee = request.EmployeeInjuredId.HasValue
                               ? _employeeRepository.GetByIdAndCompanyId(request.EmployeeInjuredId.Value, request.CompanyId)
                               : null;

            var personInvolvedDescription = request.PersonInvolved != PersonInvolvedEnum.Other ? null : request.PersonInvolvedOtherDescriptionId != 9
                ? request.PersonInvolvedOtherDescription
                : request.PersonInvolvedOtherDescriptionOther;

            var personInvovedDescriptionId = request.PersonInvolved != PersonInvolvedEnum.Other
                ? null
                : request.PersonInvolvedOtherDescriptionId;

            accidentRecord.UpdateInjuredPerson(request.PersonInvolved,
                                               personInvolvedDescription,
                                               employee, 
                                               request.NonEmployeeInjuredForename,
                                               request.NonEmployeeInjuredSurname,
                                               request.NonEmployeeInjuredAddress1,
                                               request.NonEmployeeInjuredAddress2,
                                               request.NonEmployeeInjuredAddress3,
                                               request.NonEmployeeInjuredCountyState,
                                               country,
                                               request.NonEmployeeInjuredPostcode,
                                               request.NonEmployeeInjuredContactNumber,
                                               request.NonEmployeeInjuredOccupation,
                                               personInvovedDescriptionId,
                                               user);

            _accidentRecordRepository.SaveOrUpdate(accidentRecord);            
        }

        public void UpdateAccidentRecordAccidentDetails(UpdateAccidentRecordAccidentDetailsRequest request)
        {
            _log.Add(request);

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(request.AccidentRecordId, request.CompanyId);

            var accidentType = GetAccidentTypeForUpdateRequest(request);
            var accidentCause = GetAccidentCauseForUpdateRequest(request);
            var site = GetSiteForUpdateRequest(request);
            var firstAider = GetFitFirstAiderForUpdateRequest(request);

            accidentRecord.UpdateAccidentDetails(
                request.DateOfAccident,
                site,
                request.OffSiteName,
                request.Location,
                accidentType,
                request.OtherAccidentType,
                accidentCause,
                request.OtherAccidentCause,
                firstAider,
                request.FirstAidAdministered ? request.NonEmployeeFirstAiderName : string.Empty,
                request.FirstAidAdministered ? request.DetailsOfFirstAid : string.Empty,
                user
                );

            _accidentRecordRepository.Save(accidentRecord);

        }

        public void AddAccidentRecordDocument(AddDocumentToAccidentReportRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(request.AccidentRecordId, user.CompanyId);

            var docType = _documentTypeRepository.GetById((long) DocumentTypeEnum.AccidentRecord);

            request.DocumentLibraryIds.ForEach(x => accidentRecord.AddAccidentDocumentRecord(
                    AccidentRecordDocument.Create(x.Description, x.Filename, x.Id, docType, accidentRecord, user)));                                                                                

       
            _accidentRecordRepository.Save(accidentRecord);
        }
        
        public void RemoveAccidentRecordDocuments(RemoveDocumentsFromAccidentRecordRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(request.AccidentRecordId, user.CompanyId);

            request.DocumentLibraryIds.ForEach(x => accidentRecord.RemoveAccidentDocumentRecord(x, user));
            _accidentRecordRepository.Save(accidentRecord);
        }

        public void SetAccidentRecordOverviewDetails(AccidentRecordOverviewRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(request.AccidentRecordId, user.CompanyId);

            accidentRecord.SetAccidentRecordOverviewDetails(request.Description, request.DoNotSendEmailNotification, user);
            _accidentRecordRepository.Save(accidentRecord);
        }

        private Employee GetFitFirstAiderForUpdateRequest(UpdateAccidentRecordAccidentDetailsRequest request)
        {
            Employee firstAider = null;
            if (request.FirstAiderEmployeeId.HasValue && request.FirstAiderEmployeeId != Guid.Empty && request.FirstAidAdministered)
            {
                firstAider = _employeeRepository.GetByIdAndCompanyId(request.FirstAiderEmployeeId.Value, request.CompanyId);
            }
            return firstAider;
        }

        private Site GetSiteForUpdateRequest(UpdateAccidentRecordAccidentDetailsRequest request)
        {
            Site site = null;
            if (request.SiteId.HasValue)
            {
                site = _siteRepository.GetByIdAndCompanyId(request.SiteId.Value, request.CompanyId);
            }
            return site;
        }

        private CauseOfAccident GetAccidentCauseForUpdateRequest(UpdateAccidentRecordAccidentDetailsRequest request)
        {
            CauseOfAccident accidentCause = null;
            if (request.AccidentCauseId.HasValue)
            {
                accidentCause = _causeOfAccidentRepository.GetById(request.AccidentCauseId.Value);
            }
            return accidentCause;
        }

        private AccidentType GetAccidentTypeForUpdateRequest(UpdateAccidentRecordAccidentDetailsRequest request)
        {
            AccidentType accidentType = null;
            if (request.AccidentTypeId != null)
            {
                accidentType = _accidentTypeRepository.GetById(request.AccidentTypeId.Value);
            }
            return accidentType;
        }

        public void UpdateInjuryDetails(UpdateInjuryDetailsRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.CurrentUserId, request.CompanyId);
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(request.AccidentRecordId, user.CompanyId);
            var bodyParts = request.BodyPartsInjured != null ? _bodyPartRepository.GetByIds(request.BodyPartsInjured).ToList() : new List<BodyPart>();
            var injuries = request.Injuries != null ? _injuryRepository.GetByIds(request.Injuries).ToList() : new List<Injury>();

            accidentRecord.UpdateInjuryDetails(request.SeverityOfInjury,
                                               bodyParts,
                                               request.CustomBodyPartDescription,
                                               injuries,
                                               request.CustomInjuryDescription,
                                               request.WasTheInjuredPersonBeenTakenToHospital,
                                               request.HasTheInjuredPersonBeenAbleToCarryOutTheirNormalWorkActivity,
                                               request.LengthOfTimeUnableToCarryOutWork, user);

            _accidentRecordRepository.Save(accidentRecord);
        }

  		public void Delete(long accidentRecordId, long companyId, Guid userId)
        {
            _log.Add(new[] { accidentRecordId, companyId });

            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(accidentRecordId, companyId);
            
            if (accidentRecord == null)
            {
                var e = new AccidentRecordNotFoundException(accidentRecordId, companyId);
                _log.Add(e);
                throw (e);
            }
            
            var user = _userForAuditingRepository.GetByIdAndCompanyId(userId, companyId);
            accidentRecord.MarkForDelete(user);
        }
        
        public IEnumerable<AccidentRecordDto> Search(SearchAccidentRecordsRequest request)
        {
            var accidentRecords = _accidentRecordRepository.Search(
               request.AllowedSiteIds,
               request.CompanyId,
               request.SiteId,
               request.Title,
               request.CreatedFrom,
               request.CreatedTo,
               request.ShowDeleted,
               request.InjuredPersonForename,
               request.InjuredPersonSurname,
               request.Page,
               request.PageSize,
               request.OrderBy,
               request.Ascending
               );

            return new AccidentRecordDtoMapper().MapWithSiteAndCreatedAndEmployeeInjured(accidentRecords);
        }

        public int Count(SearchAccidentRecordsRequest request)
        {
            var count = _accidentRecordRepository.Count(
                request.AllowedSiteIds,
                request.CompanyId,
                request.SiteId,
                request.Title,
                request.CreatedFrom,
                request.CreatedTo,
                request.ShowDeleted,
                request.InjuredPersonForename,
                request.InjuredPersonSurname
                );

            return count;
        }

        public IEnumerable<AccidentRecordDto> SearchWithEverthing(SearchAccidentRecordsRequest request)
        {
            var accidentRecords = _accidentRecordRepository.Search(
               request.AllowedSiteIds,
               request.CompanyId,
               request.SiteId,
               request.Title,
               request.CreatedFrom,
               request.CreatedTo,
               request.ShowDeleted,
               request.InjuredPersonForename,
               request.InjuredPersonSurname,
               request.Page,
               request.PageSize,
               AccidentRecordstOrderByColumn.None, 
               true);

            return new AccidentRecordDtoMapper().MapWithEverything(accidentRecords);
        }

        public void UpdateAccidentRecordStatus(long accidentRecordId, long companyId, Guid userId, AccidentRecordStatusEnum status)
        {
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(accidentRecordId, companyId);

            if (accidentRecord == null)
            {
                var e = new AccidentRecordNotFoundException(accidentRecordId, companyId);
                _log.Add(e);
                throw (e);
            }

            var user = _userForAuditingRepository.GetByIdAndCompanyId(userId, companyId);
            accidentRecord.UpdateAccidentRecordStatus(status, user);
        }

        public void SendAccidentRecordEmails(long accidentRecordId, long companyId, Guid userId)
        {
            var accidentRecord = _accidentRecordRepository.GetByIdAndCompanyId(accidentRecordId, companyId);

            var accidentRecordNotificationMembers = (accidentRecord.SiteWhereHappened == null)
                ? new List<AccidentRecordNotificationMember>()
                : _siteRepository.GetById(accidentRecord.SiteWhereHappened.Id)
                    .AccidentRecordNotificationMembers.Where(a => a.HasEmail())
                    .ToList();
            
            //if notification has not been sent already
            if (!accidentRecord.EmailNotificationSent && accidentRecordNotificationMembers.Any())
            {
                _bus.Send(new SendAccidentRecordEmail()
                {
                    AccidentRecordId = accidentRecordId,
                    CompanyId = companyId
                });

                var user = _userForAuditingRepository.GetByIdAndCompanyId(userId, companyId);

                accidentRecord.UpdateEmailNotificationSentStatus(true, user);
                _accidentRecordRepository.Save(accidentRecord);
            }
        }
    }
}
