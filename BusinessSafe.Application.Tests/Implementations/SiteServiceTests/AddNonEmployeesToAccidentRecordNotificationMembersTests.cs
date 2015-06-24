using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.Sites;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using Moq.Protected;

namespace BusinessSafe.Application.Tests.Implementations.SiteServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class AddNonEmployeesToAccidentRecordNotificationMembersTests
    {
        private Mock<ISiteRepository> _siteAddressRepository;
        private Mock<ISiteStructureElementRepository> _siteRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;

        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _siteAddressRepository = new Mock<ISiteRepository>();
            _siteRepository = new Mock<ISiteStructureElementRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();

            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_site_exists__and_non_employee_in_distribution_list_when_GetAccidentRecordNotificationMembers_called_Then_nonemployee_returned_in_site_distribution_list()
        {
            //Given
            var site = new SiteStructureElementForTesting();
            site.Id = 112312312L; // , ClientId = 123123, SiteId = 745435};

            string nonEmployeeEmail = "Non.Employee@test.com";
            string nonEmployeeName = "Norman Notemployedhere";

            var accidentRecordNotificationMembers = new List<AccidentRecordNotificationMember>();
            accidentRecordNotificationMembers.Add(
                new AccidentRecordNotificationNonEmployeeMember()
                {                    
                    NonEmployeeEmail = nonEmployeeEmail,
                    NonEmployeeName = nonEmployeeName
                });

            site.ProtectedAccidentRecordNotificationMembers = accidentRecordNotificationMembers;

            var target = CreateSiteService();
            _siteAddressRepository.Setup(x => x.GetById(site.Id))
                .Returns(() => site);

            //When
            var result = target.GetAccidentRecordNotificationMembers(site.Id);

            //Then            
            Assert.AreEqual(1, result.Count);

            Assert.AreEqual(nonEmployeeName, result[0].FullName());
            Assert.AreEqual(nonEmployeeEmail, result[0].Email());
        }

        [Test]
        public void Given_site_exists_when_AddNonEmployeeToAccidentRecordNotificationMembers_Then_employee_added_to_site_distribution_list()
        {
            // given 
            long siteId = 1234;
            Guid employeeId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            Site savedSite = null;
            _siteAddressRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<Site>()))
                .Callback<Site>(y => savedSite = y);

            var site = new Site();
            site.Id = siteId;

            _siteAddressRepository
                .Setup(x => x.GetById(site.Id))
                .Returns(() => site);

            string nonEmployeeEmail = "Non.Employee@test.com";
            string nonEmployeeName = "Norman Notemployedhere";


            UserForAuditing user = new UserForAuditing()
            {
                Id = Guid.NewGuid()
            };

            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(user);

            // when
            var target = CreateSiteService();
            target.AddNonEmployeeToAccidentRecordNotificationMembers(siteId, nonEmployeeName, nonEmployeeEmail, userId);

            // then
            Assert.IsNotNull(savedSite.AccidentRecordNotificationMembers);
            Assert.AreEqual(1, savedSite.AccidentRecordNotificationMembers.Count);

            Assert.AreEqual(nonEmployeeName, savedSite.AccidentRecordNotificationMembers[0].FullName());
            Assert.AreEqual(nonEmployeeEmail, savedSite.AccidentRecordNotificationMembers[0].Email());
        }



        [Test]
        public void Given_site_and_employee_exists_when_RemoveAccidentRecordNotificationMemberToSite_Then_employee_is_removed_from_site_distribution_list()
        {
            // given 
            long siteId = 1234;
            var site = new Site { Id = siteId };

            Guid userId = Guid.NewGuid();           
            UserForAuditing user = new UserForAuditing()
            {
                Id = Guid.NewGuid()
            };

            string nonEmployeeEmail = "Non.Employee@test.com";
            string nonEmployeeName = "Norman Notemployedhere";

            site.AddNonEmployeeToAccidentRecordNotificationMembers(nonEmployeeName, nonEmployeeEmail, user);

            Site savedSite = null;
            _siteAddressRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<Site>()))
                .Callback<Site>(y => savedSite = y);

            _siteAddressRepository
                .Setup(x => x.GetById(site.Id))
                .Returns(() => site);
          
            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(user);

            // when                        
            var target = CreateSiteService();
            target.RemoveNonEmployeeAccidentRecordNotificationMemberFromSite( siteId, nonEmployeeEmail, userId);

            // then
            Assert.IsNotNull(savedSite);
            Assert.IsNotNull(savedSite.AccidentRecordNotificationMembers);
            Assert.AreEqual(0, savedSite.AccidentRecordNotificationMembers.Count);
        }

        private SiteService CreateSiteService()
        {
            return new SiteService(_siteAddressRepository.Object, _siteRepository.Object, _userForAuditingRepository.Object, _employeeRepository.Object);
        }
    }
}
