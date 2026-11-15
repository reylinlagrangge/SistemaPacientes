using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Core;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Infrastructure.Repositories;

public class DiagnosisRepository : BaseRepository<Diagnosis>, IDiagnosisRepository
{
    public DiagnosisRepository(PacientesContext context) : base(context) { }

    public async Task<IEnumerable<Diagnosis>> GetByMedicalRecordAsync(int medicalRecordId) =>
        await _set.AsNoTracking().Where(d => d.MedicalRecordId == medicalRecordId).ToListAsync();
}
