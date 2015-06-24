namespace BusinessSafe.Infrastructure.Security
{
    /// <summary>
    /// Interface for the UserLoginProvider, which supplies the name of the user currently logged in.
    /// </summary>
    /// <remarks>
    /// Author  :   Paul Davies
    /// Date    :   2nd April 2011
    /// </remarks>
    public interface IUserLoginProvider
    {
        /// <summary>
        /// Gets the login name of the current person accessing the application.
        /// </summary>
        /// <returns>Current user login.</returns>
        string GetUserLogin();
    }
}
