using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Commands;
using BusinessSafe.EscalationService.Queries;
using Moq;
using NHibernate;
using NServiceBus;
using NUnit.Framework;
using BusinessSafe.EscalationService.Activation;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;

namespace BusinessSafe.EscalationService.Tests.IntegrationTests
{
    [TestFixture]
    public class TaskOverDueEscalationTests
    {
        [SetUp]
        public void Setup()
        {
            Mock<IBus> bus = new Mock<IBus>();
            Bootstrapper.Run(bus.Object);


        }

        [Ignore]
        [Test]
        public void test()
        {
            
            var query = ObjectFactory.GetInstance<IGetOverDueTasksQuery>();
            var result = new List<Task>();
            using (var session = ObjectFactory.GetInstance<IBusinessSafeSessionManager>().Session)
            {
                result = query.Execute(session).ToList();
    
            }
            //var taskWIthNoRiskAssessment = result.Where(task => task.RiskAssessment == null);
            //var taskWIthRiskAssessment = result.Where(task => task.RiskAssessment != null);

            foreach (var task in result.Where(task => task.RiskAssessment != null))
            {
                Assert.IsFalse(task.RiskAssessment.Deleted);
            }
        }
    }
}
