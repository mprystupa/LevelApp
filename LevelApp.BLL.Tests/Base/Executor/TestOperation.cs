using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Tests.Base.Executor
{
    public class TestOperation : BaseOperation<int, int>
    {
        public override async Task<bool> Validate()
        {
            if (Parameter == 1)
            {
                Errors.Add("Validation failed.");
            }
            
            return await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            if (Parameter == 5)
            {
                OperationResult = Parameter;
            }
            
            await base.ExecuteValidated();
        }
    }
}