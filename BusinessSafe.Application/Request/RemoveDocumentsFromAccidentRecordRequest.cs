using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.Request
{
    public class RemoveDocumentsFromAccidentRecordRequest
    {
        public long AccidentRecordId { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
        /// <summary>
        /// list of document library ids to remove from accident record
        /// </summary>
        public List<long> DocumentLibraryIds { get; set; }
        
     
    }


}
