using Microsoft.EntityFrameworkCore;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Context;
using Pacientes.Infrastructure.Core;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Infrastructure.Repositories;

public class InsuranceProviderRepository : BaseRepository<InsuranceProvider>, IInsuranceProviderRepository
{
    public InsuranceProviderRepository(PacientesContext context) : base(context) { }

    public async Task<InsuranceProvider?> GetByNameAsync(string name) =>
        await _set.AsNoTracking().FirstOrDefaultAsync(i => i.Name == name);
}
