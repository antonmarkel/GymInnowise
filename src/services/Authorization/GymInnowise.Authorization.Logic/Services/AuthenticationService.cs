﻿using GymInnowise.Authorization.Logic.Helpers;
using GymInnowise.Authorization.Logic.Interfaces;
using GymInnowise.Authorization.Logic.Results;
using GymInnowise.Authorization.Persistence.Models.Entities;
using GymInnowise.Authorization.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Authorization.Dtos.RequestModels;
using GymInnowise.Shared.Authorization.Dtos.ResponseModels;
using Microsoft.Extensions.Logging;
using OneOf;

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

        public async Task<OneOf<LoginResponse, InvalidCredentials>> LoginAsync(
            LoginRequest loginRequest)
        {
            var account = await _accountsRepo.GetAccountByEmailAsync(
                loginRequest.Email);
            if (account is null || !PasswordHelper.VerifyPassword(loginRequest.Password, account.PasswordHash))
            {
                _logger.LogWarning("Logging in failed. Reason: '"
                                   + (account is null
                                       ? "Account does not exist"
                                       : "Wrong Password")
                                   + $"'email: {loginRequest.Email}.");

                return new InvalidCredentials();
            }

            (string accessToken, string refreshToken) = await GeneratePairOfTokens(account);
            _logger.LogInformation("An account with email '{Email}' has just logged in.", account.Email);

            return new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken, };
        }

        public async Task<OneOf<RefreshResponse, InvalidRefreshToken>> RefreshAsync(RefreshRequest refreshRequest)
        {
            var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(
                refreshRequest.RefreshToken, loadAccount: true);
            if (storedRefreshToken is null)
            {
                _logger.LogWarning("Refresh token is invalid.");

                return new InvalidRefreshToken();
            }

            if (!_tokenService.ValidateRefreshToken(storedRefreshToken))
            {
                _logger.LogInformation("Refresh token is invalid. User: {@Email}", storedRefreshToken.Account!.Email);
                await _refreshTokenRepository.DeleteRefreshTokenAsync(storedRefreshToken);

                return new InvalidRefreshToken();
            }

            (string accessToken, string refreshToken) = await GeneratePairOfTokens(
                storedRefreshToken.Account!);
            var response = new RefreshResponse() { RefreshToken = refreshToken, AccessToken = accessToken, };
            await _refreshTokenRepository.DeleteRefreshTokenAsync(storedRefreshToken);
            _logger.LogInformation("Refreshing has been done successfully. User: '{@Email}'.",
                storedRefreshToken.Account!.Email);

            return response;
        }

        public async Task RevokeAsync(RevokeRequest revokeRequest)
        {
            var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(
                revokeRequest.RefreshToken, loadAccount: true);
            if (storedRefreshToken is null)
            {
                _logger.LogWarning("Refresh token is invalid.");

                return;
            }

            _logger.LogInformation("Refresh token has been revoked. User: '{@Email}'.",
                storedRefreshToken.Account!.Email);
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