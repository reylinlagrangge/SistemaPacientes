using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Contract;
using Pacientes.Application.Dtos.Patients;

namespace Pacientes.Api.Controllers;

[Route("api/[controller]")]
public class PatientsController : CrudController<PatientDto, SavePatientDto>
{
    public PatientsController(IPatientService service) : base(service) { }
}
