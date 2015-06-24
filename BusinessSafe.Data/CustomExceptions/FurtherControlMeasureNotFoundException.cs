using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class FurtherControlMeasureNotFoundException : NullReferenceException
    {
        public FurtherControlMeasureNotFoundException(long furtherControlMeasureId)
            : base(string.Format("Further Control Measure Not Found. Further Control Measure not found for id {0}", furtherControlMeasureId))
        {
        }
    }
}