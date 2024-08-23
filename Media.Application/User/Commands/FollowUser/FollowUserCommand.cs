using ErrorOr;
using Media.Application.User.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.User.Commands.FollowUser
{
    public class FollowUserCommand : IRequest<ErrorOr<FollowerResult>>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid FollowedId { get; set; }
    }
}
