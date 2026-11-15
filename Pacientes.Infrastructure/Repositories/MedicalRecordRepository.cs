using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Core;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Infrastructure.Repositories;

public class MedicalRecordRepository : BaseRepository<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(PacientesContext context) : base(context) { }

    public async Task<IEnumerable<MedicalRecord>> GetByPatientAsync(int patientId) =>
        await _set.AsNoTracking().Where(m => m.PatientId == patientId).ToListAsync();
}
