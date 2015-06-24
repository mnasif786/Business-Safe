using System;

namespace BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.AccidentDetails
{
    public class UpdateAccidentRecordAccidentDetailsRequest
    {
        public long CompanyId { get; set; }
        public long AccidentRecordId { get; set; }
        public DateTime? DateOfAccident { get; set; }
        public long? SiteId { get; set; }
        public string OffSiteName { get; set; }
        public string Location { get; set; }
        public long? AccidentTypeId { get; set; }
        public string OtherAccidentType { get; set; }
        public long? AccidentCauseId { get; set; }
        public string OtherAccidentCause { get; set; }
        public bool FirstAidAdministered { get; set; }
        public Guid? FirstAiderEmployeeId { get; set; }
        public string NonEmployeeFirstAiderName { get; set; }
        public string DetailsOfFirstAid { get; set; }
        public Guid UserId { get; set; }

        public static UpdateAccidentRecordAccidentDetailsRequest Create(long companyId, long accidentRecordId,
                                                                        string dateOfAccident, string timeOfAccident,
                                                                        long? siteId, string offSiteName,
                                                                        string location, long? accidentTypeId,
                                                                        string otherAccidentType, long? accidentCauseId,
                                                                        string otherAccidentCause,
                                                                        bool firstAidAdministered,
                                                                        Guid? firstAiderEmployeeId,
                                                                        string nonEmployeeFirstAiderName,
                                                                        string detailsOfFirstAid, Guid userId)
        {
            return new UpdateAccidentRecordAccidentDetailsRequest
                       {
                           CompanyId = companyId,
                           AccidentRecordId = accidentRecordId,
                           DateOfAccident = SetDateAndTimeOfAccident(dateOfAccident,timeOfAccident),
                           SiteId = siteId.HasValue  ? siteId.Value : default(long?),
                           OffSiteName = offSiteName,
                           Location = location,
                           AccidentTypeId = accidentTypeId,
                           OtherAccidentType = otherAccidentType,
                           AccidentCauseId =accidentCauseId,
                           OtherAccidentCause = otherAccidentCause,
                           FirstAidAdministered = firstAidAdministered,
                           FirstAiderEmployeeId = firstAiderEmployeeId,
                           NonEmployeeFirstAiderName = nonEmployeeFirstAiderName,
                           DetailsOfFirstAid = detailsOfFirstAid,
                           UserId = userId
                       };
        }

        private static DateTime? SetDateAndTimeOfAccident(string dateOfAccident,string timeOfAccident)
        {
            DateTime? date = null;
            DateTime d2;
            bool success = DateTime.TryParse(dateOfAccident, out d2);
            if (success) date = d2;

            DateTime? time = null;
            DateTime t2;
            success = DateTime.TryParse(timeOfAccident, out t2);
            if (success) time = t2;


            DateTime? dateAndTimeOfAccident = null;
            if (date.HasValue && time.HasValue)
            {
                dateAndTimeOfAccident = date.Value.Date.Add(time.Value.TimeOfDay);
            }
            else if (date.HasValue)
            {
                dateAndTimeOfAccident = date.Value;
            }
            return dateAndTimeOfAccident;
        }
    }
}