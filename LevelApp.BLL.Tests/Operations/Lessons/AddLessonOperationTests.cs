using System.Diagnostics.CodeAnalysis;
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
    public class AddLessonOperationTests : BaseOperationTests<AddLessonOperation, LessonDto, int>
    {
        [Fact]
        public async Task AddLessonOperation_Should_Return_Added_Lesson_Id()
        {
            // Arrange
            const int returnId = 1;
            var lessonToAdd = new LessonDto
            {
                Name = "Test Lesson"
            };
            
            var repository = new Mock<ILessonRepository>();
            repository.Setup(x => x.Insert(It.IsAny<Lesson>())).Returns(returnId);
            MockRepository(repository);

            Parameter = lessonToAdd;

            // Act
            await Operation.ExecuteValidated();
            
            // Assert
            Assert.Equal(returnId, Operation.OperationResult);
        }
    }
}