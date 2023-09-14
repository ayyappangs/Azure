using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureRedisFunc.Modals;
using Microsoft.EntityFrameworkCore;

namespace AzureRedisFunc.DBContext
{
    public class WeatherDb : DbContext
    {
        public WeatherDb(DbContextOptions<WeatherDb> options)
        : base(options)
        {

        }

        public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

    }
}
