using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LevelApp.Crosscutting.Exceptions;
using LevelApp.DAL.Models.Base;
using LevelApp.DAL.Repositories.Base;
using LevelApp.DAL.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LevelApp.DAL.Tests.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BaseRepositoryTests : IClassFixture<ContextFixture>
    {
        private readonly ContextFixture _fixture;

        public BaseRepositoryTests(ContextFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetContext();
        }

        [Fact]
        public void GetAll_Method_Should_Return_All_Entities_In_Set()
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
        public void Get_Method_Should_Return_Entities_By_Id()
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
        public void Get_Method_Should_Return_Entities_By_Expression()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);

            // Act
            var result = repository.Get(x => x.Id == 1);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_Detail_Method_Should_Return_Entity_By_Expression()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);

            // Act
            var result = repository.GetDetail(x => x.Id == 1);

            // Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public async Task Get_Detail_Async_Method_Should_Return_Entity_By_Expression()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);

            // Act
            var result = await repository.GetDetailAsync(x => x.Id == 1);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_Detail_Method_Should_Throw_Exception_On_Null()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            
            // Act
            void Act() => repository.GetDetail(x => x.Id == 99);

            // Assert
            Assert.Throws<NotFoundException>((Action) Act);
        }
        
        [Fact]
        public async Task Get_Detail_Async_Method_Should_Throw_Exception_On_Null()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            
            // Act
            async Task Act() => await repository.GetDetailAsync(x => x.Id == 99);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(Act);
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
        public void Delete_Should_Delete_Entity_And_Return_Id()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            var entityToDelete = new TestEntity()
            {
                Id = 3
            };

            _fixture.Context.Add(entityToDelete);
            repository.Save();
            
            // Act
            var result = repository.Delete(entityToDelete);
            repository.Save();
            var removedEntity = _fixture.Context.TestEntities.FirstOrDefault(x => x.Id == entityToDelete.Id);

            // Assert
            Assert.Null(removedEntity);
            Assert.Equal(entityToDelete.Id, result);
        }
        
        [Fact]
        public void Delete_Should_Delete_Not_Attached_Entity()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            var entityToDelete = new TestEntity()
            {
                Id = 3
            };

            _fixture.Context.Add(entityToDelete);
            repository.Save();
            _fixture.Context.Entry(entityToDelete).State = EntityState.Detached;

            // Act
            var result = repository.Delete(entityToDelete);
            repository.Save();
            var removedEntity = _fixture.Context.TestEntities.FirstOrDefault(x => x.Id == entityToDelete.Id);

            // Assert
            Assert.Null(removedEntity);
            Assert.Equal(entityToDelete.Id, result);
        }
        
        [Fact]
        public void DeleteBatch_Should_Delete_Entities()
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
        public void Save_Should_Save_Changes_In_Context()
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
        public void SaveAsync_Should_Save_Changes_In_Context()
        {
            // Arrange
            var mockContext = new Mock<DbContext>();
            var repository = new BaseRepository<TestEntity, int>(mockContext.Object);
            
            // Act
            repository.SaveAsync();
            
            // Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void CheckIfExists_Should_Return_True_When_Entity_Exists()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            var entityId = 1;

            // Act
            var result = repository.CheckIfExists(x => x.Id == entityId);
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public async Task CheckIfExists_Async_Should_Return_True_When_Entity_Exists()
        {
            // Arrange
            var repository = new BaseRepository<TestEntity, int>(_fixture.Context);
            var entityId = 1;

            // Act
            var result = await repository.CheckIfExistsAsync(x => x.Id == entityId);
            
            // Assert
            Assert.True(result);
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