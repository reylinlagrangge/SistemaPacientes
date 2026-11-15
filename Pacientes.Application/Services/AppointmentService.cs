using Pacientes.Application.Contract;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.Appointments;
using Pacientes.Domain.Entities;
using Pacientes.Domain.Enums;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Application.Services;

public class AppointmentService : BaseService<Appointment, AppointmentDto, SaveAppointmentDto>, IAppointmentService
{
    private readonly IAppointmentRepository _appointments;
    private readonly IPatientRepository _patients;
    private readonly IDoctorRepository _doctors;

    public AppointmentService(IAppointmentRepository appointments, IPatientRepository patients,
        IDoctorRepository doctors) : base(appointments, "cita")
    {
        _appointments = appointments;
        _patients = patients;
        _doctors = doctors;
    }

    public async Task<ServiceResult<List<AppointmentDto>>> GetByPatientAsync(int patientId) =>
        ServiceResult<List<AppointmentDto>>.Ok(ToDtos(await _appointments.GetByPatientAsync(patientId)));

    public async Task<ServiceResult<List<AppointmentDto>>> GetByDoctorAsync(int doctorId) =>
        ServiceResult<List<AppointmentDto>>.Ok(ToDtos(await _appointments.GetByDoctorAsync(doctorId)));

    protected override async Task<string?> ValidateAsync(SaveAppointmentDto d, int? id)
    {
        var error = Rules.First(
            Rules.Required(d.Reason, "El motivo", 300),
            Enum.IsDefined(typeof(AppointmentStatus), d.Status)
                ? null : "El estado no es válido (1 = programada, 2 = completada, 3 = cancelada).",
            d.AppointmentDate == default ? "La fecha de la cita es requerida." : null,
            id is null && d.Status == (int)AppointmentStatus.Scheduled && d.AppointmentDate.Date < DateTime.Today
                ? "No se puede programar una cita en el pasado." : null);
        if (error != null) return error;

        if (await _patients.GetByIdAsync(d.PatientId) is null) return "El paciente indicado no existe.";
        if (await _doctors.GetByIdAsync(d.DoctorId) is null) return "El médico indicado no existe.";

        if (d.Status != (int)AppointmentStatus.Cancelled)
        {
            var doctorAppointments = await _appointments.GetByDoctorAsync(d.DoctorId);
            if (doctorAppointments.Any(a => a.Id != id
                    && a.AppointmentDate == d.AppointmentDate
                    && a.Status != AppointmentStatus.Cancelled))
                return "El médico ya tiene una cita a esa fecha y hora.";
        }

        return null;
    }

    protected override AppointmentDto ToDto(Appointment e) => new()
    {
        Id = e.Id,
        AppointmentDate = e.AppointmentDate,
        Reason = e.Reason,
        Status = (int)e.Status,
        PatientId = e.PatientId,
        DoctorId = e.DoctorId
    };

    protected override Appointment ToEntity(SaveAppointmentDto d) { var e = new Appointment(); Apply(d, e); return e; }

    protected override void Apply(SaveAppointmentDto d, Appointment e)
    {
        e.AppointmentDate = d.AppointmentDate;
        e.Reason = d.Reason.Trim();
        e.Status = (AppointmentStatus)d.Status;
        e.PatientId = d.PatientId;
        e.DoctorId = d.DoctorId;
    }
}
