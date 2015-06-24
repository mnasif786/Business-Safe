namespace BusinessSafe.Application.Contracts
{
    public interface ITemplateEngine
    {
        string Render<TModel>(TModel model, string template);
    }
}
