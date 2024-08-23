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

namespace Media.Application.User.Commands.UnFollowUser
{
    public class UnFollowCommandHandler : IRequestHandler<UnFollowCommand, ErrorOr<FollowerResult>>
    {
        private readonly IUserRepository _userRepository;

        public UnFollowCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<FollowerResult>> Handle(UnFollowCommand request, CancellationToken cancellationToken)
        {
            var hasUser = await _userRepository.GetUserById(request.UserId);
            if(hasUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var hasFollwer = await _userRepository.GetUserById(request.FollowedId);
            if(hasFollwer == null)
            {
                return Errors.User.UserNotFound;
            }

            var follower = new Followers()
            {
                UserId = request.UserId,
                FollowedId = request.FollowedId,
            };

            var isFollowing = await _userRepository.GetFollower(follower);
            if(isFollowing == null)
            {
                return Errors.User.UserNotFound;
            }

            await _userRepository.UnFollow(follower);

            return new ErrorOr<FollowerResult>();
        }
    }
}
