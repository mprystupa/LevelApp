﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.DAL.UnitOfWork;

namespace LevelApp.BLL.Base.Operation
{
    public class BaseOperation<TParameter, TResult> : IBaseOperation<TParameter, TResult>
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public TParameter Parameter { get; set; }
        public TResult OperationResult { get; set; }
        public List<string> Errors { get; set; }

        protected BaseOperation()
        {
            Errors = new List<string>();
        }

        public void SetupOperation(IUnitOfWork unitOfWork, TParameter parameter)
        {
            UnitOfWork = unitOfWork;
            Parameter = parameter;
        }

        public virtual Task Validate()
        {
            if (!Errors.Any())
            {
                return Task.CompletedTask;
            }

            throw new BusinessValidationException(Errors.Last());
        }

        public virtual Task ExecuteValidated()
        {
            return Task.CompletedTask;
        }
    }
}