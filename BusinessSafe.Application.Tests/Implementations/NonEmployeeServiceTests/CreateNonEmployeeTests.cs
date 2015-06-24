using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.NonEmployeeServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateNonEmployeeTests
    {
        private Mock<INonEmployeeRepository> _nonEmployeeRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;
        [SetUp]
        public void SetUp()
        {
            _nonEmployeeRepository = new Mock<INonEmployeeRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }


        [Test]
        public void Given_that_request_is_valid_for_new_non_employee_When_save_non_employee_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeervice();

            var request = SaveNonEmployeeRequestBuilder
                                 .Create()
                                 .Build();

            _nonEmployeeRepository
                .Setup(x => x.SaveOrUpdate(It.Is<NonEmployee>(y => y.Name == request.Name && y.Position == request.Position && y.Company == request.NonEmployeeCompanyName)));

            _userRepository
               .Setup(x => x.GetById(request.UserId))
               .Returns(_user);

            //When
            target.SaveNonEmployee(request);

            //Then
            _nonEmployeeRepository.Verify(x => x.SaveOrUpdate(It.Is<NonEmployee>(y => y.Name == request.Name && y.Position == request.Position && y.Company == request.NonEmployeeCompanyName)));
        }

        [Test]
        public void Given_that_request_is_valid_for_edit_non_employee_When_save_non_employee_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateNonEmployeeervice();

            var request = SaveNonEmployeeRequestBuilder
                                .Create()
                                .WithId(1)
                                .Build();


            var nonEmployee = new Mock<NonEmployee>();
            _nonEmployeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.Id, request.CompanyId))
                .Returns(nonEmployee.Object);

            _nonEmployeeRepository
                .Setup(x => x.SaveOrUpdate(It.Is<NonEmployee>(y => y.Name == request.Name && y.Position == request.Position && y.Company == request.NonEmployeeCompanyName)));

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            //When
            target.SaveNonEmployee(request);

            //Then
            nonEmployee.Verify(x => x.Update(request.Name, request.Position, request.NonEmployeeCompanyName, _user));
            _nonEmployeeRepository.VerifyAll();
        }


        [Test]
        public void Given_that_request_is_valid_for_new_non_employee_When_save_non_employee_Then_should_return_correct_response()
        {
            //Given
            var target = CreateNonEmployeeervice();

            var request = SaveNonEmployeeRequestBuilder.Create().Build();

            _userRepository
               .Setup(x => x.GetById(request.UserId))
               .Returns(_user);

            //When
            var result = target.SaveNonEmployee(request);

            //Then
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message.Length, Is.EqualTo(0));
        }

        [Test]
        public void Given_that_request_is_valid_for_edit_non_employee_When_save_non_employee_Then_should_return_correct_response()
        {
            //Given
            var target = CreateNonEmployeeervice();

            var request = SaveNonEmployeeRequestBuilder
                    .Create()
                    .WithId(1)
                    .Build();


            var nonEmployee = new Mock<NonEmployee>();
            _nonEmployeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.Id, request.CompanyId))
                .Returns(nonEmployee.Object);

            _nonEmployeeRepository
                .Setup(x => x.SaveOrUpdate(It.Is<NonEmployee>(y => y.Name == request.Name && y.Position == request.Position && y.Company == request.NonEmployeeCompanyName)));

            _userRepository
               .Setup(x => x.GetById(request.UserId))
               .Returns(_user);

            //When
            var result = target.SaveNonEmployee(request);

            //Then
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message.Length, Is.EqualTo(0));
        }

        private NonEmployeeService CreateNonEmployeeervice()
        {
            var target = new NonEmployeeService(_nonEmployeeRepository.Object, _userRepository.Object, _log.Object);
            return target;
        }

    }
}