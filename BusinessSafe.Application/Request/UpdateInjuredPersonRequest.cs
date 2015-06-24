using BusinessSafe.Domain.Entities;
using System;

namespace BusinessSafe.Application.Request
{
    public class UpdateInjuredPersonRequest
    {
        public long AccidentRecordId { get; set; }

        public Guid CurrentUserId { get; set; }
        public long CompanyId { get; set; }


        public PersonInvolvedEnum? PersonInvolved { get; set; }
        public string PersonInvolvedOtherDescription { get; set; }
        public int? PersonInvolvedOtherDescriptionId { get; set; }
        public string PersonInvolvedOtherDescriptionOther { get; set; }
        public Guid? EmployeeInjuredId { get; set; }
        public string NonEmployeeInjuredForename { get; set; }
        public string NonEmployeeInjuredSurname { get; set; }
        public string NonEmployeeInjuredAddress1 { get; set; }
        public string NonEmployeeInjuredAddress2 { get; set; }
        public string NonEmployeeInjuredAddress3 { get; set; }
        public string NonEmployeeInjuredCountyState { get; set; }
        public int? NonEmployeeInjuredCountry { get; set; }
        public string NonEmployeeInjuredPostcode { get; set; }
        public string NonEmployeeInjuredContactNumber { get; set; }
        public string NonEmployeeInjuredOccupation { get; set; }
    }
}
