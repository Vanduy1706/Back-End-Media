using ErrorOr;
using Media.Application.Reply.Common;
using MediatR;

namespace Media.Application.Reply.Queries.GetRepliesByComment
{
    public class GetRepliesByCommentQuery : IRequest<ErrorOr<List<ReplyResult>>>
    {
        public string CommentId { get; set; } = default!;
    }
}
