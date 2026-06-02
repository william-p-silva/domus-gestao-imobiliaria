

namespace Domus.Application.DTOs.ApiResponse;

public class SuccessApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
}
