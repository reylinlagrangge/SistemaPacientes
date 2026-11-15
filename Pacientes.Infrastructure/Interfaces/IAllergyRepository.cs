using Pacientes.Domain.Entities;
using Pacientes.Domain.Repository;

namespace Pacientes.Infrastructure.Interfaces;

public interface IAllergyRepository : IBaseRepository<Allergy>
{
    Task<IEnumerable<Allergy>> GetByPatientAsync(int patientId);
}
