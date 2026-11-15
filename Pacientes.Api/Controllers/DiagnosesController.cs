using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Contract;
using Pacientes.Application.Dtos.Diagnoses;

namespace Pacientes.Api.Controllers;

[Route("api/[controller]")]
public class DiagnosesController : CrudController<DiagnosisDto, SaveDiagnosisDto>
{
    private readonly IDiagnosisService _service;

    public DiagnosesController(IDiagnosisService service) : base(service) => _service = service;

    [HttpGet("by-record/{medicalRecordId:int}")]
    public async Task<IActionResult> GetByRecord(int medicalRecordId) =>
        ToActionResult(await _service.GetByMedicalRecordAsync(medicalRecordId));
}
