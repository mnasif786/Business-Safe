using System;
using System.Runtime.Serialization;

namespace EvaluationChecklist.ClientDetails.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ChecklistDetails
    {
        public ChecklistDetails()
        {
            Id = null;
            CreatedOn = null;
            CreatedBy = string.Empty;
            VisitDate = null;
            VisitBy = string.Empty;
            Status = string.Empty;

        }
        [DataMember]
        public Guid? Id { get; set; }
        [DataMember]
        public DateTime? CreatedOn { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public DateTime? VisitDate { get; set; }
        [DataMember]
        public string VisitBy { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}