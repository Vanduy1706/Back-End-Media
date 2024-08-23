using ErrorOr;
using Media.Application.Posts.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Media.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest<ErrorOr<PostResult>>
    {
        [Required]
        public string PostId { get; set; } = default!;
        public string PostContent { get; set; } = default!;

        public string PostImageUrl { get; set; } = default!;

        public string PostVideoUrl { get; set; } = default!;

        public string PostFileUrl { get; set; } = default!;

        [Required]
        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
