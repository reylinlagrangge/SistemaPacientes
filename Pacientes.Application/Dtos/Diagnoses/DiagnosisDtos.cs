namespace Pacientes.Application.Dtos.Diagnoses;

public class SaveDiagnosisDto
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MedicalRecordId { get; set; }
}

public class DiagnosisDto : SaveDiagnosisDto, IHasId
{
    public int Id { get; set; }
}
