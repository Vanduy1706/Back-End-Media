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

namespace Media.Application.User.Commands.FollowUser
{
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, ErrorOr<FollowerResult>>
    {
        private readonly IUserRepository _userRepository;

        public FollowUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<FollowerResult>> Handle(FollowUserCommand request, CancellationToken cancellationToken)
        {
            var hasUser = await _userRepository.GetUserById(request.UserId);
            if (hasUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var hasFollower = await _userRepository.GetUserById(request.FollowedId);
            if (hasFollower == null)
            {
                return Errors.User.UserNotFound;
            }

            var follower = new Followers()
            {
                UserId = request.UserId,
                FollowedId = request.FollowedId,
                FollowingStatus = true,
                CreatedAt = DateTime.UtcNow,
            };

            var isFollowing = await _userRepository.GetFollower(follower);
            if (isFollowing != null)
            {
                return Errors.User.UserNotFound;
            }

            await _userRepository.Follow(follower);

            return new ErrorOr<FollowerResult>();
        }
    }
}
