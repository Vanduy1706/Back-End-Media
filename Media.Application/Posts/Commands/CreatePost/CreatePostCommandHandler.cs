using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Posts.Common;
using Media.Domain.Common.Errors;
using MediatR;

namespace Media.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ErrorOr<PostResult>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public CreatePostCommandHandler(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<PostResult>> Handle(CreatePostCommand command, CancellationToken cancellationToken)
        {
            var hasUser = await _userRepository.GetUserById(command.UserId);
            if (hasUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var post = new Media.Domain.Models.Posts
            {
                PostContent = command.PostContent,
                PostImageUrl = command.PostImageUrl,
                PostVideoUrl = command.PostVideoUrl,
                PostFileUrl = command.PostFileUrl,
                PostTotalLikes = 0,
                PostTotalComments = 0,
                PostTotalShares = 0,
                PostTotalMarks = 0,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow,
            };

            _postRepository.Add(post);

            var result = new PostResult
            {
                PostId = post.PostId,
                PostContent = post.PostContent,
                PostImageUrl = post.PostImageUrl,
                PostVideoUrl = post.PostVideoUrl,
                PostFileUrl = post.PostFileUrl,
                PostTotalLikes = post.PostTotalLikes,
                PostTotalComments = post.PostTotalComments,
                PostTotalShares = post.PostTotalShares,
                PostTotalMarks = post.PostTotalMarks,
                UserId = post.UserId,
                ReplyId = post.ReplyId,
                CreatedAt = post.CreatedAt,
            };

            return result;
        }
    }
}
