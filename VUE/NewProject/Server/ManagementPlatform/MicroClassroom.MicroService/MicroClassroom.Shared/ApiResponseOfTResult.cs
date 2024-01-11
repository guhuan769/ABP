namespace MicroClassroom.Shared;

/// <summary>
/// 接口返回模型，返回值
/// </summary>
public class ApiResponse<TResult> : ApiResponse where TResult : class
{
    public TResult Result { get; set; }

    public void IsSuccess(TResult result, string message = "")
    {
        Code = ApiResponseCode.Succeed;
        Message = message;
        Result = result;
    }
}
