//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using BusinessSafe.Application.Contracts.Checklists;
//using BusinessSafe.Application.Contracts.Employee;
//using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
//using BusinessSafe.Application.DataTransferObjects;
//using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
//using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
//using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
//using Moq;
//using NServiceBus;
//using NUnit.Framework;

//namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ChecklistGenerator
//{
//    [TestFixture]
//    public class SaveTests
//    {
//        private IChecklistGeneratorViewModelFactory _checklistGeneratorViewModelFactory;
//        private Mock<IEmployeeService> _employeeService;
//        private Mock<IEmployeeChecklistEmailService> _employeeChecklistEmailService;
//        private Mock<IPersonalRiskAssessmentService> _personalRiskAssessmentService;
//        private Mock<IChecklistService> _checklistService;
//        private Mock<IBus> _bus;
//        private long _companyId;
//        private long _personalRiskAssessmentId;
//        private string _message;
//        private IList<EmployeeDto> _employees;
//        private IList<ChecklistDto> _checklists; 
//        private ChecklistGeneratorViewModel _baseInputViewModel;
//        private FormCollection _formCollection;

//        [SetUp]
//        public void Setup()
//        {
//            _employeeService = new Mock<IEmployeeService>();
//            _employeeChecklistEmailService = new Mock<IEmployeeChecklistEmailService>();
//            _personalRiskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
//            _checklistService = new Mock<IChecklistService>();
//            _bus = new Mock<IBus>();
//            _companyId = 23522L;
//            _personalRiskAssessmentId = 342L;

//            _employees = new List<EmployeeDto>
//                             {
//                                 new EmployeeDto
//                                     {
//                                         Id = new Guid("a9d827d2-cbfc-4d9c-9491-f8e271df77b8"),
//                                         FullName = "Mark Mauve",
//                                         Email = "mark.mauve@email.com"
//                                     },
//                                 new EmployeeDto
//                                     {
//                                         Id = new Guid("cf7ce0b2-8a97-4c32-8af1-ce3c96716fdd"),
//                                         FullName = "Peter Pink"
//                                     },
//                                 new EmployeeDto
//                                     {
//                                         Id = new Guid("0b20512d-3b4e-4da3-ab8c-6433c3fa4118"),
//                                         FullName = "Guy Grey",
//                                         Email = "guy.grey@email.com"
//                                     }
//                             };

//            _checklists = new List<ChecklistDto>
//                              {
//                                  new ChecklistDto
//                                      {
//                                          Id = 1L,
//                                          Title = "Test Checklist 01"
//                                      },
//                                  new ChecklistDto
//                                      {
//                                          Id = 2L,
//                                          Title = "Test Checklist 02"
//                                      },
//                                  new ChecklistDto
//                                      {
//                                          Id = 3L,
//                                          Title = "Test Checklist 03"
//                                      },
//                              };

//            _message = "Test Message";

//            _employeeService
//                .Setup(x => x.GetAll(_companyId))
//                .Returns(_employees);

//            _checklistService
//                .Setup(x => x.GetAllUndeleted())
//                .Returns(_checklists);

//            _checklistGeneratorViewModelFactory = new ChecklistGeneratorViewModelFactory(_employeeService.Object, _checklistService.Object, _personalRiskAssessmentService.Object);

//            _baseInputViewModel = new ChecklistGeneratorViewModel
//                                      {
//                                          EmployeeId = _employees[0].Id,
//                                          RiskAssessmentId = _personalRiskAssessmentId,
//                                          IsForMultipleEmployees = "single",
//                                          SingleEmployeesSectionVisible = true,
//                                          MultipleEmployeesSectionVisible = false,
//                                          Employees = null,
//                                          NewEmployeeEmail = null,
//                                          NewEmployeeEmailVisible = false,
//                                          Checklists = null,
//                                          Message = _message
//                                      };

//            _formCollection = new FormCollection();
//            _formCollection.Add("IncludeChecklist_1", "false");
//            _formCollection.Add("IncludeChecklist_2", "true,false");
//            _formCollection.Add("IncludeChecklist_3", "false");
//        }

//        [Test]
//        public void SaveReturnsCorrectActionResultAndViewModel()
//        {
//            var result = GetTarget().Save(_baseInputViewModel, _formCollection);
//            Assert.That(result, Is.TypeOf<ViewResult>());
//            var viewResult = result as ViewResult;
//            Assert.That(viewResult.Model, Is.TypeOf<ChecklistGeneratorViewModel>());
//        }

