using ErrorOr;
using Media.Application.User.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.User.Queries.SearchUsers
{
    public class SearchUsersQuery : IRequest<ErrorOr<List<CurrentUser>>>
    {
        public string UserName { get; set; } = default!;
    }
}
