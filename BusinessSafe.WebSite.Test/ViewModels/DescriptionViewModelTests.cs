using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels
{
    [TestFixture]
    [Category("Unit")]
    public class DescriptionViewModelTests
    {
        [Test]
        public void Given_user_without_permissions_When_IsSaveEnabled_Then_returns_correct_result()
        {
            // Arrange
            var user = new Mock<ICustomPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(false);

            // Act
            var result = new DescriptionViewModel().IsSaveButtonEnabled(user.Object);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void Given_user_with_permissions_When_IsSaveEnabled_Then_returns_correct_result()
        {
            // Arrange
            var user = new Mock<ICustomPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var descriptionViewModel = new DescriptionViewModel();
                                         

            // Act
            var result = descriptionViewModel.IsSaveButtonEnabled(user.Object);

            // Assert
            Assert.True(result);
        }

     
    }
}