using ErrorOr;
using Media.Application.Comment.Common;
using Media.Application.Interfaces.Persistence;
using Media.Domain.Common.Errors;
using MediatR;

namespace Media.Application.Comment.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, ErrorOr<CommentResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        public DeleteCommentCommandHandler(IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public async Task<ErrorOr<CommentResult>> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
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

            if(existingComment.UserId != command.UserId)
            {
                return Errors.Authorization.UnAuthorized;
            }

            await _commentRepository.DeleteComment(command.CommentId, command.UserId);

            var result = new CommentResult();

            return result;
        }
    }
}
