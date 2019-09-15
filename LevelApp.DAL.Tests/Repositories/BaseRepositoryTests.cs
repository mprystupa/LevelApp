using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LevelApp.DAL.Entities.Base;
using LevelApp.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LevelApp.DAL.Tests.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BaseRepositoryTests : IClassFixture<ContextFixture>
    {
        private ContextFixture _fixture;

        public BaseRepositoryTests(ContextFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public async Task GetAll_Method_Should_Return_All_Entities_In_Set()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            
            // Act
            var result = repository.GetAll();
            
            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        
        [Fact]
        public async Task GetAllAsync_Method_Should_Return_All_Entities_In_Set()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            
            // Act
            var result = await repository.GetAllAsync();
            
            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        
        [Fact]
        public async Task Get_Method_Should_Return_Entities_By_Id()
        {
            // Arrange
            var entityId = 1;
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            
            // Act
            var result = repository.Get(entityId);
            
            // Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task Get_Method_Should_Return_Entities_By_Expression()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            
            // Act
            var result = repository.Get(x => x.Id == 1);
            
            // Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task GetAsync_Method_Should_Return_Entities_By_Id()
        {
            // Arrange
            var entityId = 1;
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            
            // Act
            var result = await repository.GetAsync(entityId);
            
            // Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task GetAsync_Method_Should_Return_Entities_By_Expression()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            
            // Act
            var result = await repository.GetAsync(x => x.Id == 1);
            
            // Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task GetDetail_Method_Should_Return_Entity()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            
            // Act
            var result = repository.GetDetail(x => x.Id == 1);
            
            // Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task Insert_Should_Insert_Entity_Into_Context_And_Return_Id()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            var entityToAdd = new TestEntity()
            {
                Id = 11,
                TestField1 = 11,
                TestField2 = "Test"
            };
            
            // Act
            var result = repository.Insert(entityToAdd);
            await _fixture.Context.SaveChangesAsync();
            
            var insertedEntity = await _fixture.Context.TestEntities.FirstAsync(x => x.Id == entityToAdd.Id);
            
            // Assert
            Assert.Equal(entityToAdd.Id, result);
            Assert.NotNull(insertedEntity);
        }
        
        [Fact]
        public async Task InsertBatch_Should_Insert_Entities_Into_Context()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            var entitiesToAdd = new List<TestEntity>
            {
                new TestEntity()
                {
                    Id = 22,
                    TestField1 = 22,
                    TestField2 = "Test_Field"
                },
                new TestEntity()
                {
                    Id = 33,
                    TestField1 = 33,
                    TestField2 = "Test_Field"
                }
            };
            
            // Act
            repository.InsertBatch(entitiesToAdd);
            await _fixture.Context.SaveChangesAsync();
            
            var insertedEntities =
                _fixture.Context.TestEntities
                    .Where(x => entitiesToAdd.Select(y => y.Id).Contains(x.Id))
                    .ToList();
            
            // Assert
            Assert.NotEmpty(insertedEntities);
            Assert.Equal(entitiesToAdd.Count, insertedEntities.Count);
        }

        [Fact]
        public async Task Update_Should_Update_Entity()
        {
            // Arrange 
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            
            var entityIdToUpdate = 1;
            var updatedFieldValue = 555;
            
            var entityToUpdate = await _fixture.Context.TestEntities.FirstAsync(x => x.Id == entityIdToUpdate);
            entityToUpdate.TestField1 = updatedFieldValue;

            // Act
            repository.Update(entityToUpdate);
            var entityAfterUpdate = await _fixture.Context.TestEntities.FirstAsync(x => x.Id == entityIdToUpdate);
            
            // Assert
            Assert.Equal(updatedFieldValue, entityAfterUpdate.TestField1);
            Assert.Equal(entityToUpdate.Id, entityAfterUpdate.Id);
        }
        
        [Fact]
        public async Task UpdateBatch_Should_Update_Entities()
        {
            // Arrange 
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            var updatedFieldValue = 555;
            
            var entitiesToUpdate = _fixture.Context.TestEntities;
            await entitiesToUpdate.ForEachAsync(x =>
                x.TestField1 = updatedFieldValue);

            // Act
            repository.UpdateBatch(entitiesToUpdate);
            var entitiesAfterUpdate = _fixture.Context.TestEntities;
            
            // Assert
            Assert.Equal(entitiesToUpdate.ToList().Count, entitiesAfterUpdate.ToList().Count);

            foreach (var entityAfterUpdate in entitiesAfterUpdate)
            {
                Assert.Equal(updatedFieldValue, entityAfterUpdate.TestField1);
            }
        }

        [Fact]
        public async Task Delete_Should_Delete_Entity_And_Return_Id()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            var entityToDelete = new TestEntity()
            {
                Id = 77
            };

            _fixture.Context.Add(entityToDelete);
            
            // Act
            var result = repository.Delete(entityToDelete);
            var removedEntity = _fixture.Context.TestEntities.FirstOrDefault(x => x.Id == entityToDelete.Id);

            // Assert
            Assert.Null(removedEntity);
            Assert.Equal(entityToDelete.Id, result);
        }
        
        [Fact]
        public async Task Delete_Should_Delete_Not_Attached_Entity()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            var entityToDelete = new TestEntity()
            {
                Id = 77
            };

            _fixture.Context.Add(entityToDelete);
            _fixture.Context.Entry(entityToDelete).State = EntityState.Detached;

            // Act
            var result = repository.Delete(entityToDelete);
            var removedEntity = _fixture.Context.TestEntities.FirstOrDefault(x => x.Id == entityToDelete.Id);

            // Assert
            Assert.Null(removedEntity);
            Assert.Equal(entityToDelete.Id, result);
        }
        
        [Fact]
        public async Task DeleteBatch_Should_Delete_Entities()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            var entitiesToDelete = new List<TestEntity>
            {
                new TestEntity()
                {
                    Id = 88,
                    TestField1 = 22,
                    TestField2 = "Test_Field"
                },
                new TestEntity()
                {
                    Id = 99,
                    TestField1 = 33,
                    TestField2 = "Test_Field"
                }
            };

            _fixture.Context.AddRange(entitiesToDelete);
            
            // Act
            repository.DeleteBatch(entitiesToDelete);
            var removedEntities =
                _fixture.Context.TestEntities.Where(x => entitiesToDelete.Select(y => y.Id).Contains(x.Id));

            // Assert
            Assert.Empty(removedEntities);
        }

        [Fact]
        public async Task Save_Should_Save_Changes_In_Context()
        {
            // Arrange
            var mockContext = new Mock<DbContext>();
            var repository = new BaseRepository<TestEntity, int>(mockContext.Object);
            
            // Act
            repository.Save();
            
            // Assert
            mockContext.Verify(x => x.SaveChanges());
        }
        
        [Fact]
        public async Task SaveAsync_Should_Save_Changes_In_Context()
        {
            // Arrange
            var mockContext = new Mock<DbContext>();
            var repository = new BaseRepository<TestEntity, int>(mockContext.Object);
            
            // Act
            repository.SaveAsync();
            
            // Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }

    public class ContextFixture : IDisposable
    {
        public TestContext Context { get; private set; }
        public ContextFixture()
        {
            var context = CreateMockContext();
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

            Context = context;
        }
        
        public void Dispose()
        {
            Context.Dispose();
        }
        
        private TestContext CreateMockContext()
        {
            var options = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase(databaseName: "Testing_Database")
                .Options;
            
            return new TestContext(options);
        }
    }

    [ExcludeFromCodeCoverage]
    public class TestContext: DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }
        
        public DbSet<TestEntity> TestEntities { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TestEntity : Entity<int>
    {
        public int TestField1 { get; set; }
        public string TestField2 { get; set; }
    }
}