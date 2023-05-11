using System.Net;
using System.Text;
using System.Text.Json;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using Xunit;

namespace eshopBackend.IntegrationTests.ControllerTests;

public class ProductsControllerTests : IntegrationTest
{
    public ProductsControllerTests(TestWebApplicationFactory fixture): base(fixture) { }
    
    private Guid MockDataSetup()
    {
        ProductDto test = new()
        {
            Name = "prodAname",
            ImageUrl = "imurl",
            Description = "desc",
            Price = 123,
            Weight = 456,
            Stock = 789
        };//not handling relations

        
        StringContent stringContent = new(JsonSerializer.Serialize(test), Encoding.UTF8, "application/json");
        
        string testGuid = Client.PostAsync("/api/Products/add", stringContent)
            .Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
        return Guid.Parse(testGuid);
    }
    
    private void MockDataDispose(Guid testGuid)
    {
        Client.DeleteAsync($"/api/Products/delete/{testGuid}");
    }

    [Fact]
    public async Task Get_Always_ReturnsAll()
    {
        Guid testGuid = MockDataSetup();
        
        HttpResponseMessage response = await Client.GetAsync("/api/Products/list/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        List<ProductEntity> data = JsonSerializer.Deserialize<List<ProductEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.NotEmpty(data);
        
        MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfExists_ReturnsDetails()
    {
        Guid testGuid = MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/details/{testGuid}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ProductEntity data = JsonSerializer.Deserialize<ProductEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal("prodAname", data.Name);
        Assert.Equal("imurl", data.ImageUrl);
        Assert.Equal("desc", data.Description);
        Assert.Equal(123, data.Price);
        Assert.Equal(456, data.Weight);
        Assert.Equal(789, data.Stock);

        MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        Guid testGuid = MockDataSetup();
            
        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/details/{testInvalidGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
        MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task EditById_IfExists_Updates()
    {
        Guid testGuid = MockDataSetup();

        ProductDto testEdit = new()
        {
            Name = "prodBname",
            ImageUrl = "imurl",
            Description = "desc3",
            Price = 123,
            Weight = 789,
            Stock = 456
        };

        StringContent stringContent = new(JsonSerializer.Serialize(testEdit), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await Client.PutAsync($"/api/Products/edit/{testGuid}", stringContent);

        ProductEntity data = JsonSerializer.Deserialize<ProductEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;
            
        Assert.Equal("prodBname", data.Name);
        Assert.Equal("imurl", data.ImageUrl);
        Assert.Equal("desc3", data.Description);
        Assert.Equal(123, data.Price);
        Assert.Equal(789, data.Weight);
        Assert.Equal(456, data.Stock);

        MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task DeleteById_IfExists_GetsDeleted()
    {
        Guid testGuid = MockDataSetup();
            
        MockDataDispose(testGuid);

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/details/{testGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SearchByName_IfFound_ReturnsDetails()
    {
        Guid testGuid = MockDataSetup();
            
        HttpResponseMessage response = await Client.GetAsync($"/api/Products/search/prodAname");

        ProductEntity data = JsonSerializer.Deserialize<ProductEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal("prodAname", data.Name);
        Assert.Equal("imurl", data.ImageUrl);
        Assert.Equal("desc", data.Description);
        Assert.Equal(123, data.Price);
        Assert.Equal(456, data.Weight);
        Assert.Equal(789, data.Stock);

        MockDataDispose(testGuid);
    }
        
    [Fact]
    public async Task SearchByName_IfNotFound_Returns404()
    {
        Guid testGuid = MockDataSetup();
            
        HttpResponseMessage response = await Client.GetAsync($"/api/Products/search/prodBname");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
        MockDataDispose(testGuid);
    }
    
    //todo review add view delete test notfound
    
    [Fact]
    public async Task GetReviewById_IfMissing_Returns404()
    {
        Guid testGuid = MockDataSetup();
            
        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/details/{testInvalidGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
        MockDataDispose(testGuid);
    }
}