using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Core;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Infrastructure.Repositories;

public class SpecialtyRepository : BaseRepository<Specialty>, ISpecialtyRepository
{
    public SpecialtyRepository(PacientesContext context) : base(context) { }

    public async Task<Specialty?> GetByNameAsync(string name) =>
        await _set.AsNoTracking().FirstOrDefaultAsync(s => s.Name == name);
}
