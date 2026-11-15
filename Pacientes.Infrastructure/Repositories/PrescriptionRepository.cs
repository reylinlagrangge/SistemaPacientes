using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Core;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Infrastructure.Repositories;

public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
{
    public PrescriptionRepository(PacientesContext context) : base(context) { }

    public async Task<IEnumerable<Prescription>> GetByMedicalRecordAsync(int medicalRecordId) =>
        await _set.AsNoTracking().Where(p => p.MedicalRecordId == medicalRecordId).ToListAsync();
}
