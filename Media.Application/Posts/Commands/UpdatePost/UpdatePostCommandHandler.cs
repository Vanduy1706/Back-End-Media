using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Posts.Common;
using Media.Domain.Common.Errors;
using MediatR;

namespace Media.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ErrorOr<PostResult>>
    {
        private readonly IPostRepository _postRepository;

        public UpdatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<ErrorOr<PostResult>> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
        {
            var post = new Media.Domain.Models.Posts
            {
                PostId = command.PostId,
                PostContent = command.PostContent,
                PostImageUrl = command.PostImageUrl,
                PostVideoUrl = command.PostVideoUrl,
                PostFileUrl = command.PostFileUrl,
                UserId = command.UserId,
                CreatedAt = DateTime.UtcNow,
            };

            var hasResult = await _postRepository.UpdatePostByUser(post);

            if (!hasResult)
            {
                return Errors.Authorization.UnAuthorized;
            }

            var result = new PostResult
            {
                PostId = post.PostId,
                PostContent = post.PostContent,
                PostImageUrl = post.PostImageUrl,
                PostVideoUrl = post.PostVideoUrl,
                PostFileUrl = post.PostFileUrl,
                CreatedAt = post.CreatedAt,
            };

            return result;
        }
    }
}
