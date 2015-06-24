using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Request;
using NUnit.Framework;
using Moq;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Application.Implementations.Sites;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Tests.Implementations.SiteServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetByUserIdAndCompanyIdTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IUserForAuditingRepository> _userRepository;

        [SetUp]
        public void SetUp()
        {
            _log = new Mock<IPeninsulaLog>();
            _siteRepository = new Mock<ISiteRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
        }

        [Test]
        public void Given_valid_parameters_When_search_is_called_correct_parameters_are_called()
        {
            //Given
            var userId = Guid.NewGuid();
            var companyId = 374L;
            var siteId = 243L;

            var site = new Site
            {
                Id = siteId,
                Name = "Test Site"
            };

            _siteRepository
                .Setup(x => x.Search(companyId, null, new List<long>{ siteId }, null))
                .Returns(new List<Site> { site });


            //When
            CreateTarget().Search(new SearchSitesRequest
                                      {
                                          CompanyId = companyId,
                                          AllowedSiteIds = new List<long> {siteId},
                                      });

            //Then
            _siteRepository.VerifyAll();
        }

        private SiteService CreateTarget()
        {
            return new SiteService(
                _siteRepository.Object,
                null,
                _userRepository.Object, null);
        }
    }
}
