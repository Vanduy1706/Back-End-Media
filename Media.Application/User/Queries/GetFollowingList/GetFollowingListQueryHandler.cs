using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.User.Common;
using Media.Domain.Common.Errors;
using Media.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.User.Queries.GetFollowingList
{
    public class GetFollowingListQueryHandler : IRequestHandler<GetFollowingListQuery, ErrorOr<List<FollowerResult>>>
    {
        private readonly IUserRepository _userRepository;

        public GetFollowingListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<List<FollowerResult>>> Handle(GetFollowingListQuery query, CancellationToken cancellationToken)
        {
            var follower = new Followers()
            {
                UserId = query.UserId,
            };

            var followers = await _userRepository.GetFollowers(follower);
            if (followers == null)
            {
                return Errors.User.UserNotFound;
            }

            var followerList = new List<FollowerResult>();

            foreach (var following in followers)
            {
                var followed = await _userRepository.GetUserById(following.FollowedId);

                var follow = new FollowerResult()
                {
                    UserId = followed.UserId,
                    UserName = followed.UserName,
                    Decription = followed.UserDescription,
                    DateOfBirth = followed.DateOfBirth,
                    PhoneNumber = followed.PhoneNumber,
                    Address = followed.Address,
                    Job = followed.UserJob,
                    PersonalImage = followed.PersonalImageUrl,
                    BackgroundImage = followed.BackgroundImageUrl,
                    AccountName = followed.AccountName,
                    AcctiveStatus = followed.ActiveStatus,
                    UserRole = followed.UserRole,
                    CreatedAt = followed.CreatedAt,
                };

                followerList.Add(follow);
            }

            return followerList;
        }
    }
}
