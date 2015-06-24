using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class AccidentRecordNextStepSectionDto
    {
        public long Id { get; set; }
        public AccidentRecordDto AccidentRecord { get; set; }
        public NextStepsSectionEnum NextStepsSection { get; set; }
    }
}
