using System;
using System.Threading.Tasks;
using LevelApp.BLL.Base;
using LevelApp.BLL.Base.Executor;
using LevelApp.BLL.Base.Operation;
using LevelApp.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace LevelApp.BLL.Tests.Base.Executor
{
    public class OperationExecutorTests
    {
        [Fact]
        public async Task Operation_Execute_Should_Return_Operation_Result()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<BaseOperation<int, int>>();
            
            var executor = new OperationExecutor(serviceCollection.BuildServiceProvider());
            const int operationParameter = 5;

            // Act
            var result = await executor.Execute<TestOperation, int, int>(operationParameter);
            
            // Assert
            Assert.Equal(operationParameter, result);
        }
    }
}