using BusinessSafe.EscalationService.Activation;
using BusinessSafe.EscalationService.EscalateTasks;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;

namespace BusinessSafe.EscalationService.Tests
{
    [TestFixture]
    public class BootstrapperTests
    {
        [Test]
        public void When_run_Then_configures_structuremap_correctly()
        {
            // Given
            // When
            Bootstrapper.Run(new Mock<IBus>().Object);

            // Then 
            var tasks = ObjectFactory.GetAllInstances<IEscalate>();
            Assert.That(tasks.Count,Is.EqualTo(5));

            var sessionManager1 = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            var sessionManager2 = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();

            Assert.That(sessionManager1, Is.EqualTo(sessionManager2));
        }
    }
}