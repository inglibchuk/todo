using ToDo.Core;

namespace ToDo.Data;

public class JustInMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly List<TEntity> _entities = new();

    public Task<ICollection<TEntity>> GetAllAsync()
    {
        return Task.FromResult(_entities as ICollection<TEntity>);
    }

    public Task AddAsync(TEntity entity)
    {
        _entities.Add(entity);
        return Task.CompletedTask;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        // it is not correct but,
        // I don't want to write a mapper with reflection to copy properties from the input entity to the exist object
        await DeleteAsync(entity);

        _entities.Add(entity);
    }

    public Task DeleteAsync(TEntity entity)
    {
        var existEntity = _entities.FirstOrDefault(x => x.Id == entity.Id);
        if (existEntity != null)
        {
            _entities.Remove(existEntity);
        }
        return Task.CompletedTask;
    }
}