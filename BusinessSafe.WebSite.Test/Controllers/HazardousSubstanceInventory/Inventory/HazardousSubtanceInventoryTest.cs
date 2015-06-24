using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Controllers;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories;
using Moq;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceInventory.Inventory
{
    public class HazardousSubtanceInventoryTest
    {
        protected Mock<IInventoryViewModelFactory> viewModelFactory;
        protected InventoryController target;
        protected Mock<IHazardousSubstancesService> hazardousSubstancesService;
        protected Mock<IHazardousSubstanceViewModelFactory> hazardousSubstanceViewModelFactory;

        public void Setup()
        {
            viewModelFactory = new Mock<IInventoryViewModelFactory>();
            hazardousSubstancesService = new Mock<IHazardousSubstancesService>();
            hazardousSubstanceViewModelFactory = new Mock<IHazardousSubstanceViewModelFactory>();

            hazardousSubstanceViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(hazardousSubstanceViewModelFactory.Object);
            
            target = GetController();
            
        }

        private InventoryController GetController()
        {
            var controller = new InventoryController(
                viewModelFactory.Object,
                hazardousSubstancesService.Object,
                hazardousSubstanceViewModelFactory.Object);

            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}