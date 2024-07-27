using MongoDB.Driver;
using Panteon.DataAcces.MongoDbContexts;
using Panteon.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panteon.Repository.Buildings
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly IMongoCollection<Build> _buildings;

        public BuildingRepository(IMongoDbContext context)
        {
            _buildings = context.GetCollection<Build>("Buildings");
        }

        public async Task<IEnumerable<Build>> GetAllAsync()
        {
            return await _buildings.Find(Builders<Build>.Filter.Empty).ToListAsync();
        }

        public async Task<Build> GetByIdAsync(string id)
        {
            return await _buildings.Find(Builders<Build>.Filter.Eq(b => b.Id, id)).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Build building)
        {
            await _buildings.InsertOneAsync(building);
        }

        public async Task UpdateAsync(Build building)
        {
            var filter = Builders<Build>.Filter.Eq(b => b.Id, building.Id);
            await _buildings.ReplaceOneAsync(filter, building);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Build>.Filter.Eq(b => b.Id, id);
            await _buildings.DeleteOneAsync(filter);
        }

        public async Task<bool> BuildingTypeExistsAsync(string buildingType)
        {
            var filter = Builders<Build>.Filter.Eq(b => b.BuildingType, buildingType);
            return await _buildings.Find(filter).AnyAsync();
        }
    }
}
