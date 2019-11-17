using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.BLL.Operations.Core.Lesson;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.Lesson;
using Moq;
using Xunit;

namespace LevelApp.BLL.Tests.Operations.Lessons
{
    [ExcludeFromCodeCoverage]
    public class GetLessonOperationTests : BaseOperationTests<GetLessonOperation, int, LessonDto>
    {
        [Fact]
        public async Task GetLessonOperation_Should_Return_Lesson()
        {
            // Arrange
            var lesson = new Lesson()
            {
                Id = 1,
                Name = "Test Lesson"
            };
            
            var repository = new Mock<ILessonRepository>();
            repository
                .Setup(x => x.GetDetailAsync(It.IsAny<Expression<Func<Lesson, bool>>>()))
                .Returns(() => Task.FromResult(lesson));
            MockRepository(repository);

            // Act
            await Operation.ExecuteValidated();

            // Assert
            Assert.Equal(lesson.Id, Operation.OperationResult.Id);
            Assert.Equal(lesson.Name, Operation.OperationResult.Name);
        }
    }
}