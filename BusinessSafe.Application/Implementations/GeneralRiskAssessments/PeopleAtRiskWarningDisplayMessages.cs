using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Implementations.GeneralRiskAssessments
{
    public static class PeopleAtRiskWarningDisplayMessages
    {
        public const int NewAndExpectantMothersId = 7;
        public const int ChildrenAndYoungPersonsId = 9;
        private const string NewAndExpectantMothersChildrenAndYoungPeopleDisplayMessage = "Remember that Personal Risk Assessments should be completed for individual employees in this category. The checklists associated with personal assessments may also be helpful when assessing more generic risks to these people. The checklists form CYPIC, Children and Young Persons Checklist, and form NEMA, New and Expectant Mothers Risk Assessment Checklist can be found at My Documentation – <a href='/Documents/ReferenceDocumentsLibrary?companyId={0}'>Reference Library</a> in the document type Risk Assessment Checklist.";
        private static readonly Dictionary<long[], string> Messages = new Dictionary<long[], string>();

        static PeopleAtRiskWarningDisplayMessages()
        {
            Messages.Add(new long[] {NewAndExpectantMothersId,ChildrenAndYoungPersonsId}, NewAndExpectantMothersChildrenAndYoungPeopleDisplayMessage);
        }

        public static IEnumerable<string> GetMessages(IEnumerable<PeopleAtRisk> peopleAtRisk, long companyId)
        {
            var selectedPeopleAtRiskIds = peopleAtRisk.Select(x => x.Id);
            var result = (from personAtRiskId in selectedPeopleAtRiskIds
                          from key in Messages.Keys
                          where key.Contains(personAtRiskId)
                          select string.Format(Messages[key], companyId)).ToList();

            return result.Distinct();
        }

        public static string Json(long companyId)
        {
            var result = new StringBuilder();
            result.Append("{");
            result.Append("\"DisplayMessages\" : [");
            result.Append("{");
            result.AppendFormat("\"IdsApplicable\" : [{0},{1}],", NewAndExpectantMothersId, ChildrenAndYoungPersonsId);
            result.AppendFormat("\"MessageToShow\" : \"{0}\"", string.Format(NewAndExpectantMothersChildrenAndYoungPeopleDisplayMessage,companyId));
            result.Append("}");
            result.Append("]");
            result.Append("}");
            return result.ToString();

        }
    }
}