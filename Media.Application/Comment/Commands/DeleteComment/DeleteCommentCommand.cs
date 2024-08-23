using MediatR;
using ErrorOr;
using Media.Application.Comment.Common;

namespace Media.Application.Comment.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<ErrorOr<CommentResult>>
    {
        public string CommentId { get; set; } = default!;
        public Guid UserId { get; set; }
    }
}
