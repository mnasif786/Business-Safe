using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Data.Queries.CompletedTaskQuery;
using BusinessSafe.Data.Queries.DueTaskQuery;
using BusinessSafe.Data.Queries.GetTaskEmployeesQuery;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using log4net;
using NHibernate;
using NHibernate.Linq;
using NServiceBus;
using StructureMap;
using BusinessSafe.Data.Queries.OverdueTaskQuery;

namespace BusinessSafe.Application.Implementations.Employees
{
    public class EmployeeNotificationsService : IEmployeeNotificationsService
    {
        private readonly IBus _bus;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly ITasksRepository _tasksRepository;
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment> _overdueGRATasksQuery;
        private readonly IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment> _overduePRATasksQuery;
        private readonly IGetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery _overdueFRATasksQuery;
        private readonly IGetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery _overdueHSRATasksQuery;
        private readonly IGetOverdueRiskAssessmentReviewTasksForEmployeeQuery _overdueRAReviewTasksQuery;
        private readonly IGetOverdueResponsibilitiesTasksForEmployeeQuery _overdueResponsibilitiesTasksForEmployee;
        private readonly IGetOverdueActionTasksForEmployeeQuery _overdueActionTasksForEmployeeQuery;

        private readonly IGetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery _completedHSRATasksQuery;
        private readonly IGetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery _completedFRATasksQuery;
        private readonly IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment> _completedGRATasksQuery;
        private readonly IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment> _completedPRATasksQuery;
        private readonly IGetCompletedRiskAssessmentReviewTasksForEmployeeQuery _completedRAReviewTasksQuery;

        private readonly IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment> _dueGraTasksQuery;
        private readonly IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment> _duePraTasksQuery;
        private readonly IGetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery _dueFraTasksQuery;
        private readonly IGetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery _dueTomorrowHSRATasksQuery;
        private readonly IGetDueRiskAssessmentReviewTasksForEmployeeQuery _dueRiskAssessmentReviewTasksForEmployeeQuery;
        private readonly IGetDueResponsibilityTasksForEmployeeQuery _dueResponsibilityTasksForEmployeeQuery;
        private readonly IGetDueActionTasksForEmployeeQuery _dueActionTasksForEmployee;

        public EmployeeNotificationsService(
             ITasksRepository tasksRepository
             , IBus bus
            , IUserForAuditingRepository userForAuditingRepository
            , IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment> overdueGraTaskQuery
            , IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment> overduePraTaskQuery
            , IGetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery overdueFraTasksQuery
            , IGetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery overdueHsraTasksQuery
            , IGetOverdueRiskAssessmentReviewTasksForEmployeeQuery overdueRaReviewTasksQuery
            , IGetOverdueResponsibilitiesTasksForEmployeeQuery overdueResponsibilitiesTasksForEmployee
            , IGetOverdueActionTasksForEmployeeQuery overdueActionTasksForEmployeeQuery
            , IGetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery completedHSRATasksQuery
            , IGetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery completedFraTasksQuery
            , IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment> completedGraTaskQuery
            , IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment> completedPraTaskQuery
            , IGetCompletedRiskAssessmentReviewTasksForEmployeeQuery completedRAReviewTasksQuery
            , IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment> dueGraTasksQuery
            , IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment> duePraTasksQuery
            , IGetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery dueFraTasksQuery
            , IGetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery dueTomorrowHsraTasksQuery
            , IGetDueRiskAssessmentReviewTasksForEmployeeQuery dueRiskAssessmentReviewTasksForEmployeeQuery
            , IGetDueResponsibilityTasksForEmployeeQuery dueResponsibilityTasksForEmployeeQuery
            , IGetDueActionTasksForEmployeeQuery dueActionTasksForEmployee)
        {
             _tasksRepository = tasksRepository;
            
            _bus = bus;
            _userForAuditingRepository = userForAuditingRepository;
            _overdueGRATasksQuery = overdueGraTaskQuery;
            _overduePRATasksQuery = overduePraTaskQuery;
            _overdueFRATasksQuery = overdueFraTasksQuery;
            _overdueHSRATasksQuery = overdueHsraTasksQuery;
            _overdueRAReviewTasksQuery = overdueRaReviewTasksQuery;
            _overdueResponsibilitiesTasksForEmployee = overdueResponsibilitiesTasksForEmployee;
            _overdueActionTasksForEmployeeQuery = overdueActionTasksForEmployeeQuery;

            _completedHSRATasksQuery = completedHSRATasksQuery;
            _completedFRATasksQuery = completedFraTasksQuery;
            _completedGRATasksQuery = completedGraTaskQuery;
            _completedPRATasksQuery = completedPraTaskQuery;
            _completedRAReviewTasksQuery = completedRAReviewTasksQuery;

            _dueGraTasksQuery = dueGraTasksQuery;
            _duePraTasksQuery = duePraTasksQuery;
            _dueFraTasksQuery = dueFraTasksQuery;
            _dueTomorrowHSRATasksQuery = dueTomorrowHsraTasksQuery;
            _dueRiskAssessmentReviewTasksForEmployeeQuery = dueRiskAssessmentReviewTasksForEmployeeQuery;
            _dueResponsibilityTasksForEmployeeQuery = dueResponsibilityTasksForEmployeeQuery;
            _dueActionTasksForEmployee = dueActionTasksForEmployee;
        }
        

