using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Contract;
using Pacientes.Application.Dtos.Doctors;

namespace Pacientes.Api.Controllers;

[Route("api/[controller]")]
public class DoctorsController : CrudController<DoctorDto, SaveDoctorDto>
{
    private readonly IDoctorService _service;

    public DoctorsController(IDoctorService service) : base(service) => _service = service;

    [HttpGet("by-specialty/{specialtyId:int}")]
    public async Task<IActionResult> GetBySpecialty(int specialtyId) =>
        ToActionResult(await _service.GetBySpecialtyAsync(specialtyId));
}
