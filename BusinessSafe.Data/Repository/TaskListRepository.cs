using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using TaskStatus = BusinessSafe.Domain.Entities.TaskStatus;
using StructureMap;

namespace BusinessSafe.Data.Repository
{
    public interface ITaskListRepository
    {
        IEnumerable<TaskListItem> Search(
            long companyId,
            IEnumerable<Guid> employeeIds,
            bool showCompleted,
            IEnumerable<long> siteIds);

        IEnumerable<TaskListItem> GetFurtherControlMeasureTasksByRiskAssessmentId(long riskAssessmentId, long companyId);
    }

    /// <summary>
    /// used in the api to see if we can improve the speed of the response. WHen fully implemented it will be accessed via a service
    /// </summary>
    public class TaskListRepository : ITaskListRepository
    {
        
        protected readonly IBusinessSafeSessionManager _sessionManager;
        protected readonly IBusinessSafeSessionManagerFactory _sessionManagerFactory;

        public TaskListRepository(IBusinessSafeSessionManager sessionManager, IBusinessSafeSessionManagerFactory sessionManagerFactory)
        {
            _sessionManager = sessionManager;
            _sessionManagerFactory = sessionManagerFactory;
        }

        public IEnumerable<TaskListItem> Search(
          long companyId,
          IEnumerable<Guid> employeeIds,
          bool showCompleted,
          IEnumerable<long> siteIds)
        {
            return SearchUsingMulitpleThreads(companyId, employeeIds, showCompleted, siteIds);
        }

        public IEnumerable<TaskListItem> SearchUsingSingleThread(
          long companyId,
          IEnumerable<Guid> employeeIds,
          bool showCompleted,
          IEnumerable<long> siteIds)
        {
            var multiHazardFCMTaskQuery = CreateQueryForMultiHazardRiskAssessmentFurtherControlMeasureTask(_sessionManager.Session, companyId, employeeIds, showCompleted, siteIds);
            var fireFCMTaskQuery = CreateQueryForFireRiskAssessmentFurtherControlMeasureTask(_sessionManager.Session, companyId, employeeIds, showCompleted, siteIds);
            var hazardSubstanceFCMTaskQuery = CreateQueryForHazardousSubstanceRiskAssessmentFurtherControlMeasureTask(_sessionManager.Session, companyId, employeeIds, showCompleted, siteIds);
 
            var result = multiHazardFCMTaskQuery.ToList();
            result.AddRange(fireFCMTaskQuery.ToList());
            result.AddRange(hazardSubstanceFCMTaskQuery.ToList());

            return result;
        }

        public IEnumerable<TaskListItem> SearchUsingMulitpleThreads(
          long companyId,
          IEnumerable<Guid> employeeIds,
          bool showCompleted,
          IEnumerable<long> siteIds)
        {
            var sessionManagerForMultiHazardFCMTaskQuery = _sessionManagerFactory.CreateInstance();// new BusinessSafeSessionManager(ObjectFactory.GetInstance<IBusinessSafeSessionFactory>());
            var multiHazardFCMTaskQuery = CreateQueryForMultiHazardRiskAssessmentFurtherControlMeasureTask(sessionManagerForMultiHazardFCMTaskQuery.Session, companyId, employeeIds, showCompleted, siteIds);

            var sessionManagerForfireFCMTaskQuery = _sessionManagerFactory.CreateInstance();// new BusinessSafeSessionManager(ObjectFactory.GetInstance<IBusinessSafeSessionFactory>());
            var fireFCMTaskQuery = CreateQueryForFireRiskAssessmentFurtherControlMeasureTask(sessionManagerForfireFCMTaskQuery.Session, companyId, employeeIds, showCompleted, siteIds);

            var sessionManagerForhazardSubstanceFCMTaskQuery = _sessionManagerFactory.CreateInstance(); // new BusinessSafeSessionManager(ObjectFactory.GetInstance<IBusinessSafeSessionFactory>());
            var hazardSubstanceFCMTaskQuery = CreateQueryForHazardousSubstanceRiskAssessmentFurtherControlMeasureTask(sessionManagerForhazardSubstanceFCMTaskQuery.Session, companyId, employeeIds, showCompleted, siteIds);

            var sessionManagerForResponsibilityTaskQuery = _sessionManagerFactory.CreateInstance();
            var responsibilityTaskQuery =CreateQueryForResponsibilityTask(sessionManagerForResponsibilityTaskQuery.Session, companyId,employeeIds, showCompleted, siteIds);

            // create threads. NHibernate sessions are not thread safe so create a new one for each thread
            var threadForMultiHazardQuery = System.Threading.Tasks.Task<List<TaskListItem>>.Factory.StartNew(multiHazardFCMTaskQuery.ToList);

            var threadForFireQuery = System.Threading.Tasks.Task<List<TaskListItem>>.Factory.StartNew(fireFCMTaskQuery.ToList);

            var threadForHazardSubstanceQuery = System.Threading.Tasks.Task<List<TaskListItem>>.Factory.StartNew(hazardSubstanceFCMTaskQuery.ToList);

            var threadForResponsibilityTaskQuery = System.Threading.Tasks.Task<List<TaskListItem>>.Factory.StartNew(responsibilityTaskQuery.ToList);

            var result = threadForMultiHazardQuery.Result;
            result.AddRange(threadForFireQuery.Result);
            result.AddRange(threadForHazardSubstanceQuery.Result);
            result.AddRange(threadForResponsibilityTaskQuery.Result);

            //close sessions
            sessionManagerForMultiHazardFCMTaskQuery.CloseSession();
            sessionManagerForfireFCMTaskQuery.CloseSession();
            sessionManagerForhazardSubstanceFCMTaskQuery.CloseSession();
            sessionManagerForResponsibilityTaskQuery.CloseSession();

            //dispose of threads
            threadForMultiHazardQuery.Dispose();
            threadForFireQuery.Dispose();
            threadForHazardSubstanceQuery.Dispose();
            threadForResponsibilityTaskQuery.Dispose();

            return result;
        }

