using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using EvaluationChecklist.Controllers;
using EvaluationChecklist.Helpers;
using Moq;
using NUnit.Framework;

namespace EvaluationChecklist.Api.Tests.QaControllerTests
{
    [TestFixture]
    internal class GetQaAdvisorsTests
    {
        private Mock<IQaAdvisorRepository> _qaAdvisorRepository;
        private Mock<IDependencyFactory> _dependencyFactory;
        private List<QaAdvisor> _qaAdvisorList;

        [SetUp]
        public void Setup()
        {
            _dependencyFactory = new Mock<IDependencyFactory>();
            _qaAdvisorRepository = new Mock<IQaAdvisorRepository>();

            _dependencyFactory.Setup(x => x.GetInstance<IQaAdvisorRepository>())
             .Returns(_qaAdvisorRepository.Object);

            _qaAdvisorList = new List<QaAdvisor>()
            {
                new QaAdvisor() { Id = Guid.NewGuid(), Forename = "name1", Surname = "surname1", Email = "email@email1.com"},
                new QaAdvisor() { Id = Guid.Parse("3A204FB3-1956-4EFC-BE34-89F7897570DB"), Forename = "H&S", Surname = "Reports", Email = "H&S.Reports@Peninsula-uk.com"},
                new QaAdvisor() { Id = Guid.NewGuid(), Forename = "name3", Surname = "surname3", Email = "email@email3.com"},
            };

            _qaAdvisorRepository.Setup(x => x.GetAll()).Returns(_qaAdvisorList);

            _qaAdvisorRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(_qaAdvisorList[1]);
        }

        [Test]
        public void Given_QA_Advisors_Ensure_HealthAndSafety_In_List_With_Correct_Values_Assigned()
        {
            // Given
            var target = new QaAdvisorController(_dependencyFactory.Object);

            // When
            var result = target.RetrieveListOfQaAdvisors();

            // Then
            Assert.That(result[2].Forename, Is.EqualTo("H&S"));
            Assert.That(result[2].Id, Is.EqualTo(Guid.Parse("3A204FB3-1956-4EFC-BE34-89F7897570DB")));
            Assert.That(result[2].Surname, Is.EqualTo("Reports"));
            Assert.That(result[2].Fullname , Is.EqualTo("H&S Reports"));
            Assert.That(result[2].Initials , Is.EqualTo("H&S Reports"));
            Assert.That(result[2].Email , Is.EqualTo("H&S.Reports@Peninsula-uk.com"));
        }


        [Test]
        public void Given_QA_Advisors_Ensure_That_HealthAndSafety_Are_At_Back_Of_List()
        {
            // Given
            var target = new QaAdvisorController(_dependencyFactory.Object);

            // When
            var result = target.RetrieveListOfQaAdvisors();

            // Then
            Assert.That(result[2].Forename, Is.EqualTo(_qaAdvisorList[1].Forename));
        }
    }
}
