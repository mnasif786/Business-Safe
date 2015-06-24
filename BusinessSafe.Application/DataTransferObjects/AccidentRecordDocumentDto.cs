namespace BusinessSafe.Application.DataTransferObjects
{
    public class AccidentRecordDocumentDto : DocumentDto
    {
        public virtual AccidentRecordDto AccidentRecord { get; set; }
        public long DocumentId { get; set; }
    }
}
