using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AzureRedisSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration config;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config)
        {
            _logger = logger;
            this.config = config;
        }



        [HttpPost(Name = "SaveToRedis")]
        public async Task SaveToRedis()
        {
            try
            {

                var lists = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    WeatherForecastId = index,
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
           .ToArray();
                var result = config.GetValue<string>("RedisConnectionString");
                ConnectionMultiplexer connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(result);


                var redisDb = connectionMultiplexer.GetDatabase();
                var isSet = redisDb.StringSet("Weather", JsonConvert.SerializeObject(lists));


            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }



}