        public List<Employee> GetEmployeesToBeNotified(ISession session, DateTime notificationDateTime)
        {
            //Log.Debug("GetEmployeesToBeNotified()");

            try
            {
                var employees = new List<Employee>();

                var queries = ObjectFactory.GetAllInstances<IGetTaskEmployeesQuery>();

                queries.ForEach(query => employees = employees.Union(query.Execute()).ToList());

                //need to implement this with the digest email configuration settings.
                var employeesToBeNotified =
                    employees.Where(x => x.DoesWantToBeNotifiedOn(notificationDateTime)).ToList();
                return employeesToBeNotified;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception  - TotalTaskCount() " + ex.Message);
                throw;
            }

        }

        public EmployeeTasks GetEmployeeTasks(ISession session, Guid employeeId)
        {
            try
            {
                //Log.Debug("GetEmployeeTasks()");
                
                var employeeNotifications = new EmployeeTasks();
                
                ////Completed Tasks
                employeeNotifications.RiskAssessmentReviewTasksCompleted = _completedRAReviewTasksQuery.Execute(employeeId, session);
                employeeNotifications.GeneralRiskAssessmentTasksCompleted = _completedGRATasksQuery.Execute(employeeId, session);
                employeeNotifications.PersonalRiskAssessmentTasksCompleted = _completedPRATasksQuery.Execute(employeeId, session);
                employeeNotifications.FireRiskAssessmentTasksCompleted = _completedFRATasksQuery.Execute(employeeId, session);
                employeeNotifications.HazardousSubstanceTasksCompleted = _completedHSRATasksQuery.Execute(employeeId, session);
              
                ////DueTomorrow Tasks
                employeeNotifications.GeneralRiskAssessmentTasksDueTomorrow = _dueGraTasksQuery.Execute(employeeId, session);
                employeeNotifications.PersonalRiskAssessmentTasksDueTomorrow = _duePraTasksQuery.Execute(employeeId, session);
                employeeNotifications.FireRiskAssessmentTasksDueTomorrow = _dueFraTasksQuery.Execute(employeeId, session);
                employeeNotifications.HazardousSubstanceTasksDueTomorrow = _dueTomorrowHSRATasksQuery.Execute(employeeId, session);
                employeeNotifications.RiskAssessmentReviewTasksDueTomorrow = _dueRiskAssessmentReviewTasksForEmployeeQuery.Execute(employeeId, session);
                employeeNotifications.ResponsibilityTasksDueTomorrow = _dueResponsibilityTasksForEmployeeQuery.Execute(employeeId, session);
                employeeNotifications.ActionTasksDueTomorrow = _dueActionTasksForEmployee.Execute(employeeId, session);
              
                ////Overdue Tasks
                employeeNotifications.GeneralRiskAssessmentTasksOverdue = _overdueGRATasksQuery.Execute(employeeId, session);
                employeeNotifications.PersonalRiskAssessmentTasksOverdue = _overduePRATasksQuery.Execute(employeeId, session);
                employeeNotifications.FireRiskAssessmentTasksOverdue = _overdueFRATasksQuery.Execute(employeeId, session);
                employeeNotifications.HazadousSubstanceRiskAssessmentTasksOverdue = _overdueHSRATasksQuery.Execute(employeeId, session);
                employeeNotifications.RiskAssessmentReviewTasksOverdue = _overdueRAReviewTasksQuery.Execute(employeeId, session);
                employeeNotifications.ResponsibilityTaskOverdue = _overdueResponsibilitiesTasksForEmployee.Execute(employeeId, session);
                employeeNotifications.ActionTasksOverdue = _overdueActionTasksForEmployeeQuery.Execute(employeeId, session);
              
                return employeeNotifications;
            }
            catch(Exception ex)
            {
                Log.Debug("Exception  - TotalTaskCount() " + ex.Message);
                throw;
            }
        }

