

namespace Domus.Application.DTOs.ApiResponse;

public static class ApiResponse
{
    public static SuccessApiResponse<T> Success<T>(T data)
    {
        return SuccessApiResponse<T>.ToResponse(data);
    }
}