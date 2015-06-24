using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class ActionTaskDto : TaskDto
    {
        public ActionDto Action { get; set; }
    }
}