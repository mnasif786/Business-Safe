using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvaluationChecklist.Models;

namespace EvaluationChecklist.Helpers
{
    public interface IComplianceReviewReportViewModelFactory
    {        
        ComplianceReviewReportViewModel GetViewModel( ChecklistViewModel checklistViewModel );
    }
}
