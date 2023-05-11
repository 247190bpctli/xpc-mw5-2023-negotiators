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

    private Guid MockDataSetup()
    {
        ManufacturerDto test = new() { Name = "manAname", Description = "desc", LogoUrl = "imurl", Origin = "EU" };
        
        StringContent stringContent = new(JsonSerializer.Serialize(test), Encoding.UTF8, "application/json");
        
        string testGuid = Client.PostAsync("/api/Manufacturers/add", stringContent)
            .Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
        return Guid.Parse(testGuid);
    }
    
    private void MockDataDispose(Guid testGuid)
    {
        Client.DeleteAsync($"/api/Manufacturers/delete/{testGuid}");
    }

    [Fact]
    public async Task Get_Always_ReturnsAll()
    {
        Guid testGuid = MockDataSetup();
        
        HttpResponseMessage response = await Client.GetAsync("/api/Manufacturers/list/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        List<ManufacturerEntity> data = JsonSerializer.Deserialize<List<ManufacturerEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.NotEmpty(data);
        
        MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfExists_ReturnsDetails()
    {
        Guid testGuid = MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/details/{testGuid}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ManufacturerEntity data = JsonSerializer.Deserialize<ManufacturerEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal("manAname", data.Name);
        Assert.Equal("desc", data.Description);
        Assert.Equal("imurl", data.LogoUrl);
        Assert.Equal("EU", data.Origin);
            
        MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        Guid testGuid = MockDataSetup();
            
        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/details/{testInvalidGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
        MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task EditById_IfExists_Updates()
    {
        Guid testGuid = MockDataSetup();

        ManufacturerDto testEdit = new()
        {
            Name = "manBname",
            Description = "desc",
            LogoUrl = "imurl",
            Origin = "AU"
        };

        StringContent stringContent = new(JsonSerializer.Serialize(testEdit), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await Client.PutAsync($"/api/Manufacturers/edit/{testGuid}", stringContent);

        ManufacturerEntity data = JsonSerializer.Deserialize<ManufacturerEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;
            
        Assert.Equal("manBname", data.Name);
        Assert.Equal("desc", data.Description);
        Assert.Equal("imurl", data.LogoUrl);
        Assert.Equal("AU", data.Origin);
            
        MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task DeleteById_IfExists_GetsDeleted()
    {
        Guid testGuid = MockDataSetup();
            
        MockDataDispose(testGuid);

        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/details/{testGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SearchByName_IfFound_ReturnsDetails()
    {
        Guid testGuid = MockDataSetup();
            
        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/search/manAname");

        ManufacturerEntity data = JsonSerializer.Deserialize<ManufacturerEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal("manAname", data.Name);
        Assert.Equal("desc", data.Description);
        Assert.Equal("imurl", data.LogoUrl);
        Assert.Equal("EU", data.Origin);
            
        MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task SearchByName_IfNotFound_Returns404()
    {
        Guid testGuid = MockDataSetup();
            
        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/search/manBname");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
        MockDataDispose(testGuid);
    }
}