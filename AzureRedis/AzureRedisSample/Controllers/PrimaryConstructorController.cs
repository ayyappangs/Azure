using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureRedisSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimaryConstructorController : ControllerBase
    {
        [HttpPost(Name = "SampleCall")]
        public async Task SampleCall()
        {

        }
    }
}
