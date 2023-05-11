using System.Net;
using System.Text;
using System.Text.Json;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using Xunit;

namespace eshopBackend.IntegrationTests.ControllerTests;

public class ProductsControllerTests : IntegrationTest
{
    public ProductsControllerTests(TestWebApplicationFactory fixture) : base(fixture)
    {
    }

    private async Task<Guid> MockDataSetup()
    {
        ProductDto test = new()
        {
            Name = "prodAname",
            ImageUrl = "imurl",
            Description = "desc",
            Price = 123,
            Weight = 456,
            Stock = 789
        }; //not handling relations

        StringContent stringContent = new(JsonSerializer.Serialize(test), Encoding.UTF8, "application/json");

        HttpResponseMessage request = await Client.PostAsync("/api/Products/add", stringContent);
        string testGuid = request.Content.ReadAsStringAsync().Result.Replace("\"", "");
        return Guid.Parse(testGuid);
    }

    private async Task MockDataDispose(Guid testGuid)
    {
        await Client.DeleteAsync($"/api/Products/delete/{testGuid}");
    }

    [Fact]
    public async Task Get_Always_ReturnsAll()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync("/api/Products/list/1");
        List<ProductEntity> data = JsonSerializer.Deserialize<List<ProductEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(data);

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfExists_ReturnsDetails()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/details/{testGuid}");
        ProductEntity data = JsonSerializer.Deserialize<ProductEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("prodAname", data.Name);
        Assert.Equal("imurl", data.ImageUrl);
        Assert.Equal("desc", data.Description);
        Assert.Equal(123, data.Price);
        Assert.Equal(456, data.Weight);
        Assert.Equal(789, data.Stock);

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        Guid testGuid = await MockDataSetup();

        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/details/{testInvalidGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task EditById_IfExists_Updates()
    {
        Guid testGuid = await MockDataSetup();

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

        HttpResponseMessage putResponse = await Client.PutAsync($"/api/Products/edit/{testGuid}", stringContent);
        Uri location = putResponse.Headers.Location!;

        HttpResponseMessage response = await Client.GetAsync(location);
        ProductEntity data = JsonSerializer.Deserialize<ProductEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.Created, putResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("prodBname", data.Name);
        Assert.Equal("imurl", data.ImageUrl);
        Assert.Equal("desc3", data.Description);
        Assert.Equal(123, data.Price);
        Assert.Equal(789, data.Weight);
        Assert.Equal(456, data.Stock);

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task DeleteById_IfExists_GetsDeleted()
    {
        Guid testGuid = await MockDataSetup();

        await MockDataDispose(testGuid);

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/details/{testGuid}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SearchByName_IfFound_ReturnsList()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync("/api/Products/search/prodAname");
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
    public async Task SearchByName_IfNotFound_ReturnsEmptyList()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync("/api/Products/search/prodBname");
        List<ProductEntity> data = JsonSerializer.Deserialize<List<ProductEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(new List<ProductEntity>(), data);

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task AddReview_Always_ReturnsProductWithReview()
    {
        Guid testGuid = await MockDataSetup();

        AddReviewDto testReview = new()
        {
            Stars = 3,
            User = "revAname",
            Description = "desc3"
        };

        StringContent stringContent = new(JsonSerializer.Serialize(testReview), Encoding.UTF8, "application/json");

        HttpResponseMessage putResponse = await Client.PostAsync($"/api/Products/review/{testGuid}", stringContent);
        Uri location = putResponse.Headers.Location!;

        HttpResponseMessage response = await Client.GetAsync(location);
        ProductEntity data = JsonSerializer.Deserialize<ProductEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        Assert.Equal(HttpStatusCode.Created, putResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(3, data.Reviews.First().Stars);
        Assert.Equal("revAname", data.Reviews.First().User);
        Assert.Equal("desc3", data.Reviews.First().Description);

        await MockDataDispose(testGuid);
    }
}