using System;
using BusinessSafe.Application.Implementations.Checklists;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.Checklists.EmployeeChecklistServiceTests
{
    public class BaseEmployeeServiceTests
    {
        protected EmployeeChecklistService _target;
        protected Mock<IPeninsulaLog> _log;
        protected Mock<IEmployeeChecklistRepository> _employeeChecklistrepo;
        protected Mock<IUserForAuditingRepository> userRepository;
        protected Mock<IQuestionRepository> _questionRepository;

        [SetUp]
        public void Setup()
        {
            _employeeChecklistrepo = new Mock<IEmployeeChecklistRepository>();
            userRepository = new Mock<IUserForAuditingRepository>();
            _employeeChecklistrepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new EmployeeChecklist());

            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));
            _log.Setup(x => x.Add(It.IsAny<object[]>()));
            _log.Setup(x => x.Add(It.IsAny<Exception>()));

            _questionRepository = new Mock<IQuestionRepository>();
        }

        protected EmployeeChecklistService GetTarget()
        {
            return new EmployeeChecklistService(userRepository.Object, _employeeChecklistrepo.Object, _questionRepository.Object, _log.Object, null);
        }
    }
}
