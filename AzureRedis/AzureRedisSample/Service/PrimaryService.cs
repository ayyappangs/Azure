using AzureRedisSample.Database;

namespace AzureRedisSample.Service
{
    public class PrimaryService(IDatabaseService databaseService) : IPrimaryService
    {
        public void Save()
        {
            databaseService.SaveToDatabase();
        }
    }
}
