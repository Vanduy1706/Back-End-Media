using ErrorOr;
using Media.Application.User.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.User.Queries.GetFollowingList
{
    public class GetFollowingListQuery : IRequest<ErrorOr<List<FollowerResult>>>
    {
        public Guid UserId { get; set; }
    }
}
