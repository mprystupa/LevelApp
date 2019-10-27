using System.Diagnostics.CodeAnalysis;
using LevelApp.Crosscutting.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LevelApp.Crosscutting.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ConfigurationExtensions
    {
        public static TokenManagement GetTokenManagementData(this IConfiguration configuration)
        {
            var tokenManagementData = configuration.GetSection("tokenManagement");
            var section = new TokenManagement();
            tokenManagementData.Bind(section);

            return section;
        }
    }
}