using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Contracts.User
{
    public class CurrentUserResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public string Decription { get; set; } = default!;
        public string DateOfBirth { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Job { get; set; } = default!;
        public string PersonalImage { get; set; } = default!;
        public string BackgroundImage { get; set; } = default!;
        public string AccountName { get; set; } = default!;
        public bool AcctiveStatus { get; set; } = default!;
        public string UserRole { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
    }
}