        private static IQueryable<TaskListItem> CreateQueryForMultiHazardRiskAssessmentFurtherControlMeasureTask(ISession session, long companyId,
          IEnumerable<Guid> employeeIds,
          bool showCompleted,
          IEnumerable<long> siteIds)
        {
            var multiHazardFCMTaskQuery = session.Query<MultiHazardRiskAssessmentFurtherControlMeasureTask>()
                .Where(t => t.TaskAssignedTo.CompanyId == companyId)
                .Where(t => t.Deleted == false)
                .Where(t => t.MultiHazardRiskAssessmentHazard.Deleted == false)
                .Where(t => t.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.Deleted == false);

            multiHazardFCMTaskQuery = showCompleted ? multiHazardFCMTaskQuery.Where(t => t.TaskStatus == TaskStatus.Completed) : multiHazardFCMTaskQuery.Where(t => t.TaskStatus != TaskStatus.Completed);

            if (AreSitesAndEmployeesSpecified(employeeIds, siteIds))
            {
                multiHazardFCMTaskQuery = multiHazardFCMTaskQuery.Where(t => employeeIds.Contains(t.TaskAssignedTo.Id)
                                                                             || siteIds.Contains(t.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessmentSite.Id)
                                                                             || t.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessmentSite == null);

            }
            else if (siteIds != null && siteIds.Any())
            {
                //just query the sites
                multiHazardFCMTaskQuery = multiHazardFCMTaskQuery
                    .Where(t => siteIds.Contains(t.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessmentSite.Id));
            }
            else if (employeeIds != null && employeeIds.Any())
            {
                //just query who the task is assigned to
                multiHazardFCMTaskQuery = multiHazardFCMTaskQuery.Where(t => employeeIds.Contains(t.TaskAssignedTo.Id));
            }

            return SelectTaskListItem(multiHazardFCMTaskQuery);
           
        }

        private static bool AreSitesAndEmployeesSpecified(IEnumerable<Guid> employeeIds, IEnumerable<long> siteIds)
        {
            return siteIds != null && siteIds.Any() && employeeIds != null && employeeIds.Any();
        }

