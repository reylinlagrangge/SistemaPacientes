using Pacientes.Domain.Entities;
using Pacientes.Domain.Repository;

namespace Pacientes.Infrastructure.Interfaces;

public interface IPrescriptionRepository : IBaseRepository<Prescription>
{
    Task<IEnumerable<Prescription>> GetByMedicalRecordAsync(int medicalRecordId);
}
