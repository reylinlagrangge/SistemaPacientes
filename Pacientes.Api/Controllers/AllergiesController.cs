using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Contract;
using Pacientes.Application.Dtos.Allergies;

namespace Pacientes.Api.Controllers;

[Route("api/[controller]")]
public class AllergiesController : CrudController<AllergyDto, SaveAllergyDto>
{
    private readonly IAllergyService _service;

    public AllergiesController(IAllergyService service) : base(service) => _service = service;

    [HttpGet("by-patient/{patientId:int}")]
    public async Task<IActionResult> GetByPatient(int patientId) =>
        ToActionResult(await _service.GetByPatientAsync(patientId));
}
