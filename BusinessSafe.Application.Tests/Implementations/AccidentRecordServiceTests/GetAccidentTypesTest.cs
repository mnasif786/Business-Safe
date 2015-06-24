using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using Moq;
using NUnit.Framework;
using BusinessSafe.Application.Implementations.AccidentRecords;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Domain.Constants;

namespace BusinessSafe.Application.Tests.Implementations.AccidentRecordServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetAccidentTypesTest
    {
        private Mock<IAccidentTypeRepository>   _accidentTypeRepository;
       
        [SetUp]
        public void Setup()
        {
            // set up repositiories
            _accidentTypeRepository = new Mock<IAccidentTypeRepository>();                          
        }


        [Test]
        public void Given_valid_company_id_When_retrieving_accident_types_Then_returned_accident_types_are_specific_to_company()
        {
            // Given
            var target = GetTarget();
            long companyId = 1234;


            IEnumerable<AccidentType> returnedAccidentTypes = new List<AccidentType>()
                                                                  {
                                                                      new AccidentType()
                                                                      { 
                                                                            Id = 1,
                                                                            Description="First Accident Type",
                                                                            CompanyId = companyId
                                                                      },
                                                                      new AccidentType()
                                                                      { 
                                                                            Id = 2,
                                                                            Description="Second Accident Type",
                                                                            CompanyId = null
                                                                      },
                                                                      new AccidentType()
                                                                      { 
                                                                            Id = 3,
                                                                            Description="Third Accident Type",
                                                                            CompanyId = companyId
                                                                      },
                                                                      new AccidentType()
                                                                      { 
                                                                            Id = 4,
                                                                            Description="Fourth Accident Type",
                                                                            CompanyId = null
                                                                      }
                                                                  };

            _accidentTypeRepository
                .Setup(x => x.GetAllForCompany(companyId))
                .Returns( returnedAccidentTypes );
            
            // When
            var result = target.GetAllForCompany ( companyId );

            // Then
            Assert.That(result, Is.InstanceOf<IEnumerable<AccidentTypeDto>>());
            Assert.That(result.Count(), Is.EqualTo(4));
        }
      
        private IAccidentTypeService GetTarget()
        {
            return new AccidentTypeService(_accidentTypeRepository.Object);            
        }            

    }
}
