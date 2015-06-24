using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.Sites;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.SiteServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetAllSitesTests
    {
        private Mock<ISiteRepository> _siteAddressRepository;
        private Mock<IPeninsulaLog> _log;
        private long _companyId;

        [SetUp]
        public void SetUp()
        {
            _log = new Mock<IPeninsulaLog>();
            _siteAddressRepository = new Mock<ISiteRepository>();
            

            _companyId = 100;
        }

        
        [Test]
        public void Given_that_get_all_sites_is_called_Then_should_call_correct_repo_methods()
        {
            //Given
            var target = CreateSiteService();

            //When
            target.GetAll(_companyId);

            //Then
            _siteAddressRepository.Verify(x => x.GetSiteAddressByCompanyId(_companyId), Times.Once());
        }

        [Test]
        public void Given_that_get_all_sites_is_called_Then_should_return_correct_result()
        {
            //Given
            var target = CreateSiteService();

            var site1 = Site.Create(1, null, 1, "Bo", "Bo Bo", "", new UserForAuditing());
            var site2 = Site.Create(2, null, 1, "Aa", "Aa Aa", "", new UserForAuditing());
           

            var sites = new Site[] { site1, site2 };

            _siteAddressRepository.Setup(x => x.GetSiteAddressByCompanyId(_companyId)).Returns(sites);

            //When
            var result = target.GetAll(_companyId,false);

            //Then
            Assert.That(result.Count(), Is.EqualTo(sites.Count()));
            Assert.That(result.First().Name, Is.EqualTo("Aa"));
            Assert.That(result.Last().Name, Is.EqualTo("Bo"));

        }

        [Test]
        public void Given_that_get_all_sites_is_called_and_one_site_is_closed_Then_should_return_correct_result()
        {
            //Given
            var target = CreateSiteService();

            //Open Site
            var site1 = Site.Create(2, null, 1, "Aa", "Aa Aa", "", new UserForAuditing());

            //closed site
            var site2 = Site.Create(1, null, 1, "Bo", "Bo Bo", "", new UserForAuditing());
            site2.SiteClosedDate = DateTime.Now;

            
            var sites = new Site[] { site1, site2 };

            _siteAddressRepository.Setup(x => x.GetSiteAddressByCompanyId(_companyId)).Returns(sites);

            //When
            var result = target.GetAll(_companyId);

            //Then
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Aa"));

        }

        [Test]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        public void When_GetAll_Then_Get_All(int totalSites)
        {
            // Givne
            var target = CreateSiteService();
            var sites = new List<Site>();
            for (var i = 0; i < totalSites; i++)
            {
                sites.Add(Site.Create(i + 1, null, 1, "Bo", "Bo Bo", "", new UserForAuditing()));
            }

            _siteAddressRepository.Setup(x => x.GetSiteAddressByCompanyId(_companyId)).Returns(sites);

            // When
            var result = target.GetAll(_companyId, true);

            // Then
            Assert.That(result.Count(), Is.EqualTo(sites.Count()));
        }


        private SiteService CreateSiteService()
        {
            var target = new SiteService(_siteAddressRepository.Object, null, null, null);
            return target;
        }
    }
}