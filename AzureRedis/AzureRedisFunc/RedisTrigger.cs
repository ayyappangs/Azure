using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.Redis;
using StackExchange.Redis;
using AzureRedisFunc.Modals;
using AzureRedisFunc.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AzureRedisFunc
{
    public static class RedisTrigger
    {
        public const string connectionString = "RedisConnectionString";

        [FunctionName("KeyeventTrigger")]
        public static async Task KeyeventTrigger(
             [RedisPubSubTrigger(connectionString, "__keyevent@0__:set")] string message,
             ILogger logger)
        {
            ConnectionMultiplexer connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(Environment.GetEnvironmentVariable(connectionString));

            //Read the cache
            var redisDb = connectionMultiplexer.GetDatabase();
            var value = redisDb.StringGet("Weather");

            //Write to inmemory DB using EF Core
            var weatherForecast = JsonConvert.DeserializeObject<List<WeatherForecast>>(value);
            DbContextOptionsBuilder<WeatherDb> dbContextOptionsBuilder = new DbContextOptionsBuilder<WeatherDb>();
            var options = dbContextOptionsBuilder.UseInMemoryDatabase(databaseName: "weatherdb").Options;
            WeatherDb weatherDb = new WeatherDb(options);

            weatherDb.WeatherForecasts.AddRange(weatherForecast);
        }
    }
}
