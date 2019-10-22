using LevelApp.Crosscutting.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LevelApp.Crosscutting.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TokenManagement GetTokenManagementData(this IConfiguration configuration)
        {
            return JsonConvert.DeserializeObject<TokenManagement>(configuration.GetSection("tokenManagement").Value);
        }
    }
}