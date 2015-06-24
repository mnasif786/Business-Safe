using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Mappers
{
    [TestFixture]
    public class ResponsibilityDtoMapperTests
    {
        private const long _id = 12453L;
        private const long _companyId = 12453L;
        private UserForAuditing _createdBy;
        private DateTime _createdOn;
        private bool _deleted;
        private const string _description = "description";
        private TaskReoccurringType _initialTaskReoccurringType;
        private UserForAuditing _lastModifiedBy;
        private DateTime _lastModifiedOn;
        private Employee _owner;
        private const string _title = "title";
        private Site _site;
        private ResponsibilityCategory _responsibilityCategory;
        private ResponsibilityReason _responsibilityReason;
        private List<ResponsibilityTask> _responsibilityTasks;
        private const DerivedTaskStatusForDisplay _status = DerivedTaskStatusForDisplay.Outstanding;
        private Mock<Responsibility> _entity;

        private StatutoryResponsibilityTaskTemplate _statutoryResponsibilityTaskTemplate;

        [SetUp]
        public void Setup()
        {
            _createdBy = new UserForAuditing() { Id = Guid.NewGuid() };
            _createdOn = DateTime.Now;
            _deleted = false;
            _initialTaskReoccurringType = TaskReoccurringType.FiveYearly;
            _lastModifiedBy = new UserForAuditing() { Id = Guid.NewGuid() };
            _lastModifiedOn = DateTime.Now;
            _owner = new Employee() { Id = Guid.NewGuid() };
            _site = new Site() { Id = 1346624L };
            _responsibilityCategory = new ResponsibilityCategory() { Id = 21345642L };
            _responsibilityReason = new ResponsibilityReason() { Id = 1234523L };

            _statutoryResponsibilityTaskTemplate = new StatutoryResponsibilityTaskTemplate
                                                       {
                                                           Id = 1L
                                                       };


            _responsibilityTasks = new List<ResponsibilityTask>
                                       {
                                           new ResponsibilityTask
                                               {
                                                   Id = 1L,
                                                   Category = TaskCategory.Create(1, "Test"),
                                                   StatutoryResponsibilityTaskTemplateCreatedFrom =
                                                       _statutoryResponsibilityTaskTemplate
                                               }
                                       };

            _entity = new Mock<Responsibility>();

            _entity.Setup(x => x.Id).Returns(_id);
            _entity.Setup(x => x.CompanyId).Returns(_companyId);
            _entity.Setup(x => x.Description).Returns(_description);
            _entity.Setup(x => x.Site).Returns(_site);
            _entity.Setup(x => x.InitialTaskReoccurringType).Returns(_initialTaskReoccurringType);
            _entity.Setup(x => x.Owner).Returns(_owner);
            _entity.Setup(x => x.Title).Returns(_title);
            _entity.Setup(x => x.ResponsibilityCategory).Returns(_responsibilityCategory);
            _entity.Setup(x => x.ResponsibilityReason).Returns(_responsibilityReason);
            _entity.Setup(x => x.CreatedOn).Returns(_createdOn);
            _entity.Setup(x => x.GetStatusDerivedFromTasks()).Returns(_status);
            _entity.Setup(x => x.Deleted).Returns(_deleted);
            _entity.Setup(x => x.CreatedBy).Returns(_createdBy);
            _entity.Setup(x => x.LastModifiedBy).Returns(_lastModifiedBy);
            _entity.Setup(x => x.LastModifiedOn).Returns(_lastModifiedOn);
            _entity.Setup(x => x.ResponsibilityTasks).Returns(_responsibilityTasks);
        }


        [Test]
        public void Given_a_no_responsibility_when_Map_then_return_null()
        {
            // Given
            Responsibility entity = null;

            // When
            var result = new ResponsibilityDtoMapper(entity).WithTasks().Map();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Given_a_responsibility_When_Map_Then_Return_ResponsibilityTaskDto()
        {
            //Given

            //When
            var result = new ResponsibilityDtoMapper(_entity.Object).WithTasks().Map();
            
            //Then
            Assert.IsInstanceOf<ResponsibilityTaskDto>(result.ResponsibilityTasks.ElementAt(0));
        }

        [Test]
        public void Given_a_responsibility_when_Map_then_properties_are_mapped()
        {
            //Given

            //When
            var result = new ResponsibilityDtoMapper(_entity.Object).WithTasks().Map();

            //Then
            Assert.That(result.Id, Is.EqualTo(_entity.Object.Id));
            Assert.That(result.CompanyId, Is.EqualTo(_entity.Object.CompanyId));
            Assert.That(result.Description, Is.EqualTo(_entity.Object.Description));
            Assert.That(result.Site.Id, Is.EqualTo(_entity.Object.Site.Id));
            Assert.That(result.InitialTaskReoccurringType, Is.EqualTo(_entity.Object.InitialTaskReoccurringType));
            Assert.That(result.Owner.Id, Is.EqualTo(_entity.Object.Owner.Id));
            Assert.That(result.Title, Is.EqualTo(_entity.Object.Title));
            Assert.That(result.ResponsibilityCategory.Id, Is.EqualTo(_entity.Object.ResponsibilityCategory.Id));
            Assert.That(result.ResponsibilityReason.Id, Is.EqualTo(_entity.Object.ResponsibilityReason.Id));
            Assert.That(result.CreatedOn, Is.EqualTo(_entity.Object.CreatedOn));

            Assert.That(result.ResponsibilityTasks.FirstOrDefault().Id, Is.EqualTo(_entity.Object.ResponsibilityTasks.FirstOrDefault().Id));
        }

        [Test]
        public void Given_Responsibility_When_Map_Then_map_StatusDerivedFromTasks()
        {
            // Given

            // When
            var result = new ResponsibilityDtoMapper(_entity.Object).WithTasks().Map();

            //Then
            Assert.That(result.StatusDerivedFromTasks, Is.EqualTo(_status));
        }
    }
}