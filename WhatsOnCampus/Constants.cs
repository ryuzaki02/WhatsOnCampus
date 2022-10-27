using System;
namespace WhatsOnCampus
{
    /// <summary>
    /// This is a global static class to store the constants for the project
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The base URI for the Datasync service.
        /// </summary>
        public static string ServiceUri = "https://demo-datasync-quickstart.azurewebsites.net";

        /// <summary>
        /// The application (client) ID for the native app within Azure Active Directory
        /// </summary>
        public static string ApplicationId = "f9a5d166-2d8d-47e4-a96d-b01d1847bae9";

        /// <summary>
        /// The list of scopes to request
        /// </summary>
        public static string[] Scopes = new[]
        {
          "api://f9a5d166-2d8d-47e4-a96d-b01d1847bae9/access_as_user"
      };
    }
}

