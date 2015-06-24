namespace BusinessSafe.Domain.Common
{
    public class Entity<T> : BaseEntity<T>, IAuditable where T : struct
    {
        public virtual string IdForAuditing
        {
            get { return Id.ToString(); }
        }
    }
}
