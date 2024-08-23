using ErrorOr;
using Media.Application.Comment.Common;
using MediatR;

namespace Media.Application.Comment.Queries.GetCommentsByPost
{
    public class GetCommentByPostQuery : IRequest<ErrorOr<List<CommentResult>>>
    {
        public string PostId { get; set; } = default!;
    }
}
