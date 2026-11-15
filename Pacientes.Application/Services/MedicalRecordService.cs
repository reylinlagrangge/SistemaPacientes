using Pacientes.Application.Contract;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos;
using Pacientes.Application.Dtos.MedicalRecords;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Application.Services;

public class MedicalRecordService : BaseService<MedicalRecord, MedicalRecordDto, SaveMedicalRecordDto>, IMedicalRecordService
{
    private readonly IMedicalRecordRepository _records;
    private readonly IPatientRepository _patients;
    private readonly IDoctorRepository _doctors;

    public MedicalRecordService(IMedicalRecordRepository records, IPatientRepository patients,
        IDoctorRepository doctors) : base(records, "historial médico")
    {
        _records = records;
        _patients = patients;
        _doctors = doctors;
    }

    public async Task<ServiceResult<List<MedicalRecordDto>>> GetByPatientAsync(int patientId) =>
        ServiceResult<List<MedicalRecordDto>>.Ok(ToDtos(await _records.GetByPatientAsync(patientId)));

    protected override async Task<string?> ValidateAsync(SaveMedicalRecordDto d, int? id)
    {
        var error = Rules.First(
            d.RecordDate == default ? "La fecha del registro es requerida." : null,
            d.RecordDate.Date > DateTime.Today ? "La fecha del registro no puede ser futura." : null,
            Rules.Optional(d.Notes, "Las notas", 1000),
            Rules.Optional(d.Treatment, "El tratamiento", 1000));
        if (error != null) return error;

        if (await _patients.GetByIdAsync(d.PatientId) is null) return "El paciente indicado no existe.";
        if (await _doctors.GetByIdAsync(d.DoctorId) is null) return "El médico indicado no existe.";
        return null;
    }

    protected override MedicalRecordDto ToDto(MedicalRecord e) => new()
    {
        Id = e.Id,
        RecordDate = e.RecordDate,
        Notes = e.Notes,
        Treatment = e.Treatment,
        PatientId = e.PatientId,
        DoctorId = e.DoctorId
    };

    protected override MedicalRecord ToEntity(SaveMedicalRecordDto d) { var e = new MedicalRecord(); Apply(d, e); return e; }

    protected override void Apply(SaveMedicalRecordDto d, MedicalRecord e)
    {
        e.RecordDate = d.RecordDate;
        e.Notes = d.Notes?.Trim();
        e.Treatment = d.Treatment?.Trim();
        e.PatientId = d.PatientId;
        e.DoctorId = d.DoctorId;
    }
}
