using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class HazardNotFoundException : ArgumentNullException
    {
        public HazardNotFoundException(long hazardId)
            : base(string.Format("Hazard Not Found. Hazard not found for hazard id {0}", hazardId))
        {
        }
    }
}