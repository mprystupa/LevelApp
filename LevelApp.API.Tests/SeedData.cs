using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LevelApp.DAL.Context;
using LevelApp.DAL.Models.Core;

namespace LevelApp.API.Tests
{
    [ExcludeFromCodeCoverage]
    public static class SeedData
    {
        public static void PopulateTestData(LevelAppContext context)
        {
            // Here you can seed test data for integration tests purposes
            for (var i = 1; i <= 10; i++)
            {
                context.AppUsers.Add(new AppUser()
                {
                    Email = $"Test{i}",
                    PasswordHash = $"Test{i}",
                    PasswordSalt = $"Test{i}"
                });
            }

            context.SaveChanges();
        }
    }
}