using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Data.Queries;
using BusinessSafe.Data.Queries.OverdueTaskQuery;
using BusinessSafe.Domain.RepositoryContracts;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using BusinessSafe.Data.Queries.GetTaskEmployeesQuery;

namespace BusinessSafe.Application.IntegrationTests.Implementations
{
    [TestFixture]
    public class EmployeeNotificationServiceTests
    {
        [TearDown]
        public void Teardown()
        {
            ObjectFactory.GetInstance<IBusinessSafeSessionManager>().CloseSession();
        }

        [Ignore]
        [Test]
        public void Given_employe_has_overdue_tasks_then_email_command_sent_to_bus()
        {
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            var target = ObjectFactory.GetInstance<IEmployeeNotificationsService>();
            var employeeRepo = ObjectFactory.GetInstance<IEmployeeRepository>();
            var ids = new[] {Guid.Parse("086838fc-76c0-4bf7-afd7-9b0d53372d7b"), Guid.Parse("a433e9b2-84f6-4ad7-a89c-050e914dff01")};

            using (var session = sessionManager.Session)
            {

                var employees = employeeRepo.GetByIds(ids);

                foreach (var employee in employees)
                {
                    target.CreateAndSendEmployeeEmailDigest(session, employee);
                }

                sessionManager.CloseSession();
            }

        }

        [Ignore]
        [Test]
        public void Get_task_employee_queries_successfully_execute()
        {
            var queries = ObjectFactory.GetAllInstances<IGetTaskEmployeesQuery>();

            foreach (var query in queries)
            {
                var result = query.Execute();
            }
        }

        [Ignore]
        [Test]
        public void GetOverdueFireRiskAssessmentFurtherControlMeasureTasks_Query_successfully_executes()
        {
            var employeeId = Guid.Parse("D2122FFF-1DCD-4A3C-83AE-E3503B394EB4");
            var query = ObjectFactory.GetInstance<IGetOverdueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery>();

            var result = query.Execute(employeeId, null);


            //GetOverdueFireRiskAssessmentFurtherControlMeasureTasksQuery
        }
    }
}