using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AutoMapper;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.BLL.Operations.Core.Lesson;
using LevelApp.DAL.Models.Core;
using LevelApp.DAL.Repositories.Lesson;
using LevelApp.DAL.Repositories.User;
using Moq;
using Xunit;

namespace LevelApp.BLL.Tests.Operations.Lessons
{
    [ExcludeFromCodeCoverage]
    public class GetAllLessonsOperationTests : BaseOperationTests<GetAllLessonsOperation, int, List<LessonDto>>
    {
        [Fact]
        public async Task GetAllLessonsOperation_Should_Return_Lessons_List()
        {
            // Arrange
            IList<Lesson> lessonList = new List<Lesson>();
            for (var i = 1; i <= 5; i++)
            {
                lessonList.Add(new Lesson() { Id = 5, Name = $"Lesson {i}"});
            }
            var repository = new Mock<ILessonRepository>();
            repository.Setup(x => x.GetAllAsync()).Returns(() => Task.FromResult(lessonList));
            MockRepository(repository);

            // Act
            await Operation.ExecuteValidated();
            
            // Assert
            Assert.Equal(lessonList.Count, Operation.OperationResult.Count);
        }
    }
}