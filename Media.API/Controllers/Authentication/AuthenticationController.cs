using ErrorOr;
using Media.Application.Authentication.Commands.Register;
using Media.Application.Authentication.Queries.Login;
using Media.Application.Authentication.Common;
using Media.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Media.Application.Authentication.Commands.ForgetPass;
using Microsoft.AspNetCore.SignalR;
using Media.API.NotificationHub;

namespace Media.API.Controllers.Authentication
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IHubContext<LoginHub> _hubContext;

        public AuthenticationController(IMediator mediator, IHubContext<LoginHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery();
                query.AccountName = request.AccountName;
                query.AccountPassword = request.AccountPassword;

            var authResult = await _mediator.Send(query);

            return authResult.Match(
                authResult =>
                {
                    _hubContext.Clients.All.SendAsync("RecieveLoginStatus", "Đã đăng nhập");
                    return Ok(ResponseAuthResult(authResult));
                },
                errors => Problem(errors)
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand();
            command.UserName = request.UserName;
            command.AccountName = request.AccountName;
            command.AccountPassword = request.AccountPassword;
            command.ConfirmAccountPassword = request.ConfirmAccountPassword;

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(
                authResult => Ok(ResponseAuthResult(authResult)),
                errors => Problem(errors)
            );
        }

        [HttpPatch("password")]
        public async Task<IActionResult> ForgetPass(LoginRequest request)
        {
            var command = new ForgetPassCommand()
            {
                AccountName = request.AccountName,
                AccountPassword = request.AccountPassword
            };

            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(),
                errors => Problem(errors));
        }

        private static AuthenticationResponse ResponseAuthResult(AuthenticationResult authResult)
        {
            var response = new AuthenticationResponse();
            response.Id = authResult.Id;
            response.UserName = authResult.UserName;
            response.AccountName = authResult.AccountName;
            response.Token = authResult.Token;
            return response;
        }
    }
}
