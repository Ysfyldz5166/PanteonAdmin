using Panteon.DataAcces.GenericRepository;
using Panteon.DataAcces.UnitOfWork;
using Panteon.Entities.Entities;
using Panteon.DataAcces.PanteonDbContexts;
using Microsoft.EntityFrameworkCore;

namespace Panteon.Repository.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork uow) : base(uow)
        {
        }
        public async Task<User> GetByEmailOrUsernameAsync(string emailOrUsername)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.Email == emailOrUsername || u.UserName == emailOrUsername);
        }

        public bool IsUsernameUnique(string username)
        {
            return !DbSet.Any(u => u.UserName == username);
        }

        public bool IsEmailUnique(string email)
        {
            return !DbSet.Any(u => u.Email == email);
        }

    }
}
