using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.Areas.AccidentReports.ViewModels
{
    public class SaveInjuryDetailsViewModel
    {
        public long AccidentRecordId { get; set; }
        public long CompanyId { get; set; }
        public SeverityOfInjuryEnum? SeverityOfInjury { get; set; }
        public long[] SelectedInjuryIds { get; set; }
        public long[] SelectedBodyPartIds { get; set; }
        public bool? InjuredPersonWasTakenToHospital { get; set; }
        public YesNoUnknownEnum? InjuredPersonAbleToCarryOutWork { get; set; }
        public LengthOfTimeUnableToCarryOutWorkEnum? LengthOfTimeUnableToCarryOutWork { get; set; }

    }

    public class InjuryDetailsViewModel
    {
        public long AccidentRecordId { get; set; }
        public long CompanyId { get; set; }
        public SeverityOfInjuryEnum? SeverityOfInjury { get; set; }       
        public List<LookupDto> Injuries { get; set; }
        public List<LookupDto> SelectedInjuries { get; set; }
        public string CustomInjuryDescription { get;  set; }
        public List<LookupDto> BodyParts { get; set; }
        public List<LookupDto> SelectedBodyParts { get; set; }
        public string CustomBodyPartyDescription { get;  set; }

        public bool? InjuredPersonWasTakenToHospital { get; set; }
        public YesNoUnknownEnum? InjuredPersonAbleToCarryOutWork { get; set; }
        public LengthOfTimeUnableToCarryOutWorkEnum? LengthOfTimeUnableToCarryOutWork { get; set; }
        public long[] SelectedInjuryIds { get; set; }
        public long[] SelectedBodyPartIds { get; set; }

        public string TakenToHospitalMessage { get; set; }
        public bool NextStepsVisible { get; set; }

        public string GuidanceNotesId { get; set; }
        public bool ShowInjuredPersonAbleToCarryOutWorkSection { get; set; }

        public InjuryDetailsViewModel()
        {
            Injuries = new List<LookupDto>();
            SelectedInjuries = new List<LookupDto>();
            BodyParts = new List<LookupDto>();
            SelectedBodyParts = new List<LookupDto>();
            SelectedInjuryIds = new long[0];
            SelectedBodyPartIds = new long[0];          
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType() != this.GetType())
                return false;

            var comp = (InjuryDetailsViewModel) obj;

            if (CustomInjuryDescription != comp.CustomInjuryDescription)
                return false;

            if (CustomBodyPartyDescription != comp.CustomBodyPartyDescription)
                return false;
          
            if (!SelectedInjuries.SequenceEqual(comp.SelectedInjuries))
                return false;

            if (!BodyParts.SequenceEqual(comp.BodyParts))
                return false;

            if (!SelectedBodyParts.SequenceEqual(comp.SelectedBodyParts))
                return false;

            return true;
        }

        
    }
}