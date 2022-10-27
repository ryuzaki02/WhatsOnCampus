namespace WhatsOnCampus
{
    /// <summary>
    /// This class is responsible to maintain authentication token and the user information related attributes
    /// </summary>
    internal class AuthenticationToken
    {
        public string DisplayName { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}