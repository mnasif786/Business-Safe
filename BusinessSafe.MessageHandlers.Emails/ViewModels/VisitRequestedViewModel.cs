﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class VisitRequestedViewModel
    {
        public string From { get; set; }       
        public string To { get; set; }
        public string Subject { get; set; }
             
        public string CompanyName     { get; set; }
        public string CAN { get; set; }
            
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        public string DateTo { get; set; }
        public string DateFrom { get; set; }
        public string RequestTime { get; set; }

        public string Comments { get; set; }
        public string SiteName { get; set; }
        public string Postcode { get; set; }

        public VisitRequestedViewModel()
        {
            From = "BusinessSafeProject@peninsula-uk.com";
            To = "BusinessSafeDiaryBooking@peninsula-uk.com";

            Subject = "Visit Requested";
        }
    }
}