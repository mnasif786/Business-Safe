using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Data.Repository.SafeCheck;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using EvaluationChecklist.ClientDetails;
using EvaluationChecklist.Controllers;
using EvaluationChecklist.Controllers.ClientDetails;
using EvaluationChecklist.Helpers;
using EvaluationChecklist.Models;
using Moq;
using NUnit.Framework;

namespace EvaluationChecklist.Api.Tests.ClientControllerTests
{
    [TestFixture]
    public class QueryTests
    {
        private Mock<IClientDetailsService> _clientDetailService;
        private Mock<ICheckListRepository> _checklistRepository;
        private Mock<IDependencyFactory> _dependencyFactory;
        
        [SetUp]
        public void Setup()
        {
            _dependencyFactory = new Mock<IDependencyFactory>();
            _clientDetailService = new Mock<IClientDetailsService>();
            _checklistRepository = new Mock<ICheckListRepository>();
            
            _dependencyFactory
               .Setup(x => x.GetInstance<IClientDetailsService>())
               .Returns(() => _clientDetailService.Object);

            _dependencyFactory
               .Setup(x => x.GetInstance<IEmployeeRepository>())
               .Returns(() => null);

            _dependencyFactory
                .Setup(x => x.GetInstance<ICheckListRepository>())
                .Returns(() => _checklistRepository.Object);

            _clientDetailService
                .Setup(cs => cs.GetByClientAccountNumber(It.IsAny<string>()))
                .Returns(() => new CompanyDetailsResponse {CAN = "Den101", CompanyName = "ABC", Id = 1, Industry = "XYZ"} );
        }

        [Test]
        public void Given_when_query_client_by_accountNummber_then_returns_the_Main_Site_Only()
        {
            var sites = new List<SiteAddressResponse>
            {
                new SiteAddressResponse
                {
                    Id = 1,
                    Address1 = "Address 1",
                    Address2 = "Address 2",
                    County = "County",
                    Postcode = "M12 3EB",
                    IsMainSite = true
                },
                new SiteAddressResponse
                {
                    Id = 1,
                    Address1 = "Address 1",
                    Address2 = "Address 2",
                    County = "County",
                    Postcode = "M12 3EB"
                }
            };

            var mainSitesCount = sites.Count(s => s.IsMainSite);

            _clientDetailService
                .Setup(cs => cs.GetSites(It.IsAny<int>()))
                .Returns(sites);

            _checklistRepository
                .Setup(x => x.GetByClientId(It.IsAny<long>(), It.IsAny<bool>()))
                .Returns(new List<Checklist>());

            //When
            var target = new ClientController(_dependencyFactory.Object);
            var result = target.Query("Den101");
            
            Assert.That(result.Sites.Count(), Is.EqualTo(mainSitesCount));
        }

        [Test]
        public void Given_when_query_client_by_accountNummber_then_returns_the_Additional_Health_and_Safety_Site_Detials_Only()
        {
            var sites = new List<SiteAddressResponse>
            {
                new SiteAddressResponse
                {
                    Id = 1,
                    Address1 = "Address 1",
                    Address2 = "Address 2",
                    County = "County",
                    Postcode = "M12 3EB",
                    IsAdditionalHealthAndSafetySite = true
                },
                new SiteAddressResponse
                {
                    Id = 1,
                    Address1 = "Address 1",
                    Address2 = "Address 2",
                    County = "County",
                    Postcode = "M12 3EB"
                }
            };
            var additionalHealthAndSafetSitesCount = sites.Count(s => s.IsAdditionalHealthAndSafetySite);

            _clientDetailService
               .Setup(cs => cs.GetSites(It.IsAny<int>()))
               .Returns(sites);



            //When
            var target = new ClientController(_dependencyFactory.Object);
            var result = target.Query("Den101");
            
            Assert.That(result.Sites.Count(), Is.EqualTo(additionalHealthAndSafetSitesCount));
        }
        