//        [Test]
//        public void SuccessfulSaveIsValid()
//        {
//            var viewResult = GetTarget().Save(_baseInputViewModel, _formCollection) as ViewResult;
//            Assert.That(viewResult.ViewData.ModelState.IsValid);
//        }

//        [Test]
//        public void SuccessfulSaveDisplaysSuccessMessage()
//        {
//            var viewResult = GetTarget().Save(_baseInputViewModel, _formCollection) as ViewResult;
//            Assert.That(viewResult.TempData["Notice"], Is.EqualTo("Checklist Generator Successfully Updated"));
//        }

//        [Test]
//        [TestCase("single")]
//        [TestCase("multiple")]
//        [TestCase(null)]
//        public void SaveReturnsSubmittedMultipleEmployees(
//            string isForMultipleEmployees,
//            bool singleEmployeesSectionVisible,
//            bool multipleEmployeesSectionVisible)
//        {
//            var inputViewModel = _baseInputViewModel;
//            inputViewModel.IsForMultipleEmployees = isForMultipleEmployees;
//            var returnViewModel = (GetTarget().Save(inputViewModel, _formCollection) as ViewResult).Model as ChecklistGeneratorViewModel;
//            Assert.That(returnViewModel.IsForMultipleEmployees, Is.EqualTo(isForMultipleEmployees));
//        }

//        [Test]
//        [TestCase(true, false)]
//        [TestCase(false, true)]
//        [TestCase(false, false)]
//        public void SaveReturnsSubmittedSingleAndMultipleEmployeesSectionVisible(
//            string isForMultipleEmployees,
//            bool singleEmployeesSectionVisible,
//            bool multipleEmployeesSectionVisible)
//        {
//            var inputViewModel = _baseInputViewModel;
//            inputViewModel.SingleEmployeesSectionVisible = singleEmployeesSectionVisible;
//            inputViewModel.MultipleEmployeesSectionVisible = multipleEmployeesSectionVisible;
//            var returnViewModel = (GetTarget().Save(inputViewModel, _formCollection) as ViewResult).Model as ChecklistGeneratorViewModel;
//            Assert.That(returnViewModel.SingleEmployeesSectionVisible, Is.EqualTo(singleEmployeesSectionVisible));
//            Assert.That(returnViewModel.MultipleEmployeesSectionVisible, Is.EqualTo(multipleEmployeesSectionVisible));
//        }

//        [Test]
//        [Ignore] //todo: why is this failing?
//        public void SingleEmployeeOptionDisplaysEmployees()
//        {
//            var returnViewModel = (GetTarget().Save(_baseInputViewModel, _formCollection) as ViewResult).Model as ChecklistGeneratorViewModel;
//            var employees = returnViewModel.Employees.ToList();
//            Assert.That(employees.Count(), Is.EqualTo(3));
//            Assert.That(employees[0].label, Is.EqualTo("Mark Mauve (mark.mauve@email.com)"));
//            Assert.That(employees[1].label, Is.EqualTo("Peter Pink"));
//            Assert.That(employees[2].label, Is.EqualTo("Guy Grey (guy.grey@email.com)"));
//        }

//        [Test]
//        [TestCase(null)]
//        [TestCase("a9d827d2-cbfc-4d9c-9491-f8e271df77b8")]
//        [TestCase("cf7ce0b2-8a97-4c32-8af1-ce3c96716fdd")]
//        [TestCase("0b20512d-3b4e-4da3-ab8c-6433c3fa4118")]
//        public void SingleEmployeeOptionHasCorrectSelectedEmployee(string employeeIdString)
//        {
//            var inputViewModel = _baseInputViewModel;
//            Guid? employeeId = null;

//            if(employeeIdString != null)
//            {
//                employeeId = new Guid(employeeIdString);
//            }

//            inputViewModel.EmployeeId = employeeId;
//            var returnViewModel = (GetTarget().Save(inputViewModel, _formCollection) as ViewResult).Model as ChecklistGeneratorViewModel;
//            Assert.That(returnViewModel.EmployeeId, Is.EqualTo(employeeId));
//        }

//        //todo: test whether displays email
//        //todo: test whether displays email box.
//        //todo: failed save displays message.
//        //todo: failed save displays correct data.
//        //todo: verify methods are called.

