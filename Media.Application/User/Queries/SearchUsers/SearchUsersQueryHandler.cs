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

namespace Media.Application.User.Queries.SearchUsers
{
    public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, ErrorOr<List<CurrentUser>>>
    {
        private readonly IUserRepository _userRepository;

        public SearchUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<List<CurrentUser>>> Handle(SearchUsersQuery query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query.UserName))
            {
                return Errors.User.UserNotFound;
            }

            var users = await _userRepository.SearchUsersByName(query.UserName);
            
            var userList = new List<CurrentUser>();

            foreach (var user in users)
            {
                var userInfo = new CurrentUser()
                {
                    UserId = user.UserId, 
                    UserName = user.UserName,
                    Decription = user.UserDescription,
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Job = user.UserJob,
                    PersonalImage = user.PersonalImageUrl,
                    BackgroundImage = user.BackgroundImageUrl,
                    AccountName = user.AccountName,
                    AcctiveStatus = user.ActiveStatus,
                    UserRole = user.UserRole,
                    CreatedAt = user.CreatedAt,
                };

                userList.Add(userInfo);
            }

            return userList;
        }
    }
}
