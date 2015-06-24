using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmployeeTests
{
    [TestFixture]
    public class EmployeeTests
    {
        private readonly UserForAuditing _user = new UserForAuditing()
                                      {
                                          Id = Guid.NewGuid()
                                      };

        [Test]
        public void Given_employee_When_add_contact_details_Then_should_attach_as_appropiate()
        {
            //Given
            var employee = new Employee();

            var contactDetails = new EmployeeContactDetail();

            //When
            employee.AddContactDetails(contactDetails);

            //Then
            Assert.That(employee.ContactDetails.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_employee_already_exists_Then_should_get_correct_exception()
        {
            //Given
            //Given
            var employee = new Employee();
            var contactDetails = new EmployeeContactDetail();
            employee.AddContactDetails(contactDetails);

            //When
            //Then
            Assert.Throws<ContactDetailsAlreadyAttachedToEmployeeException>(() => employee.AddContactDetails(contactDetails));
        }

        [Test]
        public void Given_employee_When_amend_Then_should_set_properties_correctly()
        {
            //Given
            var employee = new Employee
                               {
                                   ContactDetails = new List<EmployeeContactDetail>
                                                      {
                                                          new EmployeeContactDetail()
                                                      }
                               };

            var amendParameters = new AddUpdateEmployeeParameters()
                                      {
                                          EmployeeReference = "Test"
                                      };
            var contactDetails = new AddUpdateEmployeeContactDetailsParameters()
                                     {
                                         Address1 = "Address1",
                                         Address2 = "Address2",
                                         Address3 = "Address3"
                                     };


            //When
            employee.Update(amendParameters, contactDetails, _user);

            //Then
            Assert.That(employee.EmployeeReference, Is.EqualTo(amendParameters.EmployeeReference));
            Assert.That(employee.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(employee.ContactDetails.Count, Is.EqualTo(1));
            Assert.That(employee.ContactDetails.First().Address1, Is.EqualTo(contactDetails.Address1));
            Assert.That(employee.ContactDetails.First().Address2, Is.EqualTo(contactDetails.Address2));
            Assert.That(employee.ContactDetails.First().Address3, Is.EqualTo(contactDetails.Address3));
        }

        [Test]
        public void Given_employee_When_mark_for_delete_Then_deleted_set_to_true()
        {
            //Given
            var employee = new Employee();
            var userDeletingEmployee = new UserForAuditing();

            //When
            employee.MarkForDelete(userDeletingEmployee);

            //Then
            Assert.That(employee.Deleted, Is.True);
            Assert.That(employee.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(employee.LastModifiedBy, Is.EqualTo(userDeletingEmployee));
        }

        [Test]
        public void Given_employee_When_reinstate_employee_as_not_deleted_Then_employee_properties_should_be_correct()
        {
            //Given
            var employee = new Employee();
            var userDeletingEmployee = new UserForAuditing();

            //When
            employee.ReinstateEmployeeAsNotDeleted(userDeletingEmployee);

            //Then
            Assert.That(employee.Deleted, Is.False);
            Assert.That(employee.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(employee.LastModifiedBy, Is.EqualTo(userDeletingEmployee));
        }

        [Test]
        public void Given_employee_When_AddEmergencyContacts_Then_employee_properties_should_be_correct()
        {
            //Given
            var employee = new Employee();
            var contacts = new List<EmployeeEmergencyContactDetail>() { new EmployeeEmergencyContactDetail(), new EmployeeEmergencyContactDetail() };

            //When
            employee.AddEmergencyContacts(contacts);

            //Then
            Assert.That(employee.EmergencyContactDetails.Count, Is.EqualTo(contacts.Count));
        }

        [Test]
        public void Given_valid_parameters_and_employee_When_validate_register_as_user_is_called_Then_should_be_valid()
        {
            //Given
            var parameters = GetRegisterEmployeeAsUserParameters();
            var employee = GetEmployee();

            //When
            var validationResult = employee.ValidateRegisterAsUser(parameters);

            //Then
            Assert.IsTrue(validationResult.IsValid);
        }

        [Test]
        public void Given_employee_has_no_email_When_validate_register_as_user_is_called_Then_should_be_valid()
        {
            //Given
            var parameters = GetRegisterEmployeeAsUserParameters();
            var employee = GetEmployee();
            employee.ContactDetails[0].Email = null;

            //When
            var validationResult = employee.ValidateRegisterAsUser(parameters);

            //Then
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Errors.Count);
        }

        [Test]
        public void Given_employee_has_no_telephone_number_When_validate_register_as_user_is_called_Then_should_be_valid()
        {
            //Given
            var parameters = GetRegisterEmployeeAsUserParameters();
            var employee = GetEmployee();
            employee.ContactDetails[0].Telephone1 = null;

            //When
            var validationResult = employee.ValidateRegisterAsUser(parameters);

            //Then
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(1, validationResult.Errors.Count);
        }

        [Test]
        public void Given_valid_parameters_When_register_as_user_is_called_Then_user_should_be_registered_successfully()
        {
            //Given
            var parameters = GetRegisterEmployeeAsUserParameters();
            parameters.Site = new Site();
            var employee = GetEmployee();

            //When
            employee.CreateUser(parameters);

            //Then
            Assert.AreEqual(parameters.NewUserId, employee.User.Id);
            Assert.AreEqual(parameters.Role.Id, employee.User.Role.Id);
            Assert.AreEqual(parameters.Site.Id, employee.User.Site.Id);
        }

        [Test]
        public void Given_valid_parameters_site_id_not_supplied_When_register_as_user_is_called_Then_user_should_take_main_site_id_as_user_site_id()
        {
            //Given
            var parameters = GetRegisterEmployeeAsUserParameters();
            var mainSite = new Site()
                               {
                                   Id = 500
                               };
            parameters.MainSite = mainSite;
            parameters.Site = null;

            var employee = GetEmployee();

            //When
            employee.CreateUser(parameters);

            //Then
            Assert.AreEqual(parameters.NewUserId, employee.User.Id);
            Assert.AreEqual(parameters.Role.Id, employee.User.Role.Id);
            Assert.AreEqual(mainSite.Id, employee.User.Site.Id);
        }

        [Test]
        public void Given_no_site_id_supplied_and_no_main_site_id_to_use_as_replacement_When_register_as_user_is_called_Then_exception_is_thrown()
        {
            //Given
            var parameters = GetRegisterEmployeeAsUserParameters();
            parameters.MainSite = null;
            parameters.Site = null;

            var employee = GetEmployee();

            //When
            Assert.Throws<AttemptingToCreateEmployeeAsUserNoUserSiteSetException>(() => employee.CreateUser(parameters));

        }

        [Test]
        [ExpectedException(typeof(AttemptingToCreateEmployeeAsUserForDifferentCompanyException))]
        public void Given_company_in_supplied_parameters_is_for_different_company_When_register_as_user_is_called_Then_exception_is_thrown()
        {
            //Given
            var parameters = GetRegisterEmployeeAsUserParameters();
            var employee = GetEmployee();
            employee.CompanyId = 333L;

            //When
            employee.CreateUser(parameters);

            //Then
            Assert.AreEqual(parameters.NewUserId, employee.User.Id);
            Assert.AreEqual(parameters.Role.Id, employee.User.Role.Id);
            Assert.AreEqual(parameters.Site.Id, employee.User.Site.Id);
        }

        [Test]
        public void Given_no_contact_details_When_HasEmail_Then_should_return_false()
        {
            // Given
            var employee = new Employee()
                               {
                                   ContactDetails = new List<EmployeeContactDetail>()
                               };

            // When
            var result = employee.HasEmail;

            // Then
            Assert.False(result);
        }

        [Test]
        public void Given_contact_details_but_no_email_When_HasEmail_Then_should_return_false()
        {
            // Given
            var employee = new Employee()
            {
                ContactDetails = new List<EmployeeContactDetail>()
                                     {
                                         new EmployeeContactDetail()
                                             {
                                                 Email = null
                                             }
                                     }
            };

            // When
            var result = employee.HasEmail;

            // Then
            Assert.False(result);
        }

        [Test]
        public void Given_contact_details_with_email_When_HasEmail_Then_should_return_true()
        {
            // Given
            var employee = new Employee()
            {
                ContactDetails = new List<EmployeeContactDetail>()
                                     {
                                         new EmployeeContactDetail()
                                             {
                                                 Email = "test@hotmail.com"
                                             }
                                     }
            };

            // When
            var result = employee.HasEmail;

            // Then
            Assert.True(result);
        }

        [Test]
        public void Given_contact_details_with_email_When_GetEmail_Then_should_return_correct_result()
        {
            // Given
            var employee = new Employee()
            {
                ContactDetails = new List<EmployeeContactDetail>()
                                     {
                                         new EmployeeContactDetail()
                                             {
                                                 Email = "test@hotmail.com"
                                             }
                                     }
            };

            // When
            var result = employee.GetEmail();

            // Then
            Assert.That(result, Is.EqualTo("test@hotmail.com"));
        }

        [Test]
        public void Given_no_contact_details_When_SetEmail_Then_should_initialize_contact_details_correctly()
        {
            // Given
            var employee = new Employee()
            {
                ContactDetails = new List<EmployeeContactDetail>() { }
            };

            // When
            employee.SetEmail("testing@hotmail.com", new UserForAuditing());

            // Then
            Assert.That(employee.ContactDetails.Count, Is.EqualTo(1));
            Assert.That(employee.ContactDetails.First().Email, Is.EqualTo("testing@hotmail.com"));
        }

        [Test]
        public void Given_contact_details_When_SetEmail_Then_should_set_contact_details_correctly()
        {
            // Given
            var employee = new Employee()
            {
                ContactDetails = new List<EmployeeContactDetail>() { new EmployeeContactDetail() }
            };

            // When
            employee.SetEmail("testing@hotmail.com", new UserForAuditing());

            // Then
            Assert.That(employee.ContactDetails.Count, Is.EqualTo(1));
            Assert.That(employee.ContactDetails.First().Email, Is.EqualTo("testing@hotmail.com"));
        }

        [Test]
        public void Given_no_emergency_contact_details_found_with_given_id_When_MarkEmergencyContactForDelete_Then_throws_exception()
        {
            // Given
            var target = new Employee();
            var userForAuditing = new UserForAuditing();

            // When
            // Then
            Assert.Throws<AttemptingToMarkForDeleteEmergencyContactEmergencyContactNotFoundOnEmployeeException>(() => target.MarkEmergencyContactForDelete(1L, userForAuditing));
        }

        [Test]
        public void Given_emergency_contact_details_found_When_MarkEmergencyContactForDelete_Then_calls_correct_methods()
        {
            // Given
            var emergencyContactDetailsId = 1;
            var emergencyContactDetails = new Mock<EmployeeEmergencyContactDetail>();
            emergencyContactDetails
                .Setup(x => x.Id)
                .Returns(emergencyContactDetailsId);

            var target = new Employee
            {
                EmergencyContactDetails = new List<EmployeeEmergencyContactDetail>
                                                               {
                                                                   emergencyContactDetails.Object
                                                               }
            };

            var userForAuditing = new UserForAuditing();

            // When
            target.MarkEmergencyContactForDelete(emergencyContactDetailsId, userForAuditing);

            // Then
            emergencyContactDetails.Verify(x => x.MarkForDelete(userForAuditing));
        }

        [Test]
        public void Given_employee_is_not_user_When_CanChangeEmail_Then_returns_true()
        {
            // Given
            var target = new Employee();

            // When
            var result = target.CanChangeEmail;

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_employee_is_registered_user_When_CanChangeEmail_Then_returns_false()
        {
            // Given
            var target = new Employee
                             {
                                 User = new User
                                          {
                                              IsRegistered = true
                                          }
                             };

            // When
            var result = target.CanChangeEmail;

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_employee_is_pending_registion_user_When_CanChangeEmail_Then_returns_true()
        {
            // Given
            var target = new Employee
            {
                User = new User
                {
                    IsRegistered = false
                }
            };

            // When
            var result = target.CanChangeEmail;

            // Then
            Assert.IsTrue(result);
        }

       

        [Test]
        public void Given_has_contact_detail_found_When_UpdateContactDetails_Then_calls_correct_method()
        {
            // Given
            var contactDetailsParameters = new AddUpdateEmployeeContactDetailsParameters
            {
                Id = 1L
            };

            var userForAuditing = new UserForAuditing();
            var target = new Employee();

            var employeeContactDetail = new Mock<EmployeeContactDetail>();
            employeeContactDetail
                .Setup(x => x.Id)
                .Returns(1L);

            employeeContactDetail
                .Setup(x => x.Update(contactDetailsParameters, userForAuditing, target));

            target.ContactDetails.Add(employeeContactDetail.Object);

            // When
            target.UpdateContactDetails(contactDetailsParameters, userForAuditing);

            // Then
            employeeContactDetail.Verify(x => x.Update(contactDetailsParameters, userForAuditing, target));
        }

        [Test]
        public void Given_no_contact_detail_found_When_UpdateEmergencyContact_Then_throws_exception()
        {
            // Given
            var contactDetailsParameters = new EmergencyContactDetailParameters()
            {
                EmergencyContactId = 1L
            };

            var userForAuditing = new UserForAuditing();

            var target = new Employee
            {
                EmergencyContactDetails = new List<EmployeeEmergencyContactDetail>
                                                    {
                                                        new EmployeeEmergencyContactDetail
                                                            {
                                                                Id=888
                                                            }
                                                    }
            };

            // When

            // Then
            Assert.Throws<AttemptingToUpdateEmergencyContactEmergencyContactsDetailsNotFoundForEmployeeException>(() => target.UpdateEmergencyContact(contactDetailsParameters, userForAuditing));
        }

        [Test]
        public void Given_has_contact_detail_found_When_UpdateEmergencyContact_Then_calls_correct_method()
        {
            // Given
            var emergencyContactDetailParameters = new EmergencyContactDetailParameters
            {
                EmergencyContactId = 1L
            };

            var userForAuditing = new UserForAuditing();
            var target = new Employee();

            var employeeEmergencyContactDetail = new Mock<EmployeeEmergencyContactDetail>();
            employeeEmergencyContactDetail
                .Setup(x => x.Id)
                .Returns(1);

            employeeEmergencyContactDetail
                .Setup(x => x.Update(emergencyContactDetailParameters, userForAuditing));

            target.EmergencyContactDetails.Add(employeeEmergencyContactDetail.Object);

            // When
            target.UpdateEmergencyContact(emergencyContactDetailParameters, userForAuditing);

            // Then
            employeeEmergencyContactDetail.Verify(x => x.Update(emergencyContactDetailParameters, userForAuditing));
        }


        public RegisterEmployeeAsUserParameters GetRegisterEmployeeAsUserParameters()
        {
            return new RegisterEmployeeAsUserParameters()
            {
                ActioningUser = new UserForAuditing
                {
                    Id = Guid.NewGuid()
                },
                CompanyId = GetCompanyId(),
                NewUserId = Guid.NewGuid(),
                Role = new Role { Id = Guid.NewGuid() },
                Site = new Site
                {
                    Id = 1L
                }
            };
        }

        public Employee GetEmployee()
        {
            return new Employee
            {
                Id = Guid.NewGuid(),
                CompanyId = GetCompanyId(),
                ContactDetails = new List<EmployeeContactDetail>
                                                        {
                                                            new EmployeeContactDetail
                                                                {
                                                                    Telephone1 = "123456789",
                                                                    Email = "test@email.com"
                                                                }
                                                        }
            };
        }

        public long GetCompanyId()
        {
            return 999L;
        }
    }
}