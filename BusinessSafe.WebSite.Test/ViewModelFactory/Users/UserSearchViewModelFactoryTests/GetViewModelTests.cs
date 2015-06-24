using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using Moq;
using NUnit.Framework;
using BusinessSafe.WebSite.Tests.Controllers;
using System.Security.Principal;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Users.UserSearchViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<IUserService> _userService;
        private Mock<ISiteService> _siteService;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
            _siteService = new Mock<ISiteService>();

            _userService
                .Setup(x => x.Search(It.IsAny<SearchUsersRequest>()))
                .Returns(new List<UserDto>()
                         {
                             new UserDto()
                             {
                                 Employee = new EmployeeDto()
                                            {
                                                Id = Guid.NewGuid(),
                                                EmployeeReference = "employee ref",
                                                Forename = "Vince",
                                                Surname = "Lee",
                                                JobTitle = "CodeMonkey"
                                            },
                                Role = new RoleDto(),
                                SiteStructureElement = new SiteStructureElementDto()
                                                           {
                                                               Id = 1,
                                                               IsMainSite = true
                                                           }
                             }
                         });

            _siteService
                .Setup(x => x.GetAll(It.IsAny<long>()))
                .Returns(new List<SiteDto>());
        }

        [Test]
        public void When_get_view_model_with_user_with_all_sites_permission_Then_site_name_set_to_all()
        {
            // Given
            var target = CreateSearchUserViewModelFactory();

            var currentUser = new Mock<ICustomPrincipal>();

            currentUser
                .Setup(x => x.UserId /*Identity.Name*/)
                .Returns(new Guid());

            // When
            var result = target
                .WithSiteGroupId(1234)
                .WithCurrentUser(currentUser.Object)
                .GetViewModel();
            var firstUser = result.Users[0];

            // Then
            Assert.That(firstUser.SiteName, Is.EqualTo("ALL"));
        }

        private UserSearchViewModelFactory CreateSearchUserViewModelFactory()
        {
            var result =  new UserSearchViewModelFactory(_userService.Object, _siteService.Object);
            return result;
        }
    }
}