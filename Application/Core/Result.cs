namespace Application.Core;

/// <summary>
/// Generic class for managing the results sended by the Application layer, it helps for controlling and validating errors between different layers
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T? Value { get; set; }
    public string Error { get; set; }
    public static Result<T?> Success(T? value) => new () { IsSuccess = true, Value = value };
    public static Result<T?> Failure(string error) => new () { IsSuccess = false, Error = error };
}