using ErrorOr;
using Media.Application.Posts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Posts.Queries.GetLikers
{
    public class GetLikersQuery : IRequest<ErrorOr<LikerResult>>
    {
        public string PostId { get; set; } = default!;
        public Guid UserId { get; set; }
    }
}
