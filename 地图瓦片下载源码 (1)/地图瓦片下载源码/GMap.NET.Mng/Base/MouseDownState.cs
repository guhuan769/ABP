using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMap.NET.Mng
{
    public class MouseDownState
    {
        public bool IsMouseDown = false;
        /// <summary>
        /// 鼠标点击时的屏幕X值
        /// </summary>
        public int ClickAtX = 0;
        /// <summary>
        /// 鼠标点击时的屏幕Y值
        /// </summary>
        public int ClickAtY = 0;
        /// <summary>
        /// 鼠标点击时的控件宽度
        /// </summary>
        public int CtrlWidth = 0;
        /// <summary>
        /// 鼠标点击时的控件高度
        /// </summary>
        public int CtrlHeight = 0;
        /// <summary>
        /// 控件内的起始X值-设置了偏移时为负值
        /// </summary>
        public int CtrlPositionX = 0;
        /// <summary>
        /// 控件内的起始Y值-设置了偏移时为负值
        /// </summary>
        public int CtrlPositionY = 0;
        /// <summary>
        /// 中心坐标屏幕X值
        /// </summary>
        public int CtrlGPointX = 0;
        /// <summary>
        /// 中心坐标屏幕Y值
        /// </summary>
        public int CtrlGPointY = 0;
        List<PointLatLng> _point = new List<PointLatLng>();
        /// <summary>
        /// 控件坐标点
        /// </summary>
        public List<PointLatLng> Points
        {
            get
            {
                return _point;
            }
        }
        /// <summary>
        /// 记录控件坐标点
        /// </summary>
        /// <param name="points"></param>
        public void SetPoints(List<PointLatLng> points)
        {
            _point.Clear();
            foreach (PointLatLng item in points)
            {
                _point.Add(new PointLatLng(item.Lat, item.Lng));
            }
        }
    }
}
