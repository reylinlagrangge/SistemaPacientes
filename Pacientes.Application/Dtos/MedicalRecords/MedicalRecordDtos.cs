namespace Pacientes.Application.Dtos.MedicalRecords;

public class SaveMedicalRecordDto
{
    public DateTime RecordDate { get; set; }
    public string? Notes { get; set; }
    public string? Treatment { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
}

public class MedicalRecordDto : SaveMedicalRecordDto, IHasId
{
    public int Id { get; set; }
}
