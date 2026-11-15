using Pacientes.Application.Dtos;

namespace Pacientes.Application.Core;

public interface IBaseService<TDto, TSave>
{
    Task<ServiceResult<List<TDto>>> GetAllAsync();
    Task<ServiceResult<TDto>> GetByIdAsync(int id);
    Task<ServiceResult<TDto>> CreateAsync(TSave dto);
    Task<ServiceResult<TDto>> UpdateAsync(int id, TSave dto);
    Task<ServiceResult<bool>> DeleteAsync(int id);
}
