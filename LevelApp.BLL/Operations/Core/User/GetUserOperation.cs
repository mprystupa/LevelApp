using System.Threading.Tasks;
using LevelApp.BLL.Dto;
using LevelApp.BLL.Dto.Core.User;
using LevelApp.DAL.Repositories.User;

namespace LevelApp.BLL.Operations.Core.User
{
    public class GetUserOperation : BaseUserOperation<int, UserSearchDto>
    {
        public override async Task ExecuteValidated()
        {
            var user = await Repository<IUserRepository>().GetDetailAsync(x => x.Id == Parameter);
            OperationResult = Mapper.Map<UserSearchDto>(user);
            
            await base.ExecuteValidated();
        }
    }
}