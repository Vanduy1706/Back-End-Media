using ErrorOr;
using Media.Application.Reply.Common;
using MediatR;

namespace Media.Application.Reply.Commands.CreateReply
{
    public class CreateReplyCommand : IRequest<ErrorOr<ReplyResult>>
    {
        public string ReplyContent { get; set; } = default!;

        public string ReplyImageUrl { get; set; } = default!;

        public string ReplyVideoUrl { get; set; } = default!;

        public string ReplyFileUrl { get; set; } = default!;

        public Guid UserId { get; set; }

        public string CommentId { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
