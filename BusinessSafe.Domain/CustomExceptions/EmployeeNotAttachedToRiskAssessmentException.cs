using System;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.CustomExceptions
{
    public class EmployeeNotAttachedToRiskAssessmentException: ApplicationException
    {
        public EmployeeNotAttachedToRiskAssessmentException(RiskAssessment riskAssessment, Employee employee)
            : base(string.Format("Trying to detach employee from risk assessment. Risk Assessment Id {0}. Employee Id {1}. Risk Assessment Type : {2}", riskAssessment.Id, employee.Id, ExceptionRiskAssessmentTypeHelper.WhatTypeOfRiskAssessment(riskAssessment)))
        {}
    }
}