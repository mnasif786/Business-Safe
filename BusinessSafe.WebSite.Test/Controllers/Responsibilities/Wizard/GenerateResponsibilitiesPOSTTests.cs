
using System;
using System.Collections.Generic;

using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;
using System.Linq;

using BusinessSafe.WebSite.Extensions;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities.Wizard
{
    [TestFixture]
    public class GenerateResponsibilitiesPOSTTests
    {
        private WizardController _target;
        private CreateResponsibilityFromSiteAndResponsibilityTemplateModel _model;
        private Mock<IResponsibilitiesService> _responsibilitiesService;

        [SetUp]
        public void Setup()
        {
            _responsibilitiesService = new Mock<IResponsibilitiesService>();
            _responsibilitiesService.Setup(x => x.CreateResponsibilitiesFromWizard(It.IsAny<CreateResponsibilityFromWizardRequest>()));

            _model = new CreateResponsibilityFromSiteAndResponsibilityTemplateModel
                     {
                         SiteIds = new [] {123L},
                         Responsibilities =  new List<ConfiguredResponsibilityFromTemplate>()
                                             {
                                                 new ConfiguredResponsibilityFromTemplate() { 
                                                     FrequencyId = TaskReoccurringType.SixMonthly,
                                                     ResponsibilityTemplateId = 1234L,
                                                     ResponsiblePersonEmployeeId = Guid.NewGuid()
                                                 },
                                                 new ConfiguredResponsibilityFromTemplate() { 
                                                     FrequencyId = TaskReoccurringType.TwentyFourMonthly,
                                                     ResponsibilityTemplateId = 352334L,
                                                     ResponsiblePersonEmployeeId = Guid.NewGuid()
                                                 }
                                             }
                     };
        }

        [Test]
        public void Given_valid_model_When_GenerateResponsibilities_Then_pass_request_to_service()
        {
            // Given
            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(_model);
            
            // Then
            _responsibilitiesService.Verify(x => x.CreateResponsibilitiesFromWizard(It.IsAny<CreateResponsibilityFromWizardRequest>()));
        }

        [Test]
        public void Given_valid_model_When_GenerateResponsibilities_Then_map_save_request()
        {
            // Given
            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(_model);

            // Then
            _responsibilitiesService.Verify(x => x.CreateResponsibilitiesFromWizard(It.Is<CreateResponsibilityFromWizardRequest>(y =>
                y.CompanyId == TestControllerHelpers.CompanyIdAssigned &&
                y.SiteIds == _model.SiteIds &&
                y.UserId == TestControllerHelpers.UserIdAssigned
            )));
        }

        [Test]
        public void Given_valid_model_When_GenerateResponsibilities_Then_map_responsibility_details_to_save_request()
        {
            // Given
            CreateResponsibilityFromWizardRequest passedCreateResponsibilityFromWizardRequest = null;
            _responsibilitiesService
                .Setup(x => x.CreateResponsibilitiesFromWizard(It.IsAny<CreateResponsibilityFromWizardRequest>()))
                .Callback<CreateResponsibilityFromWizardRequest>(y => passedCreateResponsibilityFromWizardRequest = y);
            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(_model);

            // Then
            var requestTemplateDetails = passedCreateResponsibilityFromWizardRequest.ResponsibilityFromTemplateDetails;

            Assert.That(requestTemplateDetails, Is.InstanceOf<List<ResponsibilityFromTemplateDetail>>());

            for (var i = 0; i < _model.Responsibilities.Count(); i++)
            {
                Assert.That(requestTemplateDetails[i].ResponsibilityTemplateId, Is.EqualTo(_model.Responsibilities.ElementAt(i).ResponsibilityTemplateId));
                Assert.That(requestTemplateDetails[i].FrequencyId, Is.EqualTo(_model.Responsibilities.ElementAt(i).FrequencyId));
                Assert.That(requestTemplateDetails[i].ResponsiblePersonEmployeeId, Is.EqualTo(_model.Responsibilities.ElementAt(i).ResponsiblePersonEmployeeId));
            }
        }

        [Test]
        public void Given_valid_model_When_GenerateResponsibilities_Then_return_json_success_equals_true()
        {
            // Given
            _target = GetTarget();

            // When
            dynamic result = _target.GenerateResponsibilities(_model);

            // Then
            Assert.That(result.Data.Success, Is.True);
        }

        [Test]
        public void Given_invalid_model_When_GenerateResponsibilities_Then_return_json_success_equals_false()
        {
            // Given
            _target = GetTarget();

            // When
            _target.ModelState.AddModelError("error", "an error");
            dynamic result = _target.GenerateResponsibilities(_model);

            // Then
            Assert.That(result.Data.Success, Is.EqualTo("false"));
        }

        [Test]
        public void Given_invalid_model_When_GenerateResponsibilities_Then_return_errors_in_json()
        {
            // Given
            _target = GetTarget();

            // When
            _target.ModelState.AddModelError("error", "an error 1");
            _target.ModelState.AddModelError("error", "an error 2");
            _target.ModelState.AddModelError("error", "an error 3");
            dynamic result = _target.GenerateResponsibilities(_model);

            // Then
            Assert.That(result.Data.Errors.Length, Is.EqualTo(3));
        }

        [Test]
        public void Given_model_with_null_siteids_When_GenerateResponsibilities_Then_add_error_to_modelstate()
        {
            // Given
            var modelWithNoSites = _model;
            modelWithNoSites.SiteIds = null;

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoSites);

            // Then
            Assert.That(_target.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Given_model_with_no_siteids_When_GenerateResponsibilities_Then_add_error_to_modelstate()
        {
            // Given
            var modelWithNoSites = _model;
            modelWithNoSites.SiteIds = new long[0];

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoSites);

            // Then
            Assert.That(_target.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Given_model_with_null_Responsibilities_When_GenerateResponsibilities_Then_add_error_to_modelstate()
        {
            // Given
            var modelWithNoResponsibilities = _model;
            modelWithNoResponsibilities.Responsibilities = null;

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoResponsibilities);

            // Then
            Assert.That(_target.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Given_model_with_no_Responsibilities_When_GenerateResponsibilities_Then_add_error_to_modelstate()
        {
            // Given
            var modelWithNoResponsibilities = _model;
            modelWithNoResponsibilities.Responsibilities = new List<ConfiguredResponsibilityFromTemplate>();

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoResponsibilities);

            // Then
            Assert.That(_target.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Given_model_with_Responsibilities_with_invalid_TaskRecurringType_When_GenerateResponsibilities_Then_add_error_to_modelstate()
        {
            // Given
            var modelWithNoResponsibilities = _model;
            modelWithNoResponsibilities.Responsibilities = new List<ConfiguredResponsibilityFromTemplate>()
                                                           {
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.None,
                                                                   ResponsiblePersonEmployeeId = Guid.NewGuid(),
                                                                   ResponsibilityTemplateId = 1234L
                                                               }
                                                           };

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoResponsibilities);

            // Then
            Assert.That(_target.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Given_model_with_Responsibilities_with_empty_ResponsiblePersonEmployeeId_When_GenerateResponsibilities_Then_add_error_to_modelstate()
        {
            // Given
            var modelWithNoResponsibilities = _model;
            modelWithNoResponsibilities.Responsibilities = new List<ConfiguredResponsibilityFromTemplate>()
                                                           {
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.TwentySixMonthly,
                                                                   ResponsiblePersonEmployeeId = Guid.Empty,
                                                                   ResponsibilityTemplateId = 1234L
                                                               }
                                                           };

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoResponsibilities);

            // Then
            Assert.That(_target.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Given_model_with_Responsibilities_with_null_ResponsiblePersonEmployeeId_When_GenerateResponsibilities_Then_add_error_to_modelstate()
        {
            // Given
            var modelWithNoResponsibilities = _model;
            modelWithNoResponsibilities.Responsibilities = new List<ConfiguredResponsibilityFromTemplate>()
                                                           {
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.TwentySixMonthly,
                                                                   ResponsiblePersonEmployeeId = null,
                                                                   ResponsibilityTemplateId = 1234L
                                                               }
                                                           };

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoResponsibilities);

            // Then
            Assert.That(_target.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Given_model_with_Responsibilities_with_null_ResponsibilityTemplateId_When_GenerateResponsibilities_Then_add_error_to_modelstate()
        {
            // Given
            var modelWithNoResponsibilities = _model;
            modelWithNoResponsibilities.Responsibilities = new List<ConfiguredResponsibilityFromTemplate>()
                                                           {
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.SixMonthly,
                                                                   ResponsiblePersonEmployeeId = Guid.NewGuid(),
                                                                   ResponsibilityTemplateId = default(long)
                                                               }
                                                           };

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoResponsibilities);

            // Then
            Assert.That(_target.ModelState.IsValid, Is.False);
        }

        [Test]
        public void Given_model_with_Responsibilities_with_more_than_one_null_ResponsibilityTemplateId_When_GenerateResponsibilities_Then_only_add_error_message_once()
        {
            // Given
            var modelWithNoResponsibilities = _model;
            modelWithNoResponsibilities.Responsibilities = new List<ConfiguredResponsibilityFromTemplate>()
                                                           {
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.SixMonthly,
                                                                   ResponsiblePersonEmployeeId = Guid.NewGuid(),
                                                                   ResponsibilityTemplateId = default(long)
                                                               },
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.SixMonthly,
                                                                   ResponsiblePersonEmployeeId = Guid.NewGuid(),
                                                                   ResponsibilityTemplateId = default(long)
                                                               }
                                                           };

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoResponsibilities);

            // Then
            Assert.That(_target.ModelState.Values.First().Errors.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_model_with_Responsibilities_with_two_null_ResponsibilityTemplateId_and_one_null_EmployeeId_When_GenerateResponsibilities_Then_add_two_messages_for_each()
        {
            // Given
            var modelWithNoResponsibilities = _model;
            modelWithNoResponsibilities.Responsibilities = new List<ConfiguredResponsibilityFromTemplate>()
                                                           {
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.SixMonthly,
                                                                   ResponsiblePersonEmployeeId = Guid.NewGuid(),
                                                                   ResponsibilityTemplateId = default(long)
                                                               },
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.SixMonthly,
                                                                   ResponsiblePersonEmployeeId = null,
                                                                   ResponsibilityTemplateId = default(long)
                                                               }
                                                           };

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoResponsibilities);

            // Then
            Assert.That(_target.ModelState.Values.First().Errors.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Given_model_with_Responsibilities_with_one_null_ResponsibilityTemplateId_and_two_null_EmployeeId_When_GenerateResponsibilities_Then_add_two_messages_for_each()
        {
            // Given
            var modelWithNoResponsibilities = _model;
            modelWithNoResponsibilities.Responsibilities = new List<ConfiguredResponsibilityFromTemplate>()
                                                           {
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.SixMonthly,
                                                                   ResponsiblePersonEmployeeId = null,
                                                                   ResponsibilityTemplateId = default(long)
                                                               },
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.SixMonthly,
                                                                   ResponsiblePersonEmployeeId = null,
                                                                   ResponsibilityTemplateId = default(long)
                                                               }
                                                           };

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoResponsibilities);

            // Then
            Assert.That(_target.ModelState.Values.First().Errors.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Given_model_with_Responsibilities_with_one_null_ResponsibilityTemplateId_and_two_null_FrequencyIds_When_GenerateResponsibilities_Then_add_two_messages_for_each()
        {
            // Given
            var modelWithNoResponsibilities = _model;
            modelWithNoResponsibilities.Responsibilities = new List<ConfiguredResponsibilityFromTemplate>()
                                                           {
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.None,
                                                                   ResponsiblePersonEmployeeId = Guid.NewGuid(),
                                                                   ResponsibilityTemplateId = 345L
                                                               },
                                                               new ConfiguredResponsibilityFromTemplate()
                                                               {
                                                                   FrequencyId = TaskReoccurringType.None,
                                                                   ResponsiblePersonEmployeeId = Guid.NewGuid(),
                                                                   ResponsibilityTemplateId = default(long)
                                                               }
                                                           };

            _target = GetTarget();

            // When
            _target.GenerateResponsibilities(modelWithNoResponsibilities);

            // Then
            Assert.That(_target.ModelState.Values.First().Errors.Count(), Is.EqualTo(2));
        }

        private WizardController GetTarget()
        {
            var controller = new WizardController(null, null, null, _responsibilitiesService.Object, null, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
