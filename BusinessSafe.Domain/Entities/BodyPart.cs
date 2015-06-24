using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class BodyPart : Entity<long>
    {
        public static long UNKOWN_BODY_PART = 17;
        public virtual string Description { get; set; }
    }
}
