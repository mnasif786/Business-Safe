using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.VisitRequest
{
    public interface IVisitRequestService
    {
        void SendVisitRequestedEmail(RequestVisitRequest request);
    }
}
