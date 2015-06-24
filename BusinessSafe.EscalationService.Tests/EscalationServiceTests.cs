using System.Threading;
using BusinessSafe.EscalationService.EscalateTasks;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.EscalationService.Tests
{
    [TestFixture]
    public class EscalationServiceTests
    {
        [Test]
        public void When_process_Then_calls_the_execute_on_all_required_escalate_tasks()
        {
            // Given
            var escalateTask1 = new Mock<IEscalate>();
            var escalateTask2 = new Mock<IEscalate>();
            var escalateTask3 = new Mock<IEscalate>();
            ObjectFactory.Container.EjectAllInstancesOf<IEscalate>();
            ObjectFactory.Inject(typeof(IEscalate), escalateTask1.Object);
            ObjectFactory.Inject(typeof(IEscalate), escalateTask2.Object);
            ObjectFactory.Inject(typeof(IEscalate), escalateTask3.Object);
            
            var escalationService = new EscalationService(100);

            // When
            escalationService.Start();

            Thread.Sleep(2000);

            escalationService.Stop();

            // Then 
            escalateTask1.Verify(x => x.Execute());
            escalateTask2.Verify(x => x.Execute());
            escalateTask3.Verify(x => x.Execute());
        }

        [Test]
        public void When_process_Then_reads_pollingInterval_from_AppSettings()
        {
            // Given
            var escalationService = new EscalationService();

            // When
            var result = escalationService.PollingIntervalInMilliSeconds;

            // Then
            Assert.That(result, Is.EqualTo(3600000));
        }
    }
}
