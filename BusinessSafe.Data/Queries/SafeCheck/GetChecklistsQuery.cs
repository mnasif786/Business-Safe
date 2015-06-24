using System;
using System.Collections.Generic;
using System.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Linq;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Data.Queries.SafeCheck
{
    public class ChecklistIndex
    {
        public Guid Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string VisitBy { get; set; }
        public string Status { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? SiteId { get; set; }
        public int? ClientId { get; set; }
        public Guid? QaAdvisorId { get; set; }
        public string QaAdvisorInitials { get; set; }
        public bool HasQaComments { get; set; }
        public bool Deleted { get; set; }

        /// <summary>
        /// has qa comments, qa NOT signed off and consultant NOT revised
        /// </summary>
        //public bool HasUnresolvedQaComments { get; set; }

        public string QaForename { get; set; }

        public string QaSurname { get; set; }
        public long? ExecutiveSummaryDocumentLibraryId { get; set; }

        public DateTime? CompletedDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? QaAdvisorAssignedOn { get; set; }

        public bool ExecutiveSummaryUpdateRequired { get; set; }
        public bool ExecutiveSummaryQACommentsResolved { get; set; }

        public bool HasResolvedQaComments  { get; set; }        
        public bool HasSignedOffQaComments  { get; set; }

        public bool ExecutiveSummaryQASignedOff { get; set; }

        public string TemplateName { get; set; }

        public string QACommentStatus()
        {
            // SGG/ALP: This logic is clearly horrible but it works. REALLY needs refactoring at 
            // a later date. Be extremely careful though - getting all the variations correct for the 
            // UI is complex.
            //
            string result = "None";

            if (HasSignedOffQaComments && ExecutiveSummaryQASignedOff)
            {
                return "None";
            }

            if (!HasQaComments && ExecutiveSummaryQASignedOff)
            {
                return "None";
            }

            if (!ExecutiveSummaryUpdateRequired && HasSignedOffQaComments)
            {
                return "None";
            }


            if ((HasQaComments && !HasResolvedQaComments && !HasSignedOffQaComments)
                || (ExecutiveSummaryUpdateRequired && !ExecutiveSummaryQACommentsResolved)
                )
            {
                return "HasUnresolvedQaComments";
            }

            if (ExecutiveSummaryUpdateRequired && !ExecutiveSummaryQACommentsResolved)
            {
                return "HasUnresolvedQaComments";
            }

            if ( HasResolvedQaComments && !HasSignedOffQaComments)
            {
                return "AllQaCommentsResolved";
            }

            if (ExecutiveSummaryQACommentsResolved && !ExecutiveSummaryQASignedOff)
            {
                return "AllQaCommentsResolved";
            }

            if (!HasQaComments && ExecutiveSummaryUpdateRequired && ExecutiveSummaryQACommentsResolved)
            {
                return "AllQaCommentsResolved";
            }

            return result;
        }
    }

    public class GetChecklistsQuery: IGetChecklistsQuery
    {
        private IQueryable<Checklist> _queryableChecklist;
        private bool _includeDeletedOnly = false;
        private string _consultantName;
        private Guid? _qaAdvisorId;
        private DateTime? _fromDate;
        private DateTime? _toDate;
        private string _status;


        public GetChecklistsQuery(IQueryableWrapper<Checklist> queryableChecklist)
        {
            _queryableChecklist = queryableChecklist.Queryable();
        }

        public IGetChecklistsQuery WithClientId(int clientId)
        {
            if (clientId > default(int))
            {
                _queryableChecklist = _queryableChecklist.Where(x => x.ClientId == clientId);
            }
            return this;
        }

        public IGetChecklistsQuery WithConsultantName(string consultantName)
        {
            _consultantName = consultantName;
            return this;

        }

        public IGetChecklistsQuery WithVisitDate(DateTime visitDate)
        {
            _queryableChecklist = _queryableChecklist.Where(x => x.VisitDate != null && x.VisitDate.Value.Date == visitDate.Date);
            return this;
        }

        public IGetChecklistsQuery WithStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                _status = status;
                _queryableChecklist = _queryableChecklist.Where(x => x.Status == status);
            }

            return this;
        }

        public IGetChecklistsQuery WithDeletedOnly()
        {
            _includeDeletedOnly = true;
            return this;
        }

        public IGetChecklistsQuery ExcludeSubmitted()
        {
            _queryableChecklist = _queryableChecklist.Where(x => x.Status != Checklist.STATUS_SUBMITTED);
            
            return this;
        }

        public IGetChecklistsQuery WithQaAdvisor(Guid qaAdvisorId)
        {
            _qaAdvisorId = qaAdvisorId;
            return this;
        }

        public IGetChecklistsQuery WithStatusDateBetween(DateTime? fromDate, DateTime? toDate)
        {
            _fromDate = fromDate;

            if (toDate.HasValue)
            {
                _toDate = new DateTime(toDate.Value.Year, toDate.Value.Month, toDate.Value.Day, 23,59,59,0);
            }
            return this;
        }

        private IQueryable<Checklist> CreateQuery()
        {
            var query = _queryableChecklist;

            if (!string.IsNullOrEmpty(_consultantName) && _qaAdvisorId.HasValue)
            {
                query = query.Where(x => x.ChecklistCreatedBy == _consultantName || ( x.QaAdvisor.Id == _qaAdvisorId && (x.Status != "Submitted")) );
            }
            else if (!string.IsNullOrEmpty(_consultantName))
            {
                query = query.Where(x => x.ChecklistCreatedBy == _consultantName);
            }
            else if (_qaAdvisorId.HasValue)
            {
                // Don't match by QA Advisor if report has been submitted
                query = query.Where(x =>( (x.QaAdvisor.Id == _qaAdvisorId) && (x.Status != "Submitted")));
            }

            query = applyStatusDateFilter(query);

            query = !_includeDeletedOnly ? query.Where(x => !x.Deleted) : query.Where(x => x.Deleted);

            return query;
        }

        private IQueryable<Checklist> applyStatusDateFilter(IQueryable<Checklist> query)
        {
            if (_includeDeletedOnly)
            {
                if (_fromDate.HasValue)
                {
                    query = query.Where(x => x.LastModifiedOn >= _fromDate);
                }
                if (_toDate.HasValue)
                {
                    query = query.Where(x => x.LastModifiedOn <= _toDate);
                }
            }
            else
            {
                switch (_status)
                {
                    case Checklist.STATUS_DRAFT:
                        if (_fromDate.HasValue)
                        {
                            query = query.Where(x => x.ChecklistCreatedOn >= _fromDate);
                        }
                        if (_toDate.HasValue)
                        {
                            query = query.Where(x => x.ChecklistCreatedOn <= _toDate);
                        }
                        break;
                    case Checklist.STATUS_COMPLETED:
                        if (_fromDate.HasValue)
                        {
                            query = query.Where(x => x.ChecklistCompletedOn >= _fromDate);
                        }
                        if (_toDate.HasValue)
                        {
                            query = query.Where(x => x.ChecklistCompletedOn <= _toDate);
                        }
                        break;
                    case Checklist.STATUS_ASSIGNED:
                        if (_fromDate.HasValue)
                        {
                            query = query.Where(x => x.QaAdvisorAssignedOn >= _fromDate);
                        }
                        if (_toDate.HasValue)
                        {
                            query = query.Where(x => x.QaAdvisorAssignedOn <= _toDate);
                        }
                        break;
                    case Checklist.STATUS_SUBMITTED:
                        if (_fromDate.HasValue)
                        {
                            query = query.Where(x => x.ChecklistSubmittedOn >= _fromDate);
                        }
                        if (_toDate.HasValue)
                        {
                            query = query.Where(x => x.ChecklistSubmittedOn <= _toDate);
                        }
                        break;
                    default:
                        break;
                }
            }

            return query;
        }

        public List<ChecklistIndex> Execute()
        {
            var query = CreateQuery();

            return query.Select(x => new ChecklistIndex()
                                         {
                                             ClientId = x.ClientId
                                             ,CreatedBy = x.ChecklistCreatedBy
                                             ,CreatedOn = x.ChecklistCreatedOn
                                             ,Deleted = x.Deleted
                                             ,HasQaComments = x.Answers.Any(ans => ans.QaComments != null && ans.QaComments != "") //Warning: Do not use string.IsNullOrEmpty because this cant be translated in to SQL by the nhibernate to linq provider
                                             ,HasResolvedQaComments = !x.Answers.Any(ans => ans.QaComments != null && ans.QaComments != "" && ans.QaCommentsResolved == false)
                                             ,HasSignedOffQaComments = !x.Answers.Any(ans => ans.QaComments != null && ans.QaComments != "" && ans.QaSignedOffBy == null )
                                             ,Id =x.Id
                                             ,Status = x.Status
                                             ,VisitBy =x.VisitBy
                                             ,VisitDate = x.VisitDate
                                             ,QaAdvisorId = x.QaAdvisor !=null ? x.QaAdvisor.Id : (Guid?)null
                                             ,SiteId = x.SiteId
                                             ,ExecutiveSummaryDocumentLibraryId = x.ExecutiveSummaryDocumentLibraryId
                                             ,CompletedDate = x.ChecklistCompletedOn
                                             ,SubmittedDate = x.ChecklistSubmittedOn
                                             ,UpdatedOn = x.LastModifiedOn
                                             ,QaAdvisorAssignedOn = x.QaAdvisorAssignedOn
                                             ,ExecutiveSummaryUpdateRequired  = x.ExecutiveSummaryUpdateRequired  
                                             ,ExecutiveSummaryQACommentsResolved = x.ExecutiveSummaryQACommentsResolved
                                             ,ExecutiveSummaryQASignedOff = x.ExecutiveSummaryQACommentsSignedOffDate.HasValue
                                             ,TemplateName = x.ChecklistTemplate != null ? x.ChecklistTemplate.Name : null
                                             
                                         }).ToList();
        }

        public int Count(int? clientId, string consultantName, DateTime? visitDate, string status, bool includeDeleted, bool excludeSubmitted, Guid? QaAdvisorId)
        {
            var query = CreateQuery();

            return query.Count();
        }
    }

    public class ParameterLessQueryNotAllowedException : Exception
    {
        public ParameterLessQueryNotAllowedException(string message)
            : base(message)
        {

        }
    }

    /// <summary>
    /// We need this wrapper because the nhibernate Query method is an extension method and they are difficult to stub
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryableWrapper<T>
    {
        IQueryable<T> Queryable();
    }

    public class NHibernateQueryable<T> : IQueryableWrapper<T>
    {
        private readonly IBusinessSafeSessionManager _sessionManager;
        public NHibernateQueryable(IBusinessSafeSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public IQueryable<T> Queryable()
        { 
            return _sessionManager.Session.Query<T>();
        }
    }
}
