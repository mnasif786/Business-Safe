using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class RiskAssessmentHazardDto
    {
        public long Id { get; set; }
        public virtual string Description { get; set; }
        public virtual HazardDto Hazard { get; set; }
        public virtual GeneralRiskAssessmentDto RiskAssessment { get; set; }
    }
}
