using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BusinessSafe.Application.Implementations.Checklists;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeChecklistEmailServiceTests
{
    [TestFixture]
    public class GetByIdsTests
    {
        private Mock<IEmployeeChecklistEmailRepository> _employeeChecklistEmailRepository;
        private Mock<IPeninsulaLog> _log;
        
        [SetUp]
        public void SetUp()
        {
            _employeeChecklistEmailRepository = new Mock<IEmployeeChecklistEmailRepository>();
            _log = new Mock<IPeninsulaLog>();    
        }

        [Test]
        public void When_GetByIds_Then_should_call_correct_methods()
        {

            // Given
            var target = CreateService();
            var ids = new List<Guid>()
                                  {
                                      Guid.NewGuid()
                                  };

            var employeeChecklistEmails = new BindingList<EmployeeChecklistEmail>()
                                              {
                                                  new EmployeeChecklistEmail()
                                              };
            _employeeChecklistEmailRepository.Setup(x => x.GetByIds(ids)).Returns(employeeChecklistEmails);

            // When
            target.GetByIds(ids);

            // Then
            _employeeChecklistEmailRepository.VerifyAll();
        }

        [Test]
        public void When_GetByIds_Then_should_return_correct_result()
        {

            // Given
            var target = CreateService();
            var ids = new List<Guid>()
                                  {
                                      Guid.NewGuid()
                                  };

            var expectedChecklistEmail = new Mock<EmployeeChecklistEmail>();
            expectedChecklistEmail.Object.Id = Guid.NewGuid();
            expectedChecklistEmail.Object.Message = "Go go go";
            expectedChecklistEmail.SetupGet(x => x.RecipientEmail).Returns("Test@hotmail.com");
            expectedChecklistEmail.Object.EmployeeChecklists = new List<EmployeeChecklist>(){ new EmployeeChecklist()};


            var employeeChecklistEmails = new BindingList<EmployeeChecklistEmail>()
                                              {
                                                  expectedChecklistEmail.Object
                                              };
            _employeeChecklistEmailRepository.Setup(x => x.GetByIds(ids)).Returns(employeeChecklistEmails);

            // When
            var result = target.GetByIds(ids);

            // Then
            Assert.That(result.Count(),Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo(expectedChecklistEmail.Object.Id));
            Assert.That(result.First().Message, Is.EqualTo(expectedChecklistEmail.Object.Message));
            Assert.That(result.First().RecipientEmail, Is.EqualTo(expectedChecklistEmail.Object.RecipientEmail));
            Assert.That(result.First().EmployeeChecklists, Is.EqualTo(expectedChecklistEmail.Object.EmployeeChecklists));
            
        }
        
        private EmployeeChecklistEmailService CreateService()
        {
            return new EmployeeChecklistEmailService(_employeeChecklistEmailRepository.Object, null,null,null, _log.Object, null, null);
        }
    }
}
