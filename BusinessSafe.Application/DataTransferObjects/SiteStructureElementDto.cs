using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    //todo: in future, this hould be an abstact class from which SiteDto and SiteGroupDto inherit (or an inteface they implement)
    public class SiteStructureElementDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ClientId { get; set; }
        public SiteStructureElementDto Parent { get; set; }
        public IList<SiteStructureElementDto> Children { get; set; }
        public bool Deleted { get; set; }
        public bool IsMainSite { get; set; }
        public IEnumerable<long> ChildIdsThatCannotBecomeParent { get; set; }
        public string SiteContact { get; set; }
        public bool SiteClosed { get; set; }

        public SiteStructureElementDto()
        {
            ChildIdsThatCannotBecomeParent = new List<long>();
        }
    }
}
