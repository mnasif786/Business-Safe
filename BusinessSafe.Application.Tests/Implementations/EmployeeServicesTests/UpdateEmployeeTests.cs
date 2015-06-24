using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using Moq.Protected;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Messages.Commands;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeServicesTests
{
    [TestFixture]
    public class UpdateEmployeeTests
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
        private Mock<IBus> _bus;
        private Mock<IPeninsulaLog> _log;
        private Mock<ISiteStructureElementRepository> _siteStructureElementRepository;

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
            _siteStructureElementRepository = new Mock<ISiteStructureElementRepository>();

            _employeeParametersMapper = new EmployeeParametersMapper(_nationalityRepository.Object,
                _companyVehicleTypeRepository.
                    Object,
                _employmentStatusRepository.
                    Object,
                _siteRepository.Object);
            _employeeContactDetailsParametersMapper = new EmployeeContactDetailsParametersMapper(_countriesRepository.Object);

            _bus = new Mock<IBus>();
            _bus.Setup(x => x.Send());

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => new UserForAuditing());

            _siteStructureElementRepository.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns<long>(id => new Site() { Id = id });

            _siteStructureElementRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns<long,long>((id,companyId) => new Site() { Id = id });

            _siteStructureElementRepository.Setup(x => x.GetByIdAndCompanyId(0, It.IsAny<long>()))
                .Throws(new SiteNotFoundException(0));

        }

        [Test]
        public void Given_valid_request_When_update_employee_Then_should_call_correct_methods()
        {

            // Given
            var securityAnswer = "212151551";
            
            var request = UpdateEmployeeRequestBuilder
                .Create()
                .WithEmployeeId(Guid.NewGuid())
                .Build();

            request.Telephone = securityAnswer;

            var currentUserId = request.UserId;
            var assigningUserId = Guid.NewGuid().ToString();

            var assigningUser = new User {Id = Guid.Parse(assigningUserId)};

            var employee = new Mock<Employee>();
            var contactDetails = new List<EmployeeContactDetail>
            {
                new EmployeeContactDetail {Telephone1 = "87987987"}
            };

            var target = CreateEmployeeService();

            employee.Setup(x => x.ContactDetails).Returns(contactDetails);
            employee.Setup(x => x.User).Returns(assigningUser);


            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(employee.Object);

            var user = new UserForAuditing();
            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(user);
            
            // When
            target.Update(request);

            // Then
            _employeeRepository.Verify(x => x.SaveOrUpdate(It.IsAny<Employee>()));
            employee.Verify(x => x.Update(It.IsAny<AddUpdateEmployeeParameters>(), It.IsAny<AddUpdateEmployeeContactDetailsParameters>(), It.Is<UserForAuditing>(u => u == user)));
            _bus.Verify(x => x.Send(It.Is<UpdateUserRegistration>(y => y.UserId.ToString() == assigningUserId && y.ActioningUserId == currentUserId && y.SecurityAnswer == securityAnswer)), Times.Once());

        }

        [Test]
        public void Given_security_answer_and_telephone_and_email_are_equal_then_update_registration_message_not_send()
        {
            // Given
            var securityAnswer = "212151551";
            var email = "email.com";

            var request = UpdateEmployeeRequestBuilder
                .Create()
                .WithEmployeeId(Guid.NewGuid())
                .Build();

            request.Telephone = securityAnswer;
            request.Email = email;

            var currentUserId = request.UserId;
            var assigningUserId = Guid.NewGuid().ToString();

            var assigningUser = new User { Id = Guid.Parse(assigningUserId) };

            var employee = new Mock<Employee>();
            var contactDetails = new List<EmployeeContactDetail>
            {
                new EmployeeContactDetail {Telephone1 = "212151551", Email = "email.com"}
            };

            var target = CreateEmployeeService();

            employee.Setup(x => x.ContactDetails).Returns(contactDetails);
            employee.Setup(x => x.User).Returns(assigningUser);
            
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(employee.Object);

            var user = new UserForAuditing();
            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(user);

            // When
            target.Update(request);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<UpdateUserRegistration>()), Times.Never());

        }

        [Test]
        public void Given_security_answer_and_telephone_are_equal_but_email_different_then_update_registration_message_is_sent()
        {
            // Given
            var securityAnswer = "212151551";
            var email = "emailDifferent@email.com";

            var request = UpdateEmployeeRequestBuilder
                .Create()
                .WithEmployeeId(Guid.NewGuid())
                .Build();

            request.Telephone = securityAnswer;
            request.Email = email;

            var currentUserId = request.UserId;
            var assigningUserId = Guid.NewGuid().ToString();

            var assigningUser = new User { Id = Guid.Parse(assigningUserId) };

            var employee = new Mock<Employee>();
            var contactDetails = new List<EmployeeContactDetail>
            {
                new EmployeeContactDetail {Telephone1 = "212151551", Email = "email.com"}
            };

            var target = CreateEmployeeService();

            employee.Setup(x => x.ContactDetails).Returns(contactDetails);
            employee.Setup(x => x.User).Returns(assigningUser);

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(employee.Object);

            var user = new UserForAuditing();
            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(user);

            // When
            target.Update(request);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<UpdateUserRegistration>()), Times.Once());
        }
        
        [Test]
        public void Given_employee_is_registered_and_email_changed_then_update_registration_not_sent()
        {
            // Given
            var securityAnswer = "212151551";

            var request = UpdateEmployeeRequestBuilder
                .Create()
                .WithEmployeeId(Guid.NewGuid())
                .WithEmail("email@email.com")
                .Build();

            request.Telephone = securityAnswer;

            var currentUserId = request.UserId;
            var assigningUserId = Guid.NewGuid().ToString();

            var assigningUser = new User { Id = Guid.Parse(assigningUserId) };

            var employee = new Mock<Employee>();
            var contactDetails = new List<EmployeeContactDetail>
            {
                new EmployeeContactDetail {Telephone1 = "212151551", Email = "newemail@email.com" }

            };

            var target = CreateEmployeeService();

            employee.Setup(x => x.ContactDetails).Returns(contactDetails);
            employee.Setup(x => x.User).Returns(assigningUser);
            employee.Setup(x => x.User.IsRegistered).Returns(false);

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(employee.Object);

            var user = new UserForAuditing();
            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(user);

            // When
            target.Update(request);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<UpdateUserRegistration>()), Times.Once());
            
        }

        [Test]
        public void Given_valid_request_When_add_employee_Then_should_save_with_the_correct_property_values()
        {

            // Given
            var request = UpdateEmployeeRequestBuilder
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
                .WithEmail("somefallea@email.com")
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
            
            var user = new UserForAuditing();
            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(user);

            _siteRepository.Setup(x => x.GetById(request.SiteId.Value)).Returns(new Site()
                                                                                     {
                                                                                         Id = request.SiteId.Value
                                                                                     });

            var employee = new Employee()
            {
                User = new User() { Id = Guid.NewGuid()},
                ContactDetails = new List<EmployeeContactDetail>()
                                                        {
                                                            new EmployeeContactDetail()
                                                        }
            };
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(employee);


            // When
            target.Update(request);

            // Then
            _employeeRepository.Verify(x => x.SaveOrUpdate(It.Is(matchesAddEmployeeRequestProperties(request))));
        }

        [Test]
        public void Given_valid_request_When_marking_employee_as_a_risk_assessor_Then_should_save_with_the_correct_risk_assessor_values()
        {
            // Given
            var request = UpdateEmployeeRequestBuilder
                .Create()
                .Build();

            request.IsRiskAssessor = true;
            request.DoNotSendTaskOverdueNotifications = true;
            request.DoNotSendTaskCompletedNotifications = true;
            request.DoNotSendReviewDueNotification = true;
            
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => new Employee());

            Employee savedEmployee = null;
            _employeeRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<Employee>()))
                .Callback<Employee>(parameter => savedEmployee = parameter);

            var target = CreateEmployeeService();


            // When
            target.Update(request);

            // Then
            Assert.That(savedEmployee.IsRiskAssessor, Is.True);
            Assert.That(savedEmployee.RiskAssessor.Employee, Is.EqualTo(savedEmployee));
            Assert.That(savedEmployee.RiskAssessor.Deleted, Is.EqualTo(false));
            Assert.That(savedEmployee.RiskAssessor.DoNotSendTaskOverdueNotifications, Is.EqualTo(true));
            Assert.That(savedEmployee.RiskAssessor.DoNotSendTaskCompletedNotifications, Is.EqualTo(true));
            Assert.That(savedEmployee.RiskAssessor.DoNotSendReviewDueNotification, Is.EqualTo(true));
        }

        [Test]
        public void Given_valid_request_When_un_marking_an_employee_as_a_risk_assessor_Then_should_mark_the_risk_assessor_as_deleted()
        {
            // Given
            var employee = new Employee();
            var request = UpdateEmployeeRequestBuilder
                .Create()
                .Build();

            request.IsRiskAssessor = false;

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => employee);

            Employee savedEmployee = null;
            _employeeRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<Employee>()))
                .Callback<Employee>(parameter => savedEmployee = parameter);

            var target = CreateEmployeeService();


            // When
            target.Update(request);

            // Then
            Assert.That(savedEmployee.IsRiskAssessor, Is.False);
            Assert.That(savedEmployee.RiskAssessors.All(x=> x.Deleted), Is.EqualTo(true));
        }

        [Test]
        public void Given_valid_request_When_updating_risk_assessor_notifications_Then_should_save_with_the_correct_values(){
            // Given
            var employee = new Employee();
            employee.RiskAssessors.Add(new RiskAssessor());
            var request = UpdateEmployeeRequestBuilder
                .Create()
                .Build();

            request.IsRiskAssessor = true;
            request.DoNotSendTaskOverdueNotifications = true;
            request.DoNotSendTaskCompletedNotifications = true;
            request.DoNotSendReviewDueNotification = true;
          
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => employee);

            Employee savedEmployee = null;
            _employeeRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<Employee>()))
                .Callback<Employee>(parameter => savedEmployee = parameter);

            var target = CreateEmployeeService();

            // When
            target.Update(request);

            // Then
            Assert.That(savedEmployee.IsRiskAssessor, Is.True);
            Assert.That(savedEmployee.RiskAssessor.Deleted, Is.EqualTo(false));
            Assert.That(savedEmployee.RiskAssessor.DoNotSendTaskOverdueNotifications, Is.EqualTo(true));
            Assert.That(savedEmployee.RiskAssessor.DoNotSendTaskCompletedNotifications, Is.EqualTo(true));
            Assert.That(savedEmployee.RiskAssessor.DoNotSendReviewDueNotification, Is.EqualTo(true));
        }

        [Test]
        public void Given_valid_request_When_updating_risk_assessor_site_Then_should_save_with_the_correct_risk_assessor_site_values()
        {
            // Given
            var employee = new Employee();
            employee.RiskAssessors.Add(new RiskAssessor());
            var request = UpdateEmployeeRequestBuilder
                .Create()
                .Build();

            request.IsRiskAssessor = true;
            request.RiskAssessorHasAccessToAllSites = false;
            request.RiskAssessorSiteId = 346134;

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => employee);

            Employee savedEmployee = null;
            _employeeRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<Employee>()))
                .Callback<Employee>(parameter => savedEmployee = parameter);

            var target = CreateEmployeeService();

            // When
            target.Update(request);

            // Then
            Assert.That(savedEmployee.RiskAssessor.HasAccessToAllSites, Is.False);
            Assert.That(savedEmployee.RiskAssessor.Site.Id, Is.EqualTo(request.RiskAssessorSiteId));
        }

        [Test]
        public void Given_valid_request_When_updating_risk_assessor_access_to_main_site_Then_should_save_with_the_correct_risk_assessor_values()
        {
            // Given
            var employee = new Employee();
            employee.RiskAssessors.Add(new RiskAssessor());
            var request = UpdateEmployeeRequestBuilder
                .Create()
                .Build();

            request.IsRiskAssessor = true;
            request.RiskAssessorHasAccessToAllSites = true;
            request.RiskAssessorSiteId = 123;

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => employee);

            Employee savedEmployee = null;
            _employeeRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<Employee>()))
                .Callback<Employee>(parameter => savedEmployee = parameter);

            var target = CreateEmployeeService();

            // When
            target.Update(request);

            // Then
            Assert.That(savedEmployee.RiskAssessor.HasAccessToAllSites, Is.True);
            Assert.That(savedEmployee.RiskAssessor.Site, Is.Null);

        }
        
        private static Expression<Func<Employee, bool>> matchesAddEmployeeRequestProperties(UpdateEmployeeRequest request)
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
            return new EmployeeService(_employeeRepository.Object, _employeeParametersMapper, _employeeContactDetailsParametersMapper, _userRepository.Object, null, null, _log.Object, _bus.Object, null, _siteStructureElementRepository.Object);
        }

    }
}