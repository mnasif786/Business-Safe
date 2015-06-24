using System;

namespace BusinessSafe.WebSite.Controllers
{
    public class MultipleReassignToIdsSpecifiedInBulkReassignRequestException: ArgumentException
    {
        public MultipleReassignToIdsSpecifiedInBulkReassignRequestException(): base("Multiple Reassign Task Ids specified in the Bulk Reassign Request")
        {
            
        }
    }
}