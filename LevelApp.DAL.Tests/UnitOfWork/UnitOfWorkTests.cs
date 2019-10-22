using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LevelApp.DAL.Repositories.User;
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
            var context = new Mock<DbContext>();
            var unitOfWork = new DAL.UnitOfWork.UnitOfWork(context.Object);
            
            // Act
            unitOfWork.Save();
            
            // Assert
            context.Verify(x => x.SaveChanges());
        }
        
        [Fact]
        public void UnitOfWork_SaveAsync_Should_Save_Changes_In_Context()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var unitOfWork = new DAL.UnitOfWork.UnitOfWork(context.Object);
            
            // Act
            unitOfWork.SaveAsync();
            
            // Assert
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void UnitOfWork_GetRepository_Returns_Repository_Of_Type()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var unitOfWork = new DAL.UnitOfWork.UnitOfWork(context.Object);
            
            // Act
            var result = unitOfWork.GetRepository<IUserRepository>();
            
            // Assert
            Assert.IsType<UserRepository>(result);
        }

        [Fact]
        public void UnitOfWork_Dispose_Disposes_Context()
        {
            // Arrange
            var context = new Mock<DbContext>();
            var unitOfWork = new DAL.UnitOfWork.UnitOfWork(context.Object);
            
            // Act
            unitOfWork.Dispose();
            
            // Assert
            context.Verify(x => x.Dispose());
        }
    }
}
