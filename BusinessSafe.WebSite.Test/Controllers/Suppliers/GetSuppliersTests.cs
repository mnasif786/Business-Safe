using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Suppliers
{
    [TestFixture]
    [Category("Unit")]
    public class GetSuppliersTests
    {
        private Mock<ISuppliersService> _suppliersService;

        [SetUp]
        public void SetUp()
        {
            _suppliersService = new Mock<ISuppliersService>();
        }

        [Test]
        public void When_GetSuppliers_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateController();
            var companyId = target.CurrentUser.CompanyId;

            var suppliers = new SupplierDto[]
                                {
                                    new SupplierDto() 
                                };
            _suppliersService
                .Setup(x => x.Search("filter", companyId, 200))
                .Returns(suppliers);

            //When
            target.GetSuppliers(companyId, "filter",200);

            //Then            
            _suppliersService.VerifyAll();
        }

        [Test]
        public void When_GetSuppliers_Then_should_return_correct_result()
        {
            //Given
            var target = CreateController();
            var companyId = target.CurrentUser.CompanyId;

            var suppliers = new SupplierDto[]
                                {
                                    new SupplierDto()
                                        {
                                            Id = 1,
                                            Name = "Test Supplier"
                                        } 
                                };
            _suppliersService
                .Setup(x => x.Search("filter", companyId, 200))
                .Returns(suppliers);

            //When
            var result = target.GetSuppliers(companyId, "filter", 200);
            var returnedSuppliers = result.Data as IEnumerable<AutoCompleteViewModel>;

            //Then            
            Assert.That(returnedSuppliers.Count(), Is.EqualTo(2));
            Assert.That(returnedSuppliers.Skip(1).Take(1).First().label, Is.EqualTo("Test Supplier"));
        }

        private SupplierController CreateController()
        {
            var result = new SupplierController(_suppliersService.Object, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}