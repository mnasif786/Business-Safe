namespace BusinessSafe.WebSite.ViewModels
{
    public class ErrorColumn
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public ErrorColumn(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }
}