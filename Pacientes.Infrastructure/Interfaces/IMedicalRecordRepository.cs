using Pacientes.Domain.Entities;
using Pacientes.Domain.Repository;

namespace Pacientes.Infrastructure.Interfaces;

public interface IMedicalRecordRepository : IBaseRepository<MedicalRecord>
{
    Task<IEnumerable<MedicalRecord>> GetByPatientAsync(int patientId);
}
