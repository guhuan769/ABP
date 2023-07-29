using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace GMap.NET.Mng
{
    public class UISizeKnob
    {
        private const int DOT_WIDTH = 8;   //UISizeDot宽度 
        private const int DOT_HEIGHT = 8;  //UISizeDot高度 
        private const int DOT_SPACE = 1;   //UISizeDot与_owner的距离 

        private MapMarkerUISizeDot[] _UISizeDots;

        public MapMarkerUISizeDot[] UISizeDots
        {
            get { return _UISizeDots; }
        }

        private List<MapMarkerPositionDot> _positionDotList;

        private GMapOverlay _overlay;
        private GMapMng _mng;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrlContainerHandler"></param> 
        public UISizeKnob(GMapOverlay overlay, GMapMng mng)
        {
            _overlay = overlay;
            _mng = mng;
            _positionDotList = new List<MapMarkerPositionDot>();
            InitUISizeDots();
        }

        /// <summary> 
        /// 注销 
        /// </summary> 
        public void Dispose()
        {
            for (int i = 0; i < this._UISizeDots.Length; i++)
            {
                this._UISizeDots[i].Dispose();
            }
        }

        private void InitUISizeDots()
        {
            this._UISizeDots = new MapMarkerUISizeDot[8];

            for (int i = 0; i < this._UISizeDots.Length; i++)
            {
                this._UISizeDots[i] = new MapMarkerUISizeDot(new PointLatLng(), DOT_WIDTH, DOT_HEIGHT, Guid.NewGuid().ToString());
                this._UISizeDots[i].IsVisible = false;
                _overlay.Markers.Add(this._UISizeDots[i]);
            }

            //设置类型
            this._UISizeDots[0].Type = DotType.WestNorth;
            this._UISizeDots[1].Type = DotType.North;
            this._UISizeDots[2].Type = DotType.EastNorth;
            this._UISizeDots[3].Type = DotType.East;
            this._UISizeDots[4].Type = DotType.EastSouth;
            this._UISizeDots[5].Type = DotType.South;
            this._UISizeDots[6].Type = DotType.WestSouth;
            this._UISizeDots[7].Type = DotType.West;

            this._UISizeDots[0].Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this._UISizeDots[1].Cursor = System.Windows.Forms.Cursors.SizeNS;
            this._UISizeDots[2].Cursor = System.Windows.Forms.Cursors.SizeNESW;
            this._UISizeDots[3].Cursor = System.Windows.Forms.Cursors.SizeWE;
            this._UISizeDots[4].Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this._UISizeDots[5].Cursor = System.Windows.Forms.Cursors.SizeNS;
            this._UISizeDots[6].Cursor = System.Windows.Forms.Cursors.SizeNESW;
            this._UISizeDots[7].Cursor = System.Windows.Forms.Cursors.SizeWE;
        }

        /// <summary>
        /// 设置捏手的呈现
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="show">呈现状态</param>
        /// <param name="setposition">是否重置位置</param>
        public void ShowUISizeDots(ICtrlDraw ctrl, bool show, bool setposition)
        {
            /*
             * 当前设计：焦点控件显示捏手
             * 焦点切换时，捏手 隐藏 与 显示
             */

            //根据焦点控件
            foreach (MapMarkerUISizeDot UISD in this._UISizeDots)
            {
                UISD.IsVisible = show;
            }

            if (setposition)
            {
                SetUISizeDotsPosition(ctrl);
            }
        }

        public void SetUISizeDotsPosition(ICtrlDraw ctrl)
        {
            SetUISizeDotsPosition(ctrl, null);
        }

        /// <summary>
        /// 设置位置点的呈现
        /// </summary>
        /// <param name="points"></param>
        /// <param name="show">呈现状态</param>
        /// <param name="setposition">是否重置位置</param>
        public void ShowUIPositionDot(List<PointLatLng> points, bool show, bool setposition)
        {
            int pCount = 0;
            if (points != null)
            {
                pCount = points.Count;
                int dotCount = _positionDotList.Count;

                if (pCount > dotCount)
                {
                    for (int i = dotCount; i < pCount; i++)
                    {
                        MapMarkerPositionDot newItem = new MapMarkerPositionDot(new PointLatLng(points[i].Lat, points[i].Lng), Guid.NewGuid().ToString());
                        newItem.IsVisible = false;
                        _positionDotList.Add(newItem);
                        _overlay.Markers.Add(newItem);
                    }
                }
            }

            for (int i = 0; i < _positionDotList.Count; i++)
            {
                if (i < pCount)
                {
                    if (show)
                    {
                        _positionDotList[i].Position = new PointLatLng(points[i].Lat, points[i].Lng);
                    }
                    _positionDotList[i].IsVisible = show;
                }
                else
                {
                    _positionDotList[i].IsVisible = false;
                }
            }
        }

        public int IndexOf(MapMarkerPositionDot dot)
        {
            return _positionDotList.IndexOf(dot);
        }

        /// <summary>
        /// 设置控件捏手的位置-根据焦点控件设置
        /// </summary>
        /// <param name="ctrl"></param>
        public void SetUISizeDotsPosition(ICtrlDraw ctrl, IUISizeDot sizeDot)
        {
            int left, width, height, top, penWidth;
            left = ((GMapMarker)ctrl).LocalPosition.X;
            top = ((GMapMarker)ctrl).LocalPosition.Y;
            width = ((GMapMarker)ctrl).Size.Width;
            height = ((GMapMarker)ctrl).Size.Height;
            penWidth = (int)(ctrl.Pen.Width / 2);

            foreach (MapMarkerUISizeDot UISD in this._UISizeDots)
            {
                if (sizeDot != null && sizeDot.Type == UISD.Type) continue;

                switch (UISD.Type)
                {
                    case DotType.WestNorth:
                        UISD.LocalPosition = new System.Drawing.Point(left - DOT_WIDTH - penWidth - DOT_SPACE, top - DOT_HEIGHT - penWidth - DOT_SPACE);
                        break;
                    case DotType.North:
                        UISD.LocalPosition = new System.Drawing.Point(left + width / 2 - DOT_WIDTH / 2, top - DOT_HEIGHT - penWidth - DOT_SPACE);
                        break;
                    case DotType.EastNorth:
                        UISD.LocalPosition = new System.Drawing.Point(left + width + penWidth, top - DOT_HEIGHT - penWidth - DOT_SPACE);
                        break;
                    case DotType.East:
                        UISD.LocalPosition = new System.Drawing.Point(left + width + penWidth, top + height / 2 - DOT_HEIGHT / 2);
                        break;
                    case DotType.EastSouth:
                        UISD.LocalPosition = new System.Drawing.Point(left + width + penWidth, top + height + penWidth);
                        break;
                    case DotType.South:
                        UISD.LocalPosition = new System.Drawing.Point(left + width / 2 - DOT_WIDTH / 2, top + height + penWidth);
                        break;
                    case DotType.WestSouth:
                        UISD.LocalPosition = new System.Drawing.Point(left - DOT_WIDTH - penWidth - DOT_SPACE, top + height + penWidth);
                        break;
                    case DotType.West:
                        UISD.LocalPosition = new System.Drawing.Point(left - DOT_WIDTH - penWidth - DOT_SPACE, top + height / 2 - DOT_HEIGHT / 2);
                        break;
                    default:
                        break;
                }
            }
        }

        public void UISizeDot_MouseMove(IUISizeDot sender, ICtrlDraw ctrl, int eX, int eY, MouseDownState down)
        {
            try
            {
                GMapMarker marker = (GMapMarker)ctrl;

                if (_mng.IsFixedCenter)
                {
                    eX = 2 * eX;
                    eY = 2 * eY;
                    switch (sender.Type)
                    {
                        case DotType.WestNorth:
                            marker.LocalPosition = new System.Drawing.Point(down.CtrlPositionX + eX, down.CtrlPositionY + eY);
                            ctrl.SetSize(down.CtrlWidth - eX, down.CtrlHeight - eY);
                            break;
                        case DotType.North:
                            marker.LocalPosition = new System.Drawing.Point(down.CtrlPositionX, down.CtrlPositionY + eY);
                            ctrl.SetSize(down.CtrlWidth, down.CtrlHeight - eY);
                            break;
                        case DotType.EastNorth:
                            marker.LocalPosition = new System.Drawing.Point(down.CtrlPositionX, down.CtrlPositionY + eY);
                            ctrl.SetSize(down.CtrlWidth + eX, down.CtrlHeight - eY);
                            break;
                        case DotType.East:
                            marker.LocalPosition = new System.Drawing.Point(down.CtrlPositionX + eX / 2, down.CtrlPositionY);
                            ctrl.SetSize(down.CtrlWidth + eX, down.CtrlHeight);
                            break;
                        case DotType.EastSouth:
                            ctrl.SetSize(down.CtrlWidth + eX, down.CtrlHeight + eY);
                            break;
                        case DotType.South:
                            ctrl.SetSize(down.CtrlWidth, down.CtrlHeight + eY);
                            break;
                        case DotType.WestSouth:
                            marker.LocalPosition = new System.Drawing.Point(down.CtrlPositionX + eX, down.CtrlPositionY);
                            ctrl.SetSize(down.CtrlWidth - eX, down.CtrlHeight + eY);
                            break;
                        case DotType.West:
                            marker.LocalPosition = new System.Drawing.Point(down.CtrlPositionX + eX, down.CtrlPositionY);
                            ctrl.SetSize(down.CtrlWidth - eX, down.CtrlHeight);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    GPoint point = _mng.GMapControl.FromLatLngToLocal(marker.Position);
                    switch (sender.Type)
                    {
                        case DotType.WestNorth:
                            marker.Position = _mng.GMapControl.FromLocalToLatLng(down.CtrlGPointX + eX / 2, down.CtrlGPointY + eY / 2);
                            ctrl.SetSize(down.CtrlWidth - eX, down.CtrlHeight - eY);
                            break;
                        case DotType.North:
                            marker.Position = _mng.GMapControl.FromLocalToLatLng(down.CtrlGPointX, down.CtrlGPointY + eY / 2);
                            ctrl.SetSize(down.CtrlWidth, down.CtrlHeight - eY);
                            break;
                        case DotType.EastNorth:
                            marker.Position = _mng.GMapControl.FromLocalToLatLng(down.CtrlGPointX + eX / 2, down.CtrlGPointY + eY / 2);
                            ctrl.SetSize(down.CtrlWidth + eX, down.CtrlHeight - eY);
                            break;
                        case DotType.East:
                            marker.Position = _mng.GMapControl.FromLocalToLatLng(down.CtrlGPointX + eX / 2, down.CtrlGPointY);
                            ctrl.SetSize(down.CtrlWidth + eX, down.CtrlHeight);
                            break;
                        case DotType.EastSouth:
                            marker.Position = _mng.GMapControl.FromLocalToLatLng(down.CtrlGPointX + eX / 2, down.CtrlGPointY + eY / 2);
                            ctrl.SetSize(down.CtrlWidth + eX, down.CtrlHeight + eY);
                            break;
                        case DotType.South:
                            marker.Position = _mng.GMapControl.FromLocalToLatLng(down.CtrlGPointX, down.CtrlGPointY + eY / 2);
                            ctrl.SetSize(down.CtrlWidth, down.CtrlHeight + eY);
                            break;
                        case DotType.WestSouth:
                            marker.Position = _mng.GMapControl.FromLocalToLatLng(down.CtrlGPointX + eX / 2, down.CtrlGPointY + eY / 2);
                            ctrl.SetSize(down.CtrlWidth - eX, down.CtrlHeight + eY);
                            break;
                        case DotType.West:
                            marker.Position = _mng.GMapControl.FromLocalToLatLng(down.CtrlGPointX + eX / 2, down.CtrlGPointY);
                            ctrl.SetSize(down.CtrlWidth - eX, down.CtrlHeight);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
