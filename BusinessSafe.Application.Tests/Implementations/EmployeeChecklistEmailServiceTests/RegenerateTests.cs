using System;
using System.Collections.Generic;
using BusinessSafe.Application.Implementations.Checklists;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeChecklistEmailServiceTests
{
    [TestFixture]
    public class RegenerateTests
    {
        private Mock<IEmployeeChecklistEmailRepository> _employeeChecklistEmailRepository;
        private Mock<IEmployeeChecklistRepository> _employeeChecklistRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _employeeChecklistEmailRepository = new Mock<IEmployeeChecklistEmailRepository>();
            _employeeChecklistRepository = new Mock<IEmployeeChecklistRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void When_Regenerate_Then_should_call_correct_methods()
        {

            // Given
            var target = CreateService();
            var request = new ResendEmployeeChecklistEmailRequest()
                              {
                                  ResendUserId = Guid.NewGuid(),
                                  RiskAssessmentId = 500,
                                  EmployeeChecklistId = Guid.NewGuid()
                              };


            var user = new UserForAuditing();
            _userRepository
                .Setup(x => x.GetById(request.ResendUserId))
                .Returns(user);

            var employeeChecklistEmail = new EmployeeChecklist()
                                             {
                                                 EmployeeChecklistEmails = new List<EmployeeChecklistEmail>()
                                                                               {
                                                                                   new EmployeeChecklistEmail()
                                                                               },
                                                 Employee = new Employee
                                                                {
                                                                    ContactDetails = new List<EmployeeContactDetail>
                                                                                         {
                                                                                             new EmployeeContactDetail
                                                                                                 {
                                                                                                     Email = "test@test.com"
                                                                                                 }
                                                                                         }
                                                                }
                                             };
            _employeeChecklistRepository
                .Setup(x => x.GetByIdAndRiskAssessmentId(request.EmployeeChecklistId, request.RiskAssessmentId))
                .Returns(employeeChecklistEmail);

            // When
            target.Regenerate(request);
            
            // Then
            _userRepository.VerifyAll();
            _employeeChecklistRepository.VerifyAll();
            _employeeChecklistEmailRepository.Verify(x => x.Save(It.IsAny<EmployeeChecklistEmail>()), Times.Once());
            _employeeChecklistEmailRepository.Verify(x => x.Flush(), Times.Once());
        }

        private EmployeeChecklistEmailService CreateService()
        {
            return new EmployeeChecklistEmailService(_employeeChecklistEmailRepository.Object, null, null, _userRepository.Object, _log.Object, _employeeChecklistRepository.Object, null);
        }
    }
}