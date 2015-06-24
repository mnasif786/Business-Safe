using System;
using System.Web;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class AccidentRecordEmailViewModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string DateOfAccident;
        public string TimeOfAccident;
        public string Site;
        public string Location;
        public PersonInvolvedEnum? PersonInvolved;
        public SeverityOfInjuryEnum? SeverityOfInjury;
        public string Injuries;
        public bool? TakenToHospital;
        public string ChainOfEvents;
        public bool RiddorReportable;
        public HtmlString BusinessSafeOnlineLink;
    }
}
