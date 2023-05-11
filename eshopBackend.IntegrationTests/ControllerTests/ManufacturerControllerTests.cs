using System.Net;
using System.Text;
using System.Text.Json;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using Xunit;

namespace eshopBackend.IntegrationTests.ControllerTests;

public class ManufacturerControllerTests : IntegrationTest
{
    public ManufacturerControllerTests(TestWebApplicationFactory fixture): base(fixture) { }

    private async Task<Guid> MockDataSetup()
    {
        ManufacturerDto test = new() { Name = "manAname", Description = "desc", LogoUrl = "imurl", Origin = "EU" };
        
        StringContent stringContent = new(JsonSerializer.Serialize(test), Encoding.UTF8, "application/json");
        
        HttpResponseMessage request = await Client.PostAsync("/api/Manufacturers/add", stringContent);
        string testGuid = request.Content.ReadAsStringAsync().Result.Replace("\"", "");
        return Guid.Parse(testGuid);
    }
    
    private async Task MockDataDispose(Guid testGuid)
    {
        await Client.DeleteAsync($"/api/Manufacturers/delete/{testGuid}");
    }

    [Fact]
    public async Task Get_Always_ReturnsAll()
    {
        Guid testGuid = await MockDataSetup();
        
        HttpResponseMessage response = await Client.GetAsync("/api/Manufacturers/list/1");
        List<ManufacturerEntity> data = JsonSerializer.Deserialize<List<ManufacturerEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(data);
        
        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfExists_ReturnsDetails()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/details/{testGuid}");
        ManufacturerEntity data = JsonSerializer.Deserialize<ManufacturerEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("manAname", data.Name);
        Assert.Equal("desc", data.Description);
        Assert.Equal("imurl", data.LogoUrl);
        Assert.Equal("EU", data.Origin);
            
        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        Guid testGuid = await MockDataSetup();
            
        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/details/{testInvalidGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
        await MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task EditById_IfExists_Updates()
    {
        Guid testGuid = await MockDataSetup();

        ManufacturerDto testEdit = new()
        {
            Name = "manBname",
            Description = "desc",
            LogoUrl = "imurl",
            Origin = "AU"
        };

        StringContent stringContent = new(JsonSerializer.Serialize(testEdit), Encoding.UTF8, "application/json");

        HttpResponseMessage putResponse = await Client.PutAsync($"/api/Manufacturers/edit/{testGuid}", stringContent);
        Uri location = putResponse.Headers.Location!;

        HttpResponseMessage response = await Client.GetAsync(location);
        ManufacturerEntity data = JsonSerializer.Deserialize<ManufacturerEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;
            
        Assert.Equal(HttpStatusCode.Created, putResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("manBname", data.Name);
        Assert.Equal("desc", data.Description);
        Assert.Equal("imurl", data.LogoUrl);
        Assert.Equal("AU", data.Origin);
            
        await MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task DeleteById_IfExists_GetsDeleted()
    {
        Guid testGuid = await MockDataSetup();
        await MockDataDispose(testGuid);

        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/details/{testGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SearchByName_IfFound_ReturnsList()
    {
        Guid testGuid = await MockDataSetup();
            
        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/search/manAname");
        List<ManufacturerEntity> data = JsonSerializer.Deserialize<List<ManufacturerEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("manAname", data.First().Name);
        Assert.Equal("desc", data.First().Description);
        Assert.Equal("imurl", data.First().LogoUrl);
        Assert.Equal("EU", data.First().Origin);
            
        await MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task SearchByName_IfNotFound_ReturnsEmptyList()
    {
        Guid testGuid = await MockDataSetup();
            
        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/search/manBname");
        List<ManufacturerEntity> data = JsonSerializer.Deserialize<List<ManufacturerEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(new List<ManufacturerEntity>(), data);

        await MockDataDispose(testGuid);
    }
}