using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.PersonalRiskAssessments
{
    [TestFixture]
    public class ChecklistGeneratorViewModelTests
    {
        private IList<EmployeeDto> _employees;
        private EmployeeChecklistGeneratorViewModel _baseInputViewModel;

        [SetUp]
        public void SetUp()
        {
            _employees = new List<EmployeeDto>
                             {
                                 new EmployeeDto
                                     {
                                         Id = new Guid("a9d827d2-cbfc-4d9c-9491-f8e271df77b8"),
                                         FullName = "Mark Mauve",
                                         MainContactDetails = new EmployeeContactDetailDto{ Email = "mark.mauve@email.com" }
                                     },
                                 new EmployeeDto
                                     {
                                         Id = new Guid("cf7ce0b2-8a97-4c32-8af1-ce3c96716fdd"),
                                         FullName = "Peter Pink"
                                     },
                                 new EmployeeDto
                                     {
                                         Id = new Guid("0b20512d-3b4e-4da3-ab8c-6433c3fa4118"),
                                         FullName = "Guy Grey",
                                         MainContactDetails = new EmployeeContactDetailDto{ Email = "guy.grey@email.com" }
                                     }
                             };

            new List<ChecklistDto>
                {
                    new ChecklistDto
                        {
                            Id = 1L,
                            Title = "Test Checklist 01"
                        },
                    new ChecklistDto
                        {
                            Id = 2L,
                            Title = "Test Checklist 02"
                        },
                    new ChecklistDto
                        {
                            Id = 3L,
                            Title = "Test Checklist 03"
                        },
                };

            _baseInputViewModel = new EmployeeChecklistGeneratorViewModel
            {
                EmployeeId = _employees[0].Id,
                RiskAssessmentId = 234L,
                IsForMultipleEmployees = "single",
                SingleEmployeesSectionVisible = true,
                MultipleEmployeesSectionVisible = false,
                Employees = null,
                NewEmployeeEmail = null,
                NewEmployeeEmailVisible = false,
                Checklists = null,
                Message = "Test Message"
            };    
        }

        [Test]
        public void Fails_validation_on_message_when_generating_and_no_message()
        {
            _baseInputViewModel.Generating = true;
            _baseInputViewModel.Message = null;
            var validationContext = new ValidationContext(_baseInputViewModel, null, null);
            var validationResults = _baseInputViewModel.Validate(validationContext);
            
            Assert.That(validationResults.Count(), Is.EqualTo(1));
            Assert.That(validationResults.Select(validationResult => validationResult.ErrorMessage).Contains("Message is required"));
        }

        [Test]
        public void Passes_validation_on_message_when_not_generating_and_no_message()
        {
            _baseInputViewModel.Message = null;
            var validationContext = new ValidationContext(_baseInputViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_baseInputViewModel, validationContext, validationResults);
            Assert.That(validationResults.Count, Is.EqualTo(0));
        }

        [Test]
        public void Fails_validation_on_new_email_when_generating_and_no_existing_email_and_no_new_email()
        {
            _baseInputViewModel.Generating = true;
            _baseInputViewModel.NewEmployeeEmailVisible = true;
            _baseInputViewModel.NewEmployeeEmail = null;
            var validationContext = new ValidationContext(_baseInputViewModel, null, null);
            var validationResults = _baseInputViewModel.Validate(validationContext);
            
            Assert.That(validationResults.Count(), Is.EqualTo(1));
            Assert.That(validationResults.Select(validationResult => validationResult.ErrorMessage).Contains("Email is required"));
        }

        [Test]
        public void Passes_validation_on_new_email_when_generating_and_has_existing_email()
        {
            _baseInputViewModel.Generating = true;
            _baseInputViewModel.NewEmployeeEmailVisible = false;
            var validationContext = new ValidationContext(_baseInputViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_baseInputViewModel, validationContext, validationResults);
            Assert.That(validationResults.Count, Is.EqualTo(0));
        }

        [Test]
        public void Passes_validation_on_new_email_when_not_generating_and_new_email_required_but_no_email()
        {
            _baseInputViewModel.Generating = false;
            _baseInputViewModel.NewEmployeeEmailVisible = true;
            _baseInputViewModel.NewEmployeeEmail = null;
            var validationContext = new ValidationContext(_baseInputViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_baseInputViewModel, validationContext, validationResults);
            Assert.That(validationResults.Count, Is.EqualTo(0));
        }

        [Test]
        public void Passes_validation_when_valid_email_is_supplied()
        {
            _baseInputViewModel.NewEmployeeEmailVisible = true;
            _baseInputViewModel.NewEmployeeEmail = "valid@email.com";
            var validationContext = new ValidationContext(_baseInputViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_baseInputViewModel, validationContext, validationResults);
            Assert.That(validationResults.Count, Is.EqualTo(0));
        }

        [Ignore("From what I can tell, TryValidateObjectdoesn't work with RegularExpressionAttribute.")]
        [Test]
        public void Fails_validation_when_valid_email_is_supplied()
        {
            _baseInputViewModel.NewEmployeeEmailVisible = true;
            _baseInputViewModel.NewEmployeeEmail = "####";
            var validationContext = new ValidationContext(_baseInputViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_baseInputViewModel, validationContext, validationResults);
            Assert.That(validationResults.Count, Is.EqualTo(1));
            Assert.That(validationResults.Select(validationResult => validationResult.ErrorMessage).Contains("Please supply a valid email address."));
        }
    }
}
