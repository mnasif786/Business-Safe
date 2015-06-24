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
    public class DocumentDtoMapperTests
    {

        [Test]
        public void Given_a_document_and_created_by_is_null_when_mapped_then_createdby_empty_string()
        {
            //given
            var document = new RiskAssessmentDocument();
            document.CreatedBy = null;
            document.RiskAssessment = new GeneralRiskAssessment();
            //when
            var documentDto = new DocumentDtoMapper().Map(document);

            //then
            Assert.AreEqual(string.Empty,documentDto.CreatedByName);
        }

        [Test]
        public void Given_a_document_and_created_by_is_not_null_when_mapped_then_createdby_is_equal()
        {
            //given
            var document = new RiskAssessmentDocument();
            document.CreatedBy = new UserForAuditing {Employee = new EmployeeForAuditing() {Forename = "foasfojasf", Surname = "dfjsdfj"}};
            document.RiskAssessment = new GeneralRiskAssessment();
            //when
            var documentDto = new DocumentDtoMapper().Map(document);

            //then
            Assert.AreEqual(document.CreatedBy.Employee.FullName, documentDto.CreatedByName);
        }
    }
}
