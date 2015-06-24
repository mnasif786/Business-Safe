﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NHibernate;

namespace BusinessSafe.Data.Queries.DueTaskQuery
{
    public interface IGetDueRiskAssessmentReviewTasksForEmployeeQuery
    {
        List<RiskAssessmentReviewTask> Execute(Guid employeeId, ISession session);
    }
}