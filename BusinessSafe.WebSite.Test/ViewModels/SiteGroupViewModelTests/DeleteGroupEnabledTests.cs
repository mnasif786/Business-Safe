using System.Collections.Generic;
using System.Security.Principal;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.SiteGroupViewModelTests
{
    [TestFixture]
    [Category("Unit")]
    public class DeleteGroupEnabledTests
    {
        private IPrincipal _user;

        [SetUp]
        public void Setup()
        {
            var userDto = new UserDto()
                              {
                                  Permissions = new List<string>() {"DeleteSiteDetails"}
                              };
            _user = new CustomPrincipal(userDto, new CompanyDto());
        }
        [Test]
        public void Given_in_adding_editing_group_that_have_no_children_and_is_saved_site_Then_delete_enabled_should_be_true()
        {
            //Given
            var target = new SiteGroupDetailsViewModel {HasChildren  = false, GroupId = 1};
            
            //When
            var result = target.IsDeleteButtonEnabled(_user);

            //Then
            Assert.That(result, Is.True);
        }

        [Test]
        public void Given_in_adding_editing_group_which_have_children_sites_or_groups_Than_delete_should_be_enabled()
        {
            //Given
            var target = new SiteGroupDetailsViewModel { HasChildren = true , GroupId = 1};

            //When
            var result = target.IsDeleteButtonEnabled(_user);

            //Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_in_adding_new_group_state_Than_delete_should_not_be_enabled()
        {
            //Given
            var target = new SiteGroupDetailsViewModel { GroupId = 0 };

            //When
            var result = target.IsDeleteButtonEnabled(_user);

            //Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_editing_new_group_state_but_without_correct_permisions_Than_delete_should_not_be_enabled()
        {
            //Given
            var target = new SiteGroupDetailsViewModel { GroupId = 1 };
            var userDto = new UserDto()
            {
                CompanyId = 0,
                Permissions = new List<string>()
            };

            //When
            _user = new CustomPrincipal(userDto, new CompanyDto());
            var result = target.IsDeleteButtonEnabled(_user);

            //Then
            Assert.That(result, Is.False);
        }    
    }
}