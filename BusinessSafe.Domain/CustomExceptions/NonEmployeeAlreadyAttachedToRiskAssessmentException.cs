using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class NonEmployeeAlreadyAttachedToRiskAssessmentException: ApplicationException
    {
        public NonEmployeeAlreadyAttachedToRiskAssessmentException(RiskAssessment  riskAssessment, NonEmployee nonEmployee)
            : base(string.Format("Trying to attach non employee to risk assessment. Risk Assessment Id {0}. Non Employee Id {1}. Risk Assessment Type : {2}", riskAssessment.Id, nonEmployee.Id, ExceptionRiskAssessmentTypeHelper.WhatTypeOfRiskAssessment(riskAssessment)))
        {}
    }
}