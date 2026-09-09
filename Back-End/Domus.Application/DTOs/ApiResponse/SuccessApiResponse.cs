

namespace Domus.Application.DTOs.ApiResponse;

public class SuccessApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }

    public static SuccessApiResponse<T> ToResponse<T>(T data)
    {
        return new SuccessApiResponse<T>
        {
            Success = true,
            Data = data
        };
    }
}
