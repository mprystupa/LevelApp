using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using LevelApp.BLL.Mappings;
using Xunit;

namespace LevelApp.BLL.Tests.ConfigurationTests
{
    [ExcludeFromCodeCoverage]
    public class MappingTests
    {
        [Fact]
        public void AutoMapper_Core_Mapping_Is_Valid()
        {
            // Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CoreProfile>();
            });
            
            // Act/Assert
            config.AssertConfigurationIsValid();
        }
    }
}