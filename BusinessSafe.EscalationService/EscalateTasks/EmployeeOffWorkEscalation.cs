using System;
using System.Diagnostics;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Activation;
using BusinessSafe.EscalationService.Commands;
using BusinessSafe.EscalationService.Queries;
using BusinessSafe.Messages.Emails.Commands;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.EscalationService.EscalateTasks
{
    public class EmployeeOffWorkEscalation : IEscalate
    {
        private IGetAccidentRecordOffWorkReminderQuery _getAccidentRecordOffWorkReminderQuery;
        private IBusinessSafeSessionManager _businessSafeSessionManager;
        private IOffWorkReminderEmailSentCommand _offWorkReminderEmailSentCommand;
        private IBus _bus;
        private IGetEmployeeQuery _getEmployeeQuery;

        public EmployeeOffWorkEscalation(   IGetAccidentRecordOffWorkReminderQuery getAccidentRecordOffWorkReminderQuery,
                                            IOffWorkReminderEmailSentCommand offWorkReminderEmailSentCommand,
                                            IBusinessSafeSessionManager businessSafeSessionManager, 
                                            IBus bus,
                                            IGetEmployeeQuery getEmployeeQuery)
        {
            _getAccidentRecordOffWorkReminderQuery = getAccidentRecordOffWorkReminderQuery;
            _offWorkReminderEmailSentCommand = offWorkReminderEmailSentCommand;
            _businessSafeSessionManager = businessSafeSessionManager;
            _bus = bus;
            _getEmployeeQuery = getEmployeeQuery;
        }

        public void Execute()
        {
            Log4NetHelper.Log.Info("Processing EmployeeOffWorkEscalation");

            using (var session = _businessSafeSessionManager.Session)
            {
                var accidentRecords = _getAccidentRecordOffWorkReminderQuery.Execute( session );

                foreach (AccidentRecord rec in accidentRecords)
                {
                    Log4NetHelper.Log.Debug(string.Format("Processing {0} off work reminders", accidentRecords.Count));

                    try
                    {
                        var creator = GetCreator(rec);

                        if ( CanSendEmailToCreator(creator))
                        {
                            _bus.Send(new SendOffWorkReminderEmail()
                                          {
                                              AccidentRecordId = rec.Id,
                                              RecipientEmail = creator.GetEmail(),
                                              AccidentRecordReference = rec.Reference,
                                              DateOfAccident = rec.DateAndTimeOfAccident.Value,
                                              Title = rec.Title
                                          }
                            );

                            _offWorkReminderEmailSentCommand.Execute(session, rec.Id, DateTime.Now);
                        }
                    }
                    catch (Exception exception)
                    {
                        Log4NetHelper.Log.Error("Exception encountered EmployeeOffWorkEscalation", exception);
                        EventLog.WriteEntry("EscalationService", exception.ToString(), EventLogEntryType.Error, EscalationServiceLogging.EventId);
                    }

                    
                }
                _businessSafeSessionManager.CloseSession();
            }
        }

        private static bool CanSendEmailToCreator(Employee creator)
        {
            return creator != null && creator.HasEmail;
        }

        private Employee GetCreator(AccidentRecord rec)
        {
            var employee =
                _getEmployeeQuery
                    .WithCompanyId(rec.CompanyId)
                    .WithEmployeeId(rec.CreatedBy.Employee.Id)
                    .Execute(_businessSafeSessionManager.Session);

            return employee;
        }
    }
}