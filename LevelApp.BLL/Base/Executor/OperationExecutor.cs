using System;
using LevelApp.Crosscutting.Exceptions;
using System.Threading.Tasks;
using AutoMapper;
using LevelApp.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace LevelApp.BLL.Base.Executor
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        
        public OperationExecutor(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }
        
        public async Task<TResult> Execute<TOperation, TParameter, TResult>(TParameter parameter) where TOperation : IBaseOperation<TParameter, TResult>
        {
            // Setup operation instance
            var operation = GetOperationInstance<TOperation>();
            operation.SetupOperation(_unitOfWork, _configuration, _mapper, parameter);

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
