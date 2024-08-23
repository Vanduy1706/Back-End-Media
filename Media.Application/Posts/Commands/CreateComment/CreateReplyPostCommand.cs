using ErrorOr;
using Media.Application.Posts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Posts.Commands.CreateComment
{
    public class CreateReplyPostCommand : IRequest<ErrorOr<PostResult>>
    {
        public string PostContent { get; set; } = default!;

        public string PostImageUrl { get; set; } = default!;

        public string PostVideoUrl { get; set; } = default!;

        public string PostFileUrl { get; set; } = default!;

        public int PostTotalLikes { get; set; }

        public Guid UserId { get; set; }
        [Required]
        public string ReplyId { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
