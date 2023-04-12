using eshopBackend.DAL.Services;
using eshopBackend.DAL;
using Microsoft.AspNetCore.Mvc;


namespace eshopBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MockDataGeneratorController : ControllerBase
{

    [HttpPost("MakeMockData/{dataAmount}/{seed}")]
    public bool CreateData(byte dataAmount, int? seed)
    {
        return DataAccessLayer.ServiceProvider.GetRequiredService<MockDataGenerator>().MakeMockData(dataAmount,seed);
    }

}