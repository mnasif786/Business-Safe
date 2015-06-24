namespace BusinessSafe.Application.DataTransferObjects
{
    public class NonEmployeeDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string FormattedName { get; set; }
    }
}