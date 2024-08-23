using ErrorOr;
using Media.Application.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Authentication.Commands.ForgetPass
{
    public class ForgetPassCommand : IRequest<ErrorOr<AuthenticationResult>>
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
