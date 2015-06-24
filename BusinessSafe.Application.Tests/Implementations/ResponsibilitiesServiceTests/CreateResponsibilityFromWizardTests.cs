using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using BusinessSafe.Application.Helpers;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateResponsibilityFromWizardTests
    {
        private Mock<IResponsibilityRepository> _responsibilityRepository;
        private Mock<IResponsibilityCategoryRepository> _responsibilityCategoryRepository;
        private Mock<IResponsibilityReasonRepository> _responsibilityReasonRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private Mock<ITaskCategoryRepository> _taskCategoryRepository;
        private Mock<IPeninsulaLog> _log;
        private IList<ResponsibilityCategory> _responsibilityCategories;
        private Mock<IStatutoryResponsibilityTemplateRepository> _statutoryTemplateRepository;
        private IList<StatutoryResponsibilityTemplate> _statutoryResponsibilityTemplates;
        private List<ResponsibilityFromTemplateDetail> _requestedResponsibilityFromTemplateDetails;
        private List<Site> _sites;
        private List<Employee> _employees;
        private UserForAuditing _testUser;
        private long _companyId;
        private List<Responsibility> _responsibilities;
            
        [SetUp]
        public void Setup()
        {
            _companyId = 234246L;
            _responsibilityRepository = new Mock<IResponsibilityRepository>();
            _responsibilityReasonRepository = new Mock<IResponsibilityReasonRepository>();
            _responsibilityCategoryRepository = new Mock<IResponsibilityCategoryRepository>();
            _statutoryTemplateRepository = new Mock<IStatutoryResponsibilityTemplateRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _taskCategoryRepository = new Mock<ITaskCategoryRepository>();
            _log = new Mock<IPeninsulaLog>();

            _responsibilities = new List<Responsibility>();

            _sites = new List<Site>
                     {
                         new Site { Id = 123L, ClientId = _companyId },
                         new Site { Id = 456L, ClientId = _companyId },
                         new Site { Id = 789L, ClientId = _companyId }
                     };

            _siteRepository
                .Setup(x => x.GetByIds(It.IsAny<long[]>()))
                .Returns(_sites);

            _employees = new List<Employee>
                         {
                             new Employee() {Id = Guid.NewGuid(), CompanyId = _companyId},
                             new Employee() {Id = Guid.NewGuid(), CompanyId = _companyId},
                             new Employee() {Id = Guid.NewGuid(), CompanyId = _companyId}
                         };

            _statutoryResponsibilityTemplates = new List<StatutoryResponsibilityTemplate>
                                                {
                                                    new StatutoryResponsibilityTemplate() { Id = 123L, ResponsibilityCategory = new ResponsibilityCategory { Id = 1234L }, Title = "title 1", Description = "description 1", ResponsibilityReason = new ResponsibilityReason() {Id = 1}},
                                                    new StatutoryResponsibilityTemplate() { Id = 456L, ResponsibilityCategory = new ResponsibilityCategory { Id = 534L }, Title = "title 2", Description = "description 2",  ResponsibilityReason = new ResponsibilityReason() {Id = 2}},
                                                    new StatutoryResponsibilityTemplate() { Id = 789L, ResponsibilityCategory = new ResponsibilityCategory { Id = 274L }, Title = "title 3", Description = "description 3",  ResponsibilityReason = new ResponsibilityReason() {Id = 3}},
                                                    new StatutoryResponsibilityTemplate() { Id = 1123L, ResponsibilityCategory = new ResponsibilityCategory { Id = 9434L }, Title = "title 4", Description = "description 4",  ResponsibilityReason = new ResponsibilityReason() {Id = 4}}
                                                };

            _requestedResponsibilityFromTemplateDetails = new List<ResponsibilityFromTemplateDetail>
            {
                new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = _statutoryResponsibilityTemplates.ElementAt(0).Id, ResponsiblePersonEmployeeId = _employees.ElementAt(0).Id, FrequencyId = TaskReoccurringType.Annually },
                new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = _statutoryResponsibilityTemplates.ElementAt(1).Id, ResponsiblePersonEmployeeId = _employees.ElementAt(1).Id, FrequencyId = TaskReoccurringType.ThreeYearly },
                new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = _statutoryResponsibilityTemplates.ElementAt(2).Id, ResponsiblePersonEmployeeId = _employees.ElementAt(2).Id, FrequencyId = TaskReoccurringType.TwentyFourMonthly },
                new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = _statutoryResponsibilityTemplates.ElementAt(3).Id, ResponsiblePersonEmployeeId = _employees.ElementAt(2).Id, FrequencyId = TaskReoccurringType.FiveYearly },
            };

            _employeeRepository
                .Setup(x => x.GetByIds(It.IsAny<IList<Guid>>()))
                .Returns(_employees.Where(x =>
                    _requestedResponsibilityFromTemplateDetails.Select(y => y.ResponsiblePersonEmployeeId).Distinct().Contains(x.Id)
                ));

            _statutoryTemplateRepository
                .Setup(x => x.GetByIds(It.IsAny<IList<long>>()))
                .Returns(_statutoryResponsibilityTemplates);

            _responsibilityRepository
                .Setup(x => x.Save(It.IsAny<Responsibility>()));

            _responsibilityRepository
                .Setup(x => x.GetStatutoryByCompanyId(It.IsAny<long>()))
                .Returns(_responsibilities);

            _responsibilityCategoryRepository
                .Setup(x => x.GetByIds(It.IsAny<IList<long>>()))
                .Returns(_responsibilityCategories);

            _testUser = new UserForAuditing() { CompanyId = _companyId, Id = Guid.NewGuid() };

            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(_testUser);
        }

        [TestCase(1, 4)]
        [TestCase(4, 1)]
        [TestCase(100, 100)]
        public void Given_request_with_sites_and_responsibilities_When_CreateResponsibilitiesFromWizard_Then_call_save_a_number_of_times_equal_to_cartesian_product(int totalSites, int totalResponsibilities)
        {
            // Given
            var requestedSiteId = _sites.First().Id;
            var requestedOwnerEmployeeId = _employees.First().Id;

            _employeeRepository
                .Setup(x => x.GetByIds(It.IsAny<IList<Guid>>()))
                .Returns(_employees.Where(x => x.Id == requestedOwnerEmployeeId));

            _siteRepository
                .Setup(x => x.GetByIds(It.IsAny<long[]>()))
                .Returns(_sites.Where(x => x.Id == requestedSiteId));

            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = new long[totalSites],
                ResponsibilityFromTemplateDetails = new List<ResponsibilityFromTemplateDetail>(),
                CompanyId = _companyId
            };
            for (var i = 0; i < totalSites; i++)
            {
                request.SiteIds[i] = requestedSiteId;
            }
            for (var i = 0; i < totalResponsibilities; i++)
            {
                request.ResponsibilityFromTemplateDetails.Add(new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = 456L, ResponsiblePersonEmployeeId = requestedOwnerEmployeeId });
            }

            // When
            target.CreateResponsibilitiesFromWizard(request);
            
            // Then
            _responsibilityRepository.Verify(x => x.Save(It.IsAny<Responsibility>()), Times.Exactly(totalSites * totalResponsibilities));

        }

        [Test]
        public void Given_request_with_sites_and_responsibilities_When_CreateResponsibilitiesFromWizard_Then_company_id_mapped()
        {
            // Given
            var requestedOwnerEmployeeId = _employees.First().Id;
            var target = GetTarget();

            _employeeRepository
                .Setup(x => x.GetByIds(It.IsAny<IList<Guid>>()))
                .Returns(_employees.Where(x => x.Id == requestedOwnerEmployeeId));

            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = new List<ResponsibilityFromTemplateDetail> { new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = 456L, ResponsiblePersonEmployeeId = requestedOwnerEmployeeId } },
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            _responsibilityRepository.Verify(x => 
                x.Save(It.Is<Responsibility>(y => y.CompanyId == request.CompanyId)), 
                Times.Exactly(request.SiteIds.Length * request.ResponsibilityFromTemplateDetails.Count));
        }

        [Test]
        public void Given_request_When_CreateResponsibilitiesFromWizard_Then_all_required_templates_retrieved()
        {
            // Given
            IList<long> passedTemplateIds = null;
            _statutoryTemplateRepository
                .Setup(x => x.GetByIds(It.IsAny<IList<long>>()))
                .Callback<IList<long>>(y => passedTemplateIds = y)
                .Returns(_statutoryResponsibilityTemplates);
            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);
            
            // Then
            foreach(var details in request.ResponsibilityFromTemplateDetails)
            {
                Assert.That(passedTemplateIds.Contains(details.ResponsibilityTemplateId));
            };
        }

        [Test]
        public void Given_request_When_CreateResponsibilitiesFromWizard_Then_required_categories_mapped_to_each_new_Responsibility()
        {
            // Given
            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            foreach (var template in _statutoryResponsibilityTemplates)
            {
                _responsibilityRepository.Verify(x =>
                    x.Save(It.Is<Responsibility>(y => y.ResponsibilityCategory == template.ResponsibilityCategory)),
                    Times.Exactly(_sites.Count));
            };
        }

        [Test]
        public void Given_request_When_CreateResponsibilitiesFromWizard_Then_required_titles_mapped_to_each_new_Responsibility()
        {
            // Given
            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            foreach (var template in _statutoryResponsibilityTemplates)
            {
                _responsibilityRepository.Verify(x =>
                    x.Save(It.Is<Responsibility>(y => y.Title == template.Title)),
                    Times.Exactly(_sites.Count));
            };
        }

        [Test]
        public void Given_request_When_CreateResponsibilitiesFromWizard_Then_required_description_mapped_to_each_new_Responsibility()
        {
            // Given
            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            foreach (var template in _statutoryResponsibilityTemplates)
            {
                _responsibilityRepository.Verify(x =>
                    x.Save(It.Is<Responsibility>(y => y.Description == template.Description)),
                    Times.Exactly(_sites.Count));
            };
        }

        [Test]
        public void Given_request_When_CreateResponsibilitiesFromWizard_Then_all_required_sites_retrieved()
        {
            // Given
            IList<long> passedSiteIds = null;
            _siteRepository
                .Setup(x => x.GetByIds(It.IsAny<IList<long>>()))
                .Callback<IList<long>>(y => passedSiteIds = y)
                .Returns(_sites);
            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            foreach (var siteId in request.SiteIds)
            {
                Assert.That(passedSiteIds.Contains(siteId));
            };
        }

        [Test]
        public void Given_request_When_CreateResponsibilitiesFromWizard_Then_required_sites_mapped_to_each_new_Responsibility()
        {
            // Given
            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            foreach (var site in request.SiteIds)
            {
                _responsibilityRepository.Verify(x =>
                    x.Save(It.Is<Responsibility>(y => y.Site.Id == site )),
                    Times.Exactly(request.ResponsibilityFromTemplateDetails.Count));
            };
        }

        [Test]
        public void Given_request_When_CreateResponsibilitiesFromWizard_Then_required_reason_mapped_to_each_new_Responsibility()
        {
            // Given
            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            foreach (var template in _statutoryResponsibilityTemplates)
            {
                _responsibilityRepository.Verify(x =>
                    x.Save(It.Is<Responsibility>(y => y.ResponsibilityReason == template.ResponsibilityReason)),
                    Times.Exactly(_sites.Count));
            };
        }

        [Test]
        public void Given_request_When_CreateResponsibilitiesFromWizard_Then_owner_mapped_to_each_new_Responsibility()
        {
            // Given
            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId,

            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            foreach (var employeeId in _requestedResponsibilityFromTemplateDetails.Select(x => x.ResponsiblePersonEmployeeId).Distinct())
            {
                _responsibilityRepository.Verify(x =>
                    x.Save(It.Is<Responsibility>(y => y.Owner.Id == employeeId)),
                    Times.Exactly(_sites.Count * _requestedResponsibilityFromTemplateDetails.Count(x => x.ResponsiblePersonEmployeeId == employeeId)));
            };
        }

        [Test]
        public void Given_request_When_CreateResponsibilitiesFromWizard_Then_frequency_mapped_to_each_new_Responsibility()
        {
            // Given
            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId,

            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            foreach (var templateDetails in request.ResponsibilityFromTemplateDetails)
            {
                _responsibilityRepository.Verify(x =>
                    x.Save(It.Is<Responsibility>(y => y.InitialTaskReoccurringType == templateDetails.FrequencyId)),
                    Times.Exactly(_sites.Count));
            };
        }

        [Test]
        public void Given_request_When_CreateResponsibilitiesFromWizard_Then_User_For_Auditing_mapped_to_each_new_Responsibility()
        {
            // Given
            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId,
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            _responsibilityRepository.Verify(x =>
                     x.Save(It.Is<Responsibility>(y => y.CreatedBy == _testUser)),
                     Times.Exactly((request.SiteIds.Length * request.ResponsibilityFromTemplateDetails.Count)));
        }

        [Test]
        public void Given_requested_sites_are_not_in_company_When_CreateResponsibilitiesFromWizard_Then_throw_exception()
        {
            // Given
            const long anotherCompanyId = 354634234L;
           var otherCompaniesSites = new List<Site>
                     {
                         new Site { Id = 123L, ClientId = anotherCompanyId },
                         new Site { Id = 456L, ClientId = anotherCompanyId },
                         new Site { Id = 789L, ClientId = anotherCompanyId }
                     };
            _siteRepository
                .Setup(x => x.GetByIds(It.IsAny<long[]>()))
                .Returns(otherCompaniesSites);

            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId,
            };

            // When
            var e = Assert.Throws<SiteRequestedForStatutoryResponsibilityNotValidException>(() => target.CreateResponsibilitiesFromWizard(request));
        }

        [Test]
        public void Given_requested_sites_are_not_found_When_CreateResponsibilitiesFromWizard_Then_throw_exception()
        {
            // Given

            var requestedSiteIds = _sites.Select(x => x.Id).ToArray().Concat(new long[] { 1245L }).ToArray();

            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = requestedSiteIds,
                ResponsibilityFromTemplateDetails = _requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId
            };

            // When
            var e = Assert.Throws<SiteRequestedForStatutoryResponsibilityNotValidException>(() => target.CreateResponsibilitiesFromWizard(request));
        }

        [Test]
        public void Given_requested_employees_are_not_in_company_When_CreateResponsibilitiesFromWizard_Then_throw_exception()
        {
            // Given
            const long anotherCompanyId = 354634234L;
            var otherCompaniesEmployees = new List<Employee>
                     {
                             new Employee() {Id = Guid.NewGuid(), CompanyId = anotherCompanyId},
                             new Employee() {Id = Guid.NewGuid(), CompanyId = anotherCompanyId},
                             new Employee() {Id = Guid.NewGuid(), CompanyId = anotherCompanyId}
                     };
            _employeeRepository
                .Setup(x => x.GetByIds(It.IsAny<List<Guid>>()))
                .Returns(otherCompaniesEmployees);

            var requestedResponsibilityFromTemplateDetails = new List<ResponsibilityFromTemplateDetail>
            {
                new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = _statutoryResponsibilityTemplates.ElementAt(0).Id, ResponsiblePersonEmployeeId = otherCompaniesEmployees.ElementAt(0).Id, FrequencyId = TaskReoccurringType.Annually },
                new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = _statutoryResponsibilityTemplates.ElementAt(1).Id, ResponsiblePersonEmployeeId = otherCompaniesEmployees.ElementAt(1).Id, FrequencyId = TaskReoccurringType.ThreeYearly },
                new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = _statutoryResponsibilityTemplates.ElementAt(2).Id, ResponsiblePersonEmployeeId = otherCompaniesEmployees.ElementAt(2).Id, FrequencyId = TaskReoccurringType.TwentyFourMonthly },
                new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = _statutoryResponsibilityTemplates.ElementAt(3).Id, ResponsiblePersonEmployeeId = otherCompaniesEmployees.ElementAt(2).Id, FrequencyId = TaskReoccurringType.FiveYearly },
            };

            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = requestedResponsibilityFromTemplateDetails,
                CompanyId = _companyId,
            };

            // When
            var e = Assert.Throws<EmployeeRequestedForStatutoryResponsibilityNotValidException>(() => target.CreateResponsibilitiesFromWizard(request));
        }

        [Test]
        public void Given_requested_Employees_are_not_found_When_CreateResponsibilitiesFromWizard_Then_throw_exception()
        {
            // Given
            var tempResponsibilityTemplateDetails = new List<ResponsibilityFromTemplateDetail>
                                                    {
                                                        new ResponsibilityFromTemplateDetail()
                                                        {
                                                            ResponsibilityTemplateId =
                                                                _statutoryResponsibilityTemplates.ElementAt(0).Id,
                                                            ResponsiblePersonEmployeeId = Guid.NewGuid(),
                                                            FrequencyId = TaskReoccurringType.Annually
                                                        }
                                                    };

            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = tempResponsibilityTemplateDetails,
                CompanyId = _companyId
            };

            // When
            var e = Assert.Throws<EmployeeRequestedForStatutoryResponsibilityNotValidException>(() => target.CreateResponsibilitiesFromWizard(request));
        }

        [Test]
        public void Given_requested_template_but_not_site_combination_has_already_been_created_for_company_When_CreateResponsibilitiesFromWizard_Then_only_save_combination_that_doesnt_yet_exist()
        {
            // Given
            var firstRequestedSiteId = _sites.First().Id;
            var secondRequestedSiteId = _sites.ElementAt(1).Id;
            var requestedOwnerEmployeeId = _employees.First().Id;
            var requestedTemplateId = _requestedResponsibilityFromTemplateDetails.First().ResponsibilityTemplateId;

            _responsibilityRepository
                .Setup(x => x.GetStatutoryByCompanyId(It.IsAny<long>()))
                .Returns(new List<Responsibility>
                         {
                             new Responsibility()
                             {
                                 Site = new Site() { Id = firstRequestedSiteId },
                                 StatutoryResponsibilityTemplateCreatedFrom = _statutoryResponsibilityTemplates.First(),
                                 CompanyId = _companyId
                             }
                         });

            _employeeRepository
                .Setup(x => x.GetByIds(It.IsAny<IList<Guid>>()))
                .Returns(_employees.Where(x => x.Id == requestedOwnerEmployeeId));

            _siteRepository
                .Setup(x => x.GetByIds(It.IsAny<long[]>()))
                .Returns(_sites.Where(x => x.Id == firstRequestedSiteId || x.Id == secondRequestedSiteId));

            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = new[] { firstRequestedSiteId, secondRequestedSiteId },
                ResponsibilityFromTemplateDetails = new List<ResponsibilityFromTemplateDetail>
                                                    {
                                                        new ResponsibilityFromTemplateDetail()
                                                        {
                                                            FrequencyId = TaskReoccurringType.Weekly,
                                                            ResponsibilityTemplateId = requestedTemplateId,
                                                            ResponsiblePersonEmployeeId = requestedOwnerEmployeeId
                                                        }
                                                    },
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            _responsibilityRepository.Verify(x =>
                x.Save(It.IsAny<Responsibility>()),
                Times.Once());
        }

        [Test]
        public void Given_requested_site_and_but_not_template_combination_has_already_been_created_for_company_When_CreateResponsibilitiesFromWizard_Then_only_save_combination_that_doesnt_yet_exist()
        {
            // Given
            var firstRequestedSiteId = _sites.First().Id;
            var requestedOwnerEmployeeId = _employees.First().Id;
            var firstRequestedTemplateId = _requestedResponsibilityFromTemplateDetails.First().ResponsibilityTemplateId;
            var secondRequestedTemplateId = _requestedResponsibilityFromTemplateDetails.ElementAt(1).ResponsibilityTemplateId;

            _responsibilityRepository
                .Setup(x => x.GetStatutoryByCompanyId(It.IsAny<long>()))
                .Returns(new List<Responsibility>
                         {
                             new Responsibility()
                             {
                                 Site = new Site() { Id = firstRequestedSiteId },
                                 StatutoryResponsibilityTemplateCreatedFrom = _statutoryResponsibilityTemplates.First(),
                                 CompanyId = _companyId
                             }
                         });

            _employeeRepository
                .Setup(x => x.GetByIds(It.IsAny<IList<Guid>>()))
                .Returns(_employees.Where(x => x.Id == requestedOwnerEmployeeId));

            _siteRepository
                .Setup(x => x.GetByIds(It.IsAny<long[]>()))
                .Returns(_sites.Where(x => x.Id == firstRequestedSiteId));

            var target = GetTarget();
            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = new[] { firstRequestedSiteId },
                ResponsibilityFromTemplateDetails = new List<ResponsibilityFromTemplateDetail>
                                                    {
                                                        new ResponsibilityFromTemplateDetail()
                                                        {
                                                            FrequencyId = TaskReoccurringType.Weekly,
                                                            ResponsibilityTemplateId = firstRequestedTemplateId,
                                                            ResponsiblePersonEmployeeId = requestedOwnerEmployeeId
                                                        },
                                                        new ResponsibilityFromTemplateDetail()
                                                        {
                                                            FrequencyId = TaskReoccurringType.Weekly,
                                                            ResponsibilityTemplateId = secondRequestedTemplateId,
                                                            ResponsiblePersonEmployeeId = requestedOwnerEmployeeId
                                                        }
                                                    },
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            _responsibilityRepository.Verify(x =>
                x.Save(It.IsAny<Responsibility>()),
                Times.Once());
        }

        [Test]
        public void Given_valid_When_CreateResponsibilitiesFromWizard_Then_template_is_mapped()
        {
            // Given
            Responsibility passedResponsibility = null;
            _responsibilityRepository.Setup(x =>
                x.Save(It.IsAny<Responsibility>()))
                .Callback<Responsibility>(y => passedResponsibility = y);

            var requestedOwnerEmployeeId = _employees.First().Id;
            var requestedTemplateId = _statutoryResponsibilityTemplates.First().Id;
            var target = GetTarget();

            _employeeRepository
                .Setup(x => x.GetByIds(It.IsAny<IList<Guid>>()))
                .Returns(_employees.Where(x => x.Id == requestedOwnerEmployeeId));

            var request = new CreateResponsibilityFromWizardRequest()
            {
                SiteIds = _sites.Select(x => x.Id).ToArray(),
                ResponsibilityFromTemplateDetails = new List<ResponsibilityFromTemplateDetail> { new ResponsibilityFromTemplateDetail() { ResponsibilityTemplateId = requestedTemplateId, ResponsiblePersonEmployeeId = requestedOwnerEmployeeId } },
                CompanyId = _companyId
            };

            // When
            target.CreateResponsibilitiesFromWizard(request);

            // Then
            _responsibilityRepository.Verify(x =>
                x.Save(It.Is<Responsibility>(y => y.StatutoryResponsibilityTemplateCreatedFrom == _statutoryResponsibilityTemplates.First())),
                Times.Exactly(request.SiteIds.Length * request.ResponsibilityFromTemplateDetails.Count));
        }

        private IResponsibilitiesService GetTarget()
        {
            return new ResponsibilitiesService(_responsibilityRepository.Object,
                _responsibilityCategoryRepository.Object,
                _responsibilityReasonRepository.Object,
                _employeeRepository.Object,
                _siteRepository.Object,
                _userForAuditingRepository.Object,
                _taskCategoryRepository.Object,
                null,
                _statutoryTemplateRepository.Object, null,
                _log.Object);
        }    
    }
}
