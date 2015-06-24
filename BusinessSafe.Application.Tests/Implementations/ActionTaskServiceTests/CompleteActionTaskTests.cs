using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.ActionPlan;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ActionTaskServiceTests
{
     [TestFixture]
    public class CompleteActionTaskTests
    {
        private Mock<IActionTaskRepository> _actionTaskRepository;
        private Mock<IBus> _bus;
        private Mock<IPeninsulaLog> _log;



        [SetUp]
        public void Setup()
        {
            _actionTaskRepository = new Mock<IActionTaskRepository>();
            _bus = new Mock<IBus>();
            _log = new Mock<IPeninsulaLog>();
        }

        //[Test]
        //public void Given_Task_And_Company_When_Send_Completed_Then_Repository_Update_is_Called()
        //{
        //    // Given
        //    var taskId = 1L;
        //    var companyId = 1234L;
        //    UserForAuditing user = new UserForAuditing() { Id = Guid.NewGuid(), CompanyId = 589 };

        //    _actionTaskRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(new ActionTask());
        //    var target = GetTarget();

        //    // When
        //    CompleteActionTaskRequest request = new CompleteActionTaskRequest()
        //                                            {
        //                                                CompanyId = companyId,
        //                                                ActionTaskId = taskId,
        //                                                CompletedComments = "comments",
        //                                                UserId = user.Id,
        //                                                CompletedDate = DateTime.Now
        
        //                                                //public List<CreateDocumentRequest> CreateDocumentRequests { get; set; }
        //                                                //public List<long> DocumentLibraryIdsToDelete { get; set; }         
        //                                            };
        
        //    target.Complete(request);

        //    // Then
        //    _actionTaskRepository.Verify(x => x.GetByIdAndCompanyId(taskId, companyId));
        //}

        public ActionTaskService GetTarget()
        {
            return new ActionTaskService(_actionTaskRepository.Object,
                                            null,  // doc parameter
                                            null, // userforauditing
                                            null, // userRepository
                                            _bus.Object,
                                            _log.Object,
                                            null // actionRepository
                                            );
        }
    }
}
