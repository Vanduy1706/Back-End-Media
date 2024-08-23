using ErrorOr;

namespace Media.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Reply
        {
            public static Error NotFound => Error.NotFound(
                code: "Reply.NotFoundResources",
                description: "No Reply here.");
        }
    }
}
