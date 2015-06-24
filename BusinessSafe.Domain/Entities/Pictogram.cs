using System;

using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Pictogram : Entity<long>
    {
        public virtual string Title { get; set; }
        public virtual HazardousSubstanceStandard HazardousSubstanceStandard { get; set; }
    }

    public class HazardousSubstancePictogram : Entity<long>
    {
        public virtual HazardousSubstance HazardousSubstance { get; set; }
        public virtual Pictogram Pictogram { get; set; }

        public static HazardousSubstancePictogram Create(HazardousSubstance hazardousSubstance, Pictogram pictogram, UserForAuditing creatingUser)
        {
            return new HazardousSubstancePictogram()
                   {
                       CreatedBy = creatingUser,
                       CreatedOn = DateTime.Now,
                       HazardousSubstance = hazardousSubstance,
                       Pictogram = pictogram
                   };
        }

    }

}
