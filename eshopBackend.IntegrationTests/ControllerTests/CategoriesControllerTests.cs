using System.Net;
using System.Text;
using System.Text.Json;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using Xunit;

namespace eshopBackend.IntegrationTests.ControllerTests;

public class CategoriesControllerTests : IntegrationTest
{
    public CategoriesControllerTests(TestWebApplicationFactory fixture): base(fixture) { }
    private Guid MockDataSetup()
    {
        CategoryDto test = new() { Name = "catAname", ImageUrl = "imurl", Description = "desc" };
        
        StringContent stringContent = new(JsonSerializer.Serialize(test), Encoding.UTF8, "application/json");
        
        string testGuid = Client.PostAsync("/api/Categories/add", stringContent)
            .Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
        return Guid.Parse(testGuid);
    }
    
    private async Task MockDataDispose(Guid testGuid)
    {
        await Client.DeleteAsync($"/api/Categories/delete/{testGuid}");
    }

    [Fact]
    public async Task Get_Always_ReturnsAll()
    {
        Guid testGuid = MockDataSetup();
        
        HttpResponseMessage response = await Client.GetAsync("/api/Categories/list/1");
        List<CategoryEntity> data = JsonSerializer.Deserialize<List<CategoryEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(data);
        
        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfExists_ReturnsDetails()
    {
        Guid testGuid = MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync($"/api/Categories/details/{testGuid}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        CategoryEntity data = JsonSerializer.Deserialize<CategoryEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("catAname", data.Name);
        Assert.Equal("imurl", data.ImageUrl);
        Assert.Equal("desc", data.Description);
            
        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        Guid testGuid = MockDataSetup();
            
        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await Client.GetAsync($"/api/Categories/details/{testInvalidGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
        await MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task EditById_IfExists_Updates()
    {
        Guid testGuid = MockDataSetup();

        CategoryDto testEdit = new()
        {
            Name = "catBname",
            ImageUrl = "imurl",
            Description = "desc3"
        };

        StringContent stringContent = new(JsonSerializer.Serialize(testEdit), Encoding.UTF8, "application/json");
        
        HttpResponseMessage putResponse = await Client.PutAsync($"/api/Categories/edit/{testGuid}", stringContent);
        Uri location = putResponse.Headers.Location!;

        HttpResponseMessage response = await Client.GetAsync(location);
        CategoryEntity data = JsonSerializer.Deserialize<CategoryEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.Created, putResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("catBname", data.Name);
        Assert.Equal("imurl", data.ImageUrl);
        Assert.Equal("desc3", data.Description);

        await MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task DeleteById_IfExists_GetsDeleted()
    {
        Guid testGuid = MockDataSetup();
            
        await MockDataDispose(testGuid);

        HttpResponseMessage response = await Client.GetAsync($"/api/Categories/details/{testGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SearchByName_IfFound_ReturnsList()
    {
        Guid testGuid = MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync($"/api/Categories/search/catAname");

        List<CategoryEntity> data = JsonSerializer.Deserialize<List<CategoryEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("catAname", data.First().Name);
        Assert.Equal("imurl", data.First().ImageUrl);
        Assert.Equal("desc", data.First().Description);

        await MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task SearchByName_IfNotFound_ReturnsEmptyList()
    {
        Guid testGuid = MockDataSetup();
            
        HttpResponseMessage response = await Client.GetAsync($"/api/Categories/search/catBname");

        List<CategoryEntity> data = JsonSerializer.Deserialize<List<CategoryEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(new List<CategoryEntity>(), data);
            
        await MockDataDispose(testGuid);
    }
}