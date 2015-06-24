using System.Web.Mvc;

using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceInventory.Inventory
{
    [TestFixture]
    public class Index : HazardousSubtanceInventoryTest
    {

        [SetUp]
        public new void Setup()
        {
            base.Setup();

            viewModelFactory.Setup(x => x.WithCompanyId(It.IsAny<long>())).Returns(viewModelFactory.Object);
            viewModelFactory.Setup(x => x.WithShowDeleted(It.IsAny<bool>())).Returns(viewModelFactory.Object);
            viewModelFactory.Setup(x => x.GetViewModel()).Returns(new InventoryViewModel());
        }

        [Test]
        public void Index_Gets_View_From_Factory()
        {
            // Given
            long passedCompanyId = 0;
            viewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(viewModelFactory.Object)
                .Callback<long>(x => passedCompanyId = x);

            viewModelFactory
                .Setup(x => x.WithSupplierId(null))
                .Returns(viewModelFactory.Object);

            viewModelFactory
                .Setup(x => x.WithSubstanceNameLike(null))
                .Returns(viewModelFactory.Object);

            // When
            target.Index(1234, null, null);

            // Then
            viewModelFactory.VerifyAll();
            Assert.That(passedCompanyId, Is.EqualTo(1234));
        }

        [Test]
        public void Index_Returns_ViewModel_To_View()
        {
            // Given
            var viewModel = new InventoryViewModel();

            viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            viewModelFactory
                .Setup(x => x.WithSupplierId(null))
                .Returns(viewModelFactory.Object);

            viewModelFactory
                .Setup(x => x.WithSubstanceNameLike(null))
                .Returns(viewModelFactory.Object);

            // When
            var result = target.Index(It.IsAny<long>(), null, null) as ViewResult;
            var model = result.Model;

            // Then
            viewModelFactory.VerifyAll();
            Assert.That(model, Is.EqualTo(viewModel));
        }
    }
}
