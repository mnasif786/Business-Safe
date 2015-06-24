namespace BusinessSafe.Infrastructure.Request
{
    public interface IContextItems
    {
        object Get(string key);
        void Set(string key, object data);
        void Remove(string key);
    }
}
