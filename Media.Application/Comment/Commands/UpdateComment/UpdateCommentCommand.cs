using ErrorOr;
using Media.Application.Comment.Common;
using MediatR;

namespace Media.Application.Comment.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<ErrorOr<CommentResult>>
    {
        public string CommentId { get; set; } = default!;

        public string CommentContent { get; set; } = default!;

        public string CommentImageUrl { get; set; } = default!;

        public string CommentVideoUrl { get; set; } = default!;

        public string CommentFileUrl { get; set; } = default!;

        public string PostId { get; set; } = default!;
        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
