using Pacientes.Domain.Entities;
using Pacientes.Domain.Repository;

namespace Pacientes.Infrastructure.Interfaces;

public interface ISpecialtyRepository : IBaseRepository<Specialty>
{
    Task<Specialty?> GetByNameAsync(string name);
}
