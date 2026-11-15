using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Contract;
using Pacientes.Application.Dtos.Medications;

namespace Pacientes.Api.Controllers;

[Route("api/[controller]")]
public class MedicationsController : CrudController<MedicationDto, SaveMedicationDto>
{
    public MedicationsController(IMedicationService service) : base(service) { }
}
