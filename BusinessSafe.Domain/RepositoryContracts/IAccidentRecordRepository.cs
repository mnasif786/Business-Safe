using System.Collections.Generic;
using System.ComponentModel;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;
using System;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public enum AccidentRecordstOrderByColumn
    {
        [Description("")]
        None,
        [Description("Reference")]
        Reference,
        [Description("Title")]
        Title,
        [Description("DescriptionHowAccidentHappened")]
        Description,
        [Description("employeeInjured.Surname")]
        InjuredPerson,
        [Description("SeverityOfInjury")]
        Severity,
        [Description("site.Name")]
        Site,
        [Description("CreatedBy")]
        ReportedBy,
        [Description("Status")]
        Status,
        [Description("DateAndTimeOfAccident")]
        DateOfAccident,
        [Description("CreatedOn")]
        DateCreated
    }

    public interface IAccidentRecordRepository : IRepository<AccidentRecord, long>
    {
        AccidentRecord GetByIdAndCompanyId(long accidentRecordId, long companyId);

        int Count(IList<long> allowedSiteIds, long companyId, long? siteId,
                  string title, DateTime? createdFrom, DateTime? createdTo,
                  bool showDeleted, string injuredPersonForename, string injuredPersonSurname);


        IEnumerable<AccidentRecord> Search( IList<long> allowedSiteIds, long companyId, long? siteId, string title, DateTime? createdFrom, DateTime? createdTo, bool showDeleted, string injuredPersonForename, string injuredPersonSurname, int page, int pageSize, AccidentRecordstOrderByColumn orderBy, bool ascending);
    }
}
