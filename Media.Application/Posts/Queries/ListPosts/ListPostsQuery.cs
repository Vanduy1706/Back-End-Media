using ErrorOr;
using Media.Application.Posts.Common;
using MediatR;


namespace Media.Application.Posts.Queries.ListPosts
{
    public class ListPostsQuery : IRequest<ErrorOr<List<PostResult>>>
    {

    }
}
