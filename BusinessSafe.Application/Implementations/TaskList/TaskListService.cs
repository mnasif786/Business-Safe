using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Data.Repository;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.TaskList
{
    public class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _taskListRepository;

        public TaskListService(ITaskListRepository taskListRepository)
        {
            _taskListRepository = taskListRepository;
            
        }

        public IEnumerable<TaskListItem> Search(long companyId, IEnumerable<Guid> employeeIds, bool showCompleted, IList<long> allowedSiteIds)
        {
            return _taskListRepository.Search(companyId, employeeIds, showCompleted, allowedSiteIds);
        }

        public IEnumerable<TaskListItem> GetFurtherControlMeasureTasksByRiskAssessmentId(long riskAssessmentId, long companyId)
        {
            return _taskListRepository.GetFurtherControlMeasureTasksByRiskAssessmentId(riskAssessmentId, companyId);
        }
    }
}
