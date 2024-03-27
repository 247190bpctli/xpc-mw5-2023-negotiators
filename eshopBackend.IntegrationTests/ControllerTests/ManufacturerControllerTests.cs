using System.Net;
using System.Text;
using System.Text.Json;
using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace eshopBackend.IntegrationTests.ControllerTests;

public class ManufacturerControllerTests : IntegrationTest
{
    public ManufacturerControllerTests(TestWebApplicationFactory fixture) : base(fixture)
    {
    }

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

        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/details/{testGuid}");
        ManufacturerEntity data = JsonSerializer.Deserialize<ManufacturerEntity>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        using (new AssertionScope())
        {
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Name.Should().Be("manAname");
            data.Description.Should().Be("desc");
            data.LogoUrl.Should().Be("imurl");
            data.Origin.Should().Be("EU");
        }

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task GetById_IfMissing_Returns404()
    {
        Guid testGuid = await MockDataSetup();

        Guid testInvalidGuid = Guid.NewGuid();

        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/details/{testInvalidGuid}");

        response.Should().HaveStatusCode(HttpStatusCode.NotFound);

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

        using (new AssertionScope())
        {
            putResponse.Should().HaveStatusCode(HttpStatusCode.Created);
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Name.Should().Be("manBname");
            data.Description.Should().Be("desc");
            data.LogoUrl.Should().Be("imurl");
            data.Origin.Should().Be("AU");
        }

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task DeleteById_IfExists_GetsDeleted()
    {
        Guid testGuid = await MockDataSetup();
        await MockDataDispose(testGuid);

        HttpResponseMessage response = await Client.GetAsync($"/api/Manufacturers/details/{testGuid}");

        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task SearchByName_IfFound_ReturnsList()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync("/api/Manufacturers/search/manAname");
        List<ManufacturerEntity> data = JsonSerializer.Deserialize<List<ManufacturerEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        using (new AssertionScope())
        {
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.First().Name.Should().Be("manAname");
            data.First().Description.Should().Be("desc");
            data.First().LogoUrl.Should().Be("imurl");
            data.First().Origin.Should().Be("EU");
        }

        await MockDataDispose(testGuid);
    }

    [Fact]
    public async Task SearchByName_IfNotFound_ReturnsEmptyList()
    {
        Guid testGuid = await MockDataSetup();

        HttpResponseMessage response = await Client.GetAsync("/api/Manufacturers/search/manBname");
        List<ManufacturerEntity> data = JsonSerializer.Deserialize<List<ManufacturerEntity>>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions)!;

        using (new AssertionScope())
        {
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            data.Should().BeEmpty();
        }

        await MockDataDispose(testGuid);
    }
}