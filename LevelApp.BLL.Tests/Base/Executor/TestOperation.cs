using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Tests.Base.Executor
{
    [ExcludeFromCodeCoverage]
    public class TestOperation : BaseOperation<int, int>
    {
        public override async Task Validate()
        {
            if (Parameter == 2)
            {
                Errors.Add("Validation failed.");
            }
            
            await base.Validate();
        }

        public override async Task ExecuteValidated()
        {
            if (Parameter == 1)
            {
                OperationResult = Parameter;
            }
            else if (Parameter == 3)
            {
                UnitOfWork.Save();
            }
            
            await base.ExecuteValidated();
        }
    }
}