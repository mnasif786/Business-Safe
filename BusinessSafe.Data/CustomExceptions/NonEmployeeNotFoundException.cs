using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class NonEmployeeNotFoundException : ArgumentNullException
    {
        public NonEmployeeNotFoundException(long nonEmployeeId)
            : base(string.Format("Non Employee Not Found. Non Employee not found for non employee id {0}", nonEmployeeId))
        {
        }
    }
}