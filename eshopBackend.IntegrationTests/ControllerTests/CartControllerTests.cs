using System.Net;
using System.Text;
using System.Text.Json;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace eshopBackend.IntegrationTests.ControllerTests;

public class CartControllerTests : IntegrationTest
{
    public CartControllerTests(TestWebApplicationFactory fixture) : base(fixture)
    {
    }

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

        using (new AssertionScope())
        {
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Products.Should().BeEmpty(); //????
            data.DeliveryType.Should().Be(default);
            data.DeliveryAddress.Should().BeEmpty();
            data.PaymentType.Should().Be(default);
            data.PaymentDetails.Should().BeEmpty();
        }

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        Guid testGuid = await MockDataSetup();

        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await Client.GetAsync($"/api/Cart/details/{testInvalidGuid}");

        response.Should().HaveStatusCode(HttpStatusCode.NotFound);

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

        using (new AssertionScope())
        {
            putResponse.Should().HaveStatusCode(HttpStatusCode.Created);
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.DeliveryType.Should().Be(2);
            data.DeliveryAddress.Should().Be("test");
            data.PaymentType.Should().Be(3);
            data.PaymentDetails.Should().Be("testt");
        }

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

        using (new AssertionScope())
        {
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Products.First().Name.Should().Be("prodAname");
        }

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

        response.Should().HaveStatusCode(HttpStatusCode.NotFound);

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task DeleteById_IfExists_GetsDeleted()
    {
        Guid testGuid = await MockDataSetup();

        await MockDataDispose(testGuid);

        HttpResponseMessage response = await Client.GetAsync($"/api/Cart/details/{testGuid}");

        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task FinalizeById_IfComplete_GetsFinalized()
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

        HttpResponseMessage finalizeResponse = await Client.GetAsync($"/api/Cart/finalizeOrder/{testGuid}");

        HttpResponseMessage response = await Client.GetAsync($"/api/Cart/details/{testGuid}");
        CartEntity data = JsonSerializer.Deserialize<CartEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        using (new AssertionScope())
        {
            putResponse.Should().HaveStatusCode(HttpStatusCode.Created);
            finalizeResponse.Should().HaveStatusCode(HttpStatusCode.Created);
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Finalized.Should().BeTrue();
        }

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task FinalizeById_IfIncomplete_Returns404()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage finalizeResponse = await Client.GetAsync($"/api/Cart/finalizeOrder/{testGuid}");

        finalizeResponse.Should().HaveStatusCode(HttpStatusCode.InternalServerError);

        await MockDataDispose(testGuid);
    }
}