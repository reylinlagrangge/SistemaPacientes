namespace Pacientes.Application.Dtos.Prescriptions;

public class SavePrescriptionDto
{
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public int DurationDays { get; set; }
    public int MedicalRecordId { get; set; }
    public int MedicationId { get; set; }
}

public class PrescriptionDto : SavePrescriptionDto, IHasId
{
    public int Id { get; set; }
}
