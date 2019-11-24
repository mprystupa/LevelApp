using System;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using LevelApp.BLL.Base.Executor;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Mappings;
using LevelApp.BLL.Operations.Core.Lesson;
using LevelApp.Crosscutting.Services;
using LevelApp.DAL.Repositories.Base;
using LevelApp.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Moq;

namespace LevelApp.BLL.Tests.Operations
{
    [ExcludeFromCodeCoverage]
    public class BaseOperationTests<TOperation, TParameter, TResult> where TOperation : BaseOperation<TParameter, TResult>
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
        private Mock<IUserResolverService> _userResolver = new Mock<IUserResolverService>();
        private IMapper _mapper;
        protected TParameter Parameter { get; set; }
        
        
        private TOperation _operation;

        protected TOperation Operation
        {
            get { return _operation ?? (_operation = GetOperation()); }
        }

        protected BaseOperationTests()
        {
            GenerateMapper();
        }

        protected TOperation GetOperation()
        {
            var operation = GetOperationInstance<TOperation>();
            operation.SetupOperation(_unitOfWorkMock.Object, _configurationMock.Object, _mapper, Parameter, _userResolver.Object);

            return operation;
        }

        protected void MockRepository<TRepository>(Mock<TRepository> repositoryMock) where TRepository : class
        {
            _unitOfWorkMock.Setup(x => x.GetRepository<TRepository>()).Returns(repositoryMock.Object);
        }

        protected void MockUserResolver(Mock<IUserResolverService> userResolverMock)
        {
            _userResolver = userResolverMock;
        }

        private void GenerateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CoreProfile());
            });
            _mapper = new Mapper(configuration);
        }
        
        private static TOperation GetOperationInstance<TOperation>()
        {
            return Activator.CreateInstance<TOperation>();
        }
    }
}