using GymInnowise.Authorization.Logic.Dtos;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymInnowise.Authorization.Logic.Services
{
    public class LoginService
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly PasswordService _passwordService;
        private readonly JwtService _jwtService;

        public LoginService(IAccountsRepository accountsRepository, PasswordService passwordService, JwtService jwtService)
        {
            _accountsRepository = accountsRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<string>Login(AccountLoginDto loginDto)
        {
            var account = await _accountsRepository.GetAccountByEmail(loginDto.Email);

            if (!_passwordService.VerifyPassword(loginDto.Password, account.PasswordHash)){

                return null;
            }

            return _jwtService.GenerateJwtToken(account.PhoneNumber, account.Email);
        }

    }
}
