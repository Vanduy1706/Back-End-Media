using ErrorOr;

namespace Media.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Post
        {
            public static Error NotFoundPost => Error.NotFound(
                code: "Post.NotFoundResources",
                description: "No post");
        }
    }
}
