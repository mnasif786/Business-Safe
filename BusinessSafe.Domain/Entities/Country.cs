using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class Country : Entity<int>
    {
        public virtual string Name { get; protected set; }

        public Country() { }

        public Country(string name)
        {
            Name = name;
        }
    }
}