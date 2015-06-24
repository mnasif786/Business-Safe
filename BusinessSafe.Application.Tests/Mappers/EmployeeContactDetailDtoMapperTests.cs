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
    public class EmployeeContactDetailDtoMapperTests
    {
        [Test]
        public void Given_contact_details_when_mapped_then_properties_are_mapped_correctly()
        {
            //given
            var contactDetails = new EmployeeContactDetail();
            contactDetails.Id = 23531245L;
            contactDetails.Address1 = "entity.Address1";
            contactDetails.Address2 = "entity.Address2,";
            contactDetails.Address3 = "entity.Address3";
            contactDetails.Town = "entity.Town,";
            contactDetails.County = "entity.County,";
            contactDetails.PostCode = "entity.PostCode,";
            contactDetails.Telephone1 = "entity.Telephone1,";
            contactDetails.Telephone2 = "entity.Telephone2,";
            contactDetails.PreferedTelephone = 431;
            contactDetails.Email = "entity.Email,";
            // Country = entity.Country != null ? new CountryDtoMapper().Map(entity

            //when
            var result = new EmployeeContactDetailDtoMapper().MapWithCountry(contactDetails);

            //then
            Assert.That(result.Id, Is.EqualTo(contactDetails.Id));
            Assert.That(result.Address1, Is.EqualTo(contactDetails.Address1));
            Assert.That(result.Address2, Is.EqualTo(contactDetails.Address2));
            Assert.That(result.Address3, Is.EqualTo(contactDetails.Address3));
            Assert.That(result.Town, Is.EqualTo(contactDetails.Town));
            Assert.That(result.County, Is.EqualTo(contactDetails.County));
            Assert.That(result.PostCode, Is.EqualTo(contactDetails.PostCode));
            Assert.That(result.Telephone1, Is.EqualTo(contactDetails.Telephone1));
            Assert.That(result.Telephone2, Is.EqualTo(contactDetails.Telephone2));
            Assert.That(result.PreferedTelephone, Is.EqualTo(contactDetails.PreferedTelephone));
            Assert.That(result.Email, Is.EqualTo(contactDetails.Email));
           
        }
    }
}
