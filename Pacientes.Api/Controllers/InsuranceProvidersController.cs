using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Contract;
using Pacientes.Application.Dtos.InsuranceProviders;

namespace Pacientes.Api.Controllers;

[Route("api/[controller]")]
public class InsuranceProvidersController : CrudController<InsuranceProviderDto, SaveInsuranceProviderDto>
{
    public InsuranceProvidersController(IInsuranceProviderService service) : base(service) { }
}
