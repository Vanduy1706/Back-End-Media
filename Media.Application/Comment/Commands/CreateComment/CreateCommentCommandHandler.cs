using ErrorOr;
using Media.Application.Comment.Common;
using Media.Application.Interfaces.Persistence;
using Media.Domain.Common.Errors;
using Media.Domain.Models;
using MediatR;

namespace Media.Application.Comment.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ErrorOr<CommentResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public CreateCommentCommandHandler(IUserRepository userRepository, 
            IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task<ErrorOr<CommentResult>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserById(command.UserId);
            if(existingUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var existingPost = await _postRepository.GetPostById(command.PostId);
            if(existingPost == null)
            {
                return Errors.Post.NotFoundPost;
            }

            Comments newComment = new Comments()
            {
                CommentContent = command.CommentContent,
                CommentImageUrl = command.CommentImageUrl,
                CommentVideoUrl = command.CommentVideoUrl,
                CommentFileUrl = command.CommentFileUrl,
                PostId = command.PostId,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow
            };

            _commentRepository.Add(newComment);

            var commentResult = new CommentResult();

            return commentResult;
        }
    }
}
