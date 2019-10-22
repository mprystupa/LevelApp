using System;
using System.Diagnostics.CodeAnalysis;
using LevelApp.DAL.Tests.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.Tests.Fixtures
{
    [ExcludeFromCodeCoverage]
    public class ContextFixture
    {
        public TestContext Context { get; set; }
        public ContextFixture()
        {
            var context = CreateMockContext();
            SeedContext(context);
            
            Context = context;
        }

        public void ResetContext()
        {
            ClearContextData(Context);
            SeedContext(Context);
        }

        private TestContext CreateMockContext()
        {
            var options = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase(databaseName: "Testing_Database")
                .Options;
            
            return new TestContext(options);
        }

        private void SeedContext(TestContext context)
        {
            context.TestEntities.Add(new TestEntity()
            {
                Id = 1,
                TestField1 = 2,
                TestField2 = "Test"
            });
            context.TestEntities.Add(new TestEntity()
            {
                Id = 2,
                TestField1 = 2,
                TestField2 = "Test"
            });
            context.SaveChanges();
        }
        
        private void ClearContextData(TestContext context)
        {
            context.RemoveRange(context.TestEntities);
        }
    }
}