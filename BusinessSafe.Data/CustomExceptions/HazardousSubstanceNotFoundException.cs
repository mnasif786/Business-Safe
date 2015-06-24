using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class HazardousSubstanceNotFoundException : ArgumentNullException
    {
        public HazardousSubstanceNotFoundException(long id)
            : base(string.Format("Hazardous Substance Not Found. Hazardous Substance not found for hazardous substance id {0}", id))
        {
        }
    }
}