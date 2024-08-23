using ErrorOr;
using Media.Application.Authentication.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Media.Application.Authentication.Queries.Login
{
    public class LoginQuery : IRequest<ErrorOr<AuthenticationResult>>
    {
        [Required(ErrorMessage = "AccountName is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "AccountName must be between 3 and 50 characters")]
        public string AccountName { get; set; } = default!;

        [Required(ErrorMessage = "AccountPassword is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "AccountPassword must be between 6 and 100 characters")]
        [DataType(DataType.Password)]
        public string AccountPassword { get; set; } = default!;
    }
}
