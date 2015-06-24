using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.CompanyDefaultServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SavePeopleAtRiskTests
    {
        private Mock<IPeopleAtRiskRepository> _peopleAtRiskRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _peopleAtRiskRepository = new Mock<IPeopleAtRiskRepository>();
            _user = new UserForAuditing();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_save_person_at_risk_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateCompanyDefaultService();
            var request = SaveCompanyDefaultRequestBuilder.Create().WithRunMatchCheck(true).Build();

            _peopleAtRiskRepository
                .Setup(rr => rr.SaveOrUpdate(It.IsAny<PeopleAtRisk>()))
                .Callback<PeopleAtRisk>(par => par.Id = 1);

            _userRepository
               .Setup(x => x.GetById(request.UserId))
               .Returns(_user);

            //When
            target.SavePeopleAtRisk(request);

            //Then
            _peopleAtRiskRepository.Verify(tp => tp.SaveOrUpdate(It.IsAny<PeopleAtRisk>()), Times.Once());

        }


        [Test]
        public void Given_valid_request_When_save_person_at_risk_Then_should_return_correct_value()
        {
            //Given
            var target = CreateCompanyDefaultService();
            var request = SaveCompanyDefaultRequestBuilder.Create().WithRunMatchCheck(true).Build();

            _peopleAtRiskRepository
                .Setup(rr => rr.SaveOrUpdate(It.IsAny<PeopleAtRisk>()))
                .Callback<PeopleAtRisk>(par => par.Id = 1);

            _userRepository
               .Setup(x => x.GetById(request.UserId))
               .Returns(_user);

            //When
            var result = target.SavePeopleAtRisk(request);

            //Then
            Assert.That(result, Is.GreaterThan(0));
        }


        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(null, _peopleAtRiskRepository.Object, _userRepository.Object, null, null, null, null, null, null, null, _log.Object);
            return target;
        }
    }
}
