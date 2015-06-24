using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class AttemptingToDeleteSystemDefaultException : Exception
    {
        public AttemptingToDeleteSystemDefaultException(string name)
            : base(string.Format("Trying to delete system default. System default deleting {0}", name))
        { }
    }
}