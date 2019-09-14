using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelApp.BLL.Exceptions;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Base.Executor
{
    public class OperationExecutor : IOperationExecutor
    {
        private IServiceProvider ServiceProvider { get; set; }
        
        public OperationExecutor(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        
        public async Task<TResult> Execute<TOperation, TParameter, TResult>(TParameter parameter) where TOperation : IBaseOperation<TParameter, TResult>
        {
            // Setup operation instance
            var operation = GetOperationInstance<TOperation, TParameter>(parameter);
            var unitOfWork = (IUnitOfWork)ServiceProvider.GetService(typeof(IUnitOfWork));
            operation.SetupOperation(unitOfWork, parameter);

            // Operation pipeline
            try
            {
                var validationResult = await operation.Validate();
                if (!validationResult) throw new ValidationException(operation.Errors.Last());
            }
            catch (Exception ex)
            {
                throw new GeneralServerException(ex.Message);
            }

            await operation.ExecuteValidated();
            return operation.OperationResult;
        }

        private TOperation GetOperationInstance<TOperation, TParameter>(TParameter operationParameter)
        {
            return Activator.CreateInstance<TOperation>();
        }
    }
}
