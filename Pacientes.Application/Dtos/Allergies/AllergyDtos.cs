namespace Pacientes.Application.Dtos.Allergies;

public class SaveAllergyDto
{
    public string Name { get; set; } = string.Empty;
    public string? Severity { get; set; }
    public int PatientId { get; set; }
}

public class AllergyDto : SaveAllergyDto, IHasId
{
    public int Id { get; set; }
}
