using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LevelApp.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace LevelApp.BLL.Base
{
    public interface IBaseOperation<TParameter, TResult>
    {
        IUnitOfWork UnitOfWork { get; set; }
        IConfiguration Configuration { get; set; }
        IMapper Mapper { get; set; }
        TParameter Parameter { get; set; }
        TResult OperationResult { get; set; }
        List<string> Errors { get; set; }

        void SetupOperation(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, TParameter parameter);
        Task GetData();
        Task Validate();
        Task ExecuteValidated();
    }
}
