using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Logic.Services
{
    public class PasswordService
    {
        public string HashPassword(string password) =>
         BCrypt.Net.BCrypt.HashPassword(password);
        public bool VerifyPassword(string password, string passwordHash) =>
            BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
