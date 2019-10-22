using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelApp.BLL.Dto;
using LevelApp.BLL.Dto.Core.User;
using LevelApp.DAL.Repositories.User;

namespace LevelApp.BLL.Operations.Core.User
{
    public class GetAllUsersOperation : BaseUserOperation<int, List<UserSearchDto>>
    {
        public override async Task Validate()
        {
            var allUsers = await Repository<IUserRepository>().GetAllAsync();
            OperationResult = allUsers.Select(x => new UserSearchDto()
            {
                Id = x.Id,
                Email = x.Email
            }).ToList();
            
            await base.Validate();
        }
    }
}