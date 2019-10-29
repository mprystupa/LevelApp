﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LevelApp.DAL.Models.Base.Interfaces;
using LevelApp.DAL.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace LevelApp.DAL.Context
{
    public class LevelAppContext : DbContext
    {
        private const string SoftDeleteColumnName = "DateDeletedUtc";

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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var parameter = Expression.Parameter(entityType.ClrType);

                var propertyMethodInfo = typeof(EF).GetMethod("Property")?.MakeGenericMethod(typeof(DateTime?));
                if (propertyMethodInfo == null) continue;
                
                // Get property DateDeletedUtc
                var dateDeletedProperty = Expression.Call(propertyMethodInfo, parameter,
                    Expression.Constant(SoftDeleteColumnName));

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
                        break;
                    
                    case EntityState.Modified:
                        ((IAuditable)entry.Entity).DateModifiedUtc = DateTime.UtcNow;
                        ((IAuditable)entry.Entity).ModifiedBy = userId;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        ((IAuditable)entry.Entity).DateDeletedUtc = DateTime.UtcNow;
                        ((IAuditable)entry.Entity).DeletedBy = userId;
                        break;
                }
            }
        }
    }
}