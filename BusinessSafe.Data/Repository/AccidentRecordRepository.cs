using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Criterion;
using System;
using NHibernate;
using NHibernate.SqlCommand;

namespace BusinessSafe.Data.Repository
{
    public class AccidentRecordRepository : Repository<AccidentRecord, long>, IAccidentRecordRepository
    {
        public AccidentRecordRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public AccidentRecord GetByIdAndCompanyId(long accidentRecordId, long companyId)
        {
            var result = SessionManager
            .Session
            .CreateCriteria<AccidentRecord>()
            .Add(Restrictions.Eq("Id", accidentRecordId))
            .Add(Restrictions.Eq("CompanyId", companyId))
            .SetMaxResults(1)
            .UniqueResult<AccidentRecord>();

            if (result != null && result.SiteWhereHappened != null)
            {
                //load site to prevent the error, "collection [BusinessSafe.Domain.Entities.SiteStructureElement.Children] was not processed by flush() " 
                NHibernateUtil.Initialize(result.SiteWhereHappened);
            }

            return result;
        }

        public int Count(IList<long> allowedSiteIds, long companyId, long? siteId, 
                         string title, DateTime? createdFrom, DateTime? createdTo, bool showDeleted,
                         string injuredPersonForename, string injuredPersonSurname)
        {
            var criteria = CreateCriteria(allowedSiteIds, companyId,
                                   siteId, title, createdFrom,
                                   createdTo, showDeleted, injuredPersonForename,
                                   injuredPersonSurname);

            var count = criteria.SetProjection(Projections.RowCount()).FutureValue<Int32>();
            return count.Value;
        }

        public IEnumerable<AccidentRecord> Search(IList<long> allowedSiteIds, long companyId, long? siteId, string title, DateTime? createdFrom, DateTime? createdTo, bool showDeleted, string injuredPersonForename, string injuredPersonSurname, int page, int pageSize, AccidentRecordstOrderByColumn orderBy, bool ascending)
        {
            var criteria = CreateCriteria(allowedSiteIds, companyId,
                                       siteId, title, createdFrom,
                                       createdTo, showDeleted, injuredPersonForename,
                                       injuredPersonSurname);

            var columnName = GetOrderColumnName(orderBy);
            criteria.AddOrder(new Order(columnName, ascending));


            if (page != default(int))
            {
                criteria.SetFirstResult(page == 1 ? 0 : (page - 1) * pageSize).SetMaxResults(pageSize);
            }

            return criteria.List<AccidentRecord>();
        }

        private string GetOrderColumnName(AccidentRecordstOrderByColumn orderBy)
        {
            string columnName;

            switch (orderBy)
            {
                case AccidentRecordstOrderByColumn.None:
                    columnName = "CreatedOn";
                    break;
                case AccidentRecordstOrderByColumn.Reference:
                    columnName = "Reference";
                    break;
                case AccidentRecordstOrderByColumn.Title:
                    columnName = "Title";
                    break;
                case AccidentRecordstOrderByColumn.Description:
                    columnName = "DescriptionHowAccidentHappened";
                    break;
                case AccidentRecordstOrderByColumn.InjuredPerson:
                    columnName = "employeeInjured.Surname";
                    break;
                case AccidentRecordstOrderByColumn.Severity:
                    columnName = "SeverityOfInjury";
                    break;
                case AccidentRecordstOrderByColumn.Site:
                    columnName = "site.Name";
                    break;
                case AccidentRecordstOrderByColumn.ReportedBy:
                    columnName = "CreatedBy";
                    break;
                case AccidentRecordstOrderByColumn.Status:
                    columnName = "Status";
                    break;
                case AccidentRecordstOrderByColumn.DateOfAccident:
                    columnName = "DateAndTimeOfAccident";
                    break;
                case AccidentRecordstOrderByColumn.DateCreated:
                    columnName = "CreatedOn";
                    break;
                default:
                    columnName = "CreatedOn";
                    break;
            }
            return columnName;
        }

        private ICriteria CreateCriteria(IList<long> allowedSiteIds, long companyId,
                                         long? siteId, string title, DateTime? createdFrom,
                                         DateTime? createdTo, bool showDeleted, string injuredPersonForename,
                                         string injuredPersonSurname)
        {
            var criteria = SessionManager.Session.CreateCriteria<AccidentRecord>()
                .Add(Restrictions.Eq("CompanyId", companyId))
                .Add(Restrictions.Eq("Deleted", showDeleted));

            criteria.CreateAlias("SiteWhereHappened", "site", JoinType.LeftOuterJoin);

            criteria = criteria.CreateAlias("EmployeeInjured", "employeeInjured", JoinType.LeftOuterJoin);

            if (siteId > 0)
            {
                criteria.Add(Restrictions.Eq("SiteWhereHappened.Id", siteId));
            }
            else if (siteId == -1)
            {
                criteria.Add(Restrictions.IsNull("SiteWhereHappened.Id"));
                criteria.Add(Restrictions.IsNotNull("OffSiteSpecifics"));
                criteria.Add(Restrictions.Not(Expression.Eq("OffSiteSpecifics", string.Empty)));
            }

            if (!string.IsNullOrEmpty(title))
            {
                criteria.Add(Restrictions.Like("Title", string.Format("%{0}%", title)));
            }

            if (createdFrom.HasValue)
            {
                criteria.Add(Restrictions.Ge("CreatedOn", createdFrom.Value));
            }

            if (createdTo.HasValue)
            {
                criteria.Add(Restrictions.Lt("CreatedOn", createdTo.Value.AddDays(1)));
            }

            if (allowedSiteIds != null && allowedSiteIds.Count > 0)
            {
                criteria.Add(   Restrictions.Or
                                ( 
                                    Restrictions.IsNull("SiteWhereHappened.Id"),                                    
                                    Restrictions.In("SiteWhereHappened.Id", allowedSiteIds.ToArray())
                                )
                            );
            }


            if (!string.IsNullOrEmpty(injuredPersonForename))
            {
                criteria.Add(Restrictions.Or
                                 (
                                     Restrictions.And
                                         (
                                             Restrictions.Eq("PersonInvolved", PersonInvolvedEnum.Employee),
                                             Restrictions.Like("employeeInjured.Forename",
                                                               string.Format("%{0}%", injuredPersonForename))
                                         ),
                                     Restrictions.And
                                         (
                                             Restrictions.Not(Restrictions.Eq("PersonInvolved",
                                                                              PersonInvolvedEnum.Employee)),
                                             Restrictions.Like("NonEmployeeInjuredForename",
                                                               string.Format("%{0}%", injuredPersonForename))
                                         )
                                 )
                    );
            }

            if (!string.IsNullOrEmpty(injuredPersonSurname))
            {
                criteria.Add(Restrictions.Or
                                 (
                                     Restrictions.And
                                         (
                                             Restrictions.Eq("PersonInvolved", PersonInvolvedEnum.Employee),
                                             Restrictions.Like("employeeInjured.Surname",
                                                               string.Format("%{0}%", injuredPersonSurname))
                                         ),
                                     Restrictions.And
                                         (
                                             Restrictions.Not(Restrictions.Eq("PersonInvolved",
                                                                              PersonInvolvedEnum.Employee)),
                                             Restrictions.Like("NonEmployeeInjuredSurname",
                                                               string.Format("%{0}%", injuredPersonSurname))
                                         )
                                 )
                    );
            }

            return criteria;
        }
    }

}