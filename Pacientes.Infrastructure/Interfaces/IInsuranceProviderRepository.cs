using Pacientes.Domain.Entities;
using Pacientes.Domain.Repository;

namespace Pacientes.Infrastructure.Interfaces;

public interface IInsuranceProviderRepository : IBaseRepository<InsuranceProvider>
{
    Task<InsuranceProvider?> GetByNameAsync(string name);
}
