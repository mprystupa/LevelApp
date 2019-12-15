using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LevelApp.Crosscutting.Enums.Main;
using LevelApp.DAL.Models.Base.Interfaces;
using LevelApp.DAL.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LevelApp.DAL.Context
{
    public class LevelAppContext : DbContext
    {
        public LevelAppContext()
        {
            
        }

        public LevelAppContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual int SaveChanges(int userId)
        {
            OnBeforeSaving(userId);
            return base.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync(int userId)
        {
            OnBeforeSaving(userId);
            return base.SaveChangesAsync();
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<AppUserLesson> AppUserLessons { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Lesson setup
            modelBuilder
                .Entity<Lesson>()
                .HasOne(u => u.Author)
                .WithMany()
                .HasForeignKey(u => u.AuthorId)
                .IsRequired();

            // AppUserLesson setup
            modelBuilder
                .Entity<AppUserLesson>()
                .HasKey(c => new {c.UserId, c.LessonId});

            modelBuilder
                .Entity<AppUserLesson>()
                .Property(e => e.Status)
                .HasConversion(new EnumToNumberConverter<LessonStatusEnum, int>());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var parameter = Expression.Parameter(entityType.ClrType);

                var propertyMethodInfo = typeof(EF).GetMethod("Property")?.MakeGenericMethod(typeof(DateTime?));
                if (propertyMethodInfo == null) continue;
                
                // Get property DateDeletedUtc
                var dateDeletedProperty = Expression.Call(propertyMethodInfo, parameter,
                    Expression.Constant(nameof(IAuditable.DateDeletedUtc)));

                // Create compare expression DateDeletedUtc != null
                var compareExpression = Expression.MakeBinary(ExpressionType.Equal,
                    dateDeletedProperty, Expression.Constant(null));
                var lambda = Expression.Lambda(compareExpression, parameter);

                // Pass function containing compare expression
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        private void OnBeforeSaving(int userId)
        {
            if (ChangeTracker == null) return;
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ((IAuditable)entry.Entity).DateCreatedUtc = DateTime.UtcNow;
                        ((IAuditable)entry.Entity).CreatedBy = userId;
                        
                        IgnoreModificationData(entry);
                        IgnoreDeletionData(entry);

                        break;
                    
                    case EntityState.Modified:
                        ((IAuditable)entry.Entity).DateModifiedUtc = DateTime.UtcNow;
                        ((IAuditable)entry.Entity).ModifiedBy = userId;
                        
                        IgnoreCreationData(entry);
                        IgnoreDeletionData(entry);
                        
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        ((IAuditable)entry.Entity).DateDeletedUtc = DateTime.UtcNow;
                        ((IAuditable)entry.Entity).DeletedBy = userId;
                        
                        IgnoreCreationData(entry);
                        IgnoreModificationData(entry);
                        
                        break;
                }
            }
        }

        private void IgnoreCreationData(EntityEntry entry)
        {
            entry.Property(nameof(IAuditable.CreatedBy)).IsModified = false;
            entry.Property(nameof(IAuditable.DateCreatedUtc)).IsModified = false;
        }

        private void IgnoreModificationData(EntityEntry entry)
        {
            entry.Property(nameof(IAuditable.ModifiedBy)).IsModified = false;
            entry.Property(nameof(IAuditable.DateModifiedUtc)).IsModified = false;
        }

        private void IgnoreDeletionData(EntityEntry entry)
        {
            entry.Property(nameof(IAuditable.DeletedBy)).IsModified = false;
            entry.Property(nameof(IAuditable.DateDeletedUtc)).IsModified = false;
        }
    }
}