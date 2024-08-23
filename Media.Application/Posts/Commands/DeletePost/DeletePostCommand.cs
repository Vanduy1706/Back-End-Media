using ErrorOr;
using Media.Application.Posts.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Media.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest<ErrorOr<PostResult>>
    {
        [Required]
        public string PostId { get; set; } = default!;
        [Required]
        public Guid UserId { get; set; }
    }
}
