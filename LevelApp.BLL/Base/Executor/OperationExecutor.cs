using System;
using LevelApp.Crosscutting.Exceptions;
using System.Threading.Tasks;
using AutoMapper;
using LevelApp.Crosscutting.Services;
using LevelApp.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace LevelApp.BLL.Base.Executor
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserResolverService _userResolver;

        public OperationExecutor(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, IUserResolverService userResolver)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
            _userResolver = userResolver;
        }
        
        public async Task<TResult> Execute<TOperation, TParameter, TResult>(TParameter parameter) where TOperation : IBaseOperation<TParameter, TResult>
        {
            // Setup operation instance
            var operation = GetOperationInstance<TOperation>();
            operation.SetupOperation(_unitOfWork, _configuration, _mapper, parameter, _userResolver);

            // Operation pipeline
            try
            {
                await operation.GetData();
                await operation.Validate();

                await operation.ExecuteValidated();
                return operation.OperationResult;
            }
            catch (ApiException)
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
