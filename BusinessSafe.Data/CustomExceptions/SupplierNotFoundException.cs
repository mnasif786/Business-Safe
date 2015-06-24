using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class SupplierNotFoundException : ArgumentNullException
    {
        public SupplierNotFoundException(long supplierId)
            : base(string.Format("Supplier Not Found. Supplier not found for supplier id {0}", supplierId))
        {
        }
    }
}