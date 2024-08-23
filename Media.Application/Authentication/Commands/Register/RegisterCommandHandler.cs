using ErrorOr;
using Media.Application.Interfaces.Authentication;
using Media.Application.Interfaces.Persistence;
using Media.Application.Authentication.Common;
using Media.Domain.Models;
using Media.Domain.Common.Errors;
using MediatR;

namespace Media.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var hasUser = await _userRepository.GetUserByAccountName(command.AccountName);
            if (hasUser != null)
            {
                return Errors.User.DuplicateAccountName;
            }

            if (command.AccountPassword != command.ConfirmAccountPassword)
            {
                return Errors.User.PasswordsNotMatching;
            }

            var user = new Users
            {
                UserName = command.UserName,
                AccountName = command.AccountName,
                AccountPassword = command.AccountPassword,
                PersonalImageUrl = "https://www.nicepng.com/png/full/128-1280406_user-icon-png.png",
                BackgroundImageUrl = "https://th.bing.com/th/id/R.55bfc1cdcc4b315b02f24b3c36e905d5?rik=4iXgkFNldTBA7A&pid=ImgRaw&r=0",
                UserDescription = "",
                Address = "",
                UserJob = "",
                DateOfBirth = "",
                PhoneNumber = "",
                ActiveStatus = false,
                AccessTimes = 0,
                UserRole = "user",
            };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            var result = new AuthenticationResult();
            result.Id = user.UserId;
            result.UserName = user.UserName;
            result.AccountName = user.AccountName;
            result.Token = token;

            return result;
        }
    }
}
