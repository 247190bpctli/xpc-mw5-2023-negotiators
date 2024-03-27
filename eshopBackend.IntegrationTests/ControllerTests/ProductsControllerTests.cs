using System.Net;
using System.Text;
using System.Text.Json;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
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

        using (new AssertionScope())
        {
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Should().NotBeEmpty();
        }

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfExists_ReturnsDetails()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/details/{testGuid}");
        ProductEntity data = JsonSerializer.Deserialize<ProductEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        using (new AssertionScope())
        {
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Name.Should().Be("prodAname");
            data.ImageUrl.Should().Be("imurl");
            data.Description.Should().Be("desc");
            data.Price.Should().Be(123);
            data.Weight.Should().Be(456);
            data.Stock.Should().Be(789);
        }

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        Guid testGuid = await MockDataSetup();

        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/details/{testInvalidGuid}");

        response.Should().HaveStatusCode(HttpStatusCode.NotFound);

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

        using (new AssertionScope())
        {
            putResponse.Should().HaveStatusCode(HttpStatusCode.Created);
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Name.Should().Be("prodBname");
            data.ImageUrl.Should().Be("imurl");
            data.Description.Should().Be("desc3");
            data.Price.Should().Be(123);
            data.Weight.Should().Be(789);
            data.Stock.Should().Be(456);
        }

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task DeleteById_IfExists_GetsDeleted()
    {
        Guid testGuid = await MockDataSetup();

        await MockDataDispose(testGuid);

        HttpResponseMessage response = await Client.GetAsync($"/api/Products/details/{testGuid}");

        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task SearchByName_IfFound_ReturnsList()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync("/api/Products/search/prodAname");
        List<ProductEntity> data = JsonSerializer.Deserialize<List<ProductEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        using (new AssertionScope())
        {
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.First().Name.Should().Be("prodAname");
            data.First().ImageUrl.Should().Be("imurl");
            data.First().Description.Should().Be("desc");
            data.First().Price.Should().Be(123);
            data.First().Weight.Should().Be(456);
            data.First().Stock.Should().Be(789);
        }

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task SearchByName_IfNotFound_ReturnsEmptyList()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync("/api/Products/search/prodBname");
        List<ProductEntity> data = JsonSerializer.Deserialize<List<ProductEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        using (new AssertionScope())
        {
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Should().BeEmpty();
        }

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

        using (new AssertionScope())
        {
            putResponse.Should().HaveStatusCode(HttpStatusCode.Created);
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Reviews.First().Stars.Should().Be(3);
            data.Reviews.First().User.Should().Be("revAname");
            data.Reviews.First().Description.Should().Be("desc3");
        }

        await MockDataDispose(testGuid);
    }
}