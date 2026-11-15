using Pacientes.Application.Contract;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos.InsuranceProviders;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Application.Services;

public class InsuranceProviderService
    : BaseService<InsuranceProvider, InsuranceProviderDto, SaveInsuranceProviderDto>, IInsuranceProviderService
{
    private readonly IInsuranceProviderRepository _providers;

    public InsuranceProviderService(IInsuranceProviderRepository providers) : base(providers, "aseguradora") =>
        _providers = providers;

    protected override async Task<string?> ValidateAsync(SaveInsuranceProviderDto d, int? id)
    {
        var error = Rules.First(
            Rules.Required(d.Name, "El nombre", 100),
            Rules.Optional(d.Phone, "El teléfono", 20));
        if (error != null) return error;

        var same = await _providers.GetByNameAsync(d.Name.Trim());
        return same != null && same.Id != id ? "Ya existe una aseguradora con ese nombre." : null;
    }

    protected override InsuranceProviderDto ToDto(InsuranceProvider e) =>
        new() { Id = e.Id, Name = e.Name, Phone = e.Phone };

    protected override InsuranceProvider ToEntity(SaveInsuranceProviderDto d)
    { var e = new InsuranceProvider(); Apply(d, e); return e; }

    protected override void Apply(SaveInsuranceProviderDto d, InsuranceProvider e)
    {
        e.Name = d.Name.Trim();
        e.Phone = d.Phone?.Trim();
    }
}
