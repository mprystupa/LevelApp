using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LevelApp.BLL.Dto;
using LevelApp.BLL.Dto.Core.User;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.Crosscutting.Extensions;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.User;
using Microsoft.IdentityModel.Tokens;

namespace LevelApp.BLL.Operations.Core.User
{
    public class AuthenticateUserOperation : BaseUserOperation<UserLoginDto, string>
    {
        private AppUser User { get; set; }
        
        public override async Task GetData()
        {
            User = await Repository<IUserRepository>().GetDetailAsync(x => x.Email == Parameter.Email);
            await base.GetData();
        }

        public override async Task Validate()
        {
            ValidateUserPassword(User, Parameter.Password);

            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            var claims = GenerateUserClaims(User);
            OperationResult = BuildToken(claims);
            await base.ExecuteValidated();
        }

        private IEnumerable<Claim> GenerateUserClaims(DAL.Models.Core.AppUser entity)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, entity.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            return claims;
        }

        private string BuildToken(IEnumerable<Claim> claims)
        {
            var test = Configuration.GetSection("tokenManagement");
            var tokenManagementData = Configuration.GetTokenManagementData();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenManagementData.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(tokenManagementData.Issuer,
                tokenManagementData.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(tokenManagementData.AccessExpiration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}