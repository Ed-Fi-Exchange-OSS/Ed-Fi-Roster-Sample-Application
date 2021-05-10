using System.Collections.Generic;
using System.Threading.Tasks;
using EdFi.Roster.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace EdFi.Roster.Services
{
    public interface IRosterDataService
    {
        Task<IEnumerable<TEntity>> ReadAllAsync<TEntity>() where TEntity : class;

        Task SaveAsync<TDataIn>(List<TDataIn> entities) where TDataIn : class;
    }

    public class RosterDataService : IRosterDataService
    {
        private readonly RosterDbContext _dbContext;

        public RosterDataService(RosterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> ReadAllAsync<TEntity>() where TEntity : class
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task SaveAsync<TDataIn>(List<TDataIn> entities) where TDataIn : class
        {
            _dbContext.Set<TDataIn>().RemoveRange(_dbContext.Set<TDataIn>());

            foreach (var entity in entities)
            {
               await _dbContext.Set<TDataIn>().AddAsync(entity);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}