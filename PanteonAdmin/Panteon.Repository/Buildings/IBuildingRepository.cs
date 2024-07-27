using Panteon.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panteon.Repository.Buildings
{
    public interface IBuildingRepository
    {
        Task<IEnumerable<Build>> GetAllAsync();
        Task<Build> GetByIdAsync(string id);
        Task AddAsync(Build building);
        Task UpdateAsync(Build building);
        Task DeleteAsync(string id);

        Task<bool> BuildingTypeExistsAsync(string buildingType);
    }
}
