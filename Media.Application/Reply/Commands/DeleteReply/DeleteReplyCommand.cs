using ErrorOr;
using Media.Application.Reply.Common;
using MediatR;

namespace Media.Application.Reply.Commands.DeleteReply
{
    public class DeleteReplyCommand : IRequest<ErrorOr<ReplyResult>>
    {
        public string ReplyId { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}
