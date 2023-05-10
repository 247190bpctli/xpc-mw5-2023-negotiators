using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace eshopBackend.IntegrationTests.ControllerTests;

[Trait("Category", "Integration")]
public abstract class IntegrationTest : IClassFixture<TestWebApplicationFactory>
{
    protected readonly TestWebApplicationFactory _factory;
    protected readonly HttpClient _client;
    protected readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
 
    public IntegrationTest(TestWebApplicationFactory fixture)
    {
        _factory = fixture;
        _client = _factory.CreateClient();
    }
}