namespace BusinessSafe.Domain.Specification
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
    }
}
