using System;
using System.Collections.Generic;
using System.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Linq;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Data.Queries.SafeCheck
{
    
    public interface IGetCompleteChecklistsQuery
    {
        List<ChecklistIndex> Execute(string consultantName);
        int Count(string consultantName);
    }

    public class GetCompletedChecklistsQuery: IGetCompleteChecklistsQuery
    {
        private readonly IQueryable<Checklist> _queryableChecklist;
        public GetCompletedChecklistsQuery(IQueryableWrapper<Checklist> queryableChecklist)
        {
            _queryableChecklist = queryableChecklist.Queryable();
        }

        private IQueryable<Checklist> CreateQuery(string consultantName)
        {
            if (string.IsNullOrEmpty(consultantName) == false && consultantName.Length <= 2)
            {
                throw new ParameterLessQueryNotAllowedException("When querying by consultant name, enter more than 2 characters");
            }

            var query = _queryableChecklist;
            query = query.Where(x => x.ChecklistCreatedBy == consultantName 
                && !x.Deleted
                && x.Status != Checklist.STATUS_DRAFT);

            return query;
        }

        public List<ChecklistIndex> Execute(string consultantName)
        {
            var query = CreateQuery(consultantName);

            return query.Select(x => new ChecklistIndex()
                                         {
                                             ClientId = x.ClientId
                                             ,
                                             CreatedBy = x.ChecklistCreatedBy
                                             ,
                                             CreatedOn = x.ChecklistCreatedOn
                                             ,
                                             Deleted = x.Deleted

                                             ,
                                             HasQaComments =
                                                 x.Answers.Any(ans => ans.QaComments != null && ans.QaComments != "")
                                             //Warning: Do not use string.IsNullOrEmpty because this cant be translated in to SQL by the nhibernate to linq provider
                                             ,
                                             HasResolvedQaComments =
                                                 x.Answers.Any(
                                                     ans =>
                                                     ans.QaComments != null && ans.QaComments != "" &&
                                                     ans.QaCommentsResolved == true)
                                             ,
                                             HasSignedOffQaComments =
                                                 x.Answers.Any(
                                                     ans =>
                                                     ans.QaComments != null && ans.QaComments != "" &&
                                                     ans.QaSignedOffBy != null)
                                             ,
                                             Id = x.Id


                                             ,
                                             Status = x.Status
                                             ,
                                             VisitBy = x.VisitBy
                                             ,
                                             VisitDate = x.VisitDate
                                             ,
                                             QaAdvisorId = x.QaAdvisor != null ? x.QaAdvisor.Id : (Guid?) null
                                             ,
                                             SiteId = x.SiteId
                                             ,
                                             ExecutiveSummaryDocumentLibraryId = x.ExecutiveSummaryDocumentLibraryId
                                         }).ToList();
        }

        public int Count(string consultantName)
        {
            var query = CreateQuery(consultantName);
            return query.Count();
        }
    }
}
