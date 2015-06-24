using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.Implementations.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.TaskServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SearchTasksTests
    {
        private Mock<ITasksRepository> _tasksRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<ISiteGroupRepository> _siteGroupRepository;
        private Guid _employeeId;
        private long _companyId;

        [SetUp]
        public void Setup()
        {
            _tasksRepository = new Mock<ITasksRepository>();
            _siteGroupRepository = new Mock<ISiteGroupRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_Search_Then_should_call_correct_methods()
        {

            //Given
            var target = GetTarget();

            _employeeId = Guid.NewGuid();
            _companyId = 200;

            var request = new SearchTasksRequest()
                              {
                                  CompanyId = _companyId,
                                  EmployeeIds = new List<Guid>(){_employeeId},
                                  TaskStatusId = (int) TaskStatus.Outstanding,
                                  Title = "Title to search for",
                              };

            _tasksRepository.Setup(x => x.Search(It.Is<long>(y => y == _companyId),
                                                 It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<long>(),
                                                 It.Is<int>(y => y == (int)TaskStatus.Outstanding),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<bool>(y => y == false),
                                                 It.IsAny<IEnumerable<long>>(),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, false))
                .Returns(new List<Task>());


            //When
            target.Search(request);

            //Then
            _tasksRepository.VerifyAll();

            _tasksRepository.Verify(x => x.Search(It.Is<long>(y => y == _companyId),
                                                 It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<long>(),
                                                 It.Is<int>(y => y == (int)TaskStatus.Outstanding),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<bool>(y => y == false),
                                                 It.IsAny<IEnumerable<long>>(),
                                                 request.Title, 0, 0, TaskOrderByColumn.None, false));
        }

        [Test]
        public void Given_valid_request_with_SiteId_set_When_Search_Then_should_call_correct_methods()
        {

            //Given
            var target = GetTarget();

            _employeeId = Guid.NewGuid();
            _companyId = 200;

            var request = new SearchTasksRequest()
            {
                CompanyId = _companyId,
                EmployeeIds = new List<Guid>() { _employeeId },
                TaskStatusId = (int)TaskStatus.Outstanding,
                SiteId = 500
            };

            _tasksRepository.Setup(x => x.Search(It.Is<long>(y => y == _companyId),
                                                 It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<long>(),
                                                 It.Is<int>(y => y == (int)TaskStatus.Outstanding),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<IEnumerable<long>>(y => y.Contains(request.SiteId.GetValueOrDefault())),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, false))
                .Returns(new List<Task>());


            //When
            target.Search(request);

            //Then
            _tasksRepository.VerifyAll();
        }

        [Test]
        public void Given_valid_request_with_SiteId_but_SiteId_not_in_allowable_sites_When_Search_Then_should_throw_correct_exception()
        {

            //Given
            var target = GetTarget();

            _employeeId = Guid.NewGuid();
            _companyId = 200;

            var request = new SearchTasksRequest()
            {
                CompanyId = _companyId,
                EmployeeIds = new List<Guid>() { _employeeId },
                TaskStatusId = (int)TaskStatus.Outstanding,
                SiteId = 500,
                AllowedSiteIds = new List<long>()
            };

            _tasksRepository.Setup(x => x.Search(It.Is<long>(y => y == _companyId),
                                                 It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<long>(),
                                                 It.Is<int>(y => y == (int)TaskStatus.Outstanding),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<IEnumerable<long>>(y => y.Contains(request.SiteId.GetValueOrDefault())),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, false))
                .Returns(new List<Task>());


            //When
            //Then
            Assert.Throws<SearchTasksSpecifiedSiteIdNotInAllowableSitesForUserException>(() => target.Search(request));
        }

        [Test]
        public void Given_valid_request_with_SiteId_and_allowable_sites_null_When_Search_Then_should_call_correct_methods()
        {

            //Given
            var target = GetTarget();

            _employeeId = Guid.NewGuid();
            _companyId = 200;

            var request = new SearchTasksRequest()
            {
                CompanyId = _companyId,
                EmployeeIds = new List<Guid>() { _employeeId },
                TaskStatusId = (int)TaskStatus.Outstanding,
                SiteId = 500,
                AllowedSiteIds = null
            };

            _tasksRepository.Setup(x => x.Search(It.Is<long>(y => y == _companyId),
                                                 It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<long>(),
                                                 It.Is<int>(y => y == (int)TaskStatus.Outstanding),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<IEnumerable<long>>(y => y.Contains(request.SiteId.GetValueOrDefault())),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, false))
                .Returns(new List<Task>());



            //When
            target.Search(request);

            //Then
            _tasksRepository.VerifyAll();
        }


        [Test]
        public void Given_valid_request_with_site_group_id_specified_When_Search_Then_should_call_correct_methods()
        {

            //Given
            var target = GetTarget();

            _employeeId = Guid.NewGuid();
            _companyId = 200;

            var request = new SearchTasksRequest()
            {
                CompanyId = _companyId,
                EmployeeIds = new List<Guid>() { _employeeId },
                TaskStatusId = (int)TaskStatus.Outstanding,
                SiteGroupId = 500
            };

            var siteGroup = new Mock<SiteGroup>();
            var descendents = new List<SiteStructureElement>()
                                  {
                                      new Site()
                                          {
                                              Id = 100
                                          },
                                      new Site()
                                          {
                                              Id = 200
                                          },
                                      new SiteGroup()
                                         {
                                              Id = 300
                                         }
                                  };
            siteGroup.Setup(x => x.GetThisAndAllDescendants()).Returns(descendents);

            _siteGroupRepository
                .Setup(x => x.GetByIdAndCompanyId(request.SiteGroupId.Value, request.CompanyId))
                .Returns(siteGroup.Object);


            _tasksRepository.Setup(x => x.Search(It.Is<long>(y => y == _companyId),
                                                 It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<long>(),
                                                 It.Is<int>(y => y == (int)TaskStatus.Outstanding),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<IEnumerable<long>>(y => y.Contains(100) && y.Contains(200)),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, false))
                .Returns(new List<Task>());



            //When
            target.Search(request);

            //Then
            _tasksRepository.VerifyAll();
            _siteGroupRepository.VerifyAll();
        }


        [Test]
        public void Given_valid_request_with_site_group_id_specified__then_only_uses_sites_from_sitegroup_in_allowable_sites_When_Search_Then_should_call_correct_methods()
        {

            //Given
            var target = GetTarget();

            _employeeId = Guid.NewGuid();
            _companyId = 200;

            var request = new SearchTasksRequest()
            {
                CompanyId = _companyId,
                EmployeeIds = new List<Guid>() { _employeeId },
                TaskStatusId = (int)TaskStatus.Outstanding,
                SiteGroupId = 500,
                AllowedSiteIds = new List<long>(){100}
            };

            var siteGroup = new Mock<SiteGroup>();
            var descendents = new List<SiteStructureElement>()
                                  {
                                      new Site()
                                          {
                                              Id = 100
                                          },
                                      new Site()
                                          {
                                              Id = 200
                                          },
                                      new SiteGroup()
                                         {
                                              Id = 300
                                         }
                                  };
            siteGroup.Setup(x => x.GetThisAndAllDescendants()).Returns(descendents);

            _siteGroupRepository
                .Setup(x => x.GetByIdAndCompanyId(request.SiteGroupId.Value, request.CompanyId))
                .Returns(siteGroup.Object);


            _tasksRepository.Setup(x => x.Search(It.Is<long>(y => y == _companyId),
                                                 It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<long>(),
                                                 It.Is<int>(y => y == (int)TaskStatus.Outstanding),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<IEnumerable<long>>(y => y.Count() == 1 && y.Contains(100)),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, false))
                .Returns(new List<Task>());



            //When
            target.Search(request);

            //Then
            _tasksRepository.VerifyAll();
            _siteGroupRepository.VerifyAll();
        }

        [Test]
        public void Given_valid_request_with_site_group_id_specified_and_allowable_sitesids_null_When_Search_Then_should_call_correct_methods()
        {

            //Given
            var target = GetTarget();

            _employeeId = Guid.NewGuid();
            _companyId = 200;

            var request = new SearchTasksRequest()
            {
                CompanyId = _companyId,
                EmployeeIds = new List<Guid>() { _employeeId },
                TaskStatusId = (int)TaskStatus.Outstanding,
                SiteGroupId = 500,
                AllowedSiteIds = null
            };

            var siteGroup = new Mock<SiteGroup>();
            var descendents = new List<SiteStructureElement>()
                                  {
                                      new Site()
                                          {
                                              Id = 100
                                          },
                                      new Site()
                                          {
                                              Id = 200
                                          },
                                      new SiteGroup()
                                         {
                                              Id = 300
                                         }
                                  };
            siteGroup.Setup(x => x.GetThisAndAllDescendants()).Returns(descendents);

            _siteGroupRepository
                .Setup(x => x.GetByIdAndCompanyId(request.SiteGroupId.Value, request.CompanyId))
                .Returns(siteGroup.Object);


            _tasksRepository.Setup(x => x.Search(It.Is<long>(y => y == _companyId),
                                                 It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<long>(),
                                                 It.Is<int>(y => y == (int)TaskStatus.Outstanding),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<IEnumerable<long>>(y => y.Count() == 2 && y.Contains(100) && y.Contains(200)),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, false))
                .Returns(new List<Task>());



            //When
            target.Search(request);

            //Then
            _tasksRepository.VerifyAll();
            _siteGroupRepository.VerifyAll();
        }

       
        private ITaskService GetTarget()
        {
            return new TaskService(
                null, 
                _log.Object, 
                _tasksRepository.Object, 
                null, 
                null, 
                _siteGroupRepository.Object);
        }
    }
}