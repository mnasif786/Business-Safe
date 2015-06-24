using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SearchTests
    {
        private Mock<IResponsibilityRepository> _responsibilityRepository;
        private Mock<IPeninsulaLog> _log;

        private IEnumerable<Responsibility> _returnedResponsibilities; 

        private SearchResponsibilitiesRequest _request;

        [SetUp]
        public void Setup()
        {
            _returnedResponsibilities = new List<Responsibility>()
                                        {
                                            new Responsibility()
                                            {
                                                Id = 1324L,
                                                CreatedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                                                CreatedOn = DateTime.Now.AddDays(-12),
                                                Deleted = false,
                                                Description = "description",
                                                InitialTaskReoccurringType = TaskReoccurringType.FiveYearly,
                                                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                                                LastModifiedOn = DateTime.Now,
                                                Owner = new Employee() { Id = Guid.NewGuid() },
                                                Title = "title",
                                                Site = new Site() { Id = 1346624L },
                                                ResponsibilityCategory = new ResponsibilityCategory() { Id = 21345642L },
                                                ResponsibilityReason = new ResponsibilityReason() { Id = 1234523L }
                                            },
                                            new Responsibility()
                                            {
                                                Id = 1325L,
                                                CreatedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                                                CreatedOn = DateTime.Now.AddDays(-12),
                                                Deleted = false,
                                                Description = "description",
                                                InitialTaskReoccurringType = TaskReoccurringType.FiveYearly,
                                                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                                                LastModifiedOn = DateTime.Now,
                                                Owner = new Employee() { Id = Guid.NewGuid() },
                                                Title = "title",
                                                Site = new Site() { Id = 1346624L },
                                                ResponsibilityCategory = new ResponsibilityCategory() { Id = 21345642L },
                                                ResponsibilityReason = new ResponsibilityReason() { Id = 1234523L }
                                            },
                                            new Responsibility()
                                            {
                                                Id = 1326L,
                                                CreatedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                                                CreatedOn = DateTime.Now.AddDays(-12),
                                                Deleted = false,
                                                Description = "description",
                                                InitialTaskReoccurringType = TaskReoccurringType.FiveYearly,
                                                LastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() },
                                                LastModifiedOn = DateTime.Now,
                                                Owner = new Employee() { Id = Guid.NewGuid() },
                                                Title = "title",
                                                Site = new Site() { Id = 1346624L },
                                                ResponsibilityCategory = new ResponsibilityCategory() { Id = 21345642L },
                                                ResponsibilityReason = new ResponsibilityReason() { Id = 1234523L }
                                            }
                                        };

            _request = new SearchResponsibilitiesRequest()
                                  {
                                      CompanyId = 12345L,
                                      AllowedSiteIds = new List<long>() { 123L, 456L, 789L },
                                      CreatedFrom = DateTime.Now.AddDays(-10),
                                      CreatedTo = DateTime.Now.AddDays(-2),
                                      CurrentUserId = Guid.NewGuid(),
                                      ResponsibilityCategoryId = 1987L,
                                      ShowCompleted = true,
                                      ShowDeleted = true,
                                      SiteGroupId = 498L,
                                      SiteId = 39236L,
                                      Title = "title"
                                  };

            _responsibilityRepository = new Mock<IResponsibilityRepository>();
            _responsibilityRepository
                .Setup(x => x.Search(
                    It.IsAny<Guid>(),
                    It.IsAny<IEnumerable<long>>(),
                    It.IsAny<long>(),
                    It.IsAny<long?>(),
                    It.IsAny<long?>(),
                    It.IsAny<long?>(),
                    It.IsAny<string>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<ResponsibilitiesRequestOrderByColumn>(),
                    It.IsAny<bool>()
                ))
                .Returns(_returnedResponsibilities);

            _log = new Mock<IPeninsulaLog>();

        }

        [Test]
        public void When_Search_Then_map_request_to_repo_call()
        {
            // Given
            var target = GetTarget();

            // When
            target.Search(_request);

            // Then
            _responsibilityRepository.Verify(x => x.Search(
                _request.CurrentUserId,
                _request.AllowedSiteIds,
                _request.CompanyId,
                _request.ResponsibilityCategoryId,
                _request.SiteId,
                _request.SiteGroupId,
                _request.Title,
                _request.CreatedFrom,
                _request.CreatedTo,
                _request.ShowDeleted,
                _request.ShowCompleted,
                _request.Page,
                _request.PageSize,
                _request.OrderBy,
                _request.Ascending
            ));
        }

        [Test]
        public void When_Search_Then_map_returned_entities_to_dtos()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Search(_request);

            // Then
            Assert.That(result, Is.InstanceOf<IEnumerable<ResponsibilityDto>>());
            Assert.That(result.Count(), Is.EqualTo(_returnedResponsibilities.Count()));
        }


        private IResponsibilitiesService GetTarget()
        {
            return new ResponsibilitiesService(_responsibilityRepository.Object, null, null, null, null, null,null, null, null, null, _log.Object);
        }
    }
}
