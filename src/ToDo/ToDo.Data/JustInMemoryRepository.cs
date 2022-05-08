using ToDo.Core;

namespace ToDo.Data;

public class JustInMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly List<TEntity> _entities = new();

    public Task<ICollection<TEntity>> GetAllAsync()
    {
        return Task.FromResult(_entities as ICollection<TEntity>);
    }
}