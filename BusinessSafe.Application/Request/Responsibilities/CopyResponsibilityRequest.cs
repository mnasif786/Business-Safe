using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.Request.Responsibilities
{
    public class CopyResponsibilityRequest
    {
        public long OriginalResponsibilityId { get; set; }
        public string Title { get; set; }
        public long CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}
