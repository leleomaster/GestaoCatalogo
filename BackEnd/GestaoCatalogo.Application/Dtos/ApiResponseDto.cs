namespace GestaoCatalogo.Application.Dtos;

public class ApiResponseDto<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; private set; }

    private ApiResponseDto(bool success, string? message, T? data, List<string>? errors = null)
    {
        Success = success;
        Message = message;
        Data = data;
        Errors = errors ?? new List<string>();
    }

    public static ApiResponseDto<T?> Ok(T data, string message = "")
    {
        return new ApiResponseDto<T?>(true, message, data);
    }

    public static ApiResponseDto<T?> Fail(string message = "")
    {
        return new ApiResponseDto<T?>(false, message, default);
    }

    public static ApiResponseDto<T?> Created(T data, string message = "Registro criado com sucesso.")
    {
        return new ApiResponseDto<T?>(true, message, data);
    }

    public static ApiResponseDto<T?> Updated(T data, string message = "Registro atualizado com sucesso.")
    {
        return new ApiResponseDto<T?>(true, message, data);
    }

    public static ApiResponseDto<T?> Deleted(string message = "Registro excluído com sucesso.")
    {
        return new ApiResponseDto<T?>(true, message, default);
    }

    public static ApiResponseDto<T?> ValidationFail(List<string> errors, string message = "Falha de validação.")
    {
        return new ApiResponseDto<T?>(false, message, default, errors);
    }
}
