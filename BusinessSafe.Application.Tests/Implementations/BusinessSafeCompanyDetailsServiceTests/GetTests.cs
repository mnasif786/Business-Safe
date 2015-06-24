using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Common;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.BusinessSafeCompanyDetailsServiceTests
{
    [TestFixture]
    public class GetTests
    {
        private long _companyId;
        private BusinessSafeCompanyDetailService _target;
        private Mock<IPeninsulaLog> _log;
        private Mock<IBusinessSafeCompanyDetailRepository> _businessSafeCompanyDetailRepository;
        private BusinessSafeCompanyDetail _companyDetails;

        [SetUp]
        public void Setup()
        {
            _companyId = 1234L;

            _companyDetails = new BusinessSafeCompanyDetail()
            {
                CompanyId = _companyId,
                BusinessSafeContactEmployee = new EmployeeForAuditing()
                {
                    Id = Guid.NewGuid(),
                    Forename = "Denis",
                    Surname = "Irwin",
                }
            };

            _businessSafeCompanyDetailRepository = new Mock<IBusinessSafeCompanyDetailRepository>();
            _businessSafeCompanyDetailRepository
                .Setup(x => x.GetByCompanyId(It.IsAny<long>()))
                .Returns(_companyDetails);
            
            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _target = GetTarget();
        }

        [Test]
        public void Given_When_Get_Then_retrieve_company_detail_from_repo()
        {
            // Given

            // When
            _target.Get(_companyId);

            // Then
            _businessSafeCompanyDetailRepository
                .Verify(x => x.GetByCompanyId(_companyId));
        }

        [Test]
        public void Given_When_Get_Then_returns_dto()
        {
            // Given

            // When
            var result = _target.Get(_companyId);

            // Then
            Assert.That(result.BusinessSafeContactEmployeeId, Is.EqualTo(_companyDetails.BusinessSafeContactEmployee.Id));
            Assert.That(result.BusinessSafeContactEmployeeFullName, Is.EqualTo(_companyDetails.BusinessSafeContactEmployee.FullName));
        }

        private BusinessSafeCompanyDetailService GetTarget()
        {
            return new BusinessSafeCompanyDetailService(_businessSafeCompanyDetailRepository.Object,null,null,_log.Object);
        }
    }
}
