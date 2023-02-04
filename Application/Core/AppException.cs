namespace Application.Core;
/// <summary>
/// Class for standarization of the Exceptions in the application
/// </summary>
public class AppException
{
    public AppException(int statusCode, string message, string details = null)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
}
