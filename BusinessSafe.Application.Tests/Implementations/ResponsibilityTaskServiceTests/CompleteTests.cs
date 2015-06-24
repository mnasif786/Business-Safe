using System;
using System.Collections.Generic;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilityTaskServiceTests
{
    [TestFixture]
    public class CompleteTests
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
        private CompleteResponsibilityTaskRequest _request;

        [SetUp]
        public void SetUp()
        {
            _responsibilityTaskRepository = new Mock<IResponsibilityTaskRepository>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _userRepository = new Mock<IUserRepository>();
            _log = new Mock<IPeninsulaLog>();

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
            _request = new CompleteResponsibilityTaskRequest();
            _request.CompanyId = 3242L;
            _request.ResponsibilityTaskId = 234L;
            _request.CompletedComments = "Completed Comments 1";
            _request.CompletedDate = DateTime.Now;
            _request.DocumentLibraryIdsToDelete = new List<long>();

            _request.CreateDocumentRequests = new List<CreateDocumentRequest>
                                                  {
                                                      new CreateDocumentRequest
                                                          {
                                                              DocumentLibraryId = 8313L
                                                          },
                                                      new CreateDocumentRequest
                                                          {
                                                              DocumentLibraryId = 2626L
                                                          },
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
        }

        [Test]
        public void Given_valid_parameters_are_supplied_When_Completed_is_called_Then_responsibility_task_is_completed()
        {
            var target = GetTarget();
            target.Complete(_request);
            _responsibilityTaskRepository.Verify(x => x.GetByIdAndCompanyId(_request.ResponsibilityTaskId, _request.CompanyId));
            _userRepository.Verify(x => x.GetByIdAndCompanyId(_request.UserId, _request.CompanyId));
            _userForAuditingRepository.Verify(x => x.GetByIdAndCompanyId(_request.UserId, _request.CompanyId));

            _documentParameterHelper.Verify(x => x.GetCreateDocumentParameters(_request.CreateDocumentRequests, _request.CompanyId));
            _responsibilityTask.Verify(x => x.Complete(
                _request.CompletedComments,
                _createDocumentParameters,
                _request.DocumentLibraryIdsToDelete,
                _userForAuditing,
                _user,
                _request.CompletedDate
                ));

            _responsibilityTaskRepository.Verify(x => x.Update(_responsibilityTask.Object));
        }

        public ResponsibilityTaskService GetTarget()
        {
            return new ResponsibilityTaskService(
                _responsibilityTaskRepository.Object,
                _documentParameterHelper.Object,
                _userForAuditingRepository.Object,
                _userRepository.Object, null,
                _log.Object, null);
        }
    }
}
