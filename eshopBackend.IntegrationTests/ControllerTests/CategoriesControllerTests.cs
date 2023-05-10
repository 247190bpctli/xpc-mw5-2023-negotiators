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
        
        StringContent stringContent = new(JsonSerializer.Serialize(test), UnicodeEncoding.UTF8, "application/json");
        
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
        
        List<CategoryEntity> data = JsonSerializer.Deserialize<List<CategoryEntity>>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);

        Assert.NotEmpty(data);
        
        MockDataDispose(testGuid);
    }

        [Fact]
        public async Task GetById_IfExists_ReturnsDetails()
        {
            Guid testGuid = MockDataSetup();

            HttpResponseMessage response = await _client.GetAsync($"/api/Categories/details/{testGuid}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            CategoryEntity data = JsonSerializer.Deserialize<CategoryEntity>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);

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
        
        //not working below
        [Fact]
        public async Task EditById_IfExists_Updates()
        {
            Guid testGuid = MockDataSetup();
            
            Guid testInvalidGuid = Guid.NewGuid();

            HttpResponseMessage response = await _client.GetAsync($"/api/Categories/details/{testInvalidGuid}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            MockDataDispose(testGuid);
        }
        
        [Fact]
        public async Task DeleteById_IfExists_GetsDeleted()
        {
            Guid testGuid = MockDataSetup();
            
            Guid testInvalidGuid = Guid.NewGuid();

            HttpResponseMessage response = await _client.GetAsync($"/api/Categories/details/{testInvalidGuid}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            MockDataDispose(testGuid);
        }
        
        [Fact]
        public async Task SearchByName_IfFound_ReturnsDetails()
        {
            Guid testGuid = MockDataSetup();
            
            Guid testInvalidGuid = Guid.NewGuid();

            HttpResponseMessage response = await _client.GetAsync($"/api/Categories/details/{testInvalidGuid}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            MockDataDispose(testGuid);
        }
        
        [Fact]
        public async Task SearchByName_IfNotFound_Returns404()
        {
            Guid testGuid = MockDataSetup();
            
            Guid testInvalidGuid = Guid.NewGuid();

            HttpResponseMessage response = await _client.GetAsync($"/api/Categories/details/{testInvalidGuid}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            MockDataDispose(testGuid);
        }

        /*[Fact]
        public async Task GetSummary_Always_ReturnsSummary()
        {
            var mockReviews = new BookReview[]
            {
                new(){ Id = 1, Title="A", Rating = 1 },
                new(){ Id = 2, Title="B", Rating = 2 },
                new(){ Id = 3, Title="C", Rating = 5 },
                new(){ Id = 4, Title="B", Rating = 4 }

            }.AsQueryable();

            _factory.ReviewRepositoryMock.Setup(r => r.AllReviews).Returns(mockReviews);

            var response = await _client.GetAsync("/BookReviews/summary");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var data = JsonConvert.DeserializeObject<IEnumerable<BookReview>>(await response.Content.ReadAsStringAsync());


            Assert.Collection(data,
                r =>
                {
                    Assert.Equal("A", r.Title);
                    Assert.Equal(1, r.Rating);
                },
                r =>
                {
                    Assert.Equal("B", r.Title);
                    Assert.Equal(3, r.Rating);
                },
                r =>
                {
                    Assert.Equal("C", r.Title);
                    Assert.Equal(5, r.Rating);
                }
            );
        }

        [Fact]
        public async Task Post_WithValidData_SavesReview()
        {
            var newReview = new BookReview { Title = "NewTitle", Rating = 4 };

            _factory.ReviewRepositoryMock.Setup(r => r.Create(It.Is<BookReview>(b => b.Title == "NewTitle" && b.Rating == 4))).Verifiable();
            _factory.ReviewRepositoryMock.Setup(r => r.SaveChanges()).Verifiable();

            var response = await _client.PostAsync("/BookReviews", JsonContent.Create(newReview));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            _factory.ReviewRepositoryMock.VerifyAll();
        }*/
}