using BusinessSafe.WebSite.Areas.Sites.Controllers;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Contracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Sites
{
    [TestFixture]
    [Category("Unit")]
    public class SiteOrganisationalChartTests
    {
        [Test]
        public void Given_that_site_organisational_chart_is_requested_Then_correct_view_is_returned()
        {
            //Given
            long siteId = TestControllerHelpers.CompanyIdAssigned;
            var siteStructureViewModelFactory = new Mock<ISiteStructureViewModelFactory>();
            siteStructureViewModelFactory.Setup(x => x.WithClientId(siteId)).Returns(siteStructureViewModelFactory.Object);
            siteStructureViewModelFactory.Setup(x => x.GetViewModel()).Returns(new SiteStructureViewModel(siteId, null, null, null, null, false, false, false, true));

            var target = TestControllerHelpers.AddUserToController(new SitesStructureController(siteStructureViewModelFactory.Object));

            //When
            var result = target.SiteOrganisationalChart(siteId);

            //Then
            Assert.That(result.ViewName, Is.EqualTo("IndexSiteOrganisationalChart"));
        }
    }
}
