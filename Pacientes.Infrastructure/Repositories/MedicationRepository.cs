using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Core;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Infrastructure.Repositories;

public class MedicationRepository : BaseRepository<Medication>, IMedicationRepository
{
    public MedicationRepository(PacientesContext context) : base(context) { }

    public async Task<Medication?> GetByNameAsync(string name) =>
        await _set.AsNoTracking().FirstOrDefaultAsync(m => m.Name == name);
}
