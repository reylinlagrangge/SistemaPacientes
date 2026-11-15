using Pacientes.Application.Dtos;
using Pacientes.Domain.Core;
using Pacientes.Domain.Repository;
using Pacientes.Infrastructure.Exceptions;

namespace Pacientes.Application.Core;

public abstract class BaseService<TEntity, TDto, TSave> : IBaseService<TDto, TSave>
    where TEntity : BaseEntity
{
    private readonly IBaseRepository<TEntity> _repo;
    private readonly string _label;

    protected BaseService(IBaseRepository<TEntity> repo, string label)
    {
        _repo = repo;
        _label = label;
    }

    protected abstract TDto ToDto(TEntity entity);
    protected abstract TEntity ToEntity(TSave dto);
    protected abstract void Apply(TSave dto, TEntity entity);
    protected abstract Task<string?> ValidateAsync(TSave dto, int? id);

    protected List<TDto> ToDtos(IEnumerable<TEntity> items) => items.Select(ToDto).ToList();
    protected string NotFoundMessage => $"No se encontró el registro ({_label}).";

    public async Task<ServiceResult<List<TDto>>> GetAllAsync() =>
        ServiceResult<List<TDto>>.Ok(ToDtos(await _repo.GetAllAsync()));

    public async Task<ServiceResult<TDto>> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity is null
            ? ServiceResult<TDto>.NotFoundResult(NotFoundMessage)
            : ServiceResult<TDto>.Ok(ToDto(entity));
    }

    public async Task<ServiceResult<TDto>> CreateAsync(TSave dto)
    {
        var error = await ValidateAsync(dto, null);
        if (error != null) return ServiceResult<TDto>.Fail(error);

        try
        {
            var created = await _repo.AddAsync(ToEntity(dto));
            return ServiceResult<TDto>.Ok(ToDto(created));
        }
        catch (RepositoryException ex)
        {
            return ServiceResult<TDto>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult<TDto>> UpdateAsync(int id, TSave dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return ServiceResult<TDto>.NotFoundResult(NotFoundMessage);

        var error = await ValidateAsync(dto, id);
        if (error != null) return ServiceResult<TDto>.Fail(error);

        Apply(dto, entity);
        try
        {
            await _repo.UpdateAsync(entity);
            return ServiceResult<TDto>.Ok(ToDto(entity));
        }
        catch (RepositoryException ex)
        {
            return ServiceResult<TDto>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult<bool>> DeleteAsync(int id)
    {
        if (await _repo.GetByIdAsync(id) is null)
            return ServiceResult<bool>.NotFoundResult(NotFoundMessage);

        try
        {
            await _repo.DeleteAsync(id);
            return ServiceResult<bool>.Ok(true);
        }
        catch (RepositoryException ex)
        {
            return ServiceResult<bool>.Fail(ex.Message);
        }
    }
}
