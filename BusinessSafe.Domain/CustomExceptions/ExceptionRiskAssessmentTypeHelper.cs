using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public static class ExceptionRiskAssessmentTypeHelper
    {
        public static string WhatTypeOfRiskAssessment(RiskAssessment riskAssessment)
        {
            return riskAssessment.GetType().Name;
        }
    }
}