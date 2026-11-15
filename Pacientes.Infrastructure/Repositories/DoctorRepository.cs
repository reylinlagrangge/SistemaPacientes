using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Core;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Infrastructure.Repositories;

public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(PacientesContext context) : base(context) { }

    public async Task<IEnumerable<Doctor>> GetBySpecialtyAsync(int specialtyId) =>
        await _set.AsNoTracking().Where(d => d.SpecialtyId == specialtyId).ToListAsync();

    public async Task<Doctor?> GetByLicenseAsync(string licenseNumber) =>
        await _set.AsNoTracking().FirstOrDefaultAsync(d => d.LicenseNumber == licenseNumber);
}
