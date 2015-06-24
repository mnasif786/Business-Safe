using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Checklists.ViewModels;

namespace BusinessSafe.Checklists.ViewModelFactories
{
    public class EmployeeChecklistViewModelFactory : IEmployeeChecklistViewModelFactory
    {
        private readonly IEmployeeChecklistService _employeeChecklistService;
        private Guid _employeeChecklistId;
        private bool _completedOnEmployeesBehalf;

        public EmployeeChecklistViewModelFactory(
            IEmployeeChecklistService employeeChecklistService)
        {
            _employeeChecklistService = employeeChecklistService;
        }

        public EmployeeChecklistViewModel GetViewModel()
        {
            var employeeChecklistViewModel = new EmployeeChecklistViewModel();
            var employeeChecklist = _employeeChecklistService.GetById(_employeeChecklistId);
            
            employeeChecklistViewModel.EmployeeChecklistId = _employeeChecklistId;
            employeeChecklistViewModel.CompletedOnEmployeesBehalf = _completedOnEmployeesBehalf;
            employeeChecklistViewModel.ChecklistTitle = employeeChecklist.Checklist.Title;
            employeeChecklistViewModel.ChecklistDescription = new HtmlString(employeeChecklist.Checklist.Description);
            employeeChecklistViewModel.FriendlyReference = employeeChecklist.FriendlyReference;
            employeeChecklistViewModel.IsCompleted = employeeChecklist.CompletedDate.HasValue;
            employeeChecklistViewModel.Sections = new List<SectionViewModel>();

            foreach (var section in employeeChecklist.Checklist.Sections.OrderBy(s => s.ListOrder))
            {
                var sectionViewModel = new SectionViewModel();
                sectionViewModel.Title = section.Title;
                sectionViewModel.Questions = new List<QuestionViewModel>();

                foreach(var question in section.Questions.OrderBy(q => q.ListOrder))
                {
                    var questionViewModel = new QuestionViewModel();
                    questionViewModel.Id = question.Id;
                    questionViewModel.ListOrder = question.ListOrder;
                    questionViewModel.Text = question.Text;
                    questionViewModel.QuestionType = question.QuestionType;
                    questionViewModel.ListOrder = question.ListOrder;
                    questionViewModel.Answer = GetAnswerForQuestion(question.Id, employeeChecklist.Answers);
                    sectionViewModel.Questions.Add(questionViewModel);

                }

                employeeChecklistViewModel.Sections.Add(sectionViewModel);
            }

            employeeChecklistViewModel.IsCompleted = employeeChecklist.CompletedDate != null;

            return employeeChecklistViewModel;
        }

        private AnswerViewModel GetAnswerForQuestion(long questionId, IEnumerable<PersonalAnswerDto> answers)
        {
            return
                answers
                .Where(x => x.Question.Id == questionId)
                .Select( x =>
                    new AnswerViewModel()
                        {
                            Id = x.Id, 
                            BooleanResponse = x.BooleanResponse, 
                            AdditionalInfo = x.AdditionalInfo
                        })
                .FirstOrDefault();
        }

        public IEmployeeChecklistViewModelFactory WithEmployeeChecklistId(Guid employeeChecklistId)
        {
            _employeeChecklistId = employeeChecklistId;
            return this;
        }

        public IEmployeeChecklistViewModelFactory WithCompletedOnEmployeesBehalf(bool completedOnEmployeesBehalf)
        {
            _completedOnEmployeesBehalf = completedOnEmployeesBehalf;
            return this;
        }
    }
}
