using System.Collections.Generic;

namespace BusinessSafe.WebSite.Factories
{
    public class SiteOrganisationalUnit
    {
        public int SiteId { get; set; }
        public List<SiteOrganisationalUnit> Children { get; set; }
        public string Name { get; set; }
        public bool IsMaster { get; set; }

        public SiteOrganisationalUnit()
        {
            Children = new List<SiteOrganisationalUnit>();
        }

    }
}