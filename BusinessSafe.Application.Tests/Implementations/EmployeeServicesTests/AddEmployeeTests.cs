using System;
using System.Linq;
using System.Linq.Expressions;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeServicesTests
{
    [TestFixture]
    public class AddEmployeeTests
    {
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<INationalityRepository> _nationalityRepository;
        private Mock<ICountriesRepository> _countriesRepository;
        private Mock<ICompanyVehicleTypeRepository> _companyVehicleTypeRepository;
        private Mock<IEmploymentStatusRepository> _employmentStatusRepository;
        private IEmployeeParametersMapper _employeeParametersMapper;
        private IEmployeeContactDetailsParametersMapper _employeeContactDetailsParametersMapper;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IPeninsulaLog> _log;
        
        [SetUp]
        public void SetUp()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _nationalityRepository = new Mock<INationalityRepository>();
            _countriesRepository = new Mock<ICountriesRepository>();
            _companyVehicleTypeRepository = new Mock<ICompanyVehicleTypeRepository>();
            _employmentStatusRepository = new Mock<IEmploymentStatusRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _log = new Mock<IPeninsulaLog>();
        
            _employeeParametersMapper = new EmployeeParametersMapper(
                _nationalityRepository.Object,
                _companyVehicleTypeRepository.Object,
                _employmentStatusRepository.Object, 
                _siteRepository.Object);

            _employeeContactDetailsParametersMapper =
                new EmployeeContactDetailsParametersMapper(_countriesRepository.Object);
            

            
            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());
        }

        [Test]
        public void Given_valid_request_When_add_employee_Then_should_call_correct_methods()
        {

            // Given
            var request = AddEmployeeRequestBuilder.Create().Build();
            var target = CreateEmployeeService();

            // When
            target.Add(request);

            // Then
            _employeeRepository.Verify(x => x.SaveOrUpdate(It.IsAny<Employee>()));
        }

        [Test]
        public void Given_valid_request_When_add_employee_Then_should_save_with_the_correct_property_values()
        {

            // Given
            var request = AddEmployeeRequestBuilder
                .Create()
                .WithEmployeeReference("Hey")
                .WithTitle("Mr")
                .WithForename("Paul")
                .WithSurname("Tester")
                .WithMiddleName("Barry")
                .WithPreviousSurname("Jane")
                .WithSex("Female")
                .WithDOB("01/01/2000")
                .WithNationalityId(1)
                .WithHasDisability(true)
                .WithDisabilityDescription("Somedescription")
                .WithNINumber("12356455")
                .WithPassportNumber("30")
                .WithPPS("202020")
                .WithWorkingVisaNumber("we we")
                .WithWorkingVisaExpirationDate(DateTime.Today.AddDays(10))
                .WithDrivingLicenseNumber("wo s sio ")
                .WithDrivingLicenseExpirationDate(DateTime.Today.AddHours(1))
                .WithHasCompanyVehicle(true)
                .WithCompanyVehicleRegistration("101010101")
                .WithCompanyVehicleTypeId(2)
                .WithSiteId(200)
                .WithJobTitle("Builder")
                .WithEmploymentStatusId(1)
                .WithAddress1("Address 1")
                .WithAddress2("Address 2")
                .WithAddress3("Address 3")
                .WithCounty("county")
                .WithCountryId(1)
                .WithPostcode("postcode")
                .WithTelephone("555 666")
                .WithMobile("5454 5454")
                .WithEmail("testing@hotmail.com")
                .WithUserId(Guid.NewGuid())
                .Build();


            var target = CreateEmployeeService();

          
            var nationality = new Mock<Nationality>();
            nationality.SetupGet(x => x.Id).Returns(request.NationalityId.Value);
            _nationalityRepository
                .Setup(x => x.LoadById(request.NationalityId.Value))
                .Returns(nationality.Object);

            var country = new Mock<Country>();
            country.SetupGet(x => x.Id).Returns(request.CountryId);
            _countriesRepository
                .Setup(x => x.LoadById(request.NationalityId.Value))
                .Returns(country.Object);

            var vehicleType = new Mock<CompanyVehicleType>();
            vehicleType.SetupGet(x => x.Id).Returns(request.CompanyVehicleTypeId.Value);
            _companyVehicleTypeRepository
                .Setup(x => x.LoadById(request.CompanyVehicleTypeId.Value))
                .Returns(vehicleType.Object);

            var employmentStatus = new Mock<EmploymentStatus>();

            employmentStatus.SetupGet(x => x.Id).Returns(request.EmploymentStatusId.Value);
            _employmentStatusRepository
                .Setup(x => x.LoadById(request.EmploymentStatusId.Value))
                .Returns(employmentStatus.Object);

            _siteRepository.Setup(x => x.GetById(request.SiteId.Value)).Returns(new Site()
                                                                                     {
                                                                                         Id = request.SiteId.Value
                                                                                     });

            // When
            target.Add(request);

            // Then
            _employeeRepository.Verify(x => x.SaveOrUpdate(It.Is(matchesAddEmployeeRequestProperties(request))));
        }

        private static Expression<Func<Employee, bool>> matchesAddEmployeeRequestProperties(AddEmployeeRequest request)
        {
            return y => y.EmployeeReference == request.EmployeeReference &&
                        y.Title == request.Title &&
                        y.Forename == request.Forename &&
                        y.Surname == request.Surname &&
                        y.MiddleName == request.MiddleName &&
                        y.PreviousSurname == request.PreviousSurname &&
                        y.Sex == request.Sex &&
                        y.DateOfBirth == request.DateOfBirth &&
                        y.Nationality.Id == request.NationalityId &&
                        y.HasDisability == request.HasDisability &&
                        y.DisabilityDescription == request.DisabilityDescription &&
                        y.NINumber == request.NINumber &&
                        y.PassportNumber == request.PassportNumber &&
                        y.PPSNumber == request.PPSNumber &&
                        y.WorkVisaNumber == request.WorkVisaNumber &&
                        y.WorkVisaExpirationDate == request.WorkVisaExpirationDate &&
                        y.DrivingLicenseNumber == request.DrivingLicenseNumber &&
                        y.DrivingLicenseExpirationDate == request.DrivingLicenseExpirationDate &&
                        y.HasCompanyVehicle == request.HasCompanyVehicle &&
                        y.CompanyVehicleRegistration == request.CompanyVehicleRegistration &&
                        y.CompanyVehicleType.Id == request.CompanyVehicleTypeId &&
                        y.Site.Id == request.SiteId &&
                        y.JobTitle == request.JobTitle &&
                        y.EmploymentStatus.Id == request.EmploymentStatusId &&
                        y.ContactDetails.First().Address1 == request.Address1 &&
                        y.ContactDetails.First().Address2 == request.Address2 &&
                        y.ContactDetails.First().Address3 == request.Address3 &&
                        y.ContactDetails.First().County == request.County &&
                        y.ContactDetails.First().PostCode == request.Postcode &&
                        y.ContactDetails.First().Country.Id == request.CountryId &&
                        y.ContactDetails.First().Telephone1 == request.Telephone &&
                        y.ContactDetails.First().Telephone2 == request.Mobile &&
                        y.ContactDetails.First().Email == request.Email;
        }




        private EmployeeService CreateEmployeeService()
        {
            return new EmployeeService(_employeeRepository.Object, _employeeParametersMapper, _employeeContactDetailsParametersMapper, _userRepository.Object, null, null, _log.Object, null, null, null);
        }
    }
}
