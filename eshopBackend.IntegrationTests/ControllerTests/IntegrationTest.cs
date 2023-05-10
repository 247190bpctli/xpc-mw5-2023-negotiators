using Xunit;

namespace eshopBackend.IntegrationTests.ControllerTests;

[Trait("Category", "Integration")]
public abstract class IntegrationTest : IClassFixture<TestWebApplicationFactory>
{
    protected readonly TestWebApplicationFactory _factory;
    protected readonly HttpClient _client;
 
    public IntegrationTest(TestWebApplicationFactory fixture)
    {
        _factory = fixture;
        _client = _factory.CreateClient();
    }
}