using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Data.Repository;
using BusinessSafe.Domain.Entities.SafeCheck;
using NHibernate.Linq;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;

namespace BusinessSafe.Application.IntegrationTests.Repository
{
    [TestFixture]
    [Ignore] //ignored becuase i want to test on my machine and configuring the test to work on a different environment is a pain. ALP
    public class TaskListRepositoryTest
    {
        [Test]
        public void Given_task_exists()
        {
            var russellWilliamsEmployeeId = Guid.Parse("D2122FFF-1DCD-4A3C-83AE-E3503B394EB4");
            var employee = new EmployeeRepository(ObjectFactory.GetInstance<IBusinessSafeSessionManager>()).GetById(russellWilliamsEmployeeId);

            var target = new TaskListRepository(ObjectFactory.GetInstance<IBusinessSafeSessionManager>(), ObjectFactory.GetInstance<IBusinessSafeSessionManagerFactory>());

            List<long> siteIds = employee.User.Site.GetThisAndAllDescendants().Select(x => x.Id).ToList();
            var result = target.Search(55881, new List<Guid>(){russellWilliamsEmployeeId}, false, siteIds);

            ObjectFactory.GetInstance<IBusinessSafeSessionManager>().CloseSession();
        }

        [Test]
        public void Given_task_exists_get_task_for_riskassessment()
        {
            var target = new TaskListRepository(ObjectFactory.GetInstance<IBusinessSafeSessionManager>(), ObjectFactory.GetInstance<IBusinessSafeSessionManagerFactory>());
            var result = target.GetFurtherControlMeasureTasksByRiskAssessmentId(66, 62473);

            Console.WriteLine(result.Count());

            ObjectFactory.GetInstance<IBusinessSafeSessionManager>().CloseSession();
        }

        [Test]
        public void Given_a_bespoke_template()
        {
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();

            var entities = new List<ChecklistTemplate>();

            using (var session = sessionManager.Session)
            {

                var entity = session.Query<ChecklistTemplate>()
                    .Where(x => x.Id == Guid.Parse("804F0C74-C0AD-4687-9F22-C95DAC20D51F"))
                    .FetchMany(x => x.Questions)
                    .ThenFetch(x => x.Question)
                    .ThenFetchMany(x => x.PossibleResponses)                   .ToFuture()
              .ToList();

                Assert.That(entity.Count, Is.EqualTo(1));
                Assert.That(entity[0].Questions.Count, Is.EqualTo(159));
                    
                sessionManager.CloseSession();
            }

          

           
        }

    }
}
