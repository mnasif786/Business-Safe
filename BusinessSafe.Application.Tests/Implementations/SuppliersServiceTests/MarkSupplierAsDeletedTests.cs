using System;
using BusinessSafe.Application.Implementations;
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
    public class MarkSupplierAsDeletedTests
    {
        private Mock<ISupplierRepository> _suppliersRepository;
        private Mock<IUserForAuditingRepository> _usersRepository;
        private Mock<IHazardousSubstancesRepository> _hazardousSubstanceRepository;

        [SetUp]
        public void Setup()
        {
            _suppliersRepository = new Mock<ISupplierRepository>();
            _usersRepository = new Mock<IUserForAuditingRepository>();
            _hazardousSubstanceRepository = new Mock<IHazardousSubstancesRepository>();
        }

        [Test]
        public void Given_valid_request_When_mark_supplier_as_deleted_Then_should_call_correct_methods()
        {
            // Given
            var target = CreateRolesService();
            
            var request =  new MarkCompanyDefaultAsDeletedRequest()
                               {
                                   UserId = Guid.NewGuid(),
                                   CompanyDefaultId = 1,
                                   CompanyId = 2
                               };

            var user = new UserForAuditing();
           _usersRepository
                .Setup(s => s.GetByIdAndCompanyId(request.UserId, request.CompanyId))
               .Returns(user);

            var supplier = new Mock<Supplier>();
            _suppliersRepository
                .Setup(x => x.GetByIdAndCompanyId(request.CompanyDefaultId, request.CompanyId))
                .Returns(supplier.Object);

            _hazardousSubstanceRepository
                .Setup(x => x.DoesHazardousSubstancesExistForSupplier(request.CompanyDefaultId, request.CompanyId))
                .Returns(false);

            // When
            target.MarkSupplierAsDeleted(request);

            // Then
            _suppliersRepository.Verify(x => x.SaveOrUpdate(supplier.Object));
            supplier.Verify(x => x.MarkForDelete(user));
            
        }

        [Test]
        public void Given_invalid_request_supplier_in_use_When_mark_supplier_as_deleted_Then_should_throw_correct_exception()
        {
            // Given
            var target = CreateRolesService();

            var request = new MarkCompanyDefaultAsDeletedRequest()
            {
                UserId = Guid.NewGuid(),
                CompanyDefaultId = 1,
                CompanyId = 2
            };
            
            _hazardousSubstanceRepository
                .Setup(x => x.DoesHazardousSubstancesExistForSupplier(request.CompanyDefaultId, request.CompanyId))
                .Returns(true);

            // When
            // Then
            Assert.Throws<TryingToDeleteSupplierThatUsedByHazardousSubstanceException>(() => target.MarkSupplierAsDeleted(request));

        }

      
        private SuppliersService CreateRolesService()
        {
            return new SuppliersService(_suppliersRepository.Object, _usersRepository.Object, _hazardousSubstanceRepository.Object);
        }
    }
}