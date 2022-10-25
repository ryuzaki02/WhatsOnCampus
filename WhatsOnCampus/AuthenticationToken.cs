namespace WhatsOnCampus
{
    internal class AuthenticationToken
    {
        public string DisplayName { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}