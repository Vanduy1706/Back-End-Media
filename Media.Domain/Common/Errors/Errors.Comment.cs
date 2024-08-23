using ErrorOr;

namespace Media.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Comment
        {
            public static Error NotFoundComment => Error.NotFound(
                code: "Comment.NotFoundResources",
                description: "No comments here");
        }
    }
}