        public void UpdateEmployeeNotificationEmailSent(Employee employee, EmployeeTasks employeeNotification)
        {
            try
            {
                UserForAuditing user = null;

                try
                {
                    if (employee.User != null)
                    {
                        user = _userForAuditingRepository.GetByIdAndCompanyId(employee.User.Id, employee.CompanyId);
                    }
                }
                catch (UserNotFoundException ex) 
                {
                    Log.Debug(String.Format("*** User Not Found. Using System User for Auditing *** - UserId: {0} CompanyId: {1} Employee - ID: {2} FullName: {3}", 
                                employee.User.Id, employee.CompanyId, employee.Id, employee.FullName ));               
                }

                if (user == null)
                {
                    user = _userForAuditingRepository.GetSystemUser();
                }

                if (employeeNotification.AnyCompletedTasks(employee))
                {

                    employeeNotification.GeneralRiskAssessmentTasksCompleted
                       .Where(task => EmployeeNotificationsHelper.CanSendTaskCompletedNotification(task, employee))
                       .ForEach(task =>
                       {
                           EmployeeNotificationsHelper.AddEmployeeTaskCompletedNotification(task, employee, user);
                           _tasksRepository.Save(task);
                       });

                    employeeNotification.PersonalRiskAssessmentTasksCompleted
                       .Where(task => EmployeeNotificationsHelper.CanSendTaskCompletedNotification(task, employee))
                       .ForEach(task =>
                       {
                           EmployeeNotificationsHelper.AddEmployeeTaskCompletedNotification(task, employee, user);
                           _tasksRepository.Save(task);
                       });

                    employeeNotification.FireRiskAssessmentTasksCompleted
                        .Where(task => EmployeeNotificationsHelper.CanSendTaskCompletedNotification(task, employee))
                        .ForEach(task =>
                        {
                            EmployeeNotificationsHelper.AddEmployeeTaskCompletedNotification(task, employee, user);
                            _tasksRepository.Save(task);
                        });

                    employeeNotification.HazardousSubstanceTasksCompleted
                        .Where(task => EmployeeNotificationsHelper.CanSendTaskCompletedNotification(task, employee))
                        .ForEach(task =>
                        {
                            EmployeeNotificationsHelper.AddEmployeeTaskCompletedNotification(task, employee, user);
                            _tasksRepository.Save(task);
                        });

                
                    _tasksRepository.Flush();
            }
                  }
            catch (Exception ex)
            {
                Log.Debug("Exception  - UpdateEmployeeNotificationEmailSent() " + ex.Message);
                throw;
            }
        }

        private long TotalTaskCount( EmployeeTasks tasks)
        {
            try
            {
                //Log.Debug("TotalTaskCount() ");

                return tasks.GeneralRiskAssessmentTasksOverdue.Count
                       + tasks.PersonalRiskAssessmentTasksOverdue.Count
                       + tasks.FireRiskAssessmentTasksOverdue.Count
                       + tasks.HazadousSubstanceRiskAssessmentTasksOverdue.Count
                       + tasks.RiskAssessmentReviewTasksOverdue.Count
                       + tasks.ResponsibilityTaskOverdue.Count
                       + tasks.ActionTasksOverdue.Count
                       + tasks.HazardousSubstanceTasksCompleted.Count
                       + tasks.FireRiskAssessmentTasksCompleted.Count
                       + tasks.PersonalRiskAssessmentTasksCompleted.Count
                       + tasks.GeneralRiskAssessmentTasksCompleted.Count
                       + tasks.RiskAssessmentReviewTasksCompleted.Count
                       + tasks.GeneralRiskAssessmentTasksDueTomorrow.Count
                       + tasks.PersonalRiskAssessmentTasksDueTomorrow.Count
                       + tasks.FireRiskAssessmentTasksDueTomorrow.Count
                       + tasks.HazardousSubstanceTasksDueTomorrow.Count
                       + tasks.RiskAssessmentReviewTasksDueTomorrow.Count
                       + tasks.ResponsibilityTasksDueTomorrow.Count
                       + tasks.ActionTasksDueTomorrow.Count;
            }
            catch (Exception ex)
            {
                Log.Debug("Exception  - TotalTaskCount() " + ex.Message);
                throw;
            }
        }

