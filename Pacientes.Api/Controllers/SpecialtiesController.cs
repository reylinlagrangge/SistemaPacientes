using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Contract;
using Pacientes.Application.Dtos.Specialties;

namespace Pacientes.Api.Controllers;

[Route("api/[controller]")]
public class SpecialtiesController : CrudController<SpecialtyDto, SaveSpecialtyDto>
{
    public SpecialtiesController(ISpecialtyService service) : base(service) { }
}
