using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Contract;
using Pacientes.Application.Dtos.MedicalRecords;

namespace Pacientes.Api.Controllers;

[Route("api/[controller]")]
public class MedicalRecordsController : CrudController<MedicalRecordDto, SaveMedicalRecordDto>
{
    private readonly IMedicalRecordService _service;

    public MedicalRecordsController(IMedicalRecordService service) : base(service) => _service = service;

    [HttpGet("by-patient/{patientId:int}")]
    public async Task<IActionResult> GetByPatient(int patientId) =>
        ToActionResult(await _service.GetByPatientAsync(patientId));
}
