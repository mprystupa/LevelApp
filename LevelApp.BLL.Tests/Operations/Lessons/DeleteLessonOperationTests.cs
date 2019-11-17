using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.BLL.Operations.Core.Lesson;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.Lesson;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using Xunit;

namespace LevelApp.BLL.Tests.Operations.Lessons
{
    [ExcludeFromCodeCoverage]
    public class DeleteLessonOperationTests : BaseOperationTests<DeleteLessonOperation, int, int>
    {
        [Fact]
        public async Task DeleteLessonOperation_Should_Return_Deleted_Lesson_Id()
        {
            // Arrange
            const int lessonToDeleteId = 1;

            var repository = new Mock<ILessonRepository>();
            repository.Setup(x => x.Delete(It.IsAny<int>())).Returns(lessonToDeleteId);
            MockRepository(repository);

            Parameter = lessonToDeleteId;

            // Act
            await Operation.ExecuteValidated();
            
            // Assert
            Assert.Equal(lessonToDeleteId, Operation.OperationResult);
        }
        
        [Fact]
        public async Task DeleteLessonOperation_Should_Not_Throw_Exception_When_Entity_Exist()
        {
            // Arrange
            var lessonToDeleteId = 1;
            
            var repository = new Mock<ILessonRepository>();
            repository
                .Setup(x => x.CheckIfExists(It.IsAny<Func<Lesson, bool>>()))
                .Returns(true);
            
            MockRepository(repository);

            Parameter = lessonToDeleteId;

            // Act
            var exception = await Record.ExceptionAsync(() => Operation.Validate());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task DeleteLessonOperation_Should_Throw_Exception_When_Entity_Does_Not_Exist()
        {
            // Arrange
            const int lessonToDeleteId = 1;
            
            var repository = new Mock<ILessonRepository>();
            repository
                .Setup(x => x.CheckIfExists(It.IsAny<Func<Lesson, bool>>()))
                .Returns(false);
            
            MockRepository(repository);

            Parameter = lessonToDeleteId;

            // Act
            async Task Act() => await Operation.Validate();
            
            // Assert
            await Assert.ThrowsAsync<BusinessValidationException>(Act);
        }
    }
}