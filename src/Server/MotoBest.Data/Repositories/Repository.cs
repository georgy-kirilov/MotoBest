using Microsoft.EntityFrameworkCore;

namespace MotoBest.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> dbSet;
    private readonly AppDbContext dbContext;

    public Repository(AppDbContext dbContext)
    {
        dbSet = dbContext.Set<TEntity>();
        this.dbContext = dbContext;
    }

    public IQueryable<TEntity> All() => dbSet;

    public async Task AddAsync(TEntity entity) => await dbSet.AddAsync(entity);

    public void Delete(TEntity entity) => dbSet.Remove(entity);

    public async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();

    public void Update(TEntity entity)
    {
        var entry = dbContext.Entry(entity);

        if (entry.State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }

        entry.State = EntityState.Modified;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            dbContext.Dispose();
        }
    }
}
