using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Mappers
{
    public class AccidentRecordDtoMapper
    {
        public AccidentRecordDto Map(AccidentRecord entity)
        {
            return new AccidentRecordDto
                       {
                           Id = entity.Id,
                           CompanyId = entity.CompanyId,
                           Title = entity.Title,
                           Reference = entity.Reference,
                           PersonInvolved = entity.PersonInvolved,
                           PersonInvolvedOtherDescription = entity.PersonInvolvedOtherDescription,
                           PersonInvolvedOtherDescriptionId = entity.PersonInvolvedOtherDescriptionId,
                           NonEmployeeInjuredForename = entity.NonEmployeeInjuredForename,
                           NonEmployeeInjuredSurname = entity.NonEmployeeInjuredSurname,
                           NonEmployeeInjuredAddress1 = entity.NonEmployeeInjuredAddress1,
                           NonEmployeeInjuredAddress2 = entity.NonEmployeeInjuredAddress2,
                           NonEmployeeInjuredAddress3 = entity.NonEmployeeInjuredAddress3,
                           NonEmployeeInjuredCountyState = entity.NonEmployeeInjuredCountyState,
                           NonEmployeeInjuredPostcode = entity.NonEmployeeInjuredPostcode,
                           NonEmployeeInjuredContactNumber = entity.NonEmployeeInjuredContactNumber,
                           NonEmployeeInjuredOccupation = entity.NonEmployeeInjuredOccupation,
                           DateAndTimeOfAccident = entity.DateAndTimeOfAccident,
                           OffSiteSpecifics = entity.OffSiteSpecifics,
                           Location = entity.Location,
                           AccidentTypeOther = entity.AccidentTypeOther,
                           CauseOfAccidentOther = entity.CauseOfAccidentOther,
                           FirstAidAdministered = entity.FirstAidAdministered,
                           NonEmployeeFirstAiderSpecifics = entity.NonEmployeeFirstAiderSpecifics,
                           DetailsOfFirstAidTreatment = entity.DetailsOfFirstAidTreatment,
                           SeverityOfInjury = entity.SeverityOfInjury,
                           InjuredPersonWasTakenToHospital = entity.InjuredPersonWasTakenToHospital,
                           InjuredPersonAbleToCarryOutWork = entity.InjuredPersonAbleToCarryOutWork,
                           AdditionalInjuryInformation = entity.AdditionalInjuryInformation,
                           AdditionalBodyPartInformation = entity.AdditionalBodyPartInformation,
                           LengthOfTimeUnableToCarryOutWork = entity.LengthOfTimeUnableToCarryOutWork,
                           DescriptionHowAccidentHappened = entity.DescriptionHowAccidentHappened,
                           NextStepsAvailable = entity.NextStepsAvailable,
                           CreatedOn = entity.CreatedOn,
                           InjuredPersonFullName = entity.InjuredPersonFullName,
                           IsDeleted = entity.Deleted,
                           IsReportable = entity.IsReportable,
                           Status = entity.Status,
                           DoNotSendEmailNotification = entity.DoNotSendEmailNotification,
                           EmailNotificationSent = entity.EmailNotificationSent
                       };
        }

        public AccidentRecordDto MapWithNextStepSections(AccidentRecord entity)
        {
            var dto = Map(entity);
            dto.AccidentRecordNextStepSections = new AccidentRecordNextStepSectionDtoMapper().Map(entity.AccidentRecordNextStepSections);
            return dto;
        }

        //For API.
        public AccidentRecordDto MapWithEverything(AccidentRecord entity)
        {
            var dto = Map(entity);

            dto.EmployeeInjured = entity.EmployeeInjured != null
                                      ? new EmployeeDtoMapper().Map(entity.EmployeeInjured)
                                      : null;

            dto.NonEmployeeInjuredCountry = entity.NonEmployeeInjuredCountry != null
                                                ? new CountryDtoMapper().Map(entity.NonEmployeeInjuredCountry)
                                                : null;
            dto.AccidentRecordBodyParts = new AccidentRecordBodyPartDtoMapper().MapWithInjury(entity.AccidentRecordBodyParts).ToList();
            dto.AccidentRecordInjuries = new AccidentRecordInjuryDtoMapper().MapWithInjury(entity.AccidentRecordInjuries).ToList();
            dto.Jurisdiction = entity.Jurisdiction != null ? new JurisdictionDtoMapper().Map(entity.Jurisdiction) : null;
            dto.AccidentRecordDocuments = entity.AccidentRecordDocuments.Select(x => new AccidentRecordDocumentDto() { Id = x.Id, Description = x.Description, DocumentLibraryId = x.DocumentLibraryId, Filename = x.Filename }).ToList();
            dto.SiteWhereHappened = entity.SiteWhereHappened != null ? new SiteDtoMapper().Map(entity.SiteWhereHappened) : null;
            dto.AccidentType = entity.AccidentType != null ? new AccidentTypeDtoMapper().Map(entity.AccidentType) : null;
            dto.CauseOfAccident = entity.CauseOfAccident != null ? new CauseOfAccidentDtoMapper().Map(entity.CauseOfAccident) : null;
            dto.EmployeeFirstAider = entity.EmployeeFirstAider != null ? new EmployeeDtoMapper().Map(entity.EmployeeFirstAider) : null;
            dto.SiteWhereHappened = entity.SiteWhereHappened != null ? new SiteDtoMapper().Map(entity.SiteWhereHappened) : null;
            dto.CreatedBy = entity.CreatedBy != null ? new AuditedUserDtoMapper().Map(entity.CreatedBy) : null;

            dto.EmployeeInjured = entity.EmployeeInjured != null
                                       ? new EmployeeDtoMapper().Map(entity.EmployeeInjured)
                                       : null;

            if (entity.SiteWhereHappened != null)
            {
                dto.SiteWhereHappened = new SiteDtoMapper().Map(entity.SiteWhereHappened);
            }
            if (entity.AccidentType != null)
            {
                dto.AccidentType = new AccidentTypeDtoMapper().Map(entity.AccidentType);
            }
            if (entity.CauseOfAccident != null)
            {
                dto.CauseOfAccident = new CauseOfAccidentDtoMapper().Map(entity.CauseOfAccident);
            }
            if (entity.EmployeeFirstAider != null)
            {
                dto.EmployeeFirstAider = new EmployeeDtoMapper().Map(entity.EmployeeFirstAider);
            }



            return dto;
        }

        //For next steps tab.
        public AccidentRecordDto MapWithAccidentRecordDocuments(AccidentRecord entity)
        {
            var dto = Map(entity);
            dto.AccidentRecordDocuments =
                entity.AccidentRecordDocuments.Select(
                    x =>
                    new AccidentRecordDocumentDto()
                        {
                            Id = x.Id,
                            Description = x.Description,
                            DocumentLibraryId = x.DocumentLibraryId,
                            Filename = x.Filename,
                            DocumentType = new DocumentTypeDto()
                                               {
                                                   Id = x.DocumentType.Id,
                                                   Name = x.DocumentType.Name
                                               }
                        }).ToList();
            return dto;
        }

        //For summary tab
        public AccidentRecordDto MapWithJurisdiction(AccidentRecord entity)
        {
            var dto = Map(entity);
            dto.Jurisdiction = entity.Jurisdiction != null ? new JurisdictionDtoMapper().Map(entity.Jurisdiction) : null;
            return dto;
        }

        //For injured person tab
        public AccidentRecordDto MapWithEmployeeInjuredAndCountry(AccidentRecord entity)
        {
            var dto = Map(entity);

            dto.EmployeeInjured = entity.EmployeeInjured != null
                                      ? new EmployeeDtoMapper().Map(entity.EmployeeInjured)
                                      : null;

            dto.NonEmployeeInjuredCountry = entity.NonEmployeeInjuredCountry != null
                                                ? new CountryDtoMapper().Map(entity.NonEmployeeInjuredCountry)
                                                : null;
            return dto;
        }

        //For accident tab
        public AccidentRecordDto MapWithSiteAndTypeAndCauseAndFirstAider(AccidentRecord entity)
        {
            var dto = Map(entity);
            dto.SiteWhereHappened = entity.SiteWhereHappened != null ? new SiteDtoMapper().Map(entity.SiteWhereHappened) : null;
            dto.AccidentType = entity.AccidentType != null ? new AccidentTypeDtoMapper().Map(entity.AccidentType) : null;
            dto.CauseOfAccident = entity.CauseOfAccident != null ? new CauseOfAccidentDtoMapper().Map(entity.CauseOfAccident) : null;
            dto.EmployeeFirstAider = entity.EmployeeFirstAider != null ? new EmployeeDtoMapper().Map(entity.EmployeeFirstAider) : null;
            return dto;
        }

        //For accident records index tab
        public AccidentRecordDto MapWithSiteAndCreatedAndEmployeeInjured(AccidentRecord entity)
        {
            var dto = Map(entity);
            dto.SiteWhereHappened = entity.SiteWhereHappened != null ? new SiteDtoMapper().Map(entity.SiteWhereHappened) : null;                         
            dto.CreatedBy = entity.CreatedBy != null ? new AuditedUserDtoMapper().Map(entity.CreatedBy) : null;

            dto.EmployeeInjured = entity.EmployeeInjured != null
                                       ? new EmployeeDtoMapper().Map(entity.EmployeeInjured)
                                       : null;
            return dto;
        }

        public AccidentRecordDto MapWithSite(AccidentRecord entity)
        {
            var dto = Map(entity);
            dto = MapWithJurisdiction(entity);

            if (entity.SiteWhereHappened != null)
            {
                dto.SiteWhereHappened = new SiteDtoMapper().Map(entity.SiteWhereHappened);
            }
            if (entity.AccidentType != null)
            {
                dto.AccidentType = new AccidentTypeDtoMapper().Map(entity.AccidentType);
            }
            if (entity.CauseOfAccident != null)
            {
                dto.CauseOfAccident = new CauseOfAccidentDtoMapper().Map(entity.CauseOfAccident);
            }
            if (entity.EmployeeFirstAider != null)
            {
                dto.EmployeeFirstAider = new EmployeeDtoMapper().Map(entity.EmployeeFirstAider);
            }
            return dto;
        }

        //For overviewTab
        public AccidentRecordDto MapWithSiteAndAccidentRecordDocuments(AccidentRecord entity)
        {
            var dto = Map(entity);
            dto.SiteWhereHappened = entity.SiteWhereHappened != null ? new SiteDtoMapper().Map(entity.SiteWhereHappened) : null;  
            dto.AccidentRecordDocuments =
                entity.AccidentRecordDocuments.Select(
                    x =>
                    new AccidentRecordDocumentDto()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        DocumentLibraryId = x.DocumentLibraryId,
                        Filename = x.Filename,
                        DocumentType = new DocumentTypeDto()
                        {
                            Id = x.DocumentType.Id,
                            Name = x.DocumentType.Name
                        }
                    }).ToList();
            return dto;
        }

        public IEnumerable<AccidentRecordDto> Map(IEnumerable<AccidentRecord> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<AccidentRecordDto> MapWithEverything(IEnumerable<AccidentRecord> entities)
        {
            return entities.Select(MapWithEverything);
        }

        public IEnumerable<AccidentRecordDto> MapWithSiteAndCreatedAndEmployeeInjured(IEnumerable<AccidentRecord> entities)
        {
            return entities.Select(MapWithSiteAndCreatedAndEmployeeInjured);
        }
    }
}
