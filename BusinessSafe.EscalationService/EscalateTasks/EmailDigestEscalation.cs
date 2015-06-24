using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Activation;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.EscalationService.EscalateTasks
{
    public class EmailDigestEscalation: IEscalate
    { 
        private readonly IEmployeeNotificationsService _employeeNotificationsService;
        
        private readonly IBusinessSafeSessionManager _sessionManager;

        public DateTime NextExecution { get; protected  set; }

        private DateTime _sendTime { get; set; }

        public EmailDigestEscalation( IEmployeeNotificationsService employeeNotificationsService
                                    , IBusinessSafeSessionManager sessionManager)
        {        
            _employeeNotificationsService = employeeNotificationsService;
            _sessionManager = sessionManager;

            // time of day to send digest email (date is not used)
            _sendTime = new DateTime(2001, 1, 1, 0, 30, 0);
           
            if( !String.IsNullOrEmpty( ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"] ) )
            {
                DateTime dt;
                if (DateTime.TryParse(ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"], out dt))
                {
                    NextExecution = new DateTime(dt.Year, dt.Month, dt.Day, _sendTime.Hour, _sendTime.Minute, _sendTime.Second).AddDays(1);                                         
                }
                else
                {
                    throw new Exception(String.Format("Exception in EmailDigestEscalation - Invalid value for EscalationService_LastSuccessfulExecutionTime in config {0}",
                                            ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"]));
                }
            }
            else
            {                
                NextExecution = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, _sendTime.Hour, _sendTime.Minute, _sendTime.Second ).AddDays(1);
            }
        }
        
        public void Execute()
        {
            try
            {
                if (DateTime.Now < NextExecution)
                {
                    return;
                }               

                Log4NetHelper.Log.Info("Started processing EmailDigestEscalation");

                using (var session = _sessionManager.Session)
                {
                    Log4NetHelper.Log.Info("EmailDigestEscalation Session started");

                    try
                    {
                        var employees = _employeeNotificationsService.GetEmployeesToBeNotified(session, DateTime.Now) ??
                                        new List<Employee>();

                        Log4NetHelper.Log.Info(string.Format("EmailDigestEscalation - {0} Employees retrieved", employees.Count ));

                        employees =
                            employees.Where(x => x.CompanyId == 33749 || x.CompanyId == 33748 || x.CompanyId == 64507)
                                .ToList(); //restrict to DEMO001, DEMO002 and DEMO006 clients for first release

                        Log4NetHelper.Log.Info(string.Format("EmailDigestEscalation Employees for demo accounts filtered - sending digest to {0} employees", employees.Count));

                        foreach (var employee in employees)
                        {
                            try
                            {
                                _employeeNotificationsService.CreateAndSendEmployeeEmailDigest(session, employee);
                            }
                            catch (Exception ex)
                            {
                                //dont error on each employee. log and continue
                                Log4NetHelper.Log.Error(string.Format("Error creating notification email command for: {0} {1}",
                                    employee.FullName, employee.Id));
                                Log4NetHelper.Log.Error(ex);
                                _sessionManager.Rollback();
                            }
                        }

                        employees = null;
                    }

                    catch (Exception ex)
                    {
                        // last chance handler to try and prevent escalation service crashing
                        Log4NetHelper.Log.Debug(string.Format("Unhandled Exception caught in Email Digest Escalation - ex: {0}", ex.Message));
                        EventLog.WriteEntry("EscalationService", ex.ToString(), EventLogEntryType.Error, EscalationServiceLogging.EventId);
                    }
                    finally
                    {
                        session.Close();
                        _sessionManager.CloseSession();
                        _sessionManager.Session.Dispose();
                        _sessionManager.DisposeFactory();
                        GC.Collect();

                        DateTime executedTime = DateTime.Now;
                       
                        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        if (config.AppSettings.Settings["EscalationService_LastSuccessfulExecutionTime"] == null)
                        {
                            config.AppSettings.Settings.Add("EscalationService_LastSuccessfulExecutionTime", executedTime.ToString());
                        }
                        else
                        {
                            config.AppSettings.Settings["EscalationService_LastSuccessfulExecutionTime"].Value = executedTime.ToString();    
                        }
                        
                        config.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");
           
                        NextExecution = new DateTime(executedTime.Year, executedTime.Month, executedTime.Day, _sendTime.Hour, _sendTime.Minute, _sendTime.Second).AddDays(1);
                        
                        Log4NetHelper.Log.Info(string.Format("NextExecution email digest execution: {0}", NextExecution.ToString()));                
                    }
                }

                Log4NetHelper.Log.Info("Finished processing EmailDigestEscalation");
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log.Error(ex);
            }
        }      
    }
}
