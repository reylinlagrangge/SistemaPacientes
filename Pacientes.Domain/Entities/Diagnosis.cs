using Pacientes.Domain.Core;

namespace Pacientes.Domain.Entities;

public class Diagnosis : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int MedicalRecordId { get; set; }
    public MedicalRecord MedicalRecord { get; set; } = null!;
}
