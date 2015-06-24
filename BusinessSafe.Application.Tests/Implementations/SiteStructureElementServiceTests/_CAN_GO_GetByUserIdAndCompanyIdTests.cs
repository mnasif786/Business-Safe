//using System;
//using System.Collections.Generic;
//using BusinessSafe.Application.Implementations.Sites;
//using BusinessSafe.Domain.Entities;
//using BusinessSafe.Domain.InfrastructureContracts.Logging;
//using BusinessSafe.Domain.RepositoryContracts;
//using Moq;
//using NUnit.Framework;

//namespace BusinessSafe.Application.Tests.Implementations.SiteStructureElementServiceTests
//{
//    [TestFixture]
//    [Category("Unit")]
//    public class _CAN_GO_GetByUserIdAndCompanyIdTests
//    {
//        private Mock<IPeninsulaLog> _log;
//        private Mock<ISiteStructureElementRepository> _siteRepository;
//        private Mock<IUserForAuditingRepository> _userRepository;

//        [SetUp]
//        public void SetUp()
//        {
//            _log = new Mock<IPeninsulaLog>();
//            _siteRepository = new Mock<ISiteStructureElementRepository>();
//            _userRepository = new Mock<IUserForAuditingRepository>();
//        }

//        [Test]
//        public void Given_user_is_assigned_to_a_site_and_does_not_have_all_sites_permissions_When_GetByUserIdAndCompanyId_called_then_correct_methods_are_called()
//        {
//            //Given
//            var userId = Guid.NewGuid();
//            var companyId = 374L;
//            var siteId = 243L;
//            var site = new Site
//                           {
//                               Id = siteId,
//                               Name = "Test Site"
//                           };

//            var user = new UserForAuditing
//                           {
//                               Id = userId,
//                               CompanyId = companyId,
//                               PermissionsApplyToAllSites = false,
//                               Site = site
//                           };

//            _userRepository
//                .Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(userId, companyId))
//                .Returns(user);

//            _siteRepository
//                //.Setup(x => x.GetByIdsAndCompanyId(It.Is<List<long>>(y => y.Count == 1 && y.Contains(siteId)), companyId))
//                .Setup(x => x.GetByIdsAndCompanyId(new List<long> {siteId}, companyId))
//                .Returns(new List<SiteStructureElement> { site });

//            //When
//            CreateTarget().GetByUserIdAndCompanyId(userId, companyId);

//            //Then
//            _siteRepository.VerifyAll();
//            _userRepository.VerifyAll();
//        }

//        [Test]
//        public void Given_user_is_assigned_to_a_site_group_and_does_not_have_all_sites_permissions_When_GetByUserIdAndCompanyId_called_then_correct_methods_are_called()
//        {
//            //Given
//            var userId = Guid.NewGuid();
//            var companyId = 374L;
//            var siteId = 243L;

//            var siteGroup = new SiteGroup
//            {
//                Id = siteId,
//                Name = "Northern Region",
//                Children = new List<SiteStructureElement>
//                               {
//                                   new Site
//                                       {
//                                           Id = 945L,
//                                           Name = "Site 1"
//                                       },
//                                   new Site
//                                       {
//                                           Id = 946L,
//                                           Name = "Site 2"
//                                       }
//                               }
//            };

//            var user = new UserForAuditing
//            {
//                Id = userId,
//                CompanyId = companyId,
//                PermissionsApplyToAllSites = false,
//                Site = siteGroup
//            };

//            _userRepository
//                .Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(userId, companyId))
//                .Returns(user);

//            _siteRepository
//                .Setup(x => x.GetByIdsAndCompanyId(It.Is<List<long>>(
//                    y => y.Count == 3 
//                    && y.Contains(siteId)
//                    && y.Contains(945L)
//                    && y.Contains(946L))
//                    , companyId))
//                .Returns(new List<SiteStructureElement> { siteGroup, siteGroup.Children[0], siteGroup.Children[1] });

//            //When
//            CreateTarget().GetByUserIdAndCompanyId(userId, companyId);

//            //Then
//            _siteRepository.VerifyAll();
//            _userRepository.VerifyAll();
//        }

//        [Test]
//        public void Given_user_is_assigned_to_a_site_and_has_all_sites_permissions_When_GetByUserIdAndCompanyId_called_then_correct_methods_are_called()
//        {
//            //Given
//            var userId = Guid.NewGuid();
//            var companyId = 374L;
//            var siteId = 243L;

//            var site = new Site
//                           {
//                               Id = siteId,
//                               Name = "Test Site"
//                           };

//            var sites = new List<Site>
//                            {
//                                new Site
//                                    {
//                                        Id = 835L,
//                                        Name = "Site 1"
//                                    },
//                                new Site
//                                    {
//                                        Id = 836L,
//                                        Name = "Site 2"
//                                    },
//                                site
//                            };

//            var user = new UserForAuditing
//            {
//                Id = userId,
//                CompanyId = companyId,
//                PermissionsApplyToAllSites = true,
//                Site = site
//            };

//            _userRepository
//                .Setup(x => x.GetByIdAndCompanyIdIncludeDeleted(userId, companyId))
//                .Returns(user);

//            _siteRepository
//                .Setup(x => x.GetByCompanyId(companyId))
//                .Returns(sites);

//            //When
//            CreateTarget().GetByUserIdAndCompanyId(userId, companyId);

//            //Then
//            _siteRepository.VerifyAll();
//            _userRepository.VerifyAll();
//        }

//        private SiteStructureElementService CreateTarget()
//        {
//            return new SiteStructureElementService(_log.Object, _siteRepository.Object, _userRepository.Object);
//        }
//    }
//}
