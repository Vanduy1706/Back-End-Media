using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Contracts.User
{
    public class ProfileRespone
    {
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
