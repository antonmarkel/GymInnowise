using FluentValidation;
using FluentValidation.Results;
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
using System.Text;

namespace GymInnowise.Authorization.Logic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountsRepository _accountsRepo;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IValidator<RegisterRequest> _registerValidator;
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IValidator<string> _refreshValidator;

        public AuthenticationService(
            IAccountsRepository accountsRepo,
            ITokenService jwtService,
            IRefreshTokenRepository refreshTokenRepository,
            IValidator<RegisterRequest> registerValidator,
            IValidator<LoginRequest> loginValidator,
            IValidator<string> refreshValidator)
        {
            _accountsRepo = accountsRepo;
            _tokenService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _refreshValidator = refreshValidator;
        }

        public async Task<OneOf<Success, AccountAlreadyExists, AccountValidationError>> RegisterAsync(
            RegisterRequest registerRequest)
        {
            var validationResult = await _registerValidator.ValidateAsync(registerRequest);
            if (!validationResult.IsValid)
            {
                return new AccountValidationError() { ErrorMessage = GetValidationErrors(validationResult) };
            }

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

        public async Task<OneOf<LoginResponse, InvalidCredentials, AccountValidationError>> LoginAsync(
            LoginRequest loginRequest)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginRequest);
            if (!validationResult.IsValid)
            {
                return new AccountValidationError() { ErrorMessage = GetValidationErrors(validationResult) };
            }

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
            var validationResult = await _refreshValidator.ValidateAsync(refreshRequest.RefreshToken);
            if (!validationResult.IsValid)
            {
                return new InvalidRefreshToken();
            }

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
            var validationResult = await _refreshValidator.ValidateAsync(revokeRequest.RefreshToken);
            if (!validationResult.IsValid)
            {
                return;
            }

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

        private string GetValidationErrors(ValidationResult result)
        {
            var validErrorsString = new StringBuilder();
            foreach (var error in result.Errors)
            {
                validErrorsString.AppendLine(error.ErrorMessage);
            }

            return validErrorsString.ToString();
        }
    }
}