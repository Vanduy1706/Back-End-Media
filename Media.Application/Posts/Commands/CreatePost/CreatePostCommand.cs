using ErrorOr;
using Media.Application.Posts.Common;
using MediatR;

namespace Media.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<ErrorOr<PostResult>>
    {
        public string PostContent { get; set; } = default!;

        public string PostImageUrl { get; set; } = default!;

        public string PostVideoUrl { get; set; } = default!;

        public string PostFileUrl { get; set; } = default!;

        public int PostTotalLikes { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
