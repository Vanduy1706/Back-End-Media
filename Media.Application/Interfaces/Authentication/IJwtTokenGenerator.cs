using Media.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Users user);
    }
}
