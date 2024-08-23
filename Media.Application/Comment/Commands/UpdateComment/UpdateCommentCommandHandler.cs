using ErrorOr;
using Media.Application.Comment.Common;
using Media.Application.Interfaces.Persistence;
using Media.Domain.Common.Errors;
using Media.Domain.Models;
using MediatR;

namespace Media.Application.Comment.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, ErrorOr<CommentResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public UpdateCommentCommandHandler(
            IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task<ErrorOr<CommentResult>> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserById(command.UserId);
            if (existingUser == null)
            {
                return Errors.User.UserNotFound;
            }
            
            var existingPost = await _postRepository.GetPostById(command.PostId);
            if (existingPost == null)
            {
                return Errors.Post.NotFoundPost;
            }

            var existingComment = await _commentRepository.GetCommentById(command.CommentId);
            if (existingComment == null)
            {
                return Errors.Comment.NotFoundComment;
            }

            if(existingPost.UserId != command.UserId)
            {
                return Errors.Authorization.UnAuthorized;
            }

            Comments newUpdateComment = new Comments()
            {
                CommentId = command.CommentId,
                CommentContent = command.CommentContent,
                CommentImageUrl = command.CommentImageUrl,
                CommentVideoUrl = command.CommentVideoUrl,
                CommentFileUrl = command.CommentFileUrl,
                PostId = command.PostId,
                UserId = command.UserId,
            };

            var result = await _commentRepository.UpdateComment(newUpdateComment);

            CommentResult updateResult = new CommentResult()
            {
                CommentId = command.CommentId,
                CommentContent = result.CommentContent,
                CommentImageUrl = result.CommentImageUrl,
                CommentVideoUrl = result.CommentVideoUrl,  
                CommentFileUrl = result.CommentFileUrl,
                PostId = command.PostId,
                UserId = command.UserId,
                CreatedAt = result.CreatedAt,
            };

            return updateResult;
        }
    }
}
