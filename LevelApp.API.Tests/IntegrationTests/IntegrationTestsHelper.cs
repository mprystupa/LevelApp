using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Newtonsoft.Json;

namespace LevelApp.API.Tests.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public static class IntegrationTestsHelper
    {
        public static HttpContent GenerateHttpContent<TObject>(TObject data)
        {
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        public static string GenerateEndpoint(IEnumerable<string> endpointParts, string moduleName)
        {
            var result = string.Empty;
            foreach (var endpointPart in endpointParts)
            {
                result = result + "/" + endpointPart;
            }

            result = result.Replace("[controller]", moduleName);

            return result;
        }

        public static void SetToken(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}