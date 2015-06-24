using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.CompanyDefaultsTaskFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class AccidentRecordDistributionListModelFactoryTest
    {     
        private Mock<IEmployeeService>  _employeeService;
        private Mock<ISiteService>      _siteService;

        private Guid _userId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _employeeService = new Mock<IEmployeeService>();
            _siteService = new Mock<ISiteService>();            
        }      

        //[Test]
        //public void Given_current_user_when_getmodel_called_then_model_contains_list_of_employees_for_user_sites()
        //{
        //    // Given
        //    List<SiteDto> siteDTOs = new List<SiteDto>();

        //    siteDTOs.Add(new SiteDto() { SiteId = 1, Name = "Test Site 1" });
        //    siteDTOs.Add(new SiteDto() { SiteId = 2, Name = "Test Site 2" });
        //    siteDTOs.Add(new SiteDto() { SiteId = 3, Name = "Test Site 3" });

        //    _siteService
        //        .Setup(x => x.GetAll(It.IsAny<long>()))
        //        .Returns(siteDTOs);

        //    List<EmployeeDto> employeeDTOs = new List<EmployeeDto>();
        //    employeeDTOs.Add(new EmployeeDto() { SiteId = 1, Id = Guid.NewGuid(), FullName = "One Alice"});            
        //    employeeDTOs.Add(new EmployeeDto() { SiteId = 2, Id = Guid.NewGuid(), FullName = "Two Alice" });
        //    employeeDTOs.Add(new EmployeeDto() { SiteId = 2, Id = Guid.NewGuid(), FullName = "Two Bob" });
        //    employeeDTOs.Add(new EmployeeDto() { SiteId = 3, Id = Guid.NewGuid(), FullName = "Three Alice" });
        //    employeeDTOs.Add(new EmployeeDto() { SiteId = 3, Id = Guid.NewGuid(), FullName = "Three Bob" });
        //    employeeDTOs.Add(new EmployeeDto() { SiteId = 3, Id = Guid.NewGuid(), FullName = "Three Carol" });

        //    _employeeService
        //        .Setup(x => x.GetAll(It.IsAny<long>()))
        //        .Returns(employeeDTOs);

        //    // When
        //    var factory = CreateFactory();

        //    IList<long> allowedSites = new List<long>(){ 2, 3 };

        //    AccidentRecordDistributionListViewModel result = factory
        //        .WithAllowedSites(allowedSites)
        //        .GetViewModel();

        //    // Then
        //    Assert.IsNotNull(result);
        //    Assert.IsNotNull(result.Employees);
        //    Assert.AreEqual(5, result.Employees.Count());
            
        //    //Assert.AreEqual("Two Alice" ,   result.Employees[0].FullName);
        //    //Assert.AreEqual("Two Bob" ,     result.Employees[1].FullName);
        //    //Assert.AreEqual("Three Alice",  result.Employees[2].FullName);
        //    //Assert.AreEqual("Three Bob",    result.Employees[3].FullName);           
        //}






        private AccidentRecordDistributionListModelFactory CreateFactory()
        {
            return new AccidentRecordDistributionListModelFactory(_employeeService.Object, _siteService.Object);
        }
    }
}
