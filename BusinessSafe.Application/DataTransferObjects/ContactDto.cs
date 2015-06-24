using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Application.DataTransferObjects
{
    [CoverageExclude]
    public class ContactDto
    {        
        public long Id { get; set; }       
        public string ContactName { get; set; }        
        public string Email { get; set; }
    }
}