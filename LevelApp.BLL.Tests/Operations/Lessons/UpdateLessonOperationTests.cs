using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LevelApp.BLL.Base.Operation;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.BLL.Operations.Core.Lesson;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.Lesson;
using Moq;
using Xunit;

namespace LevelApp.BLL.Tests.Operations.Lessons
{
    [ExcludeFromCodeCoverage]
    public class UpdateLessonOperationTests : BaseOperationTests<UpdateLessonOperation, LessonDto, int>
    {
        [Fact]
        public async Task UpdateLessonOperation_Should_Return_Updated_Lesson_Id()
        {
            // Arrange
            const int returnId = 1;
            var lessonToUpdate = new LessonDto
            {
                Id = returnId,
                Name = "Test Lesson"
            };
            
            var repository = new Mock<ILessonRepository>();
            repository.Setup(x => x.Update(It.IsAny<Lesson>())).Returns(returnId);
            MockRepository(repository);

            Parameter = lessonToUpdate;

            // Act
            await Operation.ExecuteValidated();
            
            // Assert
            Assert.Equal(returnId, Operation.OperationResult);
        }

        [Fact]
        public async Task UpdateLessonOperation_Should_Not_Throw_Exception_When_Entity_Does_Not_Exist()
        {
            // Arrange
            var lessonToUpdate = new LessonDto
            {
                Id = 1,
                Name = "Test Lesson"
            };
            
            var repository = new Mock<ILessonRepository>();
            repository
                .Setup(x => x.CheckIfExists(It.IsAny<Func<Lesson, bool>>()))
                .Returns(true);
            
            MockRepository(repository);

            Parameter = lessonToUpdate;

            // Act
            var exception = await Record.ExceptionAsync(() => Operation.Validate());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task UpdateLessonOperation_Should_Throw_Exception_When_Entity_Does_Not_Exist()
        {
            // Arrange
            var lessonToUpdate = new LessonDto
            {
                Id = 1,
                Name = "Test Lesson"
            };
            
            var repository = new Mock<ILessonRepository>();
            repository
                .Setup(x => x.CheckIfExists(It.IsAny<Func<Lesson, bool>>()))
                .Returns(false);
            
            MockRepository(repository);

            Parameter = lessonToUpdate;

            // Act
            async Task Act() => await Operation.Validate();
            
            // Assert
            await Assert.ThrowsAsync<BusinessValidationException>(Act);
        }
    }
}