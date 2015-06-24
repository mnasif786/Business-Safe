using BusinessSafe.WebSite.Areas.Sites.Controllers;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Contracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Sites
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private Mock<ISiteStructureViewModelFactory> _siteStructureViewModelFactory;

        [SetUp]
        public void SetUp()
        {
            _siteStructureViewModelFactory = new Mock<ISiteStructureViewModelFactory>();
        }

        [Test]
        public void Given_a_supplied_company_id_When_index_Then_should_return_correct_view()
        {
            //Given
            const string viewNameExpected = "Index";
            var target = CreateSitesController();

            _siteStructureViewModelFactory
             .Setup(x => x.WithClientId(TestControllerHelpers.CompanyIdAssigned))
             .Returns(_siteStructureViewModelFactory.Object);

            _siteStructureViewModelFactory
                .Setup(x => x.DisplaySiteDetails())
                .Returns(_siteStructureViewModelFactory.Object);

            _siteStructureViewModelFactory
                    .Setup(x => x.GetViewModel())
                    .Returns(GetSiteStructureViewModel());

            //When
            var result = target.Index();

            //Then            
            Assert.That(result.ViewName, Is.EqualTo(viewNameExpected));
        }

        [Test]
        public void Given_a_supplied_company_id_When_index_Then_should_return_correct_view_model()
        {
            //Given            
            var target = CreateSitesController();

            _siteStructureViewModelFactory
                .Setup(x => x.WithClientId(TestControllerHelpers.CompanyIdAssigned))
                .Returns(_siteStructureViewModelFactory.Object);

            _siteStructureViewModelFactory
                         .Setup(x => x.DisplaySiteDetails())
                         .Returns(_siteStructureViewModelFactory.Object);

            _siteStructureViewModelFactory
                    .Setup(x => x.GetViewModel())
                    .Returns(GetSiteStructureViewModel());
         
            //When
            var result = target.Index();

            //Then
            Assert.That(result.Model, Is.TypeOf<SiteStructureViewModel>());
        }

        private static SiteStructureViewModel GetSiteStructureViewModel()
        {
            return new SiteStructureViewModel(1, null, null, null, null, false, false, false, true);
        }

        private SitesStructureController CreateSitesController()
        {
            return TestControllerHelpers.AddUserToController(new SitesStructureController(_siteStructureViewModelFactory.Object));
        }
    }
}
