using System.Globalization;
using BusinessSafe.Application.Contracts;

namespace BusinessSafe.Application.Common
{
    public class TemplateEngine : ITemplateEngine
    {
        public string Render<TModel>(TModel model, string template)
        {
            var templateResult = new Antlr4.StringTemplate.Template(template, '$', '$');
            templateResult.Add("Model", model);

            return templateResult.Render(CultureInfo.InvariantCulture);
        }
    }
}