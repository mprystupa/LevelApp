using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto;
using LevelApp.DAL.Entities;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Operations.Core.User
{
    public class AddUserOperation : BaseOperation<UserDto, int>
    {
        public override async Task ExecuteValidated()
        {
            var newUser = new CoreUser()
            {

            };
            
            UnitOfWork.UserRepository.Insert(newUser);
            await base.ExecuteValidated();
        }
    }
}
