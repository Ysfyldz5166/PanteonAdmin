using MongoDB.Driver;

namespace Panteon.DataAcces.MongoDbContexts
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
