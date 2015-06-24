namespace BusinessSafe.WebSite.Contracts
{
    public interface IViewModelFactory<T>
    {
        T GetViewModel();
    }
}