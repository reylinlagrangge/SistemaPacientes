using Pacientes.Domain.Core;

namespace Pacientes.Domain.Entities;

public class MedicalRecord : BaseEntity
{
    public DateTime RecordDate { get; set; }
    public string? Notes { get; set; }
    public string? Treatment { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
