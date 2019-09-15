using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto;
using LevelApp.DAL.Entities;
using LevelApp.DAL.Repositories.User;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Operations.Core.User
{
    public class AddUserOperation : BaseOperation<UserDto, int>
    {
        public override Task Validate()
        {
            Errors.Add("Dupa");
            return base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            var newUser = new CoreUser()
            {

            };

            var repository = UnitOfWork.GetRepository<IUserRepository>();
            repository.Insert(newUser);
            await base.ExecuteValidated();
        }
    }
}
