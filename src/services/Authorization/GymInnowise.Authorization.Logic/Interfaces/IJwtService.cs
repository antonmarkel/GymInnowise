using GymInnowise.Authorization.Logic.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Logic.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(string phoneNumber, string email);
 
    }
}
