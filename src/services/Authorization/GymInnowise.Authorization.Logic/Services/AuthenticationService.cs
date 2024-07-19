using GymInnowise.Authorization.Logic.Helpers;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;
using GymInnowise.Authorization.Shared.Dtos.ResponseModels;
using GymInnowise.Authorization.Shared.Enums;

namespace GymInnowise.Authorization.Logic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountsRepository _accountsRepo;
        private readonly IRolesRepository _rolesRepo;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationService(
            IAccountsRepository accountsRepo,
            IRolesRepository rolesRepo,
            ITokenService jwtService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _accountsRepo = accountsRepo;
            _rolesRepo = rolesRepo;
            _tokenService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest accountRegistrationDto)
        {
            if (await _accountsRepo.DoesAccountExistAsync(accountRegistrationDto))
            {
                return new RegisterResponse();
            }

            var account = new AccountEntity
            {
                Email = accountRegistrationDto.Email,
                PhoneNumber = accountRegistrationDto.PhoneNumber,
                PasswordHash = PasswordHelper.HashPassword(accountRegistrationDto.Password),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Roles =
                [
                    await _rolesRepo.GetRoleAsync(RoleEnum.Client) ??
                    throw new InvalidOperationException("a standard role wasn't found"),
                ],
            };
            await _accountsRepo.CreateAccountAsync(account);

            return new RegisterResponse() { IsSuccessful = true, };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var account = await _accountsRepo.GetAccountByEmailAsync(loginRequest.Email, loadRoles: true);
            if (account is null || !PasswordHelper.VerifyPassword(loginRequest.Password, account.PasswordHash))
            {
                return new LoginResponse();
            }

            (string accessToken, string refreshToken) = await GeneratePairOfTokens(account);

            return new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken, };
        }

        public async Task<RefreshResponse> RefreshAsync(RefreshRequest refreshRequest)
        {
            var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(
                refreshRequest.RefreshToken, loadAccount: true);
            if (storedRefreshToken is null)
            {
                return new RefreshResponse();
            }

            if (!_tokenService.ValidateRefreshToken(storedRefreshToken))
            {
                await _refreshTokenRepository.DeleteRefreshTokenAsync(storedRefreshToken);

                return new RefreshResponse();
            }

            (string accessToken, string refreshToken) = await GeneratePairOfTokens(
                storedRefreshToken.Account!);
            await _refreshTokenRepository.DeleteRefreshTokenAsync(storedRefreshToken);

            return new RefreshResponse() { RefreshToken = refreshToken, AccessToken = accessToken, };
        }

        public async Task RevokeAsync(RevokeRequest revokeRequest)
        {
            var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(
                revokeRequest.RefreshToken);
            if (storedRefreshToken is null)
            {
                return;
            }

            await _refreshTokenRepository.DeleteRefreshTokenAsync(storedRefreshToken);
        }

        private async Task<(string accessToken, string refreshToken)> GeneratePairOfTokens(
            AccountEntity account)
        {
            var accessToken = _tokenService.GenerateJwtToken(account);
            var refreshToken = _tokenService.GenerateRefreshToken(account.Id);
            await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);

            return (accessToken, refreshToken.Token);
        }
    }
}