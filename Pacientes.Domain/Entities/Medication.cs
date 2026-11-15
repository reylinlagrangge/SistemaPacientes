using Pacientes.Domain.Core;

namespace Pacientes.Domain.Entities;

public class Medication : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Presentation { get; set; }
    public string? Description { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
