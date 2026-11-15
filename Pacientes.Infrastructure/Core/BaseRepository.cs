using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Core;
using Pacientes.Domain.Repository;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Exceptions;

namespace Pacientes.Infrastructure.Core;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly PacientesContext _context;
    protected readonly DbSet<T> _set;

    public BaseRepository(PacientesContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync() =>
        await _set.AsNoTracking().ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id) =>
        await _set.FindAsync(id);

    public virtual async Task<T> AddAsync(T entity)
    {
        _set.Add(entity);
        await SaveAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _set.Update(entity);
        await SaveAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _set.FindAsync(id);
        if (entity is null) return;
        _set.Remove(entity);
        await SaveAsync();
    }

    private async Task SaveAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new RepositoryException(
                "No se pudo guardar: hay un valor duplicado o el registro está relacionado con otros datos.", ex);
        }
    }
}
