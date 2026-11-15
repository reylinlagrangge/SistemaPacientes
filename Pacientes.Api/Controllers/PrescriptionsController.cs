using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Contract;
using Pacientes.Application.Dtos.Prescriptions;

namespace Pacientes.Api.Controllers;

[Route("api/[controller]")]
public class PrescriptionsController : CrudController<PrescriptionDto, SavePrescriptionDto>
{
    private readonly IPrescriptionService _service;

    public PrescriptionsController(IPrescriptionService service) : base(service) => _service = service;

    [HttpGet("by-record/{medicalRecordId:int}")]
    public async Task<IActionResult> GetByRecord(int medicalRecordId) =>
        ToActionResult(await _service.GetByMedicalRecordAsync(medicalRecordId));
}
