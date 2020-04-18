using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LevelApp.Crosscutting.Services;
using LevelApp.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace LevelApp.BLL.Base
{
    public interface IBaseOperation<TParameter, TResult>
    {
        IUnitOfWork UnitOfWork { get; set; }
        IConfiguration Configuration { get; set; }
        IMapper Mapper { get; set; }
        IFileService FileService { get; set; }
        TParameter Parameter { get; set; }
        TResult OperationResult { get; set; }
        Dictionary<string, HttpStatusCode> Errors { get; set; }
        
        int CurrentUserId { get; }
        string CurrentUserEmail { get;  }

        void SetupOperation(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, IFileService fileService, TParameter parameter, IUserResolverService userResolver);
        Task GetData();
        Task Validate();
        Task ExecuteValidated();
        Task AddFrontendPermissions();
    }
}
