using Microsoft.AspNetCore.Mvc;
using Pacientes.Application.Core;
using Pacientes.Application.Dtos;

namespace Pacientes.Api.Controllers;

[ApiController]
public abstract class CrudController<TDto, TSave> : ControllerBase where TDto : IHasId
{
    private readonly IBaseService<TDto, TSave> _service;

    protected CrudController(IBaseService<TDto, TSave> service) => _service = service;

    protected IActionResult ToActionResult<T>(ServiceResult<T> result) =>
        result.Success ? Ok(result.Data)
        : result.NotFound ? NotFound(result.Message)
        : BadRequest(result.Message);

    [HttpGet]
    public async Task<IActionResult> GetAll() => ToActionResult(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) => ToActionResult(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(TSave dto)
    {
        var result = await _service.CreateAsync(dto);
        return result.Success
            ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data)
            : ToActionResult(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TSave dto) =>
        ToActionResult(await _service.UpdateAsync(id, dto));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return result.Success ? NoContent() : ToActionResult(result);
    }
}
