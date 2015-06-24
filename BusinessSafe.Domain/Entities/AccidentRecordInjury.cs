using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class AccidentRecordInjury : Entity<long>
    {
        public virtual AccidentRecord AccidentRecord { get; set; }
        public virtual Injury Injury { get; set; }
        public virtual string AdditionalInformation { get; set; }

        internal static AccidentRecordInjury Create(AccidentRecord accidentRecord, Injury injury, UserForAuditing user)
        {
            return new AccidentRecordInjury()
                       {
                           AccidentRecord = accidentRecord,
                           CreatedBy = user,
                           CreatedOn = DateTime.Now,
                           Deleted = false,
                           Injury = injury,
                       };
            
        }
    }
}
