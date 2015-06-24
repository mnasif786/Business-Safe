
using StructureMap;

namespace EvaluationChecklist.Helpers
{
    public class StructureMapDependencyFactory : IDependencyFactory
    {
        public T GetInstance<T>()
        {
            return ObjectFactory.Container.GetInstance<T>();
        }

        public T GetNamedInstance<T>(string name)
        {
            return ObjectFactory.GetNamedInstance<T>(name);
        }

    }
}