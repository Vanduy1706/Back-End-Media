using ErrorOr;
using Media.Application.User.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.User.Commands
{
    public class UpdateProfileCommand : IRequest<ErrorOr<UpdateProfileResult>>
    {
        [Required]
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public string Decription { get; set; } = default!;
        public string DateOfBirth { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Job { get; set; } = default!;
        public string PersonalImage { get; set; } = default!;
        public string BackgroundImage { get; set; } = default!;
    }
}
