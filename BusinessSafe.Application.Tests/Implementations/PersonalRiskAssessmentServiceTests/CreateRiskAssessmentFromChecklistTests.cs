using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.PersonalRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.PersonalRiskAssessmentServiceTests
{
    [TestFixture]
    public class CreateRiskAssessmentFromChecklistTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IUserForAuditingRepository> _userRepo;
        private Mock<IPersonalRiskAssessmentRepository> _personalRiskAssessmentRepo;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IChecklistRepository> _checklistRepository;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IEmployeeChecklistRepository> _employeeChecklistRepository;

        [SetUp]
        public void Setup()
        {
            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _userRepo = new Mock<IUserForAuditingRepository>();
            _userRepo.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(new UserForAuditing());

            _personalRiskAssessmentRepo = new Mock<IPersonalRiskAssessmentRepository>();
            _personalRiskAssessmentRepo.Setup(x => x.Save(It.IsAny<PersonalRiskAssessment>()));

            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<PersonalRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()));

            _employeeRepository = new Mock<IEmployeeRepository>();
            _checklistRepository = new Mock<IChecklistRepository>();
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _employeeChecklistRepository = new Mock<IEmployeeChecklistRepository>();
        }

        [Test]
        public void Given_checklist_then_title_of_the_PRA_Title_is_created_from_the_employee_checklist_Recipent_Title_and_Reference()
        {
            //GIVEN
            var employeeChecklist = new EmployeeChecklist();
            employeeChecklist.Employee = new Employee() {Forename = "Sandor", Surname = "Clegane"};
            employeeChecklist.ReferencePrefix = "RefPrefix";
            employeeChecklist.ReferenceIncremental = 5;
            employeeChecklist.Checklist = new Checklist() {Title = "Checklist Title"};
            employeeChecklist.PersonalRiskAssessment = new PersonalRiskAssessment();

            _employeeChecklistRepository.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(employeeChecklist);


            PersonalRiskAssessment savedPRA = null;
            _personalRiskAssessmentRepo.Setup(x => x.Save(It.IsAny<PersonalRiskAssessment>()))
                .Callback<PersonalRiskAssessment>(pra => savedPRA = pra);

            var target = GetTarget();

            //when
            target.CreateRiskAssessmentFromChecklist(Guid.NewGuid(), Guid.NewGuid());

            //THEN
            var expectedPRATitle = string.Format("{0}_{1}{2}", employeeChecklist.Employee.FullName, employeeChecklist.Checklist.Title, employeeChecklist.FriendlyReference);
            Assert.That(savedPRA.Title, Is.EqualTo(expectedPRATitle ));
        }

        [Test]
        public void Given_checklist_then_reference_of_the_PRA_Reference_is_created_from_the_employee_checklist_Reference()
        {
            //GIVEN
            var employeeChecklist = new EmployeeChecklist();
            employeeChecklist.Employee = new Employee() { Forename = "Sandor", Surname = "Clegane" };
            employeeChecklist.ReferencePrefix = "RefPrefix";
            employeeChecklist.ReferenceIncremental = 5;
            employeeChecklist.Checklist = new Checklist() { Title = "Checklist Title" };
            employeeChecklist.PersonalRiskAssessment = new PersonalRiskAssessment();

            _employeeChecklistRepository.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(employeeChecklist);

            PersonalRiskAssessment savedPRA = null;
            _personalRiskAssessmentRepo.Setup(x => x.Save(It.IsAny<PersonalRiskAssessment>()))
                .Callback<PersonalRiskAssessment>(pra => savedPRA = pra);

            var target = GetTarget();

            //when
            target.CreateRiskAssessmentFromChecklist(Guid.NewGuid(), Guid.NewGuid());
            
            //THEN
            Assert.That(savedPRA.Reference, Is.EqualTo(employeeChecklist.FriendlyReference));
        }

        [Test]
        public void Given_checklist_when_new_PRA_created_then_checklist_is_added_to_new_PRA()
        {
            //GIVEN
            var employeeChecklist = new EmployeeChecklist();
            employeeChecklist.Employee = new Employee() { Forename = "Sandor", Surname = "Clegane" };
            employeeChecklist.ReferencePrefix = "RefPrefix";
            employeeChecklist.ReferenceIncremental = 5;
            employeeChecklist.Checklist = new Checklist() { Title = "Checklist Title" };
            employeeChecklist.PersonalRiskAssessment = new PersonalRiskAssessment();

            _employeeChecklistRepository.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(employeeChecklist);

            PersonalRiskAssessment savedPRA = null;
            _personalRiskAssessmentRepo.Setup(x => x.Save(It.IsAny<PersonalRiskAssessment>()))
                .Callback<PersonalRiskAssessment>(pra => savedPRA = pra);

            var target = GetTarget();

            //when
            target.CreateRiskAssessmentFromChecklist(Guid.NewGuid(), Guid.NewGuid());

            //THEN
            Assert.That(employeeChecklist.PersonalRiskAssessment, Is.EqualTo(savedPRA));
            Assert.That(savedPRA.EmployeeChecklists.First(), Is.EqualTo(employeeChecklist));

        }

        private PersonalRiskAssessmentService GetTarget()
        {

            return new PersonalRiskAssessmentService(_personalRiskAssessmentRepo.Object, _userRepo.Object
                                                     , _employeeRepository.Object
                                                     , _checklistRepository.Object
                                                     , _log.Object
                                                     , _riskAssessmentRepository.Object
                                                     , null
                                                     , null, _employeeChecklistRepository.Object);
        }

    }
}
