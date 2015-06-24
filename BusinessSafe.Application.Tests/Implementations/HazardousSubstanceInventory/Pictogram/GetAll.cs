using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Implementations.HazardousSubstanceInventory;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstances.Pictogram
{
    [TestFixture]
    public class GetAll
    {
        private Mock<IPictogramRepository> pictogramRepository;
        private PictogramService target;

        [SetUp]
        public void Setup()
        {
            pictogramRepository = new Mock<IPictogramRepository>();
            pictogramRepository.Setup(x => x.GetAll()).Returns(new List<Domain.Entities.Pictogram>());

            target = new PictogramService(pictogramRepository.Object);
        }

        [Test]
        public void When_GetAll_Then_Get_Pictograms_From_Repo()
        {
            // Given

            // When
            var result = target.GetAll();

            // Then
            pictogramRepository.Verify(x => x.GetAll(), Times.Once());
        }

        [Test]
        public void When_GetAll_Then_Map_Entities_To_Dtos()
        {
            // Given
            var returnedPictogramEntities = new List<Domain.Entities.Pictogram>()
                                            {
                                                new Domain.Entities.Pictogram()
                                                {
                                                    Id = 1,
                                                    Title = "Picto 1",
                                                    HazardousSubstanceStandard = HazardousSubstanceStandard.Global
                                                },
                                                new Domain.Entities.Pictogram()
                                                {
                                                    Id = 2,
                                                    Title = "Picto 2",
                                                    HazardousSubstanceStandard = HazardousSubstanceStandard.Global
                                                },
                                                new Domain.Entities.Pictogram()
                                                {
                                                    Id = 3,
                                                    Title = "Picto 3",
                                                    HazardousSubstanceStandard = HazardousSubstanceStandard.European,
                                                    
                                                },
                                            };

            pictogramRepository
                .Setup(x => x.GetAll())
                .Returns(returnedPictogramEntities);

            // When
            var result = target.GetAll().ToList();

            // Then
            Assert.That(result.Count, Is.EqualTo(3));
            for (var i = 0; i < result.Count(); i++)
            {

                Assert.That(result[i].Id, Is.EqualTo(returnedPictogramEntities[i].Id));
                Assert.That(result[i].Title, Is.EqualTo(returnedPictogramEntities[i].Title));
                Assert.That(result[i].HazardousSubstanceStandard, Is.EqualTo(returnedPictogramEntities[i].HazardousSubstanceStandard));
            }
        }
    }
}
