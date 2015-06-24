namespace BusinessSafe.Application.DataTransferObjects
{
    public class PersonalAnswerDto : AnswerDto
    {
        public EmployeeChecklistDto EmployeeChecklist { get; set; }
        public bool? BooleanResponse { get; set; }
    }
}
