using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AutoMapper;
using LevelApp.BLL.Base.Executor;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace LevelApp.BLL.Tests.Base.Executor
{
    [ExcludeFromCodeCoverage]
    public class OperationExecutorTests
    {
        private readonly OperationExecutor _executor;
        
        // Setup test
        public OperationExecutorTests()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.Save(1)).Throws(new GeneralServerException("Test message"));
            var configuration = new Mock<IConfiguration>();
            var mapper = new Mock<IMapper>();

            _executor = new OperationExecutor(unitOfWork.Object, configuration.Object, mapper.Object);
        }
        
        [Fact]
        public async Task Operation_Execute_Should_Return_Operation_Result()
        {
            // Arrange
            const int operationParameter = 1;

            // Act
            var result = await _executor.Execute<TestOperation, int, int>(operationParameter);
            
            // Assert
            Assert.Equal(operationParameter, result);
        }

        [Fact]
        public void Operation_Execute_Should_Throw_Business_Validation_Exception_On_Validation_Error()
        {
            // Arrange
            const int operationParameter = 2;
            
            // Act
            var exception = Record.ExceptionAsync(() => _executor.Execute<TestOperation, int, int>(operationParameter)).GetAwaiter().GetResult();
            
            // Assert
            Assert.IsType<BusinessValidationException>(exception);
        }

        [Fact]
        public void Operation_Execute_Should_Throw_General_Server_Exception_On_Server_Error()
        {
            // Arrange
            const int operationParameter = 3;
            
            // Act
            var exception = Record.ExceptionAsync(() => _executor.Execute<TestOperation, int, int>(operationParameter)).GetAwaiter().GetResult();
            
            // Assert
            Assert.IsType<GeneralServerException>(exception);
        }
    }
}