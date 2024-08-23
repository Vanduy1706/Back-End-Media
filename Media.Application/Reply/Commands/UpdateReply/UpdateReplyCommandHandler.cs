using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Reply.Common;
using Media.Domain.Common.Errors;
using Media.Domain.Models;
using MediatR;

namespace Media.Application.Reply.Commands.UpdateReply
{
    public class UpdateReplyCommandHandler : IRequestHandler<UpdateReplyCommand, ErrorOr<ReplyResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IReplyRepository _replyRepository;
        private readonly ICommentRepository _commentRepository;

        public UpdateReplyCommandHandler(
            IUserRepository userRepository, IReplyRepository replyRepository, ICommentRepository commentRepository)
        {
            _userRepository = userRepository;
            _replyRepository = replyRepository;
            _commentRepository = commentRepository;
        }

        public async Task<ErrorOr<ReplyResult>> Handle(UpdateReplyCommand command, CancellationToken cancellationToken)
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

            var existingReply = await _replyRepository.GetReplyById(command.ReplyId);
            if (existingReply == null)
            {
                return Errors.Reply.NotFound;
            }

            if(existingReply.UserId != command.UserId)
            {
                return Errors.Authorization.UnAuthorized;
            }

            Replies newUpdateReply = new Replies()
            {
                ReplyId = command.ReplyId,
                ReplyContent = command.ReplyContent,
                ReplyImageUrl = command.ReplyImageUrl,
                ReplyVideoUrl = command.ReplyVideoUrl,
                ReplyFileUrl = command.ReplyFileUrl,
                UserId = command.UserId,
                CommentId = command.CommentId,
                CreatedAt = DateTime.UtcNow,
            };

            var result = await _replyRepository.UpdateReply(newUpdateReply);

            ReplyResult updateResult = new ReplyResult()
            {
                ReplyId = result.ReplyId,
                ReplyContent = result.ReplyContent,
                ReplyImageUrl = result.ReplyImageUrl,
                ReplyVideoUrl = result.ReplyVideoUrl,
                ReplyFileUrl = result.ReplyFileUrl,
                UserId = result.UserId,
                CommentId = result.CommentId,
                CreatedAt = result.CreatedAt,
            };

            return updateResult;
        }
    }
}
