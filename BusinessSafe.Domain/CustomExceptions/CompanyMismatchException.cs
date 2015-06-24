using System;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class CompanyMismatchException<TFirstEntity, TSecondEntity> : Exception
    {
        public CompanyMismatchException()
            : base(
                "Attempt to set child entity when parent entity belongs to different company. Affected entities are: " +
                typeof (TFirstEntity).ToString() + ", " + typeof (TSecondEntity).ToString())
        {
        }
    }
}
