

namespace Media.Contracts.Authentication
{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
