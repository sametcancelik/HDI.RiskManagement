using System.Text.Json.Serialization;

namespace HDI.Application.Common;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    
    [JsonIgnore]
    public int StatusCode { get; set; }

    public static ApiResponse Success(string message = "İşlem başarılı.", int statusCode = 200) 
        => new() { IsSuccess = true, Message = message, StatusCode = statusCode };

    public static ApiResponse Failure(string error, int statusCode = 400) 
        => new() { IsSuccess = false, Errors = new List<string> { error }, StatusCode = statusCode };
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string message = "İşlem başarılı.", int statusCode = 200) 
        => new() { Data = data, IsSuccess = true, Message = message, StatusCode = statusCode };

    public new static ApiResponse<T> Failure(string error, int statusCode = 400) 
        => new() { IsSuccess = false, Errors = new List<string> { error }, StatusCode = statusCode };
}