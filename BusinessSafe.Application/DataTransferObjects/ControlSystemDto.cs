namespace BusinessSafe.Application.DataTransferObjects
{
    public class ControlSystemDto
    {
        public virtual long Id { get; set; }
        public virtual string Description { get; set; }
        public virtual long DocumentLibraryId { get; set; }
    }
}
