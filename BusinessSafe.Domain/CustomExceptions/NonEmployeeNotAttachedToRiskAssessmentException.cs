using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class NonEmployeeNotAttachedToRiskAssessmentException: ApplicationException
    {
        public NonEmployeeNotAttachedToRiskAssessmentException(RiskAssessment riskAssessment, NonEmployee nonEmployee)
            : base(string.Format("Trying to detach non employee from risk assessment. Risk Assessment Id {0}. Non Employee Id {1}. Risk Assessment Type : {2}", riskAssessment.Id, nonEmployee.Id, ExceptionRiskAssessmentTypeHelper.WhatTypeOfRiskAssessment(riskAssessment)))
        {}
    }
}