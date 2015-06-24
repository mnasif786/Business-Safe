using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Mappers
{
    [TestFixture]
    public class FireRiskAssessmentDtoMapperTests
    {
        [Test]
        public void Given_that_the_checklist_is_null_when_MappingWithChecklist_then_latestFireChecklist_is_null()
        {
            var target = new FireRiskAssessmentChecklistDtoMapper();
            FireRiskAssessmentChecklist fireRA = null;
       
            var result = target.MapWithChecklist(fireRA);

            Assert.IsNull(result);
        }
    }
}
