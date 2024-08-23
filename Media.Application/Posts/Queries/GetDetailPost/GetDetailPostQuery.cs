using ErrorOr;
using Media.Application.Posts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Posts.Queries.GetDetailPost
{
    public class GetDetailPostQuery : IRequest<ErrorOr<PostResult>>
    {
        [Required]
        public string PostId { get; set; } = default!;
    }
}
