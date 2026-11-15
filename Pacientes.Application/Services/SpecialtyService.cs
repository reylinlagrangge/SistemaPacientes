using Pacientes.Application.Contract;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos.Specialties;
using Pacientes.Domain.Entities;
using Pacientes.Infrastructure.Interfaces;

namespace Pacientes.Application.Services;

public class SpecialtyService : BaseService<Specialty, SpecialtyDto, SaveSpecialtyDto>, ISpecialtyService
{
    private readonly ISpecialtyRepository _specialties;

    public SpecialtyService(ISpecialtyRepository specialties) : base(specialties, "especialidad") =>
        _specialties = specialties;

    protected override async Task<string?> ValidateAsync(SaveSpecialtyDto d, int? id)
    {
        var error = Rules.First(
            Rules.Required(d.Name, "El nombre", 100),
            Rules.Optional(d.Description, "La descripción", 300));
        if (error != null) return error;

        var same = await _specialties.GetByNameAsync(d.Name.Trim());
        return same != null && same.Id != id ? "Ya existe una especialidad con ese nombre." : null;
    }

    protected override SpecialtyDto ToDto(Specialty e) =>
        new() { Id = e.Id, Name = e.Name, Description = e.Description };

    protected override Specialty ToEntity(SaveSpecialtyDto d) { var e = new Specialty(); Apply(d, e); return e; }

    protected override void Apply(SaveSpecialtyDto d, Specialty e)
    {
        e.Name = d.Name.Trim();
        e.Description = d.Description?.Trim();
    }
}
