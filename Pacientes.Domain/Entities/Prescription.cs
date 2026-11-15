using Pacientes.Domain.Core;

namespace Pacientes.Domain.Entities;

public class Prescription : BaseEntity
{
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public int DurationDays { get; set; }

    public int MedicalRecordId { get; set; }
    public MedicalRecord MedicalRecord { get; set; } = null!;

    public int MedicationId { get; set; }
    public Medication Medication { get; set; } = null!;
}
