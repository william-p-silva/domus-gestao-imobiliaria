

namespace Domus.Application.DTOs.ApiResponse;

public sealed record ErrorResponseApi
{
    public int Status { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }
}
