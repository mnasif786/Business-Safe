using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Messages.Commands;
using NServiceBus;

namespace BusinessSafe.MessageHandlers.CommandHandlers
{
    public class EnsureMainSiteExistsHandler : IHandleMessages<EnsureMainSiteExists>
    {
        private readonly ISiteService _siteService;
        public EnsureMainSiteExistsHandler(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public void Handle(EnsureMainSiteExists message)
        {
            _siteService.CreateMainSite(message.ClientId, message.MainPeninsulaSiteId);

            Log4NetHelper.Log.Info(string.Format("EnsureMainSiteExists Command Handled For ClientId : {0} And MainSiteId : {1}", message.ClientId, message.ClientId));
        }
    }
}
