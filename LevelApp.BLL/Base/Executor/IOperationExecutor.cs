using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LevelApp.BLL.Base
{
    public interface IOperationExecutor
    {
        Task<TResult> Execute<TOperation, TParameter, TResult>(TParameter parameter)
            where TOperation : IBaseOperation<TParameter, TResult>;
    }
}
