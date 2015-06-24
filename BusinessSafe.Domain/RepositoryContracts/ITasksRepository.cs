using System;
using System.Collections.Generic;
using System.ComponentModel;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts

{
    public enum TaskOrderByColumn
    {
        None,
        TaskCategory,
        Reference,
        Title,
        Description,
        TaskAssignedTo,
        CreatedDate,
        TaskCompletionDueDate,
        DerivedDisplayStatus
    }

    public interface ITasksRepository : IRepository<Task, long>
    {
     
        Task GetByIdAndCompanyId(long id, long companyId);

        IEnumerable<Task> Search(long companyId, IEnumerable<Guid> employeeIds, DateTime? createdFrom, DateTime? createdTo, DateTime? completedFrom, DateTime? completedTo, long taskCategoryId, int taskStatusId, bool showDeleted, bool showCompleted, IEnumerable<long> siteIds, string title, int page, int pageSize, TaskOrderByColumn orderBy, bool ascending);

        int Count(long companyId, IEnumerable<Guid> employeeIds, DateTime? createdFrom, DateTime? createdTo,
                  DateTime? completedFrom, DateTime? completedTo, long taskCategoryId, int taskStatusId,
                  bool showDeleted, bool showCompleted, IEnumerable<long> siteIds, string title);

        Task GetByTaskGuid(Guid taskGuid);
    }
}