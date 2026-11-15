namespace Pacientes.Application.Dtos.Specialties;

public class SaveSpecialtyDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class SpecialtyDto : SaveSpecialtyDto, IHasId
{
    public int Id { get; set; }
}
