using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementPlatform.Shared;


/// <summary>
/// 接口返回编码
/// </summary>
public enum ApiResponseCode : int
{
    /// <summary>
    /// 成功
    /// </summary>
    Succeed = 0,

    /// <summary>
    /// 失败
    /// </summary>
    Failed = 1,

    /// <summary>
    /// 认证异常
    /// </summary>
    AuthorizationException = 401,

    /// <summary>
    /// 验证异常
    /// </summary>
    ValidationException = 411,

    /// <summary>
    /// 未找到实体
    /// </summary>
    EntityNotFoundException = 421,

    /// <summary>
    /// 业务异常
    /// </summary>
    BusinessException = 431

}
