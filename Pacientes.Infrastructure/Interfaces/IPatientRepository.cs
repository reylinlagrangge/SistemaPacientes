using Pacientes.Domain.Entities;
using Pacientes.Domain.Repository;

namespace Pacientes.Infrastructure.Interfaces;

public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<Patient?> GetByEmailAsync(string email);
    Task<Patient?> GetByIdentityNumberAsync(string identityNumber);
}
