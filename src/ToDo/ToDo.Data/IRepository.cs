namespace ToDo.Data;

public interface IRepository<TEntity>
{
    Task<ICollection<TEntity>> GetAllAsync();
}