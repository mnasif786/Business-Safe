using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class AccidentRecordNextStepSection : Entity<long>
    {
        public virtual AccidentRecord AccidentRecord { get; set; }
        public virtual NextStepsSectionEnum NextStepsSection { get; set; }
    }
}