        private static IQueryable<TaskListItem> CreateQueryForFireRiskAssessmentFurtherControlMeasureTask(ISession session, long companyId,
         IEnumerable<Guid> employeeIds,
         bool showCompleted,
         IEnumerable<long> siteIds)
        {
            var fireFCMTaskQuery = session.Query<FireRiskAssessmentFurtherControlMeasureTask>()
                .Where(t => t.TaskAssignedTo.CompanyId == companyId)
                .Where(t => t.Deleted == false)
                .Where(t => t.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.Deleted == false)
                .Where(t => t.SignificantFinding.Deleted == false);

            fireFCMTaskQuery = showCompleted ? fireFCMTaskQuery.Where(t => t.TaskStatus == TaskStatus.Completed) : fireFCMTaskQuery.Where(t => t.TaskStatus != TaskStatus.Completed);

            if (AreSitesAndEmployeesSpecified(employeeIds, siteIds))
            {
                fireFCMTaskQuery = fireFCMTaskQuery.Where(t => employeeIds.Contains(t.TaskAssignedTo.Id)
                                                               || siteIds.Contains(t.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessmentSite.Id)
                                                               || t.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessmentSite == null);

            }
            else if (siteIds != null && siteIds.Any())
            {
                //just query the sites
                fireFCMTaskQuery = fireFCMTaskQuery
                    .Where(t => siteIds.Contains(t.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessmentSite.Id));
            }
            else if (employeeIds != null && employeeIds.Any())
            {
                //just query who the task is assigned to
                fireFCMTaskQuery = fireFCMTaskQuery.Where(t => employeeIds.Contains(t.TaskAssignedTo.Id));
            }


            return SelectTaskListItem(fireFCMTaskQuery);
        }

        private static IQueryable<TaskListItem> CreateQueryForHazardousSubstanceRiskAssessmentFurtherControlMeasureTask(ISession session, long companyId,
         IEnumerable<Guid> employeeIds,
         bool showCompleted,
         IEnumerable<long> siteIds)
        {
            var hazardSubstanceFCMTaskQuery = session.Query<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>()
               .Where(t => t.TaskAssignedTo.CompanyId == companyId)
               .Where(t => t.Deleted == false)
               .Where(t => t.HazardousSubstanceRiskAssessment.Deleted == false);


            if (showCompleted)
            {
                hazardSubstanceFCMTaskQuery = hazardSubstanceFCMTaskQuery.Where(t => t.TaskStatus == TaskStatus.Completed);
            }
            else
            {
                hazardSubstanceFCMTaskQuery = hazardSubstanceFCMTaskQuery.Where(t => t.TaskStatus != TaskStatus.Completed);
            }

            if (AreSitesAndEmployeesSpecified(employeeIds, siteIds))
            {
                hazardSubstanceFCMTaskQuery = hazardSubstanceFCMTaskQuery.Where(t => employeeIds.Contains(t.TaskAssignedTo.Id)
                                                               || siteIds.Contains(t.HazardousSubstanceRiskAssessment.RiskAssessmentSite.Id)
                                                               || t.HazardousSubstanceRiskAssessment.RiskAssessmentSite == null);

            }
            else if (siteIds != null && siteIds.Any())
            {
                //just query the sites
                hazardSubstanceFCMTaskQuery = hazardSubstanceFCMTaskQuery
                    .Where(t => siteIds.Contains(t.HazardousSubstanceRiskAssessment.RiskAssessmentSite.Id));
            }
            else if (employeeIds != null && employeeIds.Any())
            {
                //just query who the task is assigned to
                hazardSubstanceFCMTaskQuery = hazardSubstanceFCMTaskQuery.Where(t => employeeIds.Contains(t.TaskAssignedTo.Id));
            }

            return SelectTaskListItem(hazardSubstanceFCMTaskQuery);
        }

        private static IQueryable<TaskListItem> CreateQueryForResponsibilityTask(ISession session, long companyId,IEnumerable<Guid> employeeIds,bool showCompleted,IEnumerable<long> siteIds)
        {
            var responsibilityTaskQuery = session.Query<ResponsibilityTask>()
                .Where(t => t.TaskAssignedTo.CompanyId == companyId)
                .Where(t => t.Deleted == false);

            responsibilityTaskQuery = showCompleted ? responsibilityTaskQuery.Where(t => t.TaskStatus == TaskStatus.Completed) : responsibilityTaskQuery.Where(t => t.TaskStatus != TaskStatus.Completed);

            if (AreSitesAndEmployeesSpecified(employeeIds, siteIds))
            {
                responsibilityTaskQuery = responsibilityTaskQuery.Where(t => employeeIds.Contains(t.TaskAssignedTo.Id)
                                                               || siteIds.Contains(t.Site.Id));

            }
            else if (siteIds != null && siteIds.Any())
            {
                //just query the sites
                responsibilityTaskQuery = responsibilityTaskQuery
                    .Where(t => siteIds.Contains(t.Site.Id));
            }
            else if (employeeIds != null && employeeIds.Any())
            {
                //just query who the task is assigned to
                responsibilityTaskQuery = responsibilityTaskQuery.Where(t => employeeIds.Contains(t.TaskAssignedTo.Id));
            }


            return SelectTaskListItem(responsibilityTaskQuery);
        }

