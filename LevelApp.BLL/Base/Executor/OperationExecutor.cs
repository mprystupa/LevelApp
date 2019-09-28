using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LevelApp.Crosscutting.Exceptions;
using System.Threading.Tasks;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Base.Executor
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public OperationExecutor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<TResult> Execute<TOperation, TParameter, TResult>(TParameter parameter) where TOperation : IBaseOperation<TParameter, TResult>
        {
            // Setup operation instance
            var operation = GetOperationInstance<TOperation>();
            operation.SetupOperation(_unitOfWork, parameter);

            // Operation pipeline
            try
            {
                await operation.Validate();

                await operation.ExecuteValidated();
                return operation.OperationResult;
            }
            catch (BusinessValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GeneralServerException(ex.Message);
            }
        }

        private static TOperation GetOperationInstance<TOperation>()
        {
            return Activator.CreateInstance<TOperation>();
        }
    }
}
