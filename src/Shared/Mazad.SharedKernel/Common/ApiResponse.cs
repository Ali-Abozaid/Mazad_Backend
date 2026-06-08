namespace Mazad.SharedKernel.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public string? Error { get; set; }
    public IDictionary<string, string[]>? Errors { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message
        };
    }

    public static ApiResponse<T> FailureResponse(string error, string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Error = error,
            Message = message
        };
    }

    public static ApiResponse<T> ValidationFailureResponse(string error, IDictionary<string, string[]> errors)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Error = error,
            Errors = errors
        };
    }
}
