namespace ToDo.Data;

public class JustInMemoryRepository<TEntity> : IRepository<TEntity>
{
    private readonly List<TEntity> _entities = new();

    public Task<ICollection<TEntity>> GetAllAsync()
    {
        return Task.FromResult(_entities as ICollection<TEntity>);
    }
}