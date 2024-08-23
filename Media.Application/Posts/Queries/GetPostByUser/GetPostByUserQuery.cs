using ErrorOr;
using Media.Application.Posts.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Media.Application.Posts.Queries.GetPostByUser
{
    public class GetPostByUserQuery : IRequest<ErrorOr<List<PostResult>>>
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
