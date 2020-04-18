using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.Crosscutting.Services;
using LevelApp.DAL.Repositories.Base;
using LevelApp.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace LevelApp.BLL.Base.Operation
{
    public class BaseOperation<TParameter, TResult> : IBaseOperation<TParameter, TResult>
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IConfiguration Configuration { get; set; }
        public IMapper Mapper { get; set; }
        public IFileService FileService { get; set; }
        public TParameter Parameter { get; set; }
        public TResult OperationResult { get; set; }
        public Dictionary<string, HttpStatusCode> Errors { get; set; }
        
        public int CurrentUserId => UserResolver.GetUserId<int>();
        public string CurrentUserEmail => UserResolver.GetUserEmail();
        private IUserResolverService UserResolver { get; set; }

        protected BaseOperation()
        {
            Errors = new Dictionary<string, HttpStatusCode>();
        }

        public void SetupOperation(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, IFileService fileService, TParameter parameter, IUserResolverService userResolver)
        {
            UnitOfWork = unitOfWork;
            Configuration = configuration;
            Mapper = mapper;
            FileService = fileService;
            Parameter = parameter;

            UserResolver = userResolver;
        }

        public virtual Task GetData()
        {
            return Task.FromResult(false);
        }

        public virtual Task Validate()
        {
            if (!Errors.Any())
            {
                return Task.FromResult(false);
            }

            throw new BusinessValidationException(Errors.Last().Key, Errors.Last().Value);
        }

        public virtual Task ExecuteValidated()
        {
            return Task.FromResult(false);
        }

        public virtual Task AddFrontendPermissions()
        {
            return Task.FromResult(false);
        }

        protected TRepository Repository<TRepository>()
        {
            return UnitOfWork.GetRepository<TRepository>();
        }
    }
}
