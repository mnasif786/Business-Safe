using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class PictogramDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public HazardousSubstanceStandard HazardousSubstanceStandard { get; set; }
        public string CssClassName { get { return Title.ToLower().Replace(" ", "-"); } }
    }
}