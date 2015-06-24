using System.Collections.Generic;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.HazardousSubstanceInventory
{
    public class SuppliersService : ISuppliersService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IHazardousSubstancesRepository _hazardousSubstancesRepository;

        public SuppliersService(ISupplierRepository supplierRepository, IUserForAuditingRepository userForAuditingRepository, IHazardousSubstancesRepository hazardousSubstancesRepository)
        {
            _supplierRepository = supplierRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _hazardousSubstancesRepository = hazardousSubstancesRepository;
        }

        public IEnumerable<SupplierDto> GetForCompany(long companyId)
        {
            var suppliers = _supplierRepository.GetByCompanyId(companyId);
            return new SupplierDtoMapper().Map(suppliers);
        }

        public void MarkSupplierAsDeleted(MarkCompanyDefaultAsDeletedRequest request)
        {
            if(_hazardousSubstancesRepository.DoesHazardousSubstancesExistForSupplier(request.CompanyDefaultId, request.CompanyId))
            {
                throw new TryingToDeleteSupplierThatUsedByHazardousSubstanceException(request.CompanyDefaultId);
            }

            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var supplier = _supplierRepository.GetByIdAndCompanyId(request.CompanyDefaultId, request.CompanyId);

            supplier.MarkForDelete(user);
            _supplierRepository.SaveOrUpdate(supplier);
        }

        public long CreateSupplier(SaveSupplierRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var supplier = Supplier.Create(request.Name, request.CompanyId, user);
            _supplierRepository.SaveOrUpdate(supplier);
            return supplier.Id;
        }

        public void UpdateSupplier(SaveSupplierRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            var supplier = _supplierRepository.GetByIdAndCompanyId(request.Id, request.CompanyId);
            supplier.Update(request.Name, user);
            _supplierRepository.SaveOrUpdate(supplier);
        }

        public IEnumerable<SupplierDto> Search(string searchTerm, long companyId, int pageLimit)
        {
            var suppliers = _supplierRepository.GetAllByNameSearch(searchTerm, 0, companyId, pageLimit);
            return new SupplierDtoMapper().Map(suppliers);
        }
   }
}