        public void CreateAndSendEmployeeEmailDigest(ISession session, Employee employee)
        {
            try
            {
                //Log.Debug("CreateAndSendEmployeeEmailDigest()");
                
                var employeeTasks = GetEmployeeTasks(session, employee.Id);

                LogEmployeeTasks(employee, employeeTasks);

                if (employeeTasks.AnyOverdueTasks(employee) || employeeTasks.AnyCompletedTasks(employee) ||
                    employeeTasks.AnyDueTomorrowTasks(employee))
                {
                    //Log.Debug(string.Format("{0} {1} - Digest Email Tasks present", employee.FullName, employee.GetEmail()));

                    var employeeDigestEmailCommand =
                        EmployeeNotificationsHelper.CreateSendEmployeeDigestEmailCommand(employee, employeeTasks);
                    //Log.Debug(string.Format("{0} {1} - Digest Email created", employee.FullName, employee.GetEmail()));

                    _bus.Send(employeeDigestEmailCommand);
                    Log.Debug(string.Format("{0} {1} - Digest Email sent", employee.FullName, employee.GetEmail()));
                    
                    //if any email is sent for completed tasks then save those tasks to employee notification History
                    if (employeeDigestEmailCommand.GeneralRiskAssessmentTasksCompleted.Any()
                        || employeeDigestEmailCommand.FireRiskAssessmentTasksCompleted.Any()
                        || employeeDigestEmailCommand.PersonalRiskAssessmentTasksCompleted.Any()
                        || employeeDigestEmailCommand.HazardousSubstanceTasksCompleted.Any()
                        || employeeDigestEmailCommand.RiskAssessmentReviewTasksCompleted.Any())
                    {
                        Log.Debug(string.Format("{0} {1} - Completed Tasks In Digest Email", employee.FullName,
                            employee.GetEmail()));
                        UpdateEmployeeNotificationEmailSent(employee, employeeTasks);
                        Log.Debug(string.Format("{0} {1} - Completed Tasks Updated", employee.FullName,
                            employee.GetEmail()));
                    }
                }

                employeeTasks.ClearEmployeeTasks();
            }
            catch (Exception ex)
            {
                Log.Debug("Exception - CreateAndSendEmployeeEmailDigest" + ex.Message);
                throw;
            }
        }

        private void LogEmployeeTasks(Employee employee, EmployeeTasks employeeTasks)
        {
            try
            {
                if (TotalTaskCount(employeeTasks) == 0)
                {
                    Log.Debug(string.Format("{0} {1} has NO Overdue, Completed or DueTomorrow tasks", employee.FullName,
                        employee.GetEmail()));
                }
                else
                {
                    Log.Debug(
                        string.Format(
                            "{0} {1} - {2} Overdue tasks - {3} Completed tasks - {4} DueTomorrow tasks", employee.FullName, employee.GetEmail()

                            , (employeeTasks.GeneralRiskAssessmentTasksOverdue.Count
                               + employeeTasks.PersonalRiskAssessmentTasksOverdue.Count
                               + employeeTasks.FireRiskAssessmentTasksOverdue.Count
                               + employeeTasks.HazadousSubstanceRiskAssessmentTasksOverdue.Count
                               + employeeTasks.RiskAssessmentReviewTasksOverdue.Count
                               + employeeTasks.ResponsibilityTaskOverdue.Count
                               + employeeTasks.ActionTasksOverdue.Count),
                               
                               
                                (employeeTasks.HazardousSubstanceTasksCompleted.Count
                              + employeeTasks.FireRiskAssessmentTasksCompleted.Count
                              + employeeTasks.PersonalRiskAssessmentTasksCompleted.Count
                              + employeeTasks.GeneralRiskAssessmentTasksCompleted.Count
                              + employeeTasks.RiskAssessmentReviewTasksCompleted.Count),
                               
                               (employeeTasks.GeneralRiskAssessmentTasksDueTomorrow.Count
                              + employeeTasks.PersonalRiskAssessmentTasksDueTomorrow.Count
                              + employeeTasks.FireRiskAssessmentTasksDueTomorrow.Count
                              + employeeTasks.HazardousSubstanceTasksDueTomorrow.Count
                              + employeeTasks.RiskAssessmentReviewTasksDueTomorrow.Count
                              + employeeTasks.ResponsibilityTasksDueTomorrow.Count
                              + employeeTasks.ActionTasksDueTomorrow.Count)
                               
                               
                               ));                 
                }
            }
            catch (Exception ex)
            {
                Log.Debug("Exception " + ex.Message);
                throw;
            }
       }
    }
}