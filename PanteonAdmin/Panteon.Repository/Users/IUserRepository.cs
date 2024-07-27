using Panteon.DataAcces.GenericRepository;
using Panteon.Entities.Entities;

namespace Panteon.Repository.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User>  GetByEmailOrUsernameAsync(string emailOrUsername);
        bool IsUsernameUnique(string username);
        bool IsEmailUnique(string email);
    }
}
