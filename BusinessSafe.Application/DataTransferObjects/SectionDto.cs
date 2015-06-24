using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class SectionDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public int ListOrder { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }
    }
}
