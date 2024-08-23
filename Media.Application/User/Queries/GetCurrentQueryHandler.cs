using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.User.Common;
using Media.Domain.Common.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.User.Queries
{
    public class GetCurrentQueryHandler : IRequestHandler<GetCurrentQuery, ErrorOr<CurrentUser>>
    {
        private readonly IUserRepository _userRepository;

        public GetCurrentQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<CurrentUser>> Handle(GetCurrentQuery query, CancellationToken cancellationToken)
        {
            var hasUser = await _userRepository.GetUserById(query.UserId);
            if (hasUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var currentUser = new CurrentUser()
            {
                UserId = hasUser.UserId,
                UserName = hasUser.UserName,
                Decription = hasUser.UserDescription,
                DateOfBirth = hasUser.DateOfBirth,
                PhoneNumber = hasUser.PhoneNumber,
                Address = hasUser.Address,
                Job = hasUser.UserJob,
                PersonalImage = hasUser.PersonalImageUrl,
                BackgroundImage = hasUser.BackgroundImageUrl,
                AccountName = hasUser.AccountName,
                AcctiveStatus = hasUser.ActiveStatus,
                UserRole = hasUser.UserRole,
                CreatedAt = hasUser.CreatedAt,
            };

            return currentUser;
        }
    }
}
