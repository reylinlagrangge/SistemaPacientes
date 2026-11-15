using Pacientes.Domain.Core;

namespace Pacientes.Domain.Entities;

public class Doctor : Person
{
    public string LicenseNumber { get; set; } = string.Empty;

    public int SpecialtyId { get; set; }
    public Specialty Specialty { get; set; } = null!;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
