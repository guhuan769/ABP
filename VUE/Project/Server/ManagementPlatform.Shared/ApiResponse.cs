using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementPlatform.Shared;

/// <summary>
/// 接口返回模型
/// </summary>
public class ApiResponse
{
    public ApiResponseCode Code { get; set; }

    public string Message { get; set; } = string.Empty;

    public bool Success => Code == ApiResponseCode.Succeed;

    public void IsSuccess(string message = "")
    {
        Code = ApiResponseCode.Succeed;
        Message = message;
    }

    public void IsFailed(string message = "")
    {
        Code = ApiResponseCode.Failed;
        Message = message;
    }

    public void IsFailed(Exception exception)
    {
        Code = ApiResponseCode.Failed;
#pragma warning disable CS8601 // Possible null reference assignment.
        Message = exception.InnerException?.StackTrace;
#pragma warning restore CS8601 // Possible null reference assignment.
    }
}

