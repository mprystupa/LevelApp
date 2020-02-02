using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto;
using LevelApp.BLL.Dto.Core.User;
using LevelApp.BLL.Helpers;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.User;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Operations.Core.User
{
    public class RegisterUserOperation : BaseOperation<UserRegisterDto, int>
    {
        public override async Task Validate()
        {
            if (await Repository<IUserRepository>().CheckIfExistsAsync(x => x.Email == Parameter.Email))
            {
                Errors.Add("User with this e-mail already exists.", HttpStatusCode.Conflict);
            }
            
            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            var (passwordHash, passwordSalt) = CryptoHelper.GeneratePasswordHashData(Parameter.Password);
            var newUser = new AppUser()
            {
                Email = Parameter.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            
            Repository<IUserRepository>().Insert(newUser);
            await UnitOfWork.SaveAsync();
            OperationResult = newUser.Id;
            
            await base.ExecuteValidated();
        }
    }
}
