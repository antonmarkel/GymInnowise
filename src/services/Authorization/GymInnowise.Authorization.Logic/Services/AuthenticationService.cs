using GymInnowise.Authorization.Logic.Helpers;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Logic.Services.Results;
using GymInnowise.Authorization.Persistence.Models.Enities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Authorization.Shared.Dtos.RequestModels;
using GymInnowise.Authorization.Shared.Dtos.ResponseModels;
using GymInnowise.Authorization.Shared.Enums;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.Authorization.Logic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountsRepository _accountsRepo;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            IAccountsRepository accountsRepo,
            ITokenService jwtService,
            IRefreshTokenRepository refreshTokenRepository,
            ILogger<AuthenticationService> logger)
        {
            _accountsRepo = accountsRepo;
            _tokenService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
        }

        public async Task<OneOf<Success, AccountAlreadyExists>> RegisterAsync(
            RegisterRequest registerRequest)
        {
            _logger.LogDebug("Got a RegisterRequest model: {@registerRequest}", registerRequest);
            if (await _accountsRepo.DoesAccountExistAsync(registerRequest))
            {
                _logger.LogInformation("Registration failed. Reason: 'Account already exists'.");

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
            _logger.LogInformation("New account with email '{Email}' has been created.", account.Email);

            return new Success();
        }

        public async Task<OneOf<LoginResponse, InvalidCredentials>> LoginAsync(
            LoginRequest loginRequest)
        {
            _logger.LogDebug("Got a LoginRequest model: {@loginRequest}", loginRequest);
            var account = await _accountsRepo.GetAccountByEmailAsync(
                loginRequest.Email);
            if (account is null || !PasswordHelper.VerifyPassword(loginRequest.Password, account.PasswordHash))
            {
                _logger.LogInformation("Logging in failed. Reason: '"
                                       + (account is null ? "Account does not exist" : "Wrong Password")
                                       + "'.");

                return new InvalidCredentials();
            }

            (string accessToken, string refreshToken) = await GeneratePairOfTokens(account);
            _logger.LogInformation("An account with email '{Email}' has just logged in.", account.Email);

            return new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken, };
        }

        public async Task<OneOf<RefreshResponse, InvalidRefreshToken>> RefreshAsync(RefreshRequest refreshRequest)
        {
            _logger.LogDebug("Got a RefreshRequest model: {@refreshRequest}", refreshRequest);
            var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(
                refreshRequest.RefreshToken, loadAccount: true);
            if (storedRefreshToken is null)
            {
                _logger.LogInformation("Refresh token is invalid");

                return new InvalidRefreshToken();
            }

            if (!_tokenService.ValidateRefreshToken(storedRefreshToken))
            {
                _logger.LogInformation("Refresh token is invalid");
                await _refreshTokenRepository.DeleteRefreshTokenAsync(storedRefreshToken);

                return new InvalidRefreshToken();
            }

            (string accessToken, string refreshToken) = await GeneratePairOfTokens(
                storedRefreshToken.Account!);
            var response = new RefreshResponse() { RefreshToken = refreshToken, AccessToken = accessToken, };
            await _refreshTokenRepository.DeleteRefreshTokenAsync(storedRefreshToken);
            _logger.LogInformation("New tokens {@response} were created from: {@storedRefreshToken}.",
                response,
                storedRefreshToken);

            return response;
        }

        public async Task RevokeAsync(RevokeRequest revokeRequest)
        {
            _logger.LogDebug("Got a RevokeRequest model: {@revokeRequest}", revokeRequest);
            var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(
                revokeRequest.RefreshToken);
            if (storedRefreshToken is null)
            {
                _logger.LogInformation("Refresh token is invalid");

                return;
            }

            _logger.LogInformation("Refresh token {@storedRefreshToken} has been revoked.", storedRefreshToken);
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