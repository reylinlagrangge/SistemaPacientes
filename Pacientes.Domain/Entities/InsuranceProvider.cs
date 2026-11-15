using Pacientes.Domain.Core;

namespace Pacientes.Domain.Entities;

public class InsuranceProvider : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }

    public ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
