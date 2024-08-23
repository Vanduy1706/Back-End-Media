using ErrorOr;

namespace Media.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authorization
        {
            public static Error UnAuthorized => Error.Unauthorized(
                code: "Auth.Unthorized",
                description: "Cant access this resources");
        }
    }
}
