using Pacientes.Domain.Core;

namespace Pacientes.Domain.Entities;

public class Patient : Person
{
    public string IdentityNumber { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public string? PolicyNumber { get; set; }

    public int? InsuranceProviderId { get; set; }
    public InsuranceProvider? InsuranceProvider { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
}
