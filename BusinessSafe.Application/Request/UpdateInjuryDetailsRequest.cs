using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Request
{
    public class UpdateInjuryDetailsRequest
    {
        public long CompanyId { get; set; }
        public long AccidentRecordId { get; set; }
        public Guid CurrentUserId { get; set; }
        public SeverityOfInjuryEnum? SeverityOfInjury { get; set; }
        public List<long> Injuries { get; set; }
        public List<long> BodyPartsInjured { get; set; }
        public bool? WasTheInjuredPersonBeenTakenToHospital { get; set; }
        public YesNoUnknownEnum? HasTheInjuredPersonBeenAbleToCarryOutTheirNormalWorkActivity { get; set; }
        public LengthOfTimeUnableToCarryOutWorkEnum? LengthOfTimeUnableToCarryOutWork { get; set; }
        public string CustomInjuryDescription { get; set; }
        public string CustomBodyPartDescription { get; set; }
    }
}