using System;
using System.Linq.Expressions;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.BusinessSafeCompanyDetailsServiceTests
{
    [TestFixture]
    public class SaveTests
    {

        private long _companyId;
        private IBusinessSafeCompanyDetailService _target;
        private Mock<IPeninsulaLog> _log;
        private Mock<IBusinessSafeCompanyDetailRepository> _businessSafeCompanyDetailRepository;
        private Mock<IEmployeeForAuditingRepository> _employeeForAuditingRepository;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private BusinessSafeCompanyDetail _companyDetails;
        private UserForAuditing _user;
        private EmployeeForAuditing _employee;
        private CompanyDetailsRequest _request;

        [SetUp]
        public void Setup()
        {
            _employeeForAuditingRepository = new Mock<IEmployeeForAuditingRepository>();
            _businessSafeCompanyDetailRepository = new Mock<IBusinessSafeCompanyDetailRepository>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();

            _companyId = 1234L;

            _employee = new EmployeeForAuditing()
            {
                Id = Guid.NewGuid(),
                Forename = "Denis",
                Surname = "Irwin"
            };

            _companyDetails = new BusinessSafeCompanyDetail()
            {
                CompanyId = _companyId,
                BusinessSafeContactEmployee = _employee
            };

            _request = new CompanyDetailsRequest
            {
                Id = default(long),
                CAN = "can",
                NewCompanyDetails = new CompanyDetailsInformation()
                {
                    AddressLine1 = "address line 1",
                    AddressLine2 = "address line 2",
                    AddressLine3 = "address line 3",
                    AddressLine4 = "address line 4",
                    CompanyName = "CompanyName",
                    MainContact = "MainContact",
                    Postcode = "PostCode",
                    Telephone = "Telephone",
                    Website = "Website",
                    BusinessSafeContactId = _employee.Id,
                    BusinessSafeContactName = _employee.FullName
                }

            };

            _businessSafeCompanyDetailRepository
               .Setup(x => x.GetByCompanyId(It.IsAny<long>()))
               .Returns(_companyDetails);

            _employeeForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
               .Returns(_employee);

            _employeeForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyIdWithoutChecking(It.IsAny<Guid>(), It.IsAny<long>()))
               .Returns(_employee);

            _userForAuditingRepository
               .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
               .Returns(_user);

            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _target = GetTarget();
        }


        [Test]
        public void Given_valid_request_When_update_company_details_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();
            
            //When
            target.UpdateBusinessSafeContact(_request);

            //Then
            _businessSafeCompanyDetailRepository.Verify(x => x.SaveOrUpdate(It.IsAny<BusinessSafeCompanyDetail>()));
        }

        [Test]
        public void Given_valid_request_When_update_company_details_Then_should_save_with_the_correct_property_values()
        {
            //Given
            var target = GetTarget();

            //When
            target.UpdateBusinessSafeContact(_request);

            //Then
            _businessSafeCompanyDetailRepository.Verify(x => x.SaveOrUpdate(It.Is(matchesUpdateCompanyDetailsRequestProperties(_request))));
        }

        private Expression<Func<BusinessSafeCompanyDetail, bool>> matchesUpdateCompanyDetailsRequestProperties(CompanyDetailsRequest request)
        {
            return y => y.BusinessSafeContactEmployee.FullName == request.NewCompanyDetails.BusinessSafeContactName;
        }


        private IBusinessSafeCompanyDetailService GetTarget()
        {
            return new BusinessSafeCompanyDetailService(_businessSafeCompanyDetailRepository.Object, _employeeForAuditingRepository.Object, _userForAuditingRepository.Object, _log.Object);
        }
    }
}
