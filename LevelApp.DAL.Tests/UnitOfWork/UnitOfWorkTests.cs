using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LevelApp.Crosscutting.Extensions;
using LevelApp.DAL.Context;
using LevelApp.DAL.Repositories.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LevelApp.DAL.Tests.UnitOfWork
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWorkTests
    {
        [Fact]
        public void UnitOfWork_Save_Should_Save_Changes_In_Context()
        {
            // Arrange
            var context = new Mock<LevelAppContext>();
            var httpAccessor = new Mock<IHttpContextAccessor>();

            var unitOfWork = new DAL.UnitOfWork.UnitOfWork(context.Object, httpAccessor.Object);
            unitOfWork.SetRequestUserIdFunction(() => 1);
            
            // Act
            unitOfWork.Save();
            
            // Assert
            context.Verify(x => x.SaveChanges(1));
        }
        
        [Fact]
        public void UnitOfWork_SaveAsync_Should_Save_Changes_In_Context()
        {
            // Arrange
            var context = new Mock<LevelAppContext>();
            var httpAccessor = new Mock<IHttpContextAccessor>();

            var unitOfWork = new DAL.UnitOfWork.UnitOfWork(context.Object, httpAccessor.Object);
            unitOfWork.SetRequestUserIdFunction(() => 1);
            
            // Act
            unitOfWork.SaveAsync();
            
            // Assert
            context.Verify(x => x.SaveChangesAsync(It.IsAny<int>()));
        }

        [Fact]
        public void UnitOfWork_GetRepository_Returns_Repository_Of_Type()
        {
            // Arrange
            var context = new Mock<LevelAppContext>();
            var httpAccessor = new Mock<IHttpContextAccessor>();

            var unitOfWork = new DAL.UnitOfWork.UnitOfWork(context.Object, httpAccessor.Object);
            unitOfWork.SetRequestUserIdFunction(() => 1);
            
            // Act
            var result = unitOfWork.GetRepository<IUserRepository>();
            
            // Assert
            Assert.IsType<UserRepository>(result);
        }

        [Fact]
        public void UnitOfWork_Dispose_Disposes_Context()
        {
            // Arrange
            var context = new Mock<LevelAppContext>();
            var httpAccessor = new Mock<IHttpContextAccessor>();

            var unitOfWork = new DAL.UnitOfWork.UnitOfWork(context.Object, httpAccessor.Object);
            unitOfWork.SetRequestUserIdFunction(() => 1);
            
            // Act
            unitOfWork.Dispose();
            
            // Assert
            context.Verify(x => x.Dispose());
        }
    }
}