        /// <summary>
        /// append the select statement this way so that the nhibernate linq query produces the correct SQL
        /// </summary>
        /// <param name="mulitHazardFCMTaskQuery"></param>
        /// <returns></returns>
        private static IQueryable<TaskListItem> SelectTaskListItem(IQueryable<MultiHazardRiskAssessmentFurtherControlMeasureTask> mulitHazardFCMTaskQuery)
        {
            return mulitHazardFCMTaskQuery.Select(task => new TaskListItem()
                                                              {
                                                                  Id = task.Id
                                                                  ,
                                                                  Title = task.Title
                                                                  ,
                                                                  Category = new LookupItem() { Id = task.Category.Id, Name = task.Category.Category }
                                                                  ,
                                                                  Description = task.Description
                                                                  ,
                                                                  Reference = task.Reference
                                                                  ,
                                                                  Site = task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessmentSite != null ? new LookupItem() { Id = task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessmentSite.Id, Name = task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessmentSite.Name } : null
                                                                  ,
                                                                  CompletedComments = task.TaskCompletedComments
                                                                  ,
                                                                  CompletedDate = task.TaskCompletedDate
                                                                  ,
                                                                  CompletedBy = task.TaskCompletedBy != null ? new EmployeeName() { Id = task.TaskCompletedBy.Employee.Id, Name = (task.TaskCompletedBy.Employee.Forename + " " + task.TaskCompletedBy.Employee.Surname).Trim() } : null
                                                                  ,
                                                                  CompletionDueDate = task.TaskCompletionDueDate
                                                                  ,
                                                                  CreatedOn = task.CreatedOn
                                                                  ,
                                                                  EmployeeAssignedTo = new EmployeeName() { Id = task.TaskAssignedTo.Id, Name = (task.TaskAssignedTo.Forename + " " + task.TaskAssignedTo.Surname).Trim() }
                                                                  ,
                                                                  Status = task.TaskStatus
                                                              });
        }
        
        private static IQueryable<TaskListItem> SelectTaskListItem(IQueryable<FireRiskAssessmentFurtherControlMeasureTask> fireFCMTaskQuery)
        {
            return fireFCMTaskQuery.Select(task => new TaskListItem()
                                                       {
                                                           Id = task.Id
                                                           ,
                                                           Title = task.Title
                                                           ,
                                                           Category = new LookupItem() {Id = task.Category.Id, Name = task.Category.Category}
                                                           ,
                                                           Description = task.Description
                                                           ,
                                                           Reference = task.Reference
                                                           ,
                                                           Site = task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessmentSite != null ? new LookupItem() {Id = task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessmentSite.Id, Name = task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessmentSite.Name} : null
                                                           ,
                                                           CompletedComments = task.TaskCompletedComments
                                                           ,
                                                           CompletedDate = task.TaskCompletedDate
                                                           ,
                                                           CompletedBy = task.TaskCompletedBy != null ? new EmployeeName() {Id = task.TaskCompletedBy.Employee.Id, Name = (task.TaskCompletedBy.Employee.Forename + " " + task.TaskCompletedBy.Employee.Surname).Trim()} : null
                                                           ,
                                                           CompletionDueDate = task.TaskCompletionDueDate
                                                           ,
                                                           CreatedOn = task.CreatedOn
                                                           ,
                                                           EmployeeAssignedTo = new EmployeeName() {Id = task.TaskAssignedTo.Id, Name = (task.TaskAssignedTo.Forename + " " + task.TaskAssignedTo.Surname).Trim()}
                                                           ,
                                                           Status = task.TaskStatus
                                                       });
        }

