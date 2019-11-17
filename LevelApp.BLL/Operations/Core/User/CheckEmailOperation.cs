using System.Threading.Tasks;
using LevelApp.DAL.Repositories.User;

namespace LevelApp.BLL.Operations.Core.User
{
    public class CheckEmailOperation : BaseUserOperation<string, bool>
    {
        public override async Task ExecuteValidated()
        {
            OperationResult = await Repository<IUserRepository>()
                .CheckIfExistsAsync(x => x.Email == Parameter);
            await base.ExecuteValidated();
        }
    }
}