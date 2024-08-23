using ErrorOr;
using Media.Application.User.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.User.Queries
{
    public class GetCurrentQuery : IRequest<ErrorOr<CurrentUser>>
    {
        public Guid UserId { get; set; }
    }
}
