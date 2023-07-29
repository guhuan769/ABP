using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMap.NET.Mng
{
    public enum EMapType { 高德, 百度, 谷歌 }

    public enum ECreateType
    {
        None,
        Marker,
        AlarmMarker,
        Rectangle,
        Circle,
        Polygon,
        DownRectangle,
        Chart,
        Route
    }

    public enum ECPType
    {
        /// <summary>
        /// 主控件
        /// </summary>
        Main,
        /// <summary>
        /// 子控件
        /// </summary>
        Child,
        /// <summary>
        /// 工具辅助控件
        /// </summary>
        Tool
    }

    public enum EOpType
    {
        None,
        /// <summary>
        /// 绘制线路
        /// </summary>
        DrawRoute,
        /// <summary>
        /// 绘制区域
        /// </summary>
        DrawPolygon
    }

    /// <summary>
    /// 捏手类型-捏手位置
    /// </summary>
    public enum DotType
    {
        /// <summary>
        /// 西北方向
        /// </summary>
        WestNorth = 0,

        /// <summary>
        /// 北方向
        /// </summary>
        North = 1,

        /// <summary>
        /// 东北方向
        /// </summary>
        EastNorth = 2,

        /// <summary>
        /// 东方向
        /// </summary>
        East = 3,

        /// <summary>
        /// 东南方向
        /// </summary>
        EastSouth = 4,

        /// <summary>
        /// 南方向
        /// </summary>
        South = 5,

        /// <summary>
        /// 西南方向
        /// </summary>
        WestSouth = 6,

        /// <summary>
        /// 西方向
        /// </summary>
        West = 7
    }
}
