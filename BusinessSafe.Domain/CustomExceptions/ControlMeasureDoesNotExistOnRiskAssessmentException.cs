using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class ControlMeasureDoesNotExistOnRiskAssessmentException: ApplicationException
    {
        public ControlMeasureDoesNotExistOnRiskAssessmentException(RiskAssessment riskAssessment, long controlMeasureId)
            : base(string.Format("Could not find control measure for Risk Assessment. Risk Assessment Id {0}. Control Measure Id {1}. Risk Assessment Type : {2}", riskAssessment.Id, controlMeasureId, ExceptionRiskAssessmentTypeHelper.WhatTypeOfRiskAssessment(riskAssessment)))
        {}
    }
}