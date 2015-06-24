using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Data.Repository;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    [TestFixture]
    class CopyResponsibilityTests
    {
        private Mock<IResponsibilityRepository> _responsibilityRepo;
        private Mock<IPeninsulaLog> _log;
        private Mock<Responsibility> _responsibility;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;

        private const long _originalResponsibilityId = 1234L;
        private const long _newResponsibilityId = 5555L;
        private const long _companyId = 5678L;
        private readonly Guid _user = Guid.NewGuid();

        private ResponsibilitiesService _target;
        private readonly DateTime _createdOn = DateTime.Now;
        private Site _site;
        private Employee _employee;
        private Responsibility _originalResponibility;

        [SetUp]
        public void Setup()
        {
            _responsibilityRepo = new Mock<IResponsibilityRepository>();
            _log = new Mock<IPeninsulaLog>();
            _responsibility = new Mock<Responsibility>();
            _site = new Site() {Id = 311};
            _employee = new Employee() {Id = Guid.NewGuid()};

            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            
            _userForAuditingRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new UserForAuditing() {Id = _user, CompanyId = _companyId});

            _originalResponibility = new Responsibility()
            {
                CompanyId = _companyId,
                ResponsibilityCategory = ResponsibilityCategory.Create(1, "Category"),
                Title = "Title Orig",
                Description = "Description",
                Site = new Site() { Id = 1 },
                ResponsibilityReason = new ResponsibilityReason() { Id = 1 },
                Owner = _employee,
                InitialTaskReoccurringType = TaskReoccurringType.ThreeMonthly,
                ResponsibilityTasks = new List<ResponsibilityTask>() { new ResponsibilityTask() { Id = 1 }, new ResponsibilityTask() { Id = 2 }, new ResponsibilityTask() { Id = 3 } },
                CreatedOn = _createdOn,
                StatutoryResponsibilityTemplateCreatedFrom = new StatutoryResponsibilityTemplate() { Id = 2 },
                Id = _originalResponsibilityId
            };
        }

        [Test]
        public void Given_Previous_ResponsibilityId_Not_Found_When_Copy_Responsibility_Then_Return_Not_Found_Exception()
        {
            // Given
            _target = GetTarget();

            _responsibilityRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Throws(new ResponsibilityNotFoundException(_originalResponsibilityId, _companyId));

            var request = new CopyResponsibilityRequest()
            {
                OriginalResponsibilityId = 1,
                Title = "Title"
            };

            // When
            Assert.Throws<ResponsibilityNotFoundException>(() => _target.CopyResponsibility(request));

            // Then
        }

        [Test]
        public void Given_ResponsibilityId_Found_When_Copy_Responsibility_Then_Check_New_Responsibility_Created_With_Originals_Parameters()
        {
            // Given
            _target = GetTarget();
            
            _responsibilityRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_originalResponibility);

            var request = new CopyResponsibilityRequest()
            {
                CompanyId = _companyId,
                OriginalResponsibilityId = _originalResponsibilityId,
                Title = "new Title"
            };

            Responsibility newResponsibility = null;

            _responsibilityRepo.Setup(x => x.Save(It.IsAny<Responsibility>()))
                .Callback<Responsibility>(r => newResponsibility = r);

            // When
            _target.CopyResponsibility(request);
            
            // Then
            Assert.True(newResponsibility.CompanyId.Equals(_originalResponibility.CompanyId));
            Assert.True(newResponsibility.ResponsibilityCategory.Equals(_originalResponibility.ResponsibilityCategory));
            Assert.False(newResponsibility.Title.Equals(_originalResponibility.Title));
            Assert.True(newResponsibility.Description.Equals(_originalResponibility.Description));
            Assert.IsNull(newResponsibility.Site);
            Assert.IsNull(newResponsibility.Owner);
            Assert.True(newResponsibility.InitialTaskReoccurringType.Equals(_originalResponibility.InitialTaskReoccurringType));
            Assert.True(newResponsibility.ResponsibilityTasks.Count.Equals(_originalResponibility.ResponsibilityTasks.Count));
            Assert.False(newResponsibility.CreatedOn.Equals(_originalResponibility.CreatedOn));
            Assert.True(newResponsibility.StatutoryResponsibilityTemplateCreatedFrom.Equals(_originalResponibility.StatutoryResponsibilityTemplateCreatedFrom));
            Assert.False(newResponsibility.Id.Equals(_originalResponibility.Id));
        }

        [Test]
        public void Given_ResponsibilityId_Found_For_Responsibility_With_Tasks_When_Copy_Responsibility_Then_Check_New_Responsibility_Created_With_Originals_Tasks()
        {
            // Given
            _target = GetTarget();

            _responsibilityRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
               .Returns(_originalResponibility);

            var request = new CopyResponsibilityRequest()
            {
                CompanyId = _companyId,
                OriginalResponsibilityId = _originalResponsibilityId,
                Title = "new Title"
            };

            Responsibility newResponsibility = null;

            _responsibilityRepo.Setup(x => x.Save(It.IsAny<Responsibility>()))
                .Callback<Responsibility>(r => newResponsibility = r);

            // When
            _target.CopyResponsibility(request);

            Assert.True(newResponsibility.ResponsibilityTasks.Count.Equals(_originalResponibility.ResponsibilityTasks.Count));

        }

        private ResponsibilitiesService GetTarget()
        {
            return new ResponsibilitiesService(_responsibilityRepo.Object, null, null, null, null, _userForAuditingRepository.Object, null, null, null, null, _log.Object);
        }
    }
}
