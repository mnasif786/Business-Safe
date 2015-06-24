using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.AuthenticationService;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.AuthenticationService
{
    [TestFixture]
    public class CustomPrincipleTests
    {
        [Test]
        public void When_constructed_Then_should_set_properties_correctly()
        {
            // Given
            // When
            var customPrinciple = CreateCustomPrinciple(new string[] { }, "Test Company");

            // Then
            Assert.That(customPrinciple.CompanyName, Is.EqualTo("Test Company"));
        }

        [Test]
        public void Given_custom_principal_has_no_permissions_When_IsInRole_Then_should_return_false()
        {
            // Given
            var customPrinciple = CreateCustomPrinciple(new string[] { });

            // When
            var result = customPrinciple.IsInRole(Permissions.AddEmployeeRecords.ToString());

            // Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_custom_principal_has_permissions_but_not_one_checking_When_IsInRole_Then_should_return_false()
        {
            // Given
            var permissions = new string[] {Permissions.ViewCompanyDetails.ToString()};

            var customPrinciple = CreateCustomPrinciple(permissions);

            // When
            var result = customPrinciple.IsInRole(Permissions.AddEmployeeRecords.ToString());

            // Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void Given_custom_principal_has_valid_permission_When_IsInRole_Then_should_return_true()
        {
            // Given
            var permissions = new string[] { Permissions.ViewCompanyDetails.ToString(), Permissions.AddEmployeeRecords.ToString() };

            var customPrinciple = CreateCustomPrinciple(permissions);

            // When
            var result = customPrinciple.IsInRole(Permissions.AddEmployeeRecords.ToString());

            // Then
            Assert.That(result, Is.True);
        }

        [Test]
        public void When_custom_principal_has_no_employer_Then_should_user_id_as_identity()
        {
            // Given
            var userDto = new UserDto()
                                  {
                                      Id = Guid.NewGuid()
                                  };
            

            // When
            var customPrinciple = new CustomPrincipal(userDto, new CompanyDto());

            // Then
            Assert.That(customPrinciple.Identity.Name, Is.EqualTo(userDto.Id.ToString()));
        }

        [Test]
        public void When_custom_principal_with_user_not_got_employer_email_Then_should_user_id_as_identity()
        {
            // Given
            var userDto = new UserDto()
            {
                Id = Guid.NewGuid(),
                Employee = new EmployeeDto()
                               {
                                   MainContactDetails = new EmployeeContactDetailDto { Email = string.Empty }
                               }
            };


            // When
            var customPrinciple = new CustomPrincipal(userDto, new CompanyDto());

            // Then
            Assert.That(customPrinciple.Identity.Name, Is.EqualTo(userDto.Id.ToString()));
        }

        [Test]
        public void When_custom_principal_with_employer_email_Then_should_employer_email_as_identity()
        {
            // Given
            var userDto = new UserDto()
            {
                Id = Guid.NewGuid(),
                Employee = new EmployeeDto()
                {
                    MainContactDetails = new EmployeeContactDetailDto { Email = "yo@man.cool.co.uk" }
                }
            };


            // When
            var customPrinciple = new CustomPrincipal(userDto, new CompanyDto());

            // Then
            Assert.That(customPrinciple.Identity.Name, Is.EqualTo(userDto.Employee.MainContactDetails.Email));
        }

        private static CustomPrincipal CreateCustomPrinciple(IEnumerable<string> permissions, string companyName = "")
        {
            var userDto = new UserDto()
            {
                Id = Guid.Empty,
                CompanyId = 0,
                Permissions = permissions
            };
            
            var companyDto = new CompanyDto()
            {
                CompanyName = companyName,
                Id = 100L
            };

            var customPrinciple = new CustomPrincipal(userDto, companyDto);
            return customPrinciple;
        }
    }
}