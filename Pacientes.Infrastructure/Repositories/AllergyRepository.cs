using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Core;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Infrastructure.Repositories;

public class AllergyRepository : BaseRepository<Allergy>, IAllergyRepository
{
    public AllergyRepository(PacientesContext context) : base(context) { }

    public async Task<IEnumerable<Allergy>> GetByPatientAsync(int patientId) =>
        await _set.AsNoTracking().Where(a => a.PatientId == patientId).ToListAsync();
}
