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
using static Media.Domain.Common.Errors.Errors;

namespace Media.Application.User.Commands
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ErrorOr<UpdateProfileResult>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateProfileCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<UpdateProfileResult>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var hasUser = await _userRepository.GetUserById(command.UserId);
            if (hasUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var profile = new Users
            {
                UserId = command.UserId,
                UserName = command.UserName,
                UserDescription = command.Decription,
                DateOfBirth = command.DateOfBirth,
                PhoneNumber = command.PhoneNumber,
                Address = command.Address,
                UserJob = command.Job,
                PersonalImageUrl = command.PersonalImage,
                BackgroundImageUrl = command.BackgroundImage
            };

            await _userRepository.UpdateProfile(profile);

            var hasResult = await _userRepository.GetUserById(command.UserId);

            var result = new UpdateProfileResult
            {
                UserName = hasResult.UserName,
                Decription = hasResult.UserDescription,
                DateOfBirth = hasResult.DateOfBirth,
                PhoneNumber = hasResult.PhoneNumber,
                Address = hasResult.Address,
                Job = hasResult.UserJob,
                PersonalImage = hasResult.PersonalImageUrl,
                BackgroundImage = hasResult.BackgroundImageUrl
            };

            return result;
        }
    }
}
