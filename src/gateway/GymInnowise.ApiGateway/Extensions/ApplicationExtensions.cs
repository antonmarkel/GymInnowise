﻿using System.Security.Claims;
using System.Text;
using GymInnowise.Shared.Configuration.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;

namespace GymInnowise.ApiGateway.Extensions
{
    public static class ApplicationExtensions
    {
        public static WebApplicationBuilder AddJwtServices(this WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtSettings);
            var key = Encoding.ASCII.GetBytes(jwtSettings.Get<JwtSettings>()!.SecretKey);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RoleClaimType = ClaimTypes.Role
                };
            });
            builder.Services.AddAuthorization();

            return builder;
        }

        public static WebApplicationBuilder AddOcelot(this WebApplicationBuilder builder)
        {
            var ocelotConfigFolder = Path.Combine(Directory.GetCurrentDirectory(), "Ocelot");
            foreach (var filePath in Directory.GetFiles(ocelotConfigFolder, "*.json"))
            {
                builder.Configuration.AddJsonFile(filePath, optional: false, reloadOnChange: true);
            }

            builder.Services.AddOcelot(builder.Configuration);

            return builder;
        }
    }
}
