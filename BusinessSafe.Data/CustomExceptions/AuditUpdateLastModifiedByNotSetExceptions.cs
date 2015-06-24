using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class AuditUpdateLastModifiedByNotSetExceptions : ApplicationException
    {
        public AuditUpdateLastModifiedByNotSetExceptions(string entityName, string entityId)
            : base(string.Format("Trying to create Audit Update. Last Modified By is not set. Entity Name is {0} and Entity Id is {1}", entityName, entityId))
        {
        }
    }

    public class AuditInsertCreatedByNotSetExceptions : ApplicationException
    {
        public AuditInsertCreatedByNotSetExceptions(string entityName, string entityId)
            : base(string.Format("Trying to create Audit Insert. Created By is not set. Entity Name is {0} and Entity Id is {1}", entityName, entityId))
        {
        }
    }
}