namespace BusinessSafe.Application.DataTransferObjects
{
    public class EmailTemplateDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
