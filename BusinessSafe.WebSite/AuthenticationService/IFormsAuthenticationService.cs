namespace BusinessSafe.WebSite.AuthenticationService
{
    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignIn(string userName, bool createPersistentCookie, string userData);
        string LoginUrl { get; }
        void RedirectToLoginPage();
        void RedirectToLoginPage(string extraQueryString);
        string DefaultUrl { get; }
        void SignOut();
    }
}