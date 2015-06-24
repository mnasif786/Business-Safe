using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Commands;
using BusinessSafe.EscalationService.EscalateTasks;
using BusinessSafe.EscalationService.Queries;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NHibernate;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NServiceBus;

namespace BusinessSafe.EscalationService.Tests
{
    [TestFixture]
    public class EmployeeOffWorkEscalationTests
    {

        private Mock<IGetAccidentRecordOffWorkReminderQuery> _query;
        private Mock<IGetEmployeeQuery> _employeeQuery;

        private Mock<IOffWorkReminderEmailSentCommand> _command;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _sessionManager;


        private long _companyId = 1234L;

        [SetUp]
        public void SetUp()
        {
            _query = new Mock<IGetAccidentRecordOffWorkReminderQuery>();
            _employeeQuery = new Mock<IGetEmployeeQuery>();
            _command = new Mock<IOffWorkReminderEmailSentCommand>();
            _bus = new Mock<IBus>();
            _sessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void given_employee_escalation_task_then_call_correct_methods()
        {
            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session));

            _employeeQuery.Setup(x => x.Execute(It.IsAny<ISession>()));

            _query
             .Setup(x => x.Execute(_sessionManager.Object.Session))
             .Returns(new List<AccidentRecord>());

            EmployeeOffWorkEscalation task = CreateEscalationTask();

            //when
            task.Execute();

            //then
            _query.VerifyAll();

        }

        [Test]
        public void given_employee_escalation_task_then_call_nservice_bus_send_method_with_correct_parameters()
        {

            //given
            var email = "pbs@test.com";
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                ContactDetails = new List<EmployeeContactDetail>() {new EmployeeContactDetail{Id = 1L,Email = email} }
            };

            _employeeQuery.Setup(x => x.WithCompanyId(It.IsAny<long>())).Returns(_employeeQuery.Object);
            _employeeQuery.Setup(x => x.WithEmployeeId(It.IsAny<Guid>())).Returns(_employeeQuery.Object);
            _employeeQuery.Setup(x => x.Execute(It.IsAny<ISession>())).Returns(employee);

            var accidentRecord = new AccidentRecord
                                     {
                                         Id = 1L,
                                         CompanyId = 1234L,
                                         Jurisdiction = new Jurisdiction {Id = 1},
                                         InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Unknown,
                                         Title = "Test accident record",
                                         Reference = "AR1",
                                         DateAndTimeOfAccident = DateTime.Now,
                                         CreatedBy =
                                             new UserForAuditing
                                                 {Employee = new EmployeeForAuditing {Id = Guid.NewGuid()}}
                                     };
            
            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns( new List<AccidentRecord>(){ accidentRecord } );

            EmployeeOffWorkEscalation task = CreateEscalationTask();

            //when
            task.Execute();

            //then
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, It.IsAny<long>(), It.IsAny<DateTime>()), Times.Once());
            _bus.Verify(x => x.Send(It.Is<SendOffWorkReminderEmail>( y => 
                                                                         y.AccidentRecordId == accidentRecord.Id &&
                                                                         y.RecipientEmail == email &&
                                                                         y.Title == accidentRecord.Title &&
                                                                         y.AccidentRecordReference == accidentRecord.Reference &&
                                                                         y.DateOfAccident == accidentRecord.DateAndTimeOfAccident
                                        )));

            
        }

        [Test]
        public void given_accident_record_when_execute_then_employee_query_returns_employee()
        {
            //given
            EmployeeOffWorkEscalation task = CreateEscalationTask();
            var employee = new Employee
                               {
                                   Id = Guid.NewGuid()
                               };

            var accidentRecord = new AccidentRecord
            {
                Id = 1L,
                CompanyId = 1234L,
                Jurisdiction = new Jurisdiction { Id = 1 },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Unknown,
                Title = "Test accident record",
                Reference = "AR1",
                DateAndTimeOfAccident = DateTime.Now,
                CreatedBy = new UserForAuditing {Employee = new EmployeeForAuditing {Id = Guid.NewGuid()}}
            };

            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<AccidentRecord>(){accidentRecord});

            _employeeQuery.Setup(x => x.WithCompanyId(It.IsAny<long>())).Returns(_employeeQuery.Object);
            _employeeQuery.Setup(x => x.WithEmployeeId(It.IsAny<Guid>())).Returns(_employeeQuery.Object);
            _employeeQuery.Setup(x => x.Execute(It.IsAny<ISession>())).Returns(employee);

            _employeeQuery.Object
                .WithCompanyId(_companyId)
                .WithEmployeeId(employee.Id);

            //when
            task.Execute();

            //then
            _employeeQuery.Verify(x => x.WithCompanyId(_companyId), Times.AtLeastOnce());
            _employeeQuery.Verify(x => x.WithEmployeeId(employee.Id), Times.AtLeastOnce());
            _employeeQuery.Verify(x=>x.Execute(It.IsAny<ISession>()));

        }


        private EmployeeOffWorkEscalation CreateEscalationTask()
        {
            return new EmployeeOffWorkEscalation(_query.Object, _command.Object, _sessionManager.Object, _bus.Object, _employeeQuery.Object); 
        }
    }
}
