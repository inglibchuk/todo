using ToDo.Core;

namespace ToDo.Data;

public class JustInMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly List<TEntity> _entities = new();

    public virtual Task<ICollection<TEntity>> GetAllAsync()
    {
        return Task.FromResult(_entities as ICollection<TEntity>);
    }

    public virtual Task AddAsync(TEntity entity)
    {
        entity.Id = Guid.NewGuid();
        _entities.Add(entity);
        return Task.CompletedTask;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        // it is not correct but,
        // I don't want to write a mapper with reflection to copy properties from the input entity to the exist object
        await DeleteAsync(entity);

        _entities.Add(entity);
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        var existEntity = _entities.FirstOrDefault(x => x.Id == entity.Id);
        if (existEntity != null)
        {
            _entities.Remove(existEntity);
        }
        return Task.CompletedTask;
    }

    public virtual Task<TEntity?> GetByIdAsync(Guid taskId)
    {
        return Task.FromResult(_entities.FirstOrDefault(x => x.Id == taskId));
    }
}

public class TodoTaskRepository : JustInMemoryRepository<TodoTask>
{
    public override async Task AddAsync(TodoTask entity)
    {
        // an emulation of a database level restriction
        var items = await GetAllAsync();

        if (items.Any(x => x.Name == entity.Name))
        {
            throw new UniqueNameException(entity.Name);
        }

        await base.AddAsync(entity);
    }
}

public sealed class TaxonomyRepository : JustInMemoryRepository<Taxonomy>
{
    public static readonly Taxonomy CategoryA = new() { Id = Guid.NewGuid(), Name = "Category A" };
    public static readonly Taxonomy CategoryB = new() { Id = Guid.NewGuid(), Name = "Category B" };
    public static readonly Taxonomy CategoryC = new() { Id = Guid.NewGuid(), Name = "Category C" };

    public TaxonomyRepository()
    {
        AddAsync(CategoryA);
        AddAsync(CategoryB);
        AddAsync(CategoryC);
    }
}

public class UniqueNameException : Exception
{
    public UniqueNameException(string? entityName) : base($"Task's name '{entityName}' is not unique")
    {
    }
}