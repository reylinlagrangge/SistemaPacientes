using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Contract;
using Pacientes.Application.Dtos.Appointments;

namespace Pacientes.Api.Controllers;

[Route("api/[controller]")]
public class AppointmentsController : CrudController<AppointmentDto, SaveAppointmentDto>
{
    private readonly IAppointmentService _service;

    public AppointmentsController(IAppointmentService service) : base(service) => _service = service;

    [HttpGet("by-patient/{patientId:int}")]
    public async Task<IActionResult> GetByPatient(int patientId) =>
        ToActionResult(await _service.GetByPatientAsync(patientId));

    [HttpGet("by-doctor/{doctorId:int}")]
    public async Task<IActionResult> GetByDoctor(int doctorId) =>
        ToActionResult(await _service.GetByDoctorAsync(doctorId));
}
