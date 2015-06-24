using System;

namespace BusinessSafe.Application.Implementations
{
    public class TryingToDeleteSupplierThatUsedByHazardousSubstanceException : ArgumentException
    {
        public TryingToDeleteSupplierThatUsedByHazardousSubstanceException(long supplierId)
            : base(string.Format("Trying to delete supplier that is used by hazardous substance. Supplier Id is {0}", supplierId))
        {}
    }
}