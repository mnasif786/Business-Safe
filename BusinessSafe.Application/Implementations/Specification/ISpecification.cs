namespace BusinessSafe.Application.Implementations.Specification
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T request);
    }
}
