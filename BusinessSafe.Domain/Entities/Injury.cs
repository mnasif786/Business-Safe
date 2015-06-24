using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Injury : Entity<long>
    {
        public static long UNKOWN_INJURY = 16;
        public virtual string Description { get; set; }
        public virtual long? CompanyId { get; set; }
        public virtual long? AccidentRecordId { get; set; }

        public static Injury Create(string name, long companyId, long? accidentRecordId, UserForAuditing user)
        {
            var injury = new Injury
                             {
                                 Description = name,
                                 CompanyId = companyId,
                                 AccidentRecordId = accidentRecordId,
                                 CreatedOn = DateTime.Now,
                                 CreatedBy = user
                             };

            return injury;
        }
    }
}
