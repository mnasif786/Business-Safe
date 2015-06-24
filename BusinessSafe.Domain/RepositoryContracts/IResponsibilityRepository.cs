using System;
using System.Collections.Generic;
using System.ComponentModel;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public enum ResponsibilitiesRequestOrderByColumn
    {
        [Description("")]
        None,

        [Description("Category")]
        Category,

        [Description("Title")]
        Title,

        [Description("Description")]
        Description,

        [Description("Site")]
        Site,

        [Description("Reason")]
        Reason,

        [Description("AssignedTo")]
        AssignedTo,

        [Description("Status")]
        Status,

        [Description("CreatedDateFormatted")]
        CreatedDateFormatted,

        [Description("Frequency")]
        Frequency,

        [Description("DueDateFormatted")]
        DueDateFormatted
    }

    public interface IResponsibilityRepository : IRepository<Responsibility, long>
    {       
        Responsibility GetByIdAndCompanyId(long id, long companyId);
        IEnumerable<Responsibility> GetStatutoryByCompanyId(long companyId);

        IEnumerable<Responsibility> Search(
            Guid currentUserId,
            IEnumerable<long> allowedSiteIds,
            long companyId,
            long? responsibilityCategoryId,
            long? siteId,
            long? siteGroupId,
            string title,
            DateTime? createdFrom,
            DateTime? createdTo, 
            bool showDeleted, 
            bool showCompleted,
            int page,
            int pageSize,
            ResponsibilitiesRequestOrderByColumn orderBy,
            bool ascending);

        int Count(
            Guid currentUserId,
            IEnumerable<long> allowedSiteIds,
            long companyId,
            long? responsibilityCategoryId,
            long? siteId,
            long? siteGroupId,
            string title,
            DateTime? createdFrom,
            DateTime? createdTo,
            bool showDeleted,
            bool showCompleted);
    }
}