namespace BusinessSafe.Application.DataTransferObjects
{
    public class AccidentRecordBodyPartDto
    {
        public long Id { get; set; } 
        public AccidentRecordDto AccidentRecord { get; set; }
        public BodyPartDto BodyPart { get; set; }
        public virtual string AdditionalInformation { get; set; }
    }
}
