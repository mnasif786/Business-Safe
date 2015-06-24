using System;

using BusinessSafe.Application.Implementations.HazardousSubstanceInventory;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.UserRolePermissionsViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateteSupplierTests
    {
        private Mock<ISupplierRepository> _suppliersRepository;
        private Mock<IUserForAuditingRepository> _usersRepository;

        [SetUp]
        public void Setup()
        {
            _suppliersRepository = new Mock<ISupplierRepository>();
            _usersRepository = new Mock<IUserForAuditingRepository>();
        }

        [Test]
        public void Given_valid_request_When_update_supplier_Then_should_call_correct_methods()
        {
            // Given
            var target = CreateRolesService();

            var request = new SaveSupplierRequest()
                              {
                                  UserId = Guid.NewGuid(),
                                  Name = "Updated Supplier Name",
                                  CompanyId = 2
                              };

            var user = new UserForAuditing();
            _usersRepository
                .Setup(s => s.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);

            var supplier = new Mock<Supplier>();
            _suppliersRepository
                .Setup(x => x.GetByIdAndCompanyId(request.Id, request.CompanyId))
                .Returns(supplier.Object);


            // When
            target.UpdateSupplier(request);

            // Then
            _suppliersRepository.Verify(x => x.SaveOrUpdate(supplier.Object));
            supplier.Verify(x => x.Update(request.Name, user));
        }


        private SuppliersService CreateRolesService()
        {
            return new SuppliersService(_suppliersRepository.Object, _usersRepository.Object, null);
        }
    }
}