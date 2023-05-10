using System.Net;
using System.Text;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using Xunit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eshopBackend.IntegrationTests.ControllerTests;

public class CategoriesControllerTests : IntegrationTest
{
    public CategoriesControllerTests(TestWebApplicationFactory fixture): base(fixture) { }
    
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    private Guid MockDataSetup()
    {
        AddCategoryDto test = new() { Name = "catAname", ImageUrl = "imurl", Description = "desc" };
        
        StringContent stringContent = new(JsonSerializer.Serialize(test), Encoding.UTF8, "application/json");
        
        string testGuid = _client.PostAsync("/api/Categories/add", stringContent)
            .Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
        return Guid.Parse(testGuid);
    }
    
    private void MockDataDispose(Guid testGuid)
    {
        _client.DeleteAsync($"/api/Categories/delete/{testGuid}");
    }

    [Fact]
    public async Task Get_Always_ReturnsAll()
    {
        Guid testGuid = MockDataSetup();
        
        HttpResponseMessage response = await _client.GetAsync("/api/Categories/list/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        List<CategoryEntity> data = JsonSerializer.Deserialize<List<CategoryEntity>>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions)!;

        Assert.NotEmpty(data);
        
        MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfExists_ReturnsDetails()
    {
        Guid testGuid = MockDataSetup();

        HttpResponseMessage response = await _client.GetAsync($"/api/Categories/details/{testGuid}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        CategoryEntity data = JsonSerializer.Deserialize<CategoryEntity>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions)!;

        Assert.Equal("catAname", data.Name);
        Assert.Equal("imurl", data.ImageUrl);
        Assert.Equal("desc", data.Description);
            
        MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        Guid testGuid = MockDataSetup();
            
        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await _client.GetAsync($"/api/Categories/details/{testInvalidGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
        MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task EditById_IfExists_Updates()
    {
        Guid testGuid = MockDataSetup();

        EditCategoryDto testEdit = new()
        {
            Id = testGuid,
            Name = "catBname",
            ImageUrl = "imurl",
            Description = "desc3"
        };

        StringContent stringContent = new(JsonSerializer.Serialize(testEdit), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PutAsync($"/api/Categories/edit/{testGuid}", stringContent);

        CategoryEntity data = JsonSerializer.Deserialize<CategoryEntity>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions)!;
            
        Assert.Equal("catBname", data.Name);
        Assert.Equal("imurl", data.ImageUrl);
        Assert.Equal("desc3", data.Description);
            
        MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task DeleteById_IfExists_GetsDeleted()
    {
        Guid testGuid = MockDataSetup();
            
        MockDataDispose(testGuid);

        HttpResponseMessage response = await _client.GetAsync($"/api/Categories/details/{testGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
        
    [Fact]
    public async Task SearchByName_IfFound_ReturnsDetails()
    {
        Guid testGuid = MockDataSetup();
            
        HttpResponseMessage response = await _client.GetAsync($"/api/Categories/details/catAname");

        CategoryEntity data = JsonSerializer.Deserialize<CategoryEntity>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions)!;

        Assert.Equal("catAname", data.Name);
        Assert.Equal("imurl", data.ImageUrl);
        Assert.Equal("desc", data.Description);
            
        MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task SearchByName_IfNotFound_Returns404()
    {
        Guid testGuid = MockDataSetup();
            
        HttpResponseMessage response = await _client.GetAsync($"/api/Categories/details/catBname");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
        MockDataDispose(testGuid);
    }
}