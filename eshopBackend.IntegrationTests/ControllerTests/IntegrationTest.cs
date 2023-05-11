using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace eshopBackend.IntegrationTests.ControllerTests;

[Trait("Category", "Integration")]
public abstract class IntegrationTest : IClassFixture<TestWebApplicationFactory>
{
    protected readonly HttpClient Client;
    protected readonly TestWebApplicationFactory Factory;

    protected readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public IntegrationTest(TestWebApplicationFactory fixture)
    {
        Factory = fixture;
        Client = Factory.CreateClient();
    }
}