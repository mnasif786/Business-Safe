using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.Application.Custom_Exceptions
{
        public class ActionTaskNotFoundException : NullReferenceException
        {
            public ActionTaskNotFoundException(long actionTaskId, long companyId)
                : base(string.Format("Could not find Action Task with id {0}, belonging to company {1}", actionTaskId, companyId))
            { }
        }
    
}
