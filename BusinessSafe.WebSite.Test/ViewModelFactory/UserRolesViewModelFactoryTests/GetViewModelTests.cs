using System;
using System.Linq;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.Users.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.UserRolesViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<IRolesService> _permissionsService;

        private const long _companyId = 1234;
        private string firstExpectedRole;
        private string _secondExpectedRole;

        [SetUp]
        public void Setup()
        {
            _permissionsService = new Mock<IRolesService>();
        }

        [Test]
        public void When_get_view_model_Then_return_correct_view_model()
        {
            //Given
            var target = CreateUserRolesViewModelFactory();

            firstExpectedRole = "Alpha";
            _secondExpectedRole = "Zoo";
            var roles = new[]{ new RoleDto()
                                                        {
                                                            Id = Guid.NewGuid(),
                                                            Name = _secondExpectedRole,
                                                            Description = _secondExpectedRole
                                                        },
                                     new RoleDto()
                                                        {
                                                            Id = Guid.NewGuid(),
                                                            Name = firstExpectedRole,
                                                            Description = firstExpectedRole
                                                        } };
            _permissionsService.Setup(x => x.GetAllRoles(_companyId)).Returns(roles);


            //When
            var result = target
                            .WithCompanyId(_companyId)
                            .GetViewModel();

            //Then
            Assert.That(result, Is.TypeOf<UserRolesViewModel>());
            Assert.That(result.CompanyRoles.Skip(1).Take(1).First().label, Is.EqualTo(firstExpectedRole));
            Assert.That(result.CompanyRoles.Last().label, Is.EqualTo(_secondExpectedRole));

        }

        private UserRolesViewModelFactory CreateUserRolesViewModelFactory()
        {
            return new UserRolesViewModelFactory(_permissionsService.Object);
        }

    }
}