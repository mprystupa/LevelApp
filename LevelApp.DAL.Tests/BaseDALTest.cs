using System.Diagnostics.CodeAnalysis;
using LevelApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.Tests
{
    [ExcludeFromCodeCoverage]
    public class BaseDALTest
    {
        protected BaseDALTest()
        { 
            SeedTestContext();
        }

        protected LevelAppContext CreateMockContext()
        {
            var options = new DbContextOptionsBuilder<LevelAppContext>()
                .UseInMemoryDatabase(databaseName: "Testing_Database")
                .Options;
            
            return new LevelAppContext(options);
        }

        private void SeedTestContext()
        {
            using (var context = CreateMockContext())
            {
                SeedCoreUsers(context);
            }
        }

        private void SeedCoreUsers(LevelAppContext context)
        {
            var testUser1 = new User()
            {
                Id = 1,
                Username = "Test_User_1",
                PasswordHash = "Test_Password_1",
                Token = "Test_Token_1"
            };

            context.User.Add(testUser1);
        }

    }
}