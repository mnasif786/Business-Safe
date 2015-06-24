using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Data.Queries;
using BusinessSafe.Data.Queries.CompletedTaskQuery;
using BusinessSafe.Data.Queries.DueTaskQuery;
using BusinessSafe.Data.Queries.GetTaskEmployeesQuery;
using BusinessSafe.Data.Queries.OverdueTaskQuery;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.EscalationService.EscalateTasks;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NHibernate;
using NHibernate.Hql.Ast.ANTLR;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace BusinessSafe.EscalationService.Tests
{
    [TestFixture]
    public class EmailDigestTests
    {
        private Mock<IGetTaskEmployeesQuery> _taskEmployeeQuery;
        private Mock<IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>> _overdueGRATaskQuery;
        private Mock<IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>> _overduePRATaskQuery;
        private Mock<IGetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery> _overdueFRATaskQuery;
        private Mock<IGetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery> _overdueHSRATaskQuery;

        private Mock<IGetOverdueRiskAssessmentReviewTasksForEmployeeQuery> _overdueRAReviewTasksQuery;
        private Mock<IGetOverdueResponsibilitiesTasksForEmployeeQuery> _overdueResponsibilitiesTaskForEmployeeQuery;
        private Mock<IGetOverdueActionTasksForEmployeeQuery> _overdueActionTasksQuery;

        private Mock<IGetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery> _completedHSRATaskQuery;
        private Mock<IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>> _completedGRATaskQuery;
        private Mock<IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>> _completedPRATaskQuery;
        private Mock<IGetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery> _completedFRATaskQuery;
        private Mock<IGetCompletedRiskAssessmentReviewTasksForEmployeeQuery> _completedRAReviewTasksQuery;

        private  Mock<IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>> _dueTomorrowGRATasksQuery;
        private  Mock<IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>> _dueTomorrowPRATasksQuery;
        private  Mock<IGetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery> _dueTomorrowFRATasksQuery;
        private  Mock<IGetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery> _dueTomorrowHSRATasksQuery;
        private  Mock<IGetDueRiskAssessmentReviewTasksForEmployeeQuery> _dueTomorrowRiskAssessmentReviewTasksForEmployeeQuery;
        private  Mock<IGetDueResponsibilityTasksForEmployeeQuery> _dueTomorrowResponsibilityTasksForEmployeeQuery;
        private  Mock<IGetDueActionTasksForEmployeeQuery> _dueTomorrowActionTasksForEmployee;

        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private EmployeeNotificationsService _emailNotificationService;
        private Mock<IBus> _bus;
        private Mock<ITasksRepository> _taskRespository;

        private Mock<IBusinessSafeSessionManager> _sessionManager;
        private Mock<ISession> _session;
        private Mock<ITransaction> _transaction;

        [SetUp]
        public void Setup()
        {
            _overdueGRATaskQuery = new Mock<IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>();
            _overdueGRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
              .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { });

            _overduePRATaskQuery = new Mock<IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>();
            _overduePRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
               .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { });

            _overdueFRATaskQuery = new Mock<IGetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
            _overdueFRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
               .Returns(() => new List<FireRiskAssessmentFurtherControlMeasureTask>() { });

            _overdueHSRATaskQuery = new Mock<IGetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
            _overdueHSRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
             .Returns(() => new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>() { });

            _overdueActionTasksQuery = new Mock<IGetOverdueActionTasksForEmployeeQuery>();
            _overdueActionTasksQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
             .Returns(() => new List<ActionTask>() { });

            _overdueRAReviewTasksQuery = new Mock<IGetOverdueRiskAssessmentReviewTasksForEmployeeQuery>();
            _overdueRAReviewTasksQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
             .Returns(() => new List<RiskAssessmentReviewTask>() { });

            _overdueResponsibilitiesTaskForEmployeeQuery = new Mock<IGetOverdueResponsibilitiesTasksForEmployeeQuery>();
            _overdueResponsibilitiesTaskForEmployeeQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
                .Returns(new List<ResponsibilityTask>());
            

            _overdueHSRATaskQuery = new Mock<IGetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
            _overdueHSRATaskQuery
                .Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
                .Returns(new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>());


            _completedHSRATaskQuery = new Mock<IGetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
            _completedHSRATaskQuery
                .Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
                .Returns(new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>());

            _completedGRATaskQuery = new Mock<IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>();
            _completedGRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
              .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { });

            _completedPRATaskQuery = new Mock<IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>();
            _completedPRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
               .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { });

            _completedFRATaskQuery = new Mock<IGetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
            _completedFRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
               .Returns(() => new List<FireRiskAssessmentFurtherControlMeasureTask>());

            _completedRAReviewTasksQuery = new Mock<IGetCompletedRiskAssessmentReviewTasksForEmployeeQuery>();
            _completedRAReviewTasksQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
                .Returns(() => new List<RiskAssessmentReviewTask>());


            _dueTomorrowGRATasksQuery = new Mock<IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>();
            _dueTomorrowGRATasksQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
              .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { });

            _dueTomorrowPRATasksQuery = new Mock<IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>();
            _dueTomorrowPRATasksQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
              .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { });

            _dueTomorrowFRATasksQuery = new Mock<IGetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
            _dueTomorrowFRATasksQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
               .Returns(() => new List<FireRiskAssessmentFurtherControlMeasureTask>() { });

            _dueTomorrowHSRATasksQuery = new Mock<IGetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();
            _dueTomorrowHSRATasksQuery.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
               .Returns(() => new List<HazardousSubstanceRiskAssessmentFurtherControlMeasureTask>() { });

            _dueTomorrowRiskAssessmentReviewTasksForEmployeeQuery = new Mock<IGetDueRiskAssessmentReviewTasksForEmployeeQuery>();
            _dueTomorrowRiskAssessmentReviewTasksForEmployeeQuery.Setup(x => x.Execute(It.IsAny<Guid>(),It.IsAny<ISession>()))
                .Returns(() => new List<RiskAssessmentReviewTask>());

            _dueTomorrowResponsibilityTasksForEmployeeQuery = new Mock<IGetDueResponsibilityTasksForEmployeeQuery>();
            _dueTomorrowResponsibilityTasksForEmployeeQuery.Setup(x => x.Execute(It.IsAny<Guid>(),It.IsAny<ISession>()))
                .Returns(() => new List<ResponsibilityTask>());

            _dueTomorrowActionTasksForEmployee = new Mock<IGetDueActionTasksForEmployeeQuery>();
            _dueTomorrowActionTasksForEmployee.Setup(x => x.Execute(It.IsAny<Guid>(), It.IsAny<ISession>()))
                .Returns(() => new List<ActionTask>());

            _taskEmployeeQuery = new Mock<IGetTaskEmployeesQuery>();
            _taskEmployeeQuery.Setup(x => x.Execute())
                .Returns(new List<Employee>());

            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _userForAuditingRepository.Setup(x => x.GetSystemUser())
                .Returns(() => new UserForAuditing());

            _taskRespository = new Mock<ITasksRepository>();

            _bus = new Mock<IBus>();
            _transaction = new Mock<ITransaction>();
            _session = new Mock<ISession>();

            _session.Setup(x => x.BeginTransaction())
                .Returns(() => _transaction.Object);

            _sessionManager = new Mock<IBusinessSafeSessionManager>();

            _sessionManager.Setup(x => x.Session)
                .Returns(() => _session.Object);

            
            ObjectFactory.Container.Configure(x =>
            {
                x.For<IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>().Use(_overdueGRATaskQuery.Object);
                x.For<IGetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>().Use(_overduePRATaskQuery.Object);
                x.For<IGetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use(_overdueFRATaskQuery.Object);
                x.For<IGetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use(_overdueHSRATaskQuery.Object);
                x.For<IGetOverdueRiskAssessmentReviewTasksForEmployeeQuery>().Use(_overdueRAReviewTasksQuery.Object);
                x.For<IGetOverdueResponsibilitiesTasksForEmployeeQuery>().Use(_overdueResponsibilitiesTaskForEmployeeQuery.Object);
                x.For<IGetOverdueActionTasksForEmployeeQuery>().Use(_overdueActionTasksQuery.Object);

                x.For<IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>().Use(_dueTomorrowGRATasksQuery.Object);
                x.For<IGetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>().Use(_dueTomorrowPRATasksQuery.Object);
                x.For<IGetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use(_dueTomorrowFRATasksQuery.Object);
                x.For<IGetDueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use(_dueTomorrowHSRATasksQuery.Object);
                x.For<IGetDueRiskAssessmentReviewTasksForEmployeeQuery>().Use(_dueTomorrowRiskAssessmentReviewTasksForEmployeeQuery.Object);
                x.For<IGetDueResponsibilityTasksForEmployeeQuery>().Use(_dueTomorrowResponsibilityTasksForEmployeeQuery.Object);
                x.For<IGetDueActionTasksForEmployeeQuery>().Use(_dueTomorrowActionTasksForEmployee.Object);
                     
                x.For<IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>>().Use(_completedGRATaskQuery.Object);
                x.For<IGetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<PersonalRiskAssessment>>().Use(_completedPRATaskQuery.Object);
                x.For<IGetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use(_completedFRATaskQuery.Object);
                x.For<IGetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>().Use(_completedHSRATaskQuery.Object);
                x.For<IGetCompletedRiskAssessmentReviewTasksForEmployeeQuery>().Use(_completedRAReviewTasksQuery.Object);

                x.For<IGetTaskEmployeesQuery>().Use(_taskEmployeeQuery.Object);
            });
        }

        private EmailDigestEscalation GetTarget()
        {
            _emailNotificationService = new EmployeeNotificationsService(
                _taskRespository.Object,
                _bus.Object, 
                _userForAuditingRepository.Object,
                _overdueGRATaskQuery.Object,
                _overduePRATaskQuery.Object,
                _overdueFRATaskQuery.Object, 
                _overdueHSRATaskQuery.Object,  
                _overdueRAReviewTasksQuery.Object,
                _overdueResponsibilitiesTaskForEmployeeQuery.Object,
                _overdueActionTasksQuery.Object,
                _completedHSRATaskQuery.Object,
                _completedFRATaskQuery.Object,
                _completedGRATaskQuery.Object, 
                _completedPRATaskQuery.Object,
                _completedRAReviewTasksQuery.Object,
                _dueTomorrowGRATasksQuery.Object,
                _dueTomorrowPRATasksQuery.Object,
                _dueTomorrowFRATasksQuery.Object,
                _dueTomorrowHSRATasksQuery.Object,
                _dueTomorrowRiskAssessmentReviewTasksForEmployeeQuery.Object,
                _dueTomorrowResponsibilityTasksForEmployeeQuery.Object,
                _dueTomorrowActionTasksForEmployee.Object
                );
           
            return new EmailDigestEscalation( _emailNotificationService, _sessionManager.Object);            
        }

        private MultiHazardRiskAssessmentFurtherControlMeasureTask CreateTask()
        {
            var riskAssessement = new GeneralRiskAssessment();
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            return new MultiHazardRiskAssessmentFurtherControlMeasureTask() {MultiHazardRiskAssessmentHazard = hazard, SendTaskOverdueNotification = true};
        }

        private MultiHazardRiskAssessmentFurtherControlMeasureTask CreatePRATask()
        {
            var riskAssessement = new PersonalRiskAssessment();
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            return new MultiHazardRiskAssessmentFurtherControlMeasureTask() { MultiHazardRiskAssessmentHazard = hazard, SendTaskOverdueNotification = true };
        }

        [Test]
        public void given_a_value_for_EscalationService_LastSuccessfulExecutionTime_in_config_then_next_executIOn_is_one_day_after_value()
        {
            //given          
            var baseDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 30, 0);
            ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"] = baseDateTime.AddDays(-5).ToString();

            var expectedDateTime = baseDateTime.AddDays(-4);
            var target = GetTarget();

            //when
            var result = target.NextExecution;

            //then
            Assert.That(result, Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void given_no_value_for_EscalationService_LastSuccessfulExecutionTime_in_config_then_next_execution_is_one_day_after_today()
        {
            //given          
            var baseDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 30, 0);           
            var expectedDateTime = baseDateTime.AddDays(1);
            ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"] = "";

            var target = GetTarget();

            //when
            var result = target.NextExecution;

            //then
            Assert.That(result, Is.EqualTo(expectedDateTime));
        }



        [Test]
        public void given_email_digest_when_executes_then_the_NextExecution_is_incremented_by_a_day()
        {
            //given
            var baseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 30, 0);
            ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"] = baseTime.AddDays(-1).ToString();

            var expectedDateTime = baseTime.AddDays(1);
            var target = GetTarget();

            //when
            target.Execute();

            //then
            Assert.That(target.NextExecution, Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void given_datetime_now_is_greater_than_the_next_execution_time_when_executes_then_generates_emails_for_all_employees()
        {
            //given
            var employee = new Employee() { Id = Guid.Parse("aaaaaaaa-e5f1-44b3-a9be-342284ed3513"), CompanyId = 33748, NotificationType = NotificationType.Daily, NotificationFrequecy = null };
            var overdueGRATask = CreateTask();
            overdueGRATask.SendTaskOverdueNotification = true;
            overdueGRATask.TaskAssignedTo = employee;

            var employees = new List<Employee>() { new Employee() { Id = Guid.NewGuid() }, new Employee() { Id = Guid.NewGuid() }, employee };

            _taskEmployeeQuery.Setup(x => x.Execute())
              .Returns(() => employees);

            _overdueGRATaskQuery.Setup(x => x.Execute(It.Is<Guid>(id => id != employee.Id), _session.Object))
                .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { });

            _overdueGRATaskQuery.Setup(x => x.Execute(employee.Id, _session.Object))
                .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { overdueGRATask });

            var baseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 30, 0);
            ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"] = baseTime.AddDays(-2).ToString();

            var target = GetTarget();

            //when
            target.Execute();

            //then
            _bus.Verify(x => x.Send(It.IsAny<SendEmployeeDigestEmail>()), Times.Once());
        }

        [Test]
        public void given_task_is_marked_as_do_not_send_task_notification_when_executes_then_email_not_sent()
        {
            //given
            var overdueDoNotSendTask = CreateTask();
            overdueDoNotSendTask.SendTaskOverdueNotification = false;
            var employees = new List<Employee>() { new Employee() { Id = Guid.NewGuid() }, new Employee() { Id = Guid.NewGuid(), CompanyId = 33748 }, new Employee() { Id = Guid.NewGuid() } };

            _taskEmployeeQuery.Setup(x => x.Execute())
                .Returns(() => employees);

            _overdueGRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), _session.Object))
              .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { overdueDoNotSendTask });


            var baseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 30, 0);
            ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"] = baseTime.AddDays(-2).ToString();
            var target = GetTarget();

            //when
            target.Execute();

            //then
            Assert.That(overdueDoNotSendTask.EmployeeTaskNotificationHistory.Count, Is.EqualTo(0));
            _bus.Verify(x => x.Send(It.IsAny<SendEmployeeDigestEmail>()), Times.Never());
        }

        [Test]
        public void given_gra_task_is_marked_as_do_not_send_task_notification_and_pra_task_is_marked_as_send_when_executes_then_email_sent()
        {
            //given
            var employee = new Employee() { Id = Guid.NewGuid(), CompanyId = 33748, NotificationType = NotificationType.Daily, NotificationFrequecy = null };
            employee.UpdateRiskAssessorDetails(true, false, false, false, null);
            var overdueDoNotSendGRATask = CreateTask();
            overdueDoNotSendGRATask.SendTaskOverdueNotification = false;

            var overduePRATask = CreatePRATask();
            overduePRATask.RiskAssessment.RiskAssessor = employee.RiskAssessor;

            var employees = new List<Employee>() {employee};

            _taskEmployeeQuery.Setup(x => x.Execute())
                .Returns(() => employees);

            _overdueGRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), _session.Object))
                .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() {overdueDoNotSendGRATask});

            _overduePRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), _session.Object))
                .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() {overduePRATask});

            SendEmployeeDigestEmail serviceBusCommand = null;
            _bus.Setup(x => x.Send(It.IsAny<SendEmployeeDigestEmail>()))
                .Callback<object[]>(p1 => serviceBusCommand = (SendEmployeeDigestEmail) p1[0]);

            var baseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 30, 0);
            ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"] = baseTime.AddDays(-2).ToString();
            var target = GetTarget();

            //when
            target.Execute();

            //then
            _bus.Verify(x => x.Send(It.IsAny<SendEmployeeDigestEmail>()), Times.Once());
            Assert.That(serviceBusCommand.GeneralRiskAssessmentsOverdueTasks.Count, Is.EqualTo(0));
            Assert.That(serviceBusCommand.PersonalRiskAssessmentTasksOverdue.Count, Is.EqualTo(1));
        }

        [Test]
        public void given_gra_task_is_marked_as_send_task_notification_and_employee_is_risk_assessor_when_executes_then_email_sent()
        {
            //given
            var employee = new Employee() { Id = Guid.NewGuid(), CompanyId = 33748, NotificationType = NotificationType.Daily, NotificationFrequecy = null };
            employee.UpdateRiskAssessorDetails(true, false, false, false, null);
            var overdueGRATask = CreateTask();
            overdueGRATask.SendTaskOverdueNotification = true;
            overdueGRATask.RiskAssessment.RiskAssessor = employee.RiskAssessor;

            var employees = new List<Employee>() { employee };

            _taskEmployeeQuery.Setup(x => x.Execute())
                .Returns(() => employees);

            _overdueGRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), _session.Object))
                .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { overdueGRATask });

            _overduePRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), _session.Object))
                .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { });

            SendEmployeeDigestEmail serviceBusCommand = null;
            _bus.Setup(x => x.Send(It.IsAny<SendEmployeeDigestEmail>()))
                .Callback<object[]>(p1 => serviceBusCommand = (SendEmployeeDigestEmail)p1[0]);

            var baseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 30, 0);
            ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"] = baseTime.AddDays(-2).ToString();
            var target = GetTarget();


            //when
            target.Execute();

            //then
            _bus.Verify(x => x.Send(It.IsAny<SendEmployeeDigestEmail>()), Times.Once());
            Assert.That(serviceBusCommand.GeneralRiskAssessmentsOverdueTasks.Count, Is.EqualTo(1));
            Assert.That(serviceBusCommand.PersonalRiskAssessmentTasksOverdue.Count, Is.EqualTo(0));
        }

        [Test]
        public void given_gra_task_is_marked_as_send_task_notification_and_employee_is_not_risk_assessor_when_executes_then_email_not_sent()
        {
            //given
            var employee = new Employee() { Id = Guid.NewGuid(), CompanyId = 33748 };
            var overdueGRATask = CreateTask();
            overdueGRATask.SendTaskOverdueNotification = true;
            overdueGRATask.RiskAssessment.RiskAssessor = new RiskAssessor() { Employee = new Employee() { Id = Guid.NewGuid() } };
            var employees = new List<Employee>() { employee };

            _taskEmployeeQuery.Setup(x => x.Execute())
                .Returns(() => employees);

            _overdueGRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), _session.Object))
                .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { overdueGRATask });

            _overduePRATaskQuery.Setup(x => x.Execute(It.IsAny<Guid>(), _session.Object))
                .Returns(() => new List<MultiHazardRiskAssessmentFurtherControlMeasureTask>() { });

            SendEmployeeDigestEmail serviceBusCommand = null;
            _bus.Setup(x => x.Send(It.IsAny<SendEmployeeDigestEmail>()))
                .Callback<object[]>(p1 => serviceBusCommand = (SendEmployeeDigestEmail)p1[0]);

            var baseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 30, 0);
            ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"] = baseTime.AddDays(-2).ToString();
            var target = GetTarget();


            //when
            target.Execute();

            //then
            Assert.That(overdueGRATask.EmployeeTaskNotificationHistory.Count, Is.EqualTo(0));
            _bus.Verify(x => x.Send(It.IsAny<SendEmployeeDigestEmail>()), Times.Never());
        }


        [Test]
        public void given_action_task_is_marked_as_send_task_notification_and_employee_is_task_assignee_when_executes_then_email_sent()
        {
            //given
            var employee = new Employee() { Id = Guid.NewGuid(), CompanyId = 33748, NotificationType = NotificationType.Daily, NotificationFrequecy = null };

            var overdueActionTask = CreateActionTask();

            overdueActionTask.SendTaskOverdueNotification = true;
            overdueActionTask.TaskAssignedTo = employee;

            var employees = new List<Employee>() { employee };

            _taskEmployeeQuery.Setup(x => x.Execute())
                .Returns(() => employees);

            _overdueActionTasksQuery
                .Setup(x => x.Execute(It.IsAny<Guid>(), _session.Object))
                .Returns(() => new List<ActionTask>() { overdueActionTask });


            SendEmployeeDigestEmail serviceBusCommand = null;
            _bus
                .Setup(x => x.Send(It.IsAny<SendEmployeeDigestEmail>()))
                .Callback<object[]>(p1 => serviceBusCommand = (SendEmployeeDigestEmail)p1[0]);

            var baseTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 30, 0);
            ConfigurationManager.AppSettings["EscalationService_LastSuccessfulExecutionTime"] = baseTime.AddDays(-2).ToString();
            var target = GetTarget();


            //when
            target.Execute();

            //then
            _bus.Verify(x => x.Send(It.IsAny<SendEmployeeDigestEmail>()), Times.Once());
            Assert.That(serviceBusCommand.ActionTasksOverdue.Count, Is.EqualTo(1));
        }

        private ActionTask CreateActionTask()
        {            
            ActionTask task = new ActionTask();

            return task;
        }
    }
}