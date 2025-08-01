public class Result
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public object Data { get; set; }

    public static Result Success(object data = null)
    {
        return new Result
        {
            Message = "Request was successful",
            StatusCode = 200,
            Data = data
        };
    }

    public static Result Failure(string message, int statusCode = 400)
    {
        return new Result
        {
            Message = message,
            StatusCode = statusCode,
            Data = null
        };
    }
}