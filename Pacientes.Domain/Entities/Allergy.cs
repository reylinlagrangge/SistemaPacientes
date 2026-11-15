using Pacientes.Domain.Core;

namespace Pacientes.Domain.Entities;

public class Allergy : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Severity { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
}
