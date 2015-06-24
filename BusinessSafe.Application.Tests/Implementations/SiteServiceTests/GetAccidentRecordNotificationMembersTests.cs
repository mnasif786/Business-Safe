﻿using System;
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
    public class  SiteStructureElementForTesting: Site
    {
        public IList<AccidentRecordNotificationMember> ProtectedAccidentRecordNotificationMembers
        {
            get { return _accidentRecordNotificationMembers; }
            set { _accidentRecordNotificationMembers = value; }
        }
    }

    [TestFixture]
    [Category("Unit")]
    public class GetAccidentRecordNotificationMembersTests
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
        public void Given_site_exists_when_GetAccidentRecordNotificationMembers_Then_return_AccidentRecordNotificationMembers_for_the_specified_site()
        {
            //Given
            var site = new SiteStructureElementForTesting();
            site.Id = 112312312L; // , ClientId = 123123, SiteId = 745435};
            var accidentRecordNotificationMembers = new List<AccidentRecordNotificationMember>();
            accidentRecordNotificationMembers.Add(new AccidentRecordNotificationEmployeeMember() { Employee = new Employee() });
            accidentRecordNotificationMembers.Add(new AccidentRecordNotificationEmployeeMember() { Employee = new Employee() });
            accidentRecordNotificationMembers.Add(new AccidentRecordNotificationEmployeeMember() { Employee = new Employee() });

            site.ProtectedAccidentRecordNotificationMembers = accidentRecordNotificationMembers;

            var target = CreateSiteService();
            _siteAddressRepository.Setup(x => x.GetById(site.Id))
                .Returns(() => site);

            //When
            var result = target.GetAccidentRecordNotificationMembers(site.Id);

            //Then
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void Given_site_exists_when_GetAccidentRecordNotificationMembers_Then_does_not_return_deleted_employees()
        {
            var site = new SiteStructureElementForTesting();
            site.Id = 112312312L; // , ClientId = 123123, SiteId = 745435};
            var accidentRecordNotificationMembers = new List<AccidentRecordNotificationMember>();
            accidentRecordNotificationMembers.Add(new AccidentRecordNotificationEmployeeMember() { Employee = new Employee() });
            accidentRecordNotificationMembers.Add(new AccidentRecordNotificationEmployeeMember() { Employee = new Employee() });
            accidentRecordNotificationMembers.Add(new AccidentRecordNotificationEmployeeMember() { Employee = new Employee() { Deleted = true } });

            site.ProtectedAccidentRecordNotificationMembers = accidentRecordNotificationMembers;

            var target = CreateSiteService();
            _siteAddressRepository.Setup(x => x.GetById(site.Id))
                .Returns(() => site);

            //When
            var result = target.GetAccidentRecordNotificationMembers(site.Id);

            //Then
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void Given_site_exists_when_GetAccidentRecordNotificationMembers_Then_does_not_return_deleted_items()
        {
            var site = new SiteStructureElementForTesting();
            site.Id = 112312312L; // , ClientId = 123123, SiteId = 745435};
            var accidentRecordNotificationMembers = new List<AccidentRecordNotificationMember>();
            accidentRecordNotificationMembers.Add(new AccidentRecordNotificationEmployeeMember() { Employee = new Employee() });
            accidentRecordNotificationMembers.Add(new AccidentRecordNotificationEmployeeMember() { Employee = new Employee() });
            accidentRecordNotificationMembers.Add(new AccidentRecordNotificationEmployeeMember() { Employee = new Employee(), Deleted = true });

            site.ProtectedAccidentRecordNotificationMembers = accidentRecordNotificationMembers;

            var target = CreateSiteService();
            _siteAddressRepository.Setup(x => x.GetById(site.Id))
                .Returns(() => site);

            //When
            var result = target.GetAccidentRecordNotificationMembers(site.Id);

            //Then
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void Given_site_and_employee_exists_when_AddAccidentRecordNotificationMemberToSite_Then_employee_added_to_site_distribution_list()
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

            var employee = new Employee();
            employee.Id = employeeId;

             _employeeRepository
                .Setup(x => x.GetById(employeeId))
                .Returns(() => employee);

            UserForAuditing user = new UserForAuditing()
                                       {
                                           Id = Guid.NewGuid()
                                       };

            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(user);

            // when
            var target = CreateSiteService();
            target.AddAccidentRecordNotificationMemberToSite(siteId, employeeId, userId );           

            // then
            Assert.IsNotNull(savedSite.AccidentRecordNotificationMembers);
            Assert.AreEqual(1, savedSite.AccidentRecordNotificationMembers.Count);
        }

       [Test]
        public void Given_site_and_employee_exists_when_RemoveAccidentRecordNotificationMemberToSite_Then_employee_is_removed_from_site_distribution_list()
        {
            // given 
            long siteId = 1234;
            Guid employeeId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            var employee = new Employee();
            employee.Id = employeeId;

            UserForAuditing user = new UserForAuditing()
            {
                Id = Guid.NewGuid()
            };

            var site = new Site { Id = siteId };
            site.AddAccidentRecordNotificationMember(employee,user);

            Site savedSite = null;
            _siteAddressRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<Site>()))
                .Callback<Site>(y => savedSite = y);

            _siteAddressRepository
                .Setup(x => x.GetById(site.Id))
                .Returns(() => site);

            _employeeRepository
               .Setup(x => x.GetById(employeeId))
               .Returns(() => employee);

            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(user);

            // when
            var target = CreateSiteService();
            target.RemoveAccidentRecordNotificationMemberFromSite(siteId, employeeId, userId);

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
