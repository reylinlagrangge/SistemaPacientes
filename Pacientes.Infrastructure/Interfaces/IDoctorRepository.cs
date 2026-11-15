using Pacientes.Domain.Entities;
using Pacientes.Domain.Repository;

namespace Pacientes.Infrastructure.Interfaces;

public interface IDoctorRepository : IBaseRepository<Doctor>
{
    Task<IEnumerable<Doctor>> GetBySpecialtyAsync(int specialtyId);
    Task<Doctor?> GetByLicenseAsync(string licenseNumber);
}
