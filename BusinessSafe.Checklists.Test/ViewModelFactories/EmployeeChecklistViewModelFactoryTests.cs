using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.Checklists;
using BusinessSafe.Checklists.ViewModelFactories;
using Moq;
using NUnit.Framework;


namespace BusinessSafe.Checklists.Test.ViewModelFactories
{
    [TestFixture]
    public class EmployeeChecklistViewModelFactoryTests
    {
        private Mock<IEmployeeChecklistService> _employeeChecklistService;
        private EmployeeChecklistDto _employeeChecklistDto;
        private EmployeeChecklistViewModelFactory _target;

        [SetUp]
        public void Setup()
        {
            _employeeChecklistDto = new EmployeeChecklistDto {Answers = new List<PersonalAnswerDto>(), Employee = new EmployeeDto(), Id = Guid.NewGuid()};
            _employeeChecklistDto.Checklist = new ChecklistDto
                                                  {
                                                      Sections = new List<SectionDto>()
                                                                     {
                                                                         new SectionDto
                                                                             {
                                                                                 Id = 1, Title = "3rd Section", ListOrder = 3
                                                                                 , Questions = new List<QuestionDto>()
                                                                                                   {
                                                                                                       new QuestionDto() {Id = 5, ListOrder = 3, Text = "question 3"}
                                                                                                       , new QuestionDto() {Id = 3, ListOrder = 1, Text = "question 1"}
                                                                                                       , new QuestionDto() {Id = 7, ListOrder = 2, Text = "question 2"}
                                                                                                   }
                                                                             }
                                                                         , new SectionDto {Id = 2, Title = "1st Section", ListOrder = 1, Questions = new List<QuestionDto>()}
                                                                         , new SectionDto {Id = 3, Title = "2nd Section", ListOrder = 2, Questions = new List<QuestionDto>()}
                                                                     }
                                                  };


            
            
            _employeeChecklistService = new Mock<IEmployeeChecklistService>();

            _target = new EmployeeChecklistViewModelFactory(_employeeChecklistService.Object);
            _employeeChecklistService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => _employeeChecklistDto);
        }

        [Test]
        public void GetViewModel_the_sections_are_ordered_by_list_order()
        {
            var orderedSections = _employeeChecklistDto.Checklist.Sections.OrderBy(x => x.ListOrder).ToList();

            var result =_target.GetViewModel();
            
            Assert.AreEqual(orderedSections[0].Title,result.Sections[0].Title);    
        }

        [Test]
        public void GetViewModel_the_questions_are_ordered_by_list_order()
        {
            var sectionToTest = "3rd Section";

            var orderedQuestions = _employeeChecklistDto.Checklist.Sections
                .Where(s => s.Title == sectionToTest)
                .SelectMany(s => s.Questions)
                .OrderBy(x => x.ListOrder).ToList();

            var result = _target.GetViewModel();

            Assert.AreEqual(orderedQuestions[0].Text, result.Sections.Where(s => s.Title == sectionToTest)
                                                          .SelectMany(s => s.Questions)
                                                          .ToList()[0].Text);
        }


    }
}
