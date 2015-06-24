using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Constants;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Entities;
using Iesi.Collections;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace BusinessSafe.EscalationService.Queries
{
    public class GetAccidentRecordOffWorkReminderQuery : IGetAccidentRecordOffWorkReminderQuery
    {
        private readonly List<string> _threeDayJurisdictions = new List<string>() 
                                                            {   JurisdictionNames.NI, 
                                                                JurisdictionNames.ROI, 
                                                                JurisdictionNames.IoM, 
                                                                JurisdictionNames.Guernsey };

        public IList<AccidentRecord> Execute(ISession session)
        {                     
            return session
                .CreateCriteria<AccidentRecord>()
                .CreateAlias("Jurisdiction", "jurisdiction",JoinType.LeftOuterJoin)
                
                .SetFetchMode("Jurisdiction", FetchMode.Eager)
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("InjuredPersonAbleToCarryOutWork", YesNoUnknownEnum.Unknown))
                .Add(Restrictions.Or(
                        Restrictions.And(Restrictions.Eq("jurisdiction.Name", JurisdictionNames.GB), 
                                            Restrictions.Le("DateAndTimeOfAccident", DateTime.Now.AddDays(-8) ) ),

                        Restrictions.And(Restrictions.In("jurisdiction.Name", _threeDayJurisdictions),
                                            Restrictions.Le("DateAndTimeOfAccident", DateTime.Now.AddDays(-4))) 
                ))
                
                .Add(Subqueries.PropertyNotIn("Id",
                                              DetachedCriteria.For<EscalationOffWorkReminder>()
                                                  .SetProjection(Projections.Property("AccidentRecordId"))
                                                  .Add(Restrictions.IsNotNull("OffWorkReminderEmailSentDate"))
                         ))
                .SetMaxResults(10000)
                .SetResultTransformer(new DistinctRootEntityResultTransformer())
                .List<AccidentRecord>();
        }
    }
}
                       