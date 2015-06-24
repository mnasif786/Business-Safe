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
    public class CreateSupplierTests
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
        public void Given_valid_request_When_create_supplier_Then_should_call_correct_methods()
        {
            // Given
            var target = CreateRolesService();

            var request = new SaveSupplierRequest()
                              {
                                  UserId = Guid.NewGuid(),
                                  Name = "New Supplier",
                                  CompanyId = 2
                              };

            var user = new UserForAuditing();
            _usersRepository
                .Setup(s => s.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);

            
            // When
            var supplierPassedToRepositoryForSave = new Supplier();
            _suppliersRepository.Setup(x => x.SaveOrUpdate(It.IsAny<Supplier>()))
                .Callback<Supplier>(y => supplierPassedToRepositoryForSave = y);

            target.CreateSupplier(request);

            // Then
            _suppliersRepository.VerifyAll();
            Assert.That(supplierPassedToRepositoryForSave.CompanyId, Is.EqualTo(request.CompanyId));
            Assert.That(supplierPassedToRepositoryForSave.Name, Is.EqualTo(request.Name));
        }


        private SuppliersService CreateRolesService()
        {
            return new SuppliersService(_suppliersRepository.Object, _usersRepository.Object, null);
        }
    }
}