        [Test]
        public void Given_when_query_client_by_accountNummber_then_returns_the_Principal_Health_and_Safety_Site_Detials_Only()
        {
            var sites = new List<SiteAddressResponse>
            {
                new SiteAddressResponse
                {
                    Id = 1,
                    Address1 = "Address 1",
                    Address2 = "Address 2",
                    County = "County",
                    Postcode = "M12 3EB",
                    IsPrincipalHealthAndSafetySite = true
                },
                new SiteAddressResponse
                {
                    Id = 1,
                    Address1 = "Address 1",
                    Address2 = "Address 2",
                    County = "County",
                    Postcode = "M12 3EB"
                }
            };
            var principalHealthAndSafetSitesCount = sites.Count(s => s.IsPrincipalHealthAndSafetySite);

            _clientDetailService
               .Setup(cs => cs.GetSites(It.IsAny<int>()))
               .Returns(sites);

            _checklistRepository
                .Setup(x => x.GetByClientId(It.IsAny<long>(), It.IsAny<bool>()))
                .Returns(new List<Checklist>());

            //When
            var target = new ClientController(_dependencyFactory.Object);
            var result = target.Query("Den101");
            
            Assert.That(result.Sites.Count, Is.EqualTo(principalHealthAndSafetSitesCount));
        }

        [Test]
        public void Given_when_query_client_by_accountNummber_then_returns_Only_Main_and_Health_and_Safety_Sites_Detials()
        {
            var sites = GetSitesAddress();
            var mainAndHsSitesCount = 
                sites.Count(s => s.IsMainSite || s.IsAdditionalHealthAndSafetySite || s.IsArchivedHealthAndSafetySite || s.IsPrincipalHealthAndSafetySite);

            _clientDetailService
               .Setup(cs => cs.GetSites(It.IsAny<int>()))
               .Returns(sites);

            _checklistRepository
                .Setup(x => x.GetByClientId(It.IsAny<long>(), It.IsAny<bool>()))
                .Returns(new List<Checklist>());

            //When
            var target = new ClientController(_dependencyFactory.Object);
            var result = target.Query("Den101");
            
            Assert.That(result.Sites.Count, Is.EqualTo(mainAndHsSitesCount));
        }

        [Test]
        public void Given_client_has_active_checklist_for_site_when_query_result_containts_checklist()
        {
            var siteId = 1234L;
            var sites = new List<SiteAddressResponse>
            {
                new SiteAddressResponse
                {
                    Id = siteId,
                    Address1 = "Address 1",
                    Address2 = "Address 2",
                    County = "County",
                    Postcode = "M12 3EB",
                    IsPrincipalHealthAndSafetySite = true
                }
            };

            var checklistId = Guid.NewGuid();

            var checklists = new List<Checklist>
                                 {
                                     new Checklist
                                         {
                                             Id = checklistId,
                                             SiteId = (int)siteId,
                                             Status = Checklist.STATUS_DRAFT
                                         }
                                 };
            
            _clientDetailService
               .Setup(cs => cs.GetSites(It.IsAny<int>()))
               .Returns(sites);

            _checklistRepository
                .Setup(x => x.GetByClientId(It.IsAny<long>(), It.IsAny<bool>()))
                .Returns(checklists);

            //When
            var target = new ClientController(_dependencyFactory.Object);
            var result = target.Query("den101");
            //then
            Assert.That(result.Sites.FirstOrDefault(x => x.Id == siteId).Checklist.Id, Is.EqualTo(checklistId));
        }

        private List<SiteAddressResponse> GetSitesAddress()
        {
            return new List<SiteAddressResponse>
            {
                    new SiteAddressResponse { Id = 1, Address1 = "Address 1", Address2 = "Address 2", County = "County", Postcode = "M12 3EB", IsMainSite = true},
                    new SiteAddressResponse { Id = 1, Address1 = "Address 1", Address2 = "Address 2", County = "County", Postcode = "M12 3EB", IsAdditionalHealthAndSafetySite = true},
                    new SiteAddressResponse { Id = 1, Address1 = "Address 1", Address2 = "Address 2", County = "County", Postcode = "M12 3EB", IsArchivedHealthAndSafetySite = true},
                    new SiteAddressResponse { Id = 1, Address1 = "Address 1", Address2 = "Address 2", County = "County", Postcode = "M12 3EB", IsPrincipalHealthAndSafetySite = true},
                    new SiteAddressResponse { Id = 1, Address1 = "Address 1", Address2 = "Address 2", County = "County", Postcode = "M12 3EB" }
                };
        }

    }
}
