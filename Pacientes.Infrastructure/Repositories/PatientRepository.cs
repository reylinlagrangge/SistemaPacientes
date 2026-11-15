using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Core;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Infrastructure.Repositories;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    public PatientRepository(PacientesContext context) : base(context) { }

    public async Task<Patient?> GetByEmailAsync(string email) =>
        await _set.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email);

    public async Task<Patient?> GetByIdentityNumberAsync(string identityNumber) =>
        await _set.AsNoTracking().FirstOrDefaultAsync(p => p.IdentityNumber == identityNumber);
}
