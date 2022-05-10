using ToDo.Core;

namespace ToDo.Data;

public interface IRepository<TEntity>
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid taskId);
}