        private static IQueryable<TaskListItem> SelectTaskListItem(IQueryable<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask> hazardSubstanceFCMTaskQuery)
        {
            return hazardSubstanceFCMTaskQuery.Select(task => new TaskListItem()
            {
                Id = task.Id
                ,
                Title = task.Title
                ,
                Category = new LookupItem() { Id = task.Category.Id, Name = task.Category.Category }
                ,
                Description = task.Description
                ,
                Reference = task.Reference
                ,
                Site = task.HazardousSubstanceRiskAssessment.RiskAssessmentSite != null ? new LookupItem() { Id = task.HazardousSubstanceRiskAssessment.RiskAssessmentSite.Id, Name = task.HazardousSubstanceRiskAssessment.RiskAssessmentSite.Name } : null
                ,
                CompletedComments = task.TaskCompletedComments
                ,
                CompletedDate = task.TaskCompletedDate
                ,
                CompletedBy = task.TaskCompletedBy != null ? new EmployeeName() { Id = task.TaskCompletedBy.Employee.Id, Name = (task.TaskCompletedBy.Employee.Forename + " " + task.TaskCompletedBy.Employee.Surname).Trim() } : null
                ,
                CompletionDueDate = task.TaskCompletionDueDate
                ,
                CreatedOn = task.CreatedOn
                ,
                EmployeeAssignedTo = new EmployeeName() { Id = task.TaskAssignedTo.Id, Name = (task.TaskAssignedTo.Forename + " " + task.TaskAssignedTo.Surname).Trim() }
                ,
                Status = task.TaskStatus
            });
        }

        private static IQueryable<TaskListItem> SelectTaskListItem(IQueryable<ResponsibilityTask> responsibilityTaskQuery)
        {
            return responsibilityTaskQuery.Select(task => new TaskListItem()
            {
                Id = task.Id
                ,
                Title = task.Title
                ,
                Category = new LookupItem() { Id = task.Category.Id, Name = task.Category.Category }
                ,
                Description = task.Description
                ,
                Reference = task.Reference
                ,
                Site = task.Site != null ? new LookupItem() { Id = task.Site.Id, Name = task.Site.Name } : null
                ,
                CompletedComments = task.TaskCompletedComments
                ,
                CompletedDate = task.TaskCompletedDate
                ,
                CompletedBy = task.TaskCompletedBy != null ? new EmployeeName() { Id = task.TaskCompletedBy.Employee.Id, Name = (task.TaskCompletedBy.Employee.Forename + " " + task.TaskCompletedBy.Employee.Surname).Trim() } : null
                ,
                CompletionDueDate = task.TaskCompletionDueDate
                ,
                CreatedOn = task.CreatedOn
                ,
                EmployeeAssignedTo = new EmployeeName() { Id = task.TaskAssignedTo.Id, Name = (task.TaskAssignedTo.Forename + " " + task.TaskAssignedTo.Surname).Trim() }
                ,
                Status = task.TaskStatus
            });
        }

        public IEnumerable<TaskListItem> GetFurtherControlMeasureTasksByRiskAssessmentId(long riskAssessmentId, long companyId)
        {
           // var riskAss = _sessionManager.Session.Get<RiskAssessment>(riskAssessmentId);

            RiskAssessment riskAss = null;
            var queryResult = _sessionManager.Session.Query<RiskAssessment>()
                .Where(ra => ra.Id == riskAssessmentId && ra.CompanyId == companyId && ra.Deleted == false)
                .ToList();

            if (!queryResult.Any())
            {
                throw new RiskAssessmentNotFoundException(riskAssessmentId);
            }
            else
            {
                riskAss = queryResult.First();
            }
           
            if(riskAss is MultiHazardRiskAssessment)
            {
                return ((MultiHazardRiskAssessment)riskAss).Hazards
                    .Where(h => h.Deleted == false)
                    .SelectMany(x => x.FurtherControlMeasureTasks)
                    .Where(t => t.Deleted == false).ToList()
                    .Select(t => MapToTaskListItem(t));

            }
            else if (riskAss is FireRiskAssessment)
            {
                if(((FireRiskAssessment) riskAss).LatestFireRiskAssessmentChecklist != null)
                {
                    return ((FireRiskAssessment)riskAss).LatestFireRiskAssessmentChecklist.SignificantFindings
                   .Where(sf => sf.Deleted == false)
                   .SelectMany(x => x.FurtherControlMeasureTasks)
                   .Where(t => t.Deleted == false).ToList()
                   .Select(t => MapToTaskListItem(t));
                }
               
            }else if (riskAss is HazardousSubstanceRiskAssessment)
            {
                return ((HazardousSubstanceRiskAssessment)riskAss)
                    .FurtherControlMeasureTasks
                   .Where(t => t.Deleted == false).ToList()
                   .Select(t => MapToTaskListItem(t));
            }

            
            return new List<TaskListItem>();
        }

        private static TaskListItem MapToTaskListItem(MultiHazardRiskAssessmentFurtherControlMeasureTask task)
        {
            return new TaskListItem()
                       {
                           Id = task.Id
                           ,
                           Title = task.Title
                           ,
                           Category = new LookupItem() {Id = task.Category.Id, Name = task.Category.Category}
                           ,
                           Description = task.Description
                           ,
                           Reference = task.Reference
                           ,
                           Site = task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessmentSite != null ? new LookupItem() {Id = task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessmentSite.Id, Name = task.MultiHazardRiskAssessmentHazard.MultiHazardRiskAssessment.RiskAssessmentSite.Name} : null
                           ,
                           CompletedComments = task.TaskCompletedComments
                           ,
                           CompletedDate = task.TaskCompletedDate
                           ,
                           CompletedBy = task.TaskCompletedBy != null ? new EmployeeName() {Id = task.TaskCompletedBy.Employee.Id, Name = (task.TaskCompletedBy.Employee.Forename + " " + task.TaskCompletedBy.Employee.Surname).Trim()} : null
                           ,
                           CompletionDueDate = task.TaskCompletionDueDate
                           ,
                           CreatedOn = task.CreatedOn
                           ,
                           EmployeeAssignedTo = new EmployeeName() {Id = task.TaskAssignedTo.Id, Name = (task.TaskAssignedTo.Forename + " " + task.TaskAssignedTo.Surname).Trim()}
                           ,
                           Status = task.TaskStatus
                       };
        }

        private static TaskListItem MapToTaskListItem(FireRiskAssessmentFurtherControlMeasureTask task)
        {
            return new TaskListItem()
            {
                Id = task.Id
                ,
                Title = task.Title
                ,
                Category = new LookupItem() { Id = task.Category.Id, Name = task.Category.Category }
                ,
                Description = task.Description
                ,
                Reference = task.Reference
                ,
                Site = task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessmentSite != null ? new LookupItem() { Id = task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessmentSite.Id, Name = task.SignificantFinding.FireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessmentSite.Name } : null
                ,
                CompletedComments = task.TaskCompletedComments
                ,
                CompletedDate = task.TaskCompletedDate
                ,
                CompletedBy = task.TaskCompletedBy != null ? new EmployeeName() { Id = task.TaskCompletedBy.Employee.Id, Name = (task.TaskCompletedBy.Employee.Forename + " " + task.TaskCompletedBy.Employee.Surname).Trim() } : null
                ,
                CompletionDueDate = task.TaskCompletionDueDate
                ,
                CreatedOn = task.CreatedOn
                ,
                EmployeeAssignedTo = new EmployeeName() { Id = task.TaskAssignedTo.Id, Name = (task.TaskAssignedTo.Forename + " " + task.TaskAssignedTo.Surname).Trim() }
                ,
                Status = task.TaskStatus
            };
        }

        private static TaskListItem MapToTaskListItem(HazardousSubstanceRiskAssessmentFurtherControlMeasureTask task)
        {
            return new TaskListItem()
            {
                Id = task.Id
                ,
                Title = task.Title
                ,
                Category = new LookupItem() { Id = task.Category.Id, Name = task.Category.Category }
                ,
                Description = task.Description
                ,
                Reference = task.Reference
                ,
                Site = task.HazardousSubstanceRiskAssessment.RiskAssessmentSite != null ? new LookupItem() { Id = task.HazardousSubstanceRiskAssessment.RiskAssessmentSite.Id, Name = task.HazardousSubstanceRiskAssessment.RiskAssessmentSite.Name } : null
                ,
                CompletedComments = task.TaskCompletedComments
                ,
                CompletedDate = task.TaskCompletedDate
                ,
                CompletionDueDate = task.TaskCompletionDueDate
                ,
                CompletedBy = task.TaskCompletedBy != null ? new EmployeeName() { Id = task.TaskCompletedBy.Employee.Id, Name = (task.TaskCompletedBy.Employee.Forename + " " + task.TaskCompletedBy.Employee.Surname).Trim() } : null
                ,
                CreatedOn = task.CreatedOn
                ,
                EmployeeAssignedTo = new EmployeeName() { Id = task.TaskAssignedTo.Id, Name = (task.TaskAssignedTo.Forename + " " + task.TaskAssignedTo.Surname).Trim() }
                ,
                Status = task.TaskStatus
            };
        }

    }
}
