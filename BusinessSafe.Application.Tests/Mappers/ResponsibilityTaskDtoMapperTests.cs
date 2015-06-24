using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Mappers
{
    [TestFixture]
    public class ResponsibilityTaskDtoMapperTests
    {

        [Test]
        public void Given_a_no_responsibility_when_Map_then_return_null()
        {
            //Given
            ResponsibilityTask entity = null;

            //When
            var result = ResponsibilityTaskDto.Map(entity);

            //Act
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Given_a_responsibilitytask_when_Map_then_properties_are_mapped()
        {
            //Given
            var entity = new ResponsibilityTask
            {
            };

            //When
            var result = ResponsibilityTaskDtoMapper.Map(entity);

            //Act
            Assert.That(result.Id, Is.EqualTo(entity.Id));
            Assert.That(result.CompanyId, Is.EqualTo(entity.CompanyId));
        }
    }
}