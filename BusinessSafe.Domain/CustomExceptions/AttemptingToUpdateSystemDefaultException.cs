using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToUpdateSystemDefaultException : Exception
    {
        public AttemptingToUpdateSystemDefaultException(string name): base(string.Format("Trying to update system default. System default updating {0}", name))
        {}
    }
}