using System;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmployeeTests
{
    [TestFixture]
    public class CreateTests
    {

        private AddUpdateEmployeeParameters _addUpdateEmployeeParameters;
        private UserForAuditing _creatingUser;

        [SetUp]
        public void Setup()
        {
            _creatingUser = new UserForAuditing()
            {
                Id = Guid.NewGuid()
            };
            _addUpdateEmployeeParameters = new AddUpdateEmployeeParameters()
            {
                ClientId = 1234L,
                CompanyVehicleRegistration = "reg",
                CompanyVehicleType = new CompanyVehicleType(),
                DateOfBirth = DateTime.Now,
                DisabilityDescription = "legless",
                DrivingLicenseExpirationDate = DateTime.Now,
                DrivingLicenseNumber = "licence",
                EmployeeReference = "reference",
                EmploymentStatus = new EmploymentStatus(),
                Forename = "first",
                HasCompanyVehicle = true,
                HasDisability = true,
                JobTitle = "job",
                MiddleName = "middle",
                NINumber = "ni number",
                Nationality = new Nationality(),
                OrganisationalUnitId = 1234L,
                PPSNumber = "pps",
                PassportNumber = "passport",
                PreviousSurname = "previous",
                Sex = "sex",
                Site = new Site(),
                SiteId = 1234L,
                Surname = "surname",
                Title = "title",
                WorkVisaExpirationDate = DateTime.Now,
                WorkVisaNumber = "visa#"
            };
        }

        [Test]
        public void When_Create_Then_Return_Employee()
        {
            // Given

            // When
            var result = Employee.Create(_addUpdateEmployeeParameters, _creatingUser);

            // Then
            Assert.That(result, Is.InstanceOf<Employee>());
        }

    }
}