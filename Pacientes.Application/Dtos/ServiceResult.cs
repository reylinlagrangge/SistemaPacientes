namespace Pacientes.Application.Dtos;

public class ServiceResult<T>
{
    public bool Success { get; set; }
    public bool NotFound { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };
    public static ServiceResult<T> Fail(string message) => new() { Success = false, Message = message };
    public static ServiceResult<T> NotFoundResult(string message) =>
        new() { Success = false, NotFound = true, Message = message };
}
