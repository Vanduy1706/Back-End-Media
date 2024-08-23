namespace Media.Application.Authentication.Common
{
    public class AuthenticationResult
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
