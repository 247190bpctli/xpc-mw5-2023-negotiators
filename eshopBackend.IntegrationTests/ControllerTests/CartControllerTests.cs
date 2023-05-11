using System.Net;
using System.Text;
using System.Text.Json;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using Xunit;

namespace eshopBackend.IntegrationTests.ControllerTests;

public class CartControllerTests : IntegrationTest
{
    public CartControllerTests(TestWebApplicationFactory fixture): base(fixture) { }
    
    private async Task<Guid> MockDataSetup()
    {
        HttpResponseMessage request = await Client.PostAsync("/api/Cart/create", null);
        string testGuid = request.Content.ReadAsStringAsync().Result.Replace("\"", "");
        return Guid.Parse(testGuid);
    }
    
    private async Task MockDataDispose(Guid testGuid)
    {
        await Client.DeleteAsync($"/api/Cart/delete/{testGuid}");
    }

    [Fact]
    public async Task GetById_IfExists_ReturnsDetails()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync($"/api/Cart/details/{testGuid}");
        CartEntity data = JsonSerializer.Deserialize<CartEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(new List<ProductEntity>(), data.Products);
        Assert.Equal(default, data.DeliveryType);
        Assert.Equal(default, data.DeliveryAddress);
        Assert.Equal(default, data.PaymentType);
        Assert.Equal(default, data.PaymentDetails);

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        Guid testGuid = await MockDataSetup();
            
        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await Client.GetAsync($"/api/Cart/details/{testInvalidGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task EditById_IfExists_Updates()
    {
        Guid testGuid = await MockDataSetup();

        EditCartDto testEdit = new()
        {
            DeliveryType = 2,
            DeliveryAddress = "test",
            PaymentType = 3,
            PaymentDetails = "testt"
        };

        StringContent stringContent = new(JsonSerializer.Serialize(testEdit), Encoding.UTF8, "application/json");

        HttpResponseMessage putResponse = await Client.PutAsync($"/api/Cart/edit/{testGuid}", stringContent);
        Uri location = putResponse.Headers.Location!;

        HttpResponseMessage response = await Client.GetAsync(location);
        CartEntity data = JsonSerializer.Deserialize<CartEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.Created, putResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, data.DeliveryType);
        Assert.Equal("test", data.DeliveryAddress);
        Assert.Equal(3, data.PaymentType);
        Assert.Equal("testt", data.PaymentDetails);

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task AddProductById_IfExists_GetsAdded()
    {
        Guid testGuid = await MockDataSetup();
        
        //make product to add
        ProductDto testProduct = new()
        {
            Name = "prodAname",
            ImageUrl = "imurl",
            Description = "desc",
            Price = 123,
            Weight = 456,
            Stock = 789
        };
        
        StringContent productContent = new(JsonSerializer.Serialize(testProduct), Encoding.UTF8, "application/json");
        
        HttpResponseMessage request = await Client.PostAsync("/api/Products/add", productContent);
        Guid productGuid = Guid.Parse(request.Content.ReadAsStringAsync().Result.Replace("\"", ""));

        AddToCartDto testAddToCartDto = new()
        {
            ProductId = productGuid,
            Amount = 15
        };
        
        StringContent stringContent = new(JsonSerializer.Serialize(testAddToCartDto), Encoding.UTF8, "application/json");

        HttpResponseMessage postResponse = await Client.PostAsync($"/api/Cart/AddToCart/{testGuid}", stringContent);
        Uri location = postResponse.Headers.Location!;

        HttpResponseMessage response = await Client.GetAsync(location);
        CartEntity data = JsonSerializer.Deserialize<CartEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("prodAname", data.Products.First().Name);
        
        await MockDataDispose(testGuid);
        
        //dispose product
        await Client.DeleteAsync($"/api/Products/delete/{productGuid}");
    }
    
    [Fact]
    public async Task AddProductById_IfMissing_Returns404()
    {
        Guid testGuid = await MockDataSetup();

        AddToCartDto testAddToCartDto = new()
        {
            ProductId = Guid.NewGuid(),
            Amount = 15
        };
        
        StringContent stringContent = new(JsonSerializer.Serialize(testAddToCartDto), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await Client.PostAsync($"/api/Cart/AddToCart/{testGuid}", stringContent);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        
        await MockDataDispose(testGuid);
    }
    
    [Fact]
    public async Task DeleteById_IfExists_GetsDeleted()
    {
        Guid testGuid = await MockDataSetup();
            
        await MockDataDispose(testGuid);

        HttpResponseMessage response = await Client.GetAsync($"/api/Cart/details/{testGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    /*[Fact]//todo finalize
    public async Task FinalizeById_IfExists_GetsFinalized()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/search/prodAname");
        List<ProductEntity> data = JsonSerializer.Deserialize<List<ProductEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal("prodAname", data.First().Name);
        Assert.Equal("imurl", data.First().ImageUrl);
        Assert.Equal("desc", data.First().Description);
        Assert.Equal(123, data.First().Price);
        Assert.Equal(456, data.First().Weight);
        Assert.Equal(789, data.First().Stock);

        await MockDataDispose(testGuid);
    }
    
    [Fact]
    public async Task FinalizeById_IfMissing_Returns404()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/search/prodAname");
        List<ProductEntity> data = JsonSerializer.Deserialize<List<ProductEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal("prodAname", data.First().Name);
        Assert.Equal("imurl", data.First().ImageUrl);
        Assert.Equal("desc", data.First().Description);
        Assert.Equal(123, data.First().Price);
        Assert.Equal(456, data.First().Weight);
        Assert.Equal(789, data.First().Stock);

        await MockDataDispose(testGuid);
    }*/
}