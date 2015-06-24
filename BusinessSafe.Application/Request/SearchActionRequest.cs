using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Request
{
    public class SearchActionRequest
    {
        public long ActionPlanId { get; set; }
        public string Status { get; set; }
        public Guid AssignedTo  { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }

        public ActionOrderByColumn OrderBy { get; set; }
        public bool Ascending { get; set; }

    }
}
