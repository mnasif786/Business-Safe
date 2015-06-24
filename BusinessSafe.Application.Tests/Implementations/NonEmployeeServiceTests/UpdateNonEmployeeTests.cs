//using System.Collections.Generic;
//using BusinessSafe.Application.Implementations;
//using BusinessSafe.Application.Request;
//using BusinessSafe.Domain.Entities;
//using BusinessSafe.Domain.InfrastructureContracts.Logging;
//using BusinessSafe.Domain.RepositoryContracts;
//using Moq;
//using NUnit.Framework;
//using Peninsula.Online.Data.NHibernate.UnitOfWork;
//using StructureMap;

//namespace BusinessSafe.Application.Tests.Implementations.NonEmployeeServiceTests
//{
//    [TestFixture]
//    public class UpdateNonEmployeeTests
//    {
//        private Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
//        private Mock<IUnitOfWork> _unitOfWork;
//        private Mock<INonEmployeeRepository> _nonEmployeeRepository;
//        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
//        private Mock<IDoesNonEmployeeAlreadyExistGuard> _newNonEmployeeGuard;

//        [SetUp]
//        public void SetUp()
//        {
//            _nonEmployeeRepository = new Mock<INonEmployeeRepository>();
//            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
//            _unitOfWork = new Mock<IUnitOfWork>();
//            _newNonEmployeeGuard = new Mock<IDoesNonEmployeeAlreadyExistGuard>();
//            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();

//            _unitOfWorkFactory
//                .Setup(x => x.CreateBusinessSafeUnitOfWork())
//                .Returns(_unitOfWork.Object);

//            ObjectFactory.Inject<ILog>(new StubLog());
//        }

//        [Test]
//        public void Given_that_request_is_valid_When_update_non_employee_Then_should_call_guard_to_check_if_name_already_exists()
//        {
//            //Given
//            var target = CreateNonEmployeeervice();

//            var request = new UpdateNonEmployeeRequest(1,2, "name", "position", "company", true);


//            var nonEmployee = new Mock<NonEmployee>();
//            nonEmployee
//                .SetupGet(x => x.Id)
//                .Returns(request.NonEmployeeId);

//            _nonEmployeeRepository
//                .Setup(x => x.GetByIdAndCompanyId(request.NonEmployeeId, request.CompanyIdLink))
//                .Returns(nonEmployee.Object);

//            _newNonEmployeeGuard
//                .Setup(tp =>tp.Execute(It.Is<GuardDefaultExistsRequest>(x => x.CompanyId == request.CompanyIdLink && x.Name == request.Name)))
//                .Returns(GuardDefaultExistsResult.NoMatches);

//            //When
//            target.UpdateNonEmployee(request);

//            //Then
//            _newNonEmployeeGuard.VerifyAll();
//        }

//        [Test]
//        public void Given_that_request_is_valid_but_dont_need_match_check_When_update_non_employee_Then_should_not_call_guard_to_check_if_name_already_exists()
//        {
//            //Given
//            var target = CreateNonEmployeeervice();

//            var request = new UpdateNonEmployeeRequest(1, 2, "name", "position", "company", false);


//            var nonEmployee = new Mock<NonEmployee>();
//            nonEmployee
//                .SetupGet(x => x.Id)
//                .Returns(request.NonEmployeeId);

//            _nonEmployeeRepository
//                .Setup(x => x.GetByIdAndCompanyId(request.NonEmployeeId, request.CompanyIdLink))
//                .Returns(nonEmployee.Object);

            

//            //When
//            target.UpdateNonEmployee(request);

//            //Then
//            _newNonEmployeeGuard.Verify(tp => tp.Execute(It.IsAny<GuardDefaultExistsRequest>()), Times.Never());
                
//        }


//        [Test]
//        public void Given_that_request_is_invalid_non_employee_already_exists_guard_returns_true_When_update_non_employee_Then_should_return_correct_results()
//        {
//            //Given
//            var target = CreateNonEmployeeervice();

//            var request = new UpdateNonEmployeeRequest(1, 2, "name", "position", "company", true);

//            _newNonEmployeeGuard
//                .Setup(tp => tp.Execute(It.Is<GuardDefaultExistsRequest>(x => x.CompanyId == request.CompanyIdLink && x.Name == request.Name)))
//                .Returns(GuardDefaultExistsResult.MatchesExist(new List<string>()));

//            //When
//            var result = target.UpdateNonEmployee(request);

//            //Then
//            Assert.That(result.Success, Is.False);
//            Assert.That(result.Message, Is.EqualTo("Name already exists"));
//        }

//        [Test]
//        public void Given_that_request_is_valid_When_update_non_employee_Then_should_call_correct_methods()
//        {
//            //Given
//            var target = CreateNonEmployeeervice();

//            var request = new UpdateNonEmployeeRequest(1, 2, "name", "position", "company", true);

//            _newNonEmployeeGuard
//                .Setup(tp => tp.Execute(It.Is<GuardDefaultExistsRequest>(x => x.CompanyId == request.CompanyIdLink && x.Name == request.Name)))
//                .Returns(GuardDefaultExistsResult.NoMatches);

//            var nonEmployee = new Mock<NonEmployee>();
//            nonEmployee
//                .SetupGet(x => x.Id)
//                .Returns(request.NonEmployeeId);

//            nonEmployee.SetupSet(x => x.Name = request.Name);
//            nonEmployee.SetupSet(x => x.Position = request.Position);
//            nonEmployee.SetupSet(x => x.Company = request.NonEmployeeCompanyName);

//            _nonEmployeeRepository
//                .Setup(x => x.GetByIdAndCompanyId(request.NonEmployeeId, request.CompanyIdLink))
//                .Returns(nonEmployee.Object);

            
//            //When
//            target.UpdateNonEmployee(request);

//            //Then
//            nonEmployee.VerifyAll();
//            _nonEmployeeRepository.Verify(x => x.SaveOrUpdate(It.IsAny<NonEmployee>()));
//            _unitOfWork.Verify(x => x.Commit());
//            _unitOfWorkFactory.Verify(x => x.CreateBusinessSafeUnitOfWork());
//        }

//        [Test]
//        public void Given_that_request_is_valid_When_update_non_employee_Then_should_return_correct_response()
//        {
//            //Given
//            var target = CreateNonEmployeeervice();

//            var request = new UpdateNonEmployeeRequest(1, 2, "name", "position", "company", true);

//            _newNonEmployeeGuard
//                .Setup(tp => tp.Execute(It.Is<GuardDefaultExistsRequest>(x => x.CompanyId == request.CompanyIdLink && x.Name == request.Name)))
//                .Returns(GuardDefaultExistsResult.NoMatches);

//            var nonEmployee = new Mock<NonEmployee>();
//            nonEmployee
//                .SetupGet(x => x.Id)
//                .Returns(request.NonEmployeeId);
//            _nonEmployeeRepository
//                .Setup(x => x.GetByIdAndCompanyId(request.NonEmployeeId, request.CompanyIdLink))
//                .Returns(nonEmployee.Object);

//            //When
//            var result = target.UpdateNonEmployee(request);

//            //Then
//            Assert.That(result.Success, Is.True);
//            Assert.That(result.Message.Length, Is.EqualTo(0));
//        }

//        private NonEmployeeService CreateNonEmployeeervice()
//        {
//            var target = new NonEmployeeService(_unitOfWorkFactory.Object, _nonEmployeeRepository.Object, _riskAssessmentRepository.Object, _newNonEmployeeGuard.Object);
//            return target;
//        }
//    }
//}