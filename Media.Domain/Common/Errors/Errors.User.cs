using ErrorOr;

namespace Media.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicateAccountName => Error.Conflict(
                code: "User.DuplicateAccountName",
                description: "AccountName is already in use.");

            public static Error PasswordsNotMatching => Error.Validation(
                code: "User.PasswordsNotMatching",
                description: "Password and Confirm Password are not match.");
            
            public static Error UserNotFound=> Error.NotFound(
                code: "User.UserNotFound",
                description: "This user is not existing.");
        }
    }
}
