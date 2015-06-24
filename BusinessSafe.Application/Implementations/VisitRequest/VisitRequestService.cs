using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.VisitRequest;
using BusinessSafe.Application.Request;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;

namespace BusinessSafe.Application.Implementations.VisitRequest
{
    public class VisitRequestService : IVisitRequestService
	{
        private readonly IBus _bus;
      
        public VisitRequestService( IBus bus)
        {
            _bus = bus;
        }

        public void SendVisitRequestedEmail(RequestVisitRequest request)
        {
            SendVisitRequestedEmail email = new SendVisitRequestedEmail();

            email.CompanyName = request.CompanyName;
            email.CAN = request.CAN;
            email.ContactName = request.ContactName;
            email.ContactEmail = request.ContactEmail;
            email.ContactPhone = request.ContactPhone;
            email.DateTo = request.DateTo;
            email.DateFrom = request.DateFrom;
            email.RequestTime = request.RequestTime;
            email.Comments = request.Comments;
            email.SiteName = request.SiteName;
            email.Postcode = request.Postcode; 

            _bus.Send( email );
        }
      
    }
} 