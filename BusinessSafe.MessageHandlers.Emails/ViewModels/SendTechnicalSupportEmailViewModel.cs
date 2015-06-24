namespace BusinessSafe.MessageHandlers.Emails.ViewModels
{
    public class SendTechnicalSupportEmailViewModel
    {
        public string Name { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
    }
}