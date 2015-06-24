using NServiceBus;
using Peninsula.Online.Messages.Events;

namespace BusinessSafe.MessageHandlers.Core
{
    //public class SubscriberEndpoint : IWantToRunAtStartup
    //{
    //    private readonly IBus _bus;

    //    public SubscriberEndpoint(IBus bus)
    //    {
    //        _bus = bus;
    //    }

    //    public void Run()
    //    {
    //        _bus.Subscribe<RegistrationCompleted>();
    //        Log4NetHelper.Log.Info("SubscriberEndpoint Bus.Subscribe RegistrationCompleted");
    //    }

    //    public void Stop()
    //    {
    //        _bus.Unsubscribe<RegistrationCompleted>();
    //        Log4NetHelper.Log.Info("SubscriberEndpoint Bus.Unsubscribe RegistrationCompleted");
    //    }
    //}
}
