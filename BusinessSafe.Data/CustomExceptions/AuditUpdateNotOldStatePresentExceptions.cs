using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class AuditUpdateNotOldStatePresentExceptions : ApplicationException
    {
        public AuditUpdateNotOldStatePresentExceptions(string entityName, string entityId)
            : base("No old state available for entity type '" + entityName +
                   "'. Make sure you're loading it into Session before modifying and saving it. Entity Id is " + entityId)
        {
        }
    }

    public class AuditDeleteLastModifiedByNotSetExceptions : ApplicationException
    {
        public AuditDeleteLastModifiedByNotSetExceptions(string entityName, string entityId)
            : base(string.Format("Trying to create Audit Delete. Last Modified By is not set. Entity Name is {0} and Entity Id is {1}", entityName, entityId))
        {
        }
    }
    
}