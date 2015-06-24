using System.Collections.Generic;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.WebSite.Custom_Exceptions;
using BusinessSafe.WebSite.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.SiteStructureViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<ISiteService> _siteService;
        private Mock<IClientService> _clientService;
        private long _clientId;

        [SetUp]
        public void SetUp()
        {
            _clientId = 0;
            _siteService = new Mock<ISiteService>();
            _clientService = new Mock<IClientService>();
        }

        [Test]
        public void When_get_view_model_Then_should_call_appropiate_methods()
        {
            // Arrange
            var siteorganisationunit = new SiteDto() { };

            _siteService
                                       .Setup(x => x.GetSiteStructureByCompanyId(_clientId))
                                       .Returns(siteorganisationunit);

            _siteService.Setup(x => x.MainSiteExists(It.IsAny<long>())).Returns(true);

            //_clientService.Setup(x => x.GetCompanyDetails(_clientId)).Returns(new CompanyDetailsDto((int)_clientId, "Test Name",
            //                                                                                       "TST001", "Add1",
            //                                                                                       "Add2", "Add3",
            //                                                                                       "Add4", 88, "M11AA",
            //                                                                                       "09999999", null,
            //                                                                                       null));

            var viewModelFactory = CreateSiteStructureViewModelFactory();

            // Act
            viewModelFactory.GetViewModel();

            // Assert
            _siteService.VerifyAll();
            //_clientService.VerifyAll();
        }

        [Test]
        public void Given_No_MainSite_Setup_When_get_view_model_Then_Throw_Exception()
        {
            // Arrange
            var siteorganisationunit = new SiteDto() { };

            _siteService
                                       .Setup(x => x.GetSiteStructureByCompanyId(_clientId))
                                       .Returns(siteorganisationunit);

            _siteService.Setup(x => x.MainSiteExists(It.IsAny<long>())).Returns(false);

            _clientService.Setup(x => x.GetCompanyDetails(_clientId)).Returns(new CompanyDetailsDto((int)_clientId, "Test Name",
                                                                                                   "TST001", "Add1",
                                                                                                   "Add2", "Add3",
                                                                                                   "Add4", 88, "M11AA",
                                                                                                   "09999999", null,
                                                                                                   null));

            var viewModelFactory = CreateSiteStructureViewModelFactory();

            // Act

            // Assert
            Assert.Throws<NoMainSiteAvailableException>(() => viewModelFactory.GetViewModel());
        }
        
        private SiteStructureViewModelFactory CreateSiteStructureViewModelFactory()
        {
            return new SiteStructureViewModelFactory(_siteService.Object, new Mock<ISiteGroupService>().Object, _clientService.Object);
        }
    }
}
