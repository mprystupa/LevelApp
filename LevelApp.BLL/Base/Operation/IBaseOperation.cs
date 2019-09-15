using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Base
{
    public interface IBaseOperation<TParameter, TResult>
    {
        IUnitOfWork UnitOfWork { get; set; }
        TParameter Parameter { get; set; }
        TResult OperationResult { get; set; }
        List<string> Errors { get; set; }

        void SetupOperation(IUnitOfWork unitOfWork, TParameter parameter);
        Task Validate();
        Task ExecuteValidated();
    }
}
