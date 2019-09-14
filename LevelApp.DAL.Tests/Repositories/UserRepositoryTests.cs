using LevelApp.DAL.DataAccess;
using LevelApp.DAL.Entities;
using LevelApp.DAL.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LevelApp.DAL.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly CoreUser _testUserEntity;
        private UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _testUserEntity = new CoreUser()
            {
                Username = "Test",
                Password = "Test",
                Token = "Test"
            };
        }

        [Fact]
        public void AddingUserToTheRepository_Test()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MainContext>()
                .UseInMemoryDatabase(databaseName: "AddingUserToTheRepository_Test")
                .Options;

            // Act
            using (var context = new MainContext(options))
            {
                context.CoreUsers.Add(_testUserEntity);
                context.SaveChanges();
            }

            //Assert
            using (var context = new MainContext(options))
            {
                _userRepository = new UserRepository(context);

                Assert.NotNull(_userRepository.GetAll());
                Assert.Equal(1, _userRepository.GetAll().Count);
            }
        }
    }
}
