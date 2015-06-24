using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class AccidentRecordBodyPart : Entity<long>
    {
        public virtual AccidentRecord AccidentRecord { get; set; }
        public virtual BodyPart BodyPart { get; set; }
        public virtual string AdditionalInformation { get; set; }

        internal static AccidentRecordBodyPart Create(AccidentRecord accidentRecord, BodyPart bodyPart, UserForAuditing user)
        {
            return new AccidentRecordBodyPart()
            {
                AccidentRecord = accidentRecord,
                CreatedBy = user,
                CreatedOn = DateTime.Now,
                Deleted = false,
                BodyPart = bodyPart,
            };
        }
    }
}
