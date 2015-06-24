using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.WebSite.Areas.Company.Tasks;

namespace BusinessSafe.WebSite.Areas.Company.Factories
{
    public class CompanyDefaultsTaskFactory : ICompanyDefaultsTaskFactory
    {
        private readonly ICompanyDefaultService _companyDefaultService;
        private readonly IDoesCompanyDefaultAlreadyExistGuard _companyDefaultAlreadyExistGuard;
        private readonly IHazardRepository _hazardRepository;
        private readonly IPeopleAtRiskRepository _peopleAtRiskRepository;
        private readonly ISuppliersService _suppliersService;
        private readonly ISupplierRepository _supplierRepository;

        public CompanyDefaultsTaskFactory(ICompanyDefaultService companyDefaultService, ISuppliersService suppliersService, IDoesCompanyDefaultAlreadyExistGuard companyDefaultAlreadyExistGuard, IHazardRepository hazardRepository, IPeopleAtRiskRepository peopleAtRiskRepository, ISupplierRepository supplierRepository)
        {
            _companyDefaultService = companyDefaultService;
            _companyDefaultAlreadyExistGuard = companyDefaultAlreadyExistGuard;
            _hazardRepository = hazardRepository;
            _peopleAtRiskRepository = peopleAtRiskRepository;
            _supplierRepository = supplierRepository;
            _suppliersService = suppliersService;
        }

        public ICompanyDefaultsSaveTask CreateSaveTask(string companyDefaultType)
        {
            switch (companyDefaultType.Replace(" ",""))
            {
                case "Hazards":
                    return new HazardSaveTask(_companyDefaultService,_hazardRepository, _companyDefaultAlreadyExistGuard);
                case "PeopleAtRisk":
                    return new PeopleAtRiskSaveTask(_companyDefaultService, _peopleAtRiskRepository ,_companyDefaultAlreadyExistGuard);
                case "SpecialistSuppliers":
                    return new SuppliersSaveTask(_suppliersService, _supplierRepository, _companyDefaultAlreadyExistGuard);
                case "FireSafetyControlMeasure":
                    return new FireSafetyControlMeasureSaveTask(_companyDefaultService,_companyDefaultAlreadyExistGuard);
                case "SourceOfFuel":
                    return new SourceOfFuelSaveTask(_companyDefaultService, _companyDefaultAlreadyExistGuard);
                case "SourceOfIgnition":
                    return new SourceOfIgnitionSaveTask(_companyDefaultService, _companyDefaultAlreadyExistGuard);
                case "Injury":
                    return new InjurySaveTask(_companyDefaultService, _companyDefaultAlreadyExistGuard);
                default:
                    throw new ArgumentException("companyDefaultType is not defined.");
            }
        }

        public ICompanyDefaultDeleteTask CreateMarkForDeletedTask(string companyDefaultType)
        {
            switch (companyDefaultType.Replace(" ", ""))
            {
                case "Hazards":
                    return new HazardMarkAsDeletedTask(_companyDefaultService);
                case "PeopleAtRisk":
                    return new PersonAtRiskMarkAsDeletedTask(_companyDefaultService);
                case "SpecialistSuppliers":
                    return new SuppliersMarkAsDeletedTask(_suppliersService);
                default:
                    throw new ArgumentException("companyDefaultType is not defined.");
            }
        }
    }
}