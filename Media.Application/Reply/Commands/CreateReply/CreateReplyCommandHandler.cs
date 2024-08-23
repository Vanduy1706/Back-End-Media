using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Reply.Common;
using Media.Domain.Common.Errors;
using Media.Domain.Models;
using MediatR;

namespace Media.Application.Reply.Commands.CreateReply
{
    public class CreateReplyCommandHandler : IRequestHandler<CreateReplyCommand, ErrorOr<ReplyResult>>
    {
        private readonly IReplyRepository _replyRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        public CreateReplyCommandHandler(
            IReplyRepository replyRepository, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _replyRepository = replyRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public async Task<ErrorOr<ReplyResult>> Handle(CreateReplyCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserById(command.UserId);
            if (existingUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var existingComment = await _commentRepository.GetCommentById(command.CommentId);
            if (existingComment == null)
            {
                return Errors.Comment.NotFoundComment;
            }

            var newReply = new Replies()
            {
                ReplyContent = command.ReplyContent,
                ReplyImageUrl = command.ReplyImageUrl,
                ReplyVideoUrl = command.ReplyVideoUrl,
                ReplyFileUrl = command.ReplyFileUrl,
                UserId = command.UserId,
                CommentId = command.CommentId,
                CreatedAt = DateTime.UtcNow,
            };

            _replyRepository.Add(newReply);

            var result = new ReplyResult();
            return result;
        }
    }
}
