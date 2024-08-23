using ErrorOr;
using Media.Application.Authentication.Common;
using Media.Application.Interfaces.Persistence;
using Media.Domain.Common.Errors;
using Media.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Authentication.Commands.ForgetPass
{
    public class ForgetPassCommandHandler : IRequestHandler<ForgetPassCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;

        public ForgetPassCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(ForgetPassCommand request, CancellationToken cancellationToken)
        {
            var hasUser = await _userRepository.GetUserByAccountName(request.AccountName);
            if (hasUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var newPass = new Users()
            {
                AccountName = request.AccountName,
                AccountPassword = request.AccountPassword,
            };

            await _userRepository.ForgetPassWord(newPass);

            return new ErrorOr<AuthenticationResult>();
        }
    }
}
