using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests;
using BusinessSafe.Messages.Emails.Commands;

using NServiceBus;

using NUnit.Framework;
using Moq;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilityTaskServiceTests
{
    [TestFixture]
    public class SendTaskCompletedNotificationEmailsTests
    {
        private Mock<IResponsibilityTaskRepository> _responsibilityTaskRepository;
        private Mock<IDocumentParameterHelper> _documentParameterHelper;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private Mock<IUserRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private User _user;
        private UserForAuditing _userForAuditing;
        private IEnumerable<CreateDocumentParameters> _createDocumentParameters;
        private Mock<ResponsibilityTask> _responsibilityTask;
        private Mock<IBus> _bus;
        private CompleteResponsibilityTaskRequest _request;

        private const string _taskTitle = "title";
        private const string _taskReference = "ref";
        private const string _taskAssignedTo = "assigned to";
        private const string _taskDescription = "description";
        private const string _responsibilityOwnerEmail = "risk assessor email";
        private const string _responsibilityOwnerName = "risk assessor name";
        private const string _completedByName = "completed by name";

        [SetUp]
        public void SetUp()
        {
            _responsibilityTaskRepository = new Mock<IResponsibilityTaskRepository>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _userRepository = new Mock<IUserRepository>();
            _log = new Mock<IPeninsulaLog>();
            _bus = new Mock<IBus>();

            _user = new User
                        {
                            Id = Guid.NewGuid(),
                            Employee = new Employee
                                           {
                                               Id = Guid.NewGuid()
                                           }
                        };

            _userForAuditing = new UserForAuditing {Id = Guid.NewGuid()};
            _responsibilityTask = new Mock<ResponsibilityTask>();
            _createDocumentParameters = new List<CreateDocumentParameters>();
            _request = new CompleteResponsibilityTaskRequest
                       {
                           CompanyId = 3242L,
                           ResponsibilityTaskId = 234L,
                           CompletedComments = "Completed Comments 1",
                           CompletedDate = DateTime.Now,
                           DocumentLibraryIdsToDelete = new List<long>(),
                           CreateDocumentRequests = new List<CreateDocumentRequest>
                                                    {
                                                        new CreateDocumentRequest
                                                        {
                                                            DocumentLibraryId = 8313L
                                                        },
                                                        new CreateDocumentRequest
                                                        {
                                                            DocumentLibraryId = 2626L
                                                        },
                                                    }
                       };


            _documentParameterHelper
                .Setup(x => x.GetCreateDocumentParameters(_request.CreateDocumentRequests, _request.CompanyId))
                .Returns(_createDocumentParameters);

            _responsibilityTaskRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.ResponsibilityTaskId, _request.CompanyId))
                .Returns(_responsibilityTask.Object);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.UserId, _request.CompanyId))
                .Returns(_user);

            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.UserId, _request.CompanyId))
                .Returns(_userForAuditing);

            _responsibilityTask.Setup(x => x.IsTaskCompletedNotificationRequired()).Returns(true);
            _responsibilityTask.Setup(x => x.Title).Returns(_taskTitle);
            _responsibilityTask.Setup(x => x.Description).Returns(_taskDescription);
            _responsibilityTask.Setup(x => x.Reference).Returns(_taskReference);
            _responsibilityTask.Setup(x => x.TaskAssignedTo.FullName).Returns(_taskAssignedTo);
            _responsibilityTask.Setup(x => x.Responsibility.Owner.FullName).Returns(_responsibilityOwnerName);
            _responsibilityTask.Setup(x => x.Responsibility.Owner.GetEmail()).Returns(_responsibilityOwnerEmail);
            _responsibilityTask.Setup(x => x.TaskCompletedBy.Employee.FullName).Returns(_completedByName);

            _bus.Setup(x => x.Send(It.IsAny<SendResponsibilityTaskCompletedEmail>()));
        }

        [Test]
        public void When_SendTaskCompletedNotificationEmail_Then_retrieve_task_from_repo()
        {
            // Given
            var target = GetTarget();

            // When
            target.SendTaskCompletedNotificationEmail(_request);

            // Then
            _responsibilityTaskRepository.Verify(x => x.GetByIdAndCompanyId(_request.ResponsibilityTaskId, _request.CompanyId));
        }

        [Test]
        public void Given_no_matching_task_When_SendTaskCompletedNotificationEmail_Then_log_and_throw_exception()
        {
            // Given
            _responsibilityTaskRepository.Setup(x => x.GetByIdAndCompanyId(_request.ResponsibilityTaskId, _request.CompanyId));
            var target = GetTarget();

            // When

            // Then
            var e = Assert.Throws<ResponsibilityTaskNotFoundException>(() => target.SendTaskCompletedNotificationEmail(_request));
            _log.Verify(x => x.Add(e));
        }

        [Test]
        public void Given_send_notification_is_required_for_task_When_SendTaskCompletedNotificationEmail_Then_tell_bus_to_send()
        {
            // Given
            SendResponsibilityTaskCompletedEmail objectPassedToBus = null;
            _bus
                .Setup(x => x.Send(It.IsAny<object[]>()))
                .Callback<object[]>(y => objectPassedToBus = y.First() as SendResponsibilityTaskCompletedEmail);
            var target = GetTarget();

            // When
            target.SendTaskCompletedNotificationEmail(_request);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<object[]>()));
            Assert.That(objectPassedToBus.Title, Is.EqualTo(_taskTitle));
            Assert.That(objectPassedToBus.Description, Is.EqualTo(_taskDescription));
            Assert.That(objectPassedToBus.TaskAssignedTo, Is.EqualTo(_taskAssignedTo));
            Assert.That(objectPassedToBus.ResponsibilityOwnerEmail, Is.EqualTo(_responsibilityOwnerEmail));
            Assert.That(objectPassedToBus.ResponsibilityOwnerName, Is.EqualTo(_responsibilityOwnerName));
            Assert.That(objectPassedToBus.TaskReference, Is.EqualTo(_taskReference));
            Assert.That(objectPassedToBus.CompletedBy, Is.EqualTo(_completedByName));
        }

        public ResponsibilityTaskService GetTarget()
        {
            return new ResponsibilityTaskService(
                _responsibilityTaskRepository.Object,
                _documentParameterHelper.Object,
                _userForAuditingRepository.Object,
                _userRepository.Object,
                _bus.Object,
                _log.Object,null);
        }
    }
}
