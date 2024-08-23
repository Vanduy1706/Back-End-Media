using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Contracts.Follower
{
    public class FollowerRequest
    {
        public Guid UserId { get; set; }
        public Guid FollowedId { get; set; }
    }
}
