namespace MovieReview.Models.DTOs;

public class ApiResponse<T> where T : class
{
    private T? Data { get; set; }
    private bool Success { get; set; }
    private string? Message { get; set; } = string.Empty;

    public ApiResponse(T? data, string? message, bool success)
    {
        Data = data;
        Message = message;
        Success = success;
    }

    public static ApiResponse<T> CreateSuccess(T data, string message = "")
        => new(
            success: true,
            data: data,
            message: message
            );

    public static ApiResponse<T> CreateFailure(string message, T? data = null)
        => new(
            success: false,
            data: data,
            message: message
            );
}