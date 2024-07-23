using GymInnowise.Authorization.Logic.Helpers;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Logic.Services.Results;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;
using GymInnowise.Authorization.Shared.Dtos.ResponseModels;
using GymInnowise.Authorization.Shared.Enums;
using OneOf;
using OneOf.Types;

namespace GymInnowise.Authorization.Logic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountsRepository _accountsRepo;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationService(
            IAccountsRepository accountsRepo,
            ITokenService jwtService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _accountsRepo = accountsRepo;
            _tokenService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<OneOf<Success, AccountAlreadyExists>> RegisterAsync(
            RegisterRequest registerRequest)
        {
            if (await _accountsRepo.DoesAccountExistAsync(registerRequest))
            {
                return new AccountAlreadyExists();
            }

            var account = new AccountEntity
            {
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                PasswordHash = PasswordHelper.HashPassword(registerRequest.Password),
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Roles = [RoleEnum.Client],
            };
            await _accountsRepo.CreateAccountAsync(account);

            return new Success();
        }

        public async Task<OneOf<LoginResponse, InvalidCredentials>> LoginAsync(
            LoginRequest loginRequest)
        {
            var account = await _accountsRepo.GetAccountByEmailAsync(
                loginRequest.Email);
            if (account is null || !PasswordHelper.VerifyPassword(loginRequest.Password, account.PasswordHash))
            {
                return new InvalidCredentials();
            }

            (string accessToken, string refreshToken) = await GeneratePairOfTokens(account);

            return new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken, };
        }

        public async Task<OneOf<RefreshResponse, InvalidRefreshToken>> RefreshAsync(RefreshRequest refreshRequest)
        {
            var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(
                refreshRequest.RefreshToken, loadAccount: true);
            if (storedRefreshToken is null)
            {
                return new InvalidRefreshToken();
            }

            if (!_tokenService.ValidateRefreshToken(storedRefreshToken))
            {
                await _refreshTokenRepository.DeleteRefreshTokenAsync(storedRefreshToken);

                return new InvalidRefreshToken();
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