namespace EvaluationChecklist.Helpers
{
    public interface IDependencyFactory
    {
        T GetInstance<T>();
        T GetNamedInstance<T>(string name);
    }
}
