using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Reply.Common;
using Media.Domain.Common.Errors;
using MediatR;

namespace Media.Application.Reply.Commands.DeleteReply
{
    public class DeleteReplyCommandHandler : IRequestHandler<DeleteReplyCommand, ErrorOr<ReplyResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IReplyRepository _replyRepository;

        public DeleteReplyCommandHandler(IUserRepository userRepository, IReplyRepository replyRepository)
        {
            _userRepository = userRepository;
            _replyRepository = replyRepository;
        }

        public async Task<ErrorOr<ReplyResult>> Handle(DeleteReplyCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserById(Guid.Parse(command.UserId));
            if (existingUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var existingReply = await _replyRepository.GetReplyById(command.ReplyId);
            if (existingReply == null)
            {
                return Errors.Reply.NotFound;
            }

            if(existingReply.UserId != Guid.Parse(command.UserId))
            {
                return Errors.Authorization.UnAuthorized;
            }

            await _replyRepository.DeleteReply(command.ReplyId, Guid.Parse(command.UserId));

            var result = new ReplyResult();

            return result;
        }
    }
}
