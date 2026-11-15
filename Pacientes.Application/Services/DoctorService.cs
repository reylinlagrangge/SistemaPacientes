using Pacientes.Application.Contract;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.Doctors;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Application.Services;

public class DoctorService : BaseService<Doctor, DoctorDto, SaveDoctorDto>, IDoctorService
{
    private readonly IDoctorRepository _doctors;
    private readonly ISpecialtyRepository _specialties;

    public DoctorService(IDoctorRepository doctors, ISpecialtyRepository specialties) : base(doctors, "médico")
    {
        _doctors = doctors;
        _specialties = specialties;
    }

    public async Task<ServiceResult<List<DoctorDto>>> GetBySpecialtyAsync(int specialtyId) =>
        ServiceResult<List<DoctorDto>>.Ok(ToDtos(await _doctors.GetBySpecialtyAsync(specialtyId)));

    protected override async Task<string?> ValidateAsync(SaveDoctorDto d, int? id)
    {
        var error = Rules.First(
            Rules.Required(d.FirstName, "El nombre", 100),
            Rules.Required(d.LastName, "El apellido", 100),
            Rules.Email(d.Email),
            Rules.Optional(d.Phone, "El teléfono", 20),
            Rules.BirthDate(d.BirthDate),
            Rules.Required(d.LicenseNumber, "El número de colegiatura", 30));
        if (error != null) return error;

        if (await _specialties.GetByIdAsync(d.SpecialtyId) is null)
            return "La especialidad indicada no existe.";

        var byLicense = await _doctors.GetByLicenseAsync(d.LicenseNumber.Trim());
        if (byLicense != null && byLicense.Id != id) return "Ya existe un médico con ese número de colegiatura.";

        return null;
    }

    protected override DoctorDto ToDto(Doctor e) => new()
    {
        Id = e.Id,
        FirstName = e.FirstName,
        LastName = e.LastName,
        Email = e.Email,
        Phone = e.Phone,
        BirthDate = e.BirthDate,
        LicenseNumber = e.LicenseNumber,
        SpecialtyId = e.SpecialtyId
    };

    protected override Doctor ToEntity(SaveDoctorDto d) { var e = new Doctor(); Apply(d, e); return e; }

    protected override void Apply(SaveDoctorDto d, Doctor e)
    {
        e.FirstName = d.FirstName.Trim();
        e.LastName = d.LastName.Trim();
        e.Email = d.Email.Trim();
        e.Phone = d.Phone?.Trim();
        e.BirthDate = d.BirthDate;
        e.LicenseNumber = d.LicenseNumber.Trim();
        e.SpecialtyId = d.SpecialtyId;
    }
}
