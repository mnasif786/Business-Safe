using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class AccidentRecordDto
    {
        public AccidentRecordDto()
        {
            AccidentRecordInjuries = new List<AccidentRecordInjuryDto>();
            AccidentRecordBodyParts = new List<AccidentRecordBodyPartDto>();
            AccidentRecordDocuments = new List<AccidentRecordDocumentDto>();
            AccidentRecordNextStepSections = new List<AccidentRecordNextStepSectionDto>();
        }

        public long Id { get; set; } 
        public long CompanyId { get; set; }
        public string Title { get; set; }
        public string Reference { get; set; }
        public JurisdictionDto Jurisdiction { get; set; }
        public PersonInvolvedEnum? PersonInvolved { get; set; }
        public string PersonInvolvedOtherDescription { get; set; }
        public int? PersonInvolvedOtherDescriptionId { get; set; }
        public EmployeeDto EmployeeInjured { get; set; }
        public string NonEmployeeInjuredForename { get; set; }
        public string NonEmployeeInjuredSurname { get; set; }
        public string NonEmployeeInjuredAddress1 { get; set; }
        public string NonEmployeeInjuredAddress2 { get; set; }
        public string NonEmployeeInjuredAddress3 { get; set; }
        public string NonEmployeeInjuredCountyState { get; set; }
        public CountryDto NonEmployeeInjuredCountry { get; set; }
        public string NonEmployeeInjuredPostcode { get; set; }
        public string NonEmployeeInjuredContactNumber { get; set; }
        public string NonEmployeeInjuredOccupation { get; set; }
        public DateTime? DateAndTimeOfAccident { get; set; }
        public SiteDto SiteWhereHappened { get; set; }
        public string OffSiteSpecifics { get; set; }
        public string Location { get; set; }
        public AccidentTypeDto AccidentType { get; set; }
        public string AccidentTypeOther { get; set; }
        public CauseOfAccidentDto CauseOfAccident { get; set; }
        public string CauseOfAccidentOther { get; set; }
        public bool? FirstAidAdministered { get; set; }
        public EmployeeDto EmployeeFirstAider { get; set; }
        public string NonEmployeeFirstAiderSpecifics { get; set; }
        public string DetailsOfFirstAidTreatment { get; set; }
        public SeverityOfInjuryEnum? SeverityOfInjury { get; set; }
        public IList<AccidentRecordInjuryDto> AccidentRecordInjuries { get; set; }
        public IList<AccidentRecordBodyPartDto> AccidentRecordBodyParts { get; set; }
        public bool? InjuredPersonWasTakenToHospital { get; set; }
        public YesNoUnknownEnum? InjuredPersonAbleToCarryOutWork { get; set; }
        public LengthOfTimeUnableToCarryOutWorkEnum? LengthOfTimeUnableToCarryOutWork { get; set; }
        public string DescriptionHowAccidentHappened { get; set; }
        public IList<AccidentRecordDocumentDto> AccidentRecordDocuments { get; set; }
        public bool NextStepsAvailable { get; set; }
        public DateTime? CreatedOn { get; set; }
        public AuditedUserDto CreatedBy { get; set; }
        public virtual string InjuredPersonFullName { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<AccidentRecordNextStepSectionDto> AccidentRecordNextStepSections { get; set; }
		public string AdditionalInjuryInformation { get; set; }
        public string AdditionalBodyPartInformation { get; set; }
		public bool IsReportable { get; set; }
        public bool Status { get; set; }
        public bool DoNotSendEmailNotification { get; set; }
        public virtual bool EmailNotificationSent { get; set; }
    }
}
