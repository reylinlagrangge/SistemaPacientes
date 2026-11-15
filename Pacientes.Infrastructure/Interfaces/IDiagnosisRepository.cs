using Pacientes.Domain.Entities;
using Pacientes.Domain.Repository;

namespace Pacientes.Infrastructure.Interfaces;

public interface IDiagnosisRepository : IBaseRepository<Diagnosis>
{
    Task<IEnumerable<Diagnosis>> GetByMedicalRecordAsync(int medicalRecordId);
}
