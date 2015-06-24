using BusinessSafe.WebSite.Areas.Sites.ViewModels;
namespace BusinessSafe.WebSite.ServiceCompositionGateways
{
    public interface ISiteUpdateCompositionGateway
    {
        bool SendEmailIfRequired(SiteDetailsViewModel viewModel);
    }
}