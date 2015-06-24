using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Data.Repository;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;

namespace BusinessSafe.Application.IntegrationTests.Repository
{
    [TestFixture]
    [Ignore] //ignored becuase i want to test on my machine and configuring the test to work on a different environment is a pain. ALP
    public class FurtherControlMeasureTasksRepositoryTest
    {
        [Test]
        public void Given_task_exists_but_different_clientid_specified_when_GetByIdAndCompanyId_then_throw_Task_not_found_exception()
        {
            var target = new FurtherControlMeasureTasksRepository(ObjectFactory.GetInstance<IBusinessSafeSessionManager>());

            Assert.Throws<TaskNotFoundException>(() => target.GetByIdAndCompanyId(11, 123213));
            
        }

        [Test]
        public void Given_task_when_GetByIdAndCompanyId_then_return_task()
        {
            var target = new FurtherControlMeasureTasksRepository(ObjectFactory.GetInstance<IBusinessSafeSessionManager>());

            var result = target.GetByIdAndCompanyId(11, 55881);

            Assert.AreEqual(11,result.Id);
        }
    }
}
