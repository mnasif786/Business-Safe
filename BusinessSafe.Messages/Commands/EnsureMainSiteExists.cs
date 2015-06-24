using NServiceBus;

namespace BusinessSafe.Messages.Commands
{
    public class EnsureMainSiteExists : ICommand
    {
        public long ClientId { get; set; }
        public long MainPeninsulaSiteId { get; set; }
    }
}