//        [Test]
//        public void SaveDisplaysChecklitsCorrectly()
//        {
//            var returnViewModel = (GetTarget().Save(_baseInputViewModel, _formCollection) as ViewResult).Model as ChecklistGeneratorViewModel;
//            Assert.That(returnViewModel.Checklists.Count, Is.EqualTo(3));
//            Assert.That(returnViewModel.Checklists[0].Id, Is.EqualTo(1));
//            Assert.That(returnViewModel.Checklists[0].Title, Is.EqualTo("Test Checklist 01"));
//            Assert.That(returnViewModel.Checklists[0].ControlId, Is.EqualTo("IncludeChecklist_1"));
//            Assert.That(returnViewModel.Checklists[0].Checked, Is.False);
//            Assert.That(returnViewModel.Checklists[1].Id, Is.EqualTo(2));
//            Assert.That(returnViewModel.Checklists[1].Title, Is.EqualTo("Test Checklist 02"));
//            Assert.That(returnViewModel.Checklists[1].ControlId, Is.EqualTo("IncludeChecklist_2"));
//            Assert.That(returnViewModel.Checklists[1].Checked, Is.True);
//            Assert.That(returnViewModel.Checklists[2].Id, Is.EqualTo(3));
//            Assert.That(returnViewModel.Checklists[2].Title, Is.EqualTo("Test Checklist 03"));
//            Assert.That(returnViewModel.Checklists[2].ControlId, Is.EqualTo("IncludeChecklist_3"));
//            Assert.That(returnViewModel.Checklists[2].Checked, Is.False);
//        }

//        [Test]
//        [Ignore]    //Wrong way to do it - testing too many concepts.
//        public void SaveWithSingleEmployeeWithExistingEmailReturnsExpectedViewModel()
//        {
//            var inputViewModel = _baseInputViewModel;
//            var formCollection = _formCollection;
//            var result = GetTarget().Save(inputViewModel, formCollection);
//            Assert.That(result, Is.TypeOf<ViewResult>());
//            var viewResult = result as ViewResult;
//            Assert.That(viewResult.Model, Is.TypeOf<ChecklistGeneratorViewModel>());
//            Assert.That(viewResult.ViewData.ModelState.IsValid);
//            Assert.That(viewResult.TempData["Notice"], Is.EqualTo("Checklist Generator Successfully Updated"));
//            var returnViewModel = viewResult.Model as ChecklistGeneratorViewModel;
//            Assert.That(returnViewModel.IsForMultipleEmployees, Is.EqualTo("single"));
//            Assert.That(returnViewModel.SingleEmployeesSectionVisible, Is.True);
//            Assert.That(returnViewModel.MultipleEmployeesSectionVisible, Is.False);
//            Assert.That(returnViewModel.EmployeeId, Is.EqualTo(_employees[0].Id));
//            Assert.That(returnViewModel.NewEmployeeEmailVisible, Is.False);
//            Assert.That(returnViewModel.NewEmployeeEmail, Is.Null);
//            Assert.That(returnViewModel.Checklists.Count, Is.EqualTo(3));
//            Assert.That(returnViewModel.Checklists[0].Id, Is.EqualTo(1));
//            Assert.That(returnViewModel.Checklists[0].Title, Is.EqualTo("Test Checklist 01"));
//            Assert.That(returnViewModel.Checklists[0].ControlId, Is.EqualTo("IncludeChecklist_1"));
//            Assert.That(returnViewModel.Checklists[0].Checked, Is.False);
//            Assert.That(returnViewModel.Checklists[1].Id, Is.EqualTo(2));
//            Assert.That(returnViewModel.Checklists[1].Title, Is.EqualTo("Test Checklist 02"));
//            Assert.That(returnViewModel.Checklists[1].ControlId, Is.EqualTo("IncludeChecklist_2"));
//            Assert.That(returnViewModel.Checklists[1].Checked, Is.True);
//            Assert.That(returnViewModel.Checklists[2].Id, Is.EqualTo(3));
//            Assert.That(returnViewModel.Checklists[2].Title, Is.EqualTo("Test Checklist 03"));
//            Assert.That(returnViewModel.Checklists[2].ControlId, Is.EqualTo("IncludeChecklist_3"));
//            Assert.That(returnViewModel.Checklists[2].Checked, Is.False);
//            Assert.That(returnViewModel.Message, Is.EqualTo(_message));
//        }

//        private ChecklistGeneratorController GetTarget()
//        {
//            var controller = new ChecklistGeneratorController(
//                _checklistGeneratorViewModelFactory, 
//                _employeeService.Object, 
//                _employeeChecklistEmailService.Object, 
//                _personalRiskAssessmentService.Object,
//                _bus.Object);

//            return TestControllerHelpers.AddUserToController(controller);
//        }
//    }
//}
