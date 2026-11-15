namespace Pacientes.Application.Dtos.Medications;

public class SaveMedicationDto
{
    public string Name { get; set; } = string.Empty;
    public string? Presentation { get; set; }
    public string? Description { get; set; }
}

public class MedicationDto : SaveMedicationDto, IHasId
{
    public int Id { get; set; }
}
