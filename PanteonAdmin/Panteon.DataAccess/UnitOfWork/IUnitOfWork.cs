using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Panteon.DataAcces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        Task<int> SaveAsync();
        DbContext Context { get; }
    }
}
