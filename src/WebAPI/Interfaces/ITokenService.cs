using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
