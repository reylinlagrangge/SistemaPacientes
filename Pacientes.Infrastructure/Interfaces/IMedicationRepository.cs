using Pacientes.Domain.Entities;
using Pacientes.Domain.Repository;

namespace Pacientes.Infrastructure.Interfaces;

public interface IMedicationRepository : IBaseRepository<Medication>
{
    Task<Medication?> GetByNameAsync(string name);
}
