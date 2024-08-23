using ErrorOr;
using Media.Application.Interfaces.Authentication;
using Media.Application.Interfaces.Persistence;
using Media.Application.Authentication.Common;
using Media.Domain.Common.Errors;
using MediatR;

namespace Media.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByAccountName(query.AccountName);
            if (user == null)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            if (user.AccountPassword != query.AccountPassword)
            {
                return Errors.Authentication.InvalidCredentials;
            }

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
