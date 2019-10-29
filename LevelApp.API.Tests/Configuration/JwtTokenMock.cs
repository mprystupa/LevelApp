using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace LevelApp.API.Tests.Configuration
{
    public static class JwtTokenMock
    {
        public static string Issuer { get; } = Guid.NewGuid().ToString();
        public static SecurityKey SecurityKey { get; }
        public static SigningCredentials SigningCredentials { get; }

        private static readonly JwtSecurityTokenHandler STokenHandler = new JwtSecurityTokenHandler();
        private static readonly RandomNumberGenerator SRng = RandomNumberGenerator.Create();
        private static readonly byte[] SKey = new byte[32];

        static JwtTokenMock()
        {
            SRng.GetBytes(SKey);
            SecurityKey = new SymmetricSecurityKey(SKey) { KeyId = Guid.NewGuid().ToString() };
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        }

        public static string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            return STokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, claims, null, DateTime.UtcNow.AddMinutes(20), SigningCredentials));
        }
    }
}