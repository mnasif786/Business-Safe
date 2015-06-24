//using System;
//using System.Collections.Generic;
//using System.Linq;
//using BusinessSafe.Application.Implementations;
//using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
//using BusinessSafe.Application.Request;
//using BusinessSafe.Application.Request.Documents;
//using BusinessSafe.Domain.Entities;
//using BusinessSafe.Domain.InfrastructureContracts.Logging;
//using BusinessSafe.Domain.ParameterClasses;
//using BusinessSafe.Domain.RepositoryContracts;

//using Moq;
//using NUnit.Framework;

//namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentFurtherActionServiceTests
//{
//    [TestFixture]
//    [Category("Unit")]
//    public class CompleteFurtherActionTaskTest
//    {
//        private Mock<IGeneralRiskAssessmentRepository> _riskAssessmentRepo;
//        private Mock<IUserForAuditingRepository> _userRepository;
//        private Mock<IEmployeeRepository> _employeeRepository;
//        private Mock<IDocumentTypeRepository> _documentTypeRepository;
//        private Mock<IFurtherControlMeasureTaskRepository> _furtherControlMeasureTaskRepository;
//        private Mock<IPeninsulaLog> _log;
//        private UserForAuditing _user;

//        [SetUp]
//        public void SetUp()
//        {
//            _riskAssessmentRepo = new Mock<IGeneralRiskAssessmentRepository>();
//            _userRepository = new Mock<IUserForAuditingRepository>();
//            _employeeRepository = new Mock<IEmployeeRepository>();
//            _documentTypeRepository = new Mock<IDocumentTypeRepository>();
//            _furtherControlMeasureTaskRepository = new Mock<IFurtherControlMeasureTaskRepository>();
//            _user = new UserForAuditing();
//            _log = new Mock<IPeninsulaLog>();
//        }

//        [Test]
//        public void Given_a_valid_request_When_Mark_As_Complete_Then_should_call_appropriate_methods()
//        {
//            // Given
//            var target = CreateRiskAssessmentFurtherActionService();

//            var request = new CompleteTaskRequest()
//                              {
//                                  CompanyId = 1,
//                                  FurtherControlMeasureTaskId = 4,
//                                  CompletedComments = "Test",
//                                  UserId = Guid.NewGuid(),
//                                  CreateDocumentRequests = new List<CreateDocumentRequest>()
//                              };

//            var furtherControlMeasureTask = new Mock<GeneralRiskAssessmentFurtherControlMeasureTask>();

//            _furtherControlMeasureTaskRepository
//                .Setup(x => x.GetByIdAndCompanyId(4, 1))
//                .Returns(furtherControlMeasureTask.Object);

//            // When
//            target.CompleteFurtherControlMeasureTask(request);

//            //Then
//            _userRepository.Verify(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId), Times.Once());
//            _furtherControlMeasureTaskRepository.Verify(x => x.GetByIdAndCompanyId(4, 1));
//            furtherControlMeasureTask.Verify(x=>x.Complete("Test", It.IsAny<List<CreateDocumentParameters>>(), It.IsAny<List<long>>(), It.IsAny<UserForAuditing>() ));
//        }

//        [Test]
//        public void Given_a_valid_request_When_Mark_As_Complete_Then_clientid_passed_to_any_attached_documents()
//        {
//            // Given
//            var target = CreateRiskAssessmentFurtherActionService();

//            var request = new CompleteTaskRequest()
//            {
//                CompanyId = 1,
//                FurtherControlMeasureTaskId = 4,
//                CompletedComments = "Test",
//                UserId = Guid.NewGuid(),
//                CreateDocumentRequests = new List<CreateDocumentRequest>()
//            };

//            var passedCreateDocumentParameters = new List<CreateDocumentParameters>();
//            var furtherControlMeasureTask = new Mock<GeneralRiskAssessmentFurtherControlMeasureTask>();
//            furtherControlMeasureTask
//                .Setup(x => x.Complete(
//                    It.IsAny<string>(),
//                    It.IsAny<IEnumerable<CreateDocumentParameters>>(),
//                    It.IsAny<IEnumerable<long>>(),
//                    It.IsAny<UserForAuditing>()
//                ))
//                .Callback<string, IEnumerable<CreateDocumentParameters>, IEnumerable<long>, UserForAuditing>(
//                    (string a, IEnumerable<CreateDocumentParameters> b, IEnumerable<long> c, UserForAuditing d) => passedCreateDocumentParameters = b.ToList());

//            _furtherControlMeasureTaskRepository
//                .Setup(x => x.GetByIdAndCompanyId(4, 1))
//                .Returns(furtherControlMeasureTask.Object);

//            // When
//            target.CompleteFurtherControlMeasureTask(request);

//            //Then

//            foreach (var docParam in passedCreateDocumentParameters)
//            {
//                Assert.That(docParam.ClientId, Is.EqualTo(request.CompanyId));
//            }

//            furtherControlMeasureTask.Verify(x => x.Complete(
//                    It.IsAny<string>(),
//                    It.IsAny<IEnumerable<CreateDocumentParameters>>(),
//                    It.IsAny<IEnumerable<long>>(),
//                    It.IsAny<UserForAuditing>())
//            , Times.Once());
//        }

//        private RiskAssessmentFurtherControlMeasureTaskService CreateRiskAssessmentFurtherActionService()
//        {
//            var target = new RiskAssessmentFurtherControlMeasureTaskService(_riskAssessmentRepo.Object, _userRepository.Object, _employeeRepository.Object,  null, _documentTypeRepository.Object, _furtherControlMeasureTaskRepository.Object,  _log.Object);

//            return target;
//        }
//    }
//}