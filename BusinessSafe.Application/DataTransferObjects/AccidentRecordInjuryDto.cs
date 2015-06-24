namespace BusinessSafe.Application.DataTransferObjects
{
    public class AccidentRecordInjuryDto
{
    public long Id { get; set; } 
        public AccidentRecordDto AccidentRecord { get; set; }
        public InjuryDto Injury { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
