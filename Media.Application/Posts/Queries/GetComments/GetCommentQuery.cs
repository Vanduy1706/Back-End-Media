using Amazon.Runtime.Internal;
using ErrorOr;
using Media.Application.Posts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Posts.Queries.GetComments
{
    public class GetCommentQuery : IRequest<ErrorOr<List<PostResult>>>
    {
        [Required]
        public string postId { get; set; } = default!;
    }
}
