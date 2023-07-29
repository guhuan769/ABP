using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace GMap.NET.Mng
{
    public class MapRouteDistance : GMapRoute, ICtrlBase
    {
        public MapRouteDistance(List<PointLatLng> points, string ctrlID, OverlayMng overlayMng)
            : base(points, ctrlID)
        {
            _ctrlID = ctrlID;
            _mainID = ctrlID;
            _overlayMng = overlayMng;

            foreach (PointLatLng p in base.Points)
            {
                AddMarker(new PointLatLng(p.Lat, p.Lng));
            }

            CalculateDistance();
        }

        private OverlayMng _overlayMng;
        private List<MapMarkerImage> _markers = new List<MapMarkerImage>();

        public List<MapMarkerImage> Markers
        {
            get { return _markers; }
        }

        #region 属性

        private bool _first_Visible = true;

        /// <summary>
        /// 是否显示开始节点
        /// </summary>
        public bool First_Visible
        {
            get { return _first_Visible; }
            set 
            {
                if (_first_Visible != value)
                {
                    _first_Visible = value;
                    CalculateDistance();
                }
            }
        }

        private bool _showTotal = true;

        /// <summary>
        /// 是否显示汇总数据
        /// </summary>
        public bool ShowTotal
        {
            get { return _showTotal; }
            set 
            {
                if (_showTotal != value)
                {
                    _showTotal = value;
                    CalculateDistance();
                }
            }
        }

        private string _ctrlID = string.Empty;
        public string CtrlID
        {
            get
            {
                return _ctrlID;
            }
        }
        private string _mainID = string.Empty;
        public string MainID
        {
            get
            {
                return _mainID;
            }
            set
            {
                _mainID = value;
            }
        }

        private ECPType _cpType = ECPType.Main;
        public ECPType CPType
        {
            get
            {
                return _cpType;
            }
            set
            {
                _cpType = value;
            }
        }
        public bool IsFocus
        {
            get
            {
                return _isFocus;

            }
            set
            {
                _isFocus = value;
            }
        }

        public bool IsEnter
        {
            get
            {
                return _isEnter;
            }
            set
            {
                _isEnter = value;
            }
        }
        private bool _isEdit = true;
        public bool IsEdit
        {
            get
            {
                return _isEdit;
            }
            set
            {
                _isEdit = value;
            }
        }

        private bool _isMove = true;
        public bool IsMove
        {
            get
            {
                return _isMove;
            }
            set
            {
                _isMove = value;
            }
        }

        private bool _isFilled = false;
        public bool IsFilled
        {
            get { return _isFilled; }
            set { _isFilled = value; }
        }

        private bool _isClick = false;
        public bool IsClick
        {
            get { return _isClick; }
            set { _isClick = value; }
        }

        private bool _isDoubleClick = false;
        public bool IsDoubleClick
        {
            get { return _isDoubleClick; }
            set { _isDoubleClick = value; }
        }
        private bool _isShowText = false;
        public bool IsShowText
        {
            get { return _isShowText; }
            set
            {
                _isShowText = value;
            }
        }

        private string _showText = string.Empty;
        public string ShowText
        {
            get { return _showText; }
            set
            {
                _showText = value;
            }
        }

        public Color CtrlColor
        {
            get
            {
                return _ctrlColor;
            }
            set
            {
                _ctrlColor = value;
            }
        }

        #endregion
        /// <summary>
        /// 更新控件UI，子控件位置变化时调用
        /// </summary>
        /// <param name="child"></param>
        public void UpdateUI(ICtrlBase child) 
        {
            if(child is MapMarkerImage)
            {
                int index = _markers.IndexOf((MapMarkerImage)child);
                if (index >= 0)
                {
                    base.Points[index] = new PointLatLng(_markers[index].Position.Lat, _markers[index].Position.Lng);
                    _overlayMng.Overlay.Control.UpdateRouteLocalPosition(this);
                }

                CalculateDistance();
            }
        }
        public void UpdateUI()
        {

        }
        /// <summary>
        /// 删除子控件，控件删除时调用
        /// </summary>
        public void DeleteCtrl()
        {
            foreach (MapMarkerImage marker in _markers)
            {
                _overlayMng.Overlay.Markers.Remove(marker);
            }

            _markers.Clear();
        }

        /// <summary>
        /// 新增路径点
        /// </summary>
        /// <param name="point"></param>
        public void AddPoint(PointLatLng point)
        {
            base.Points.Add(new PointLatLng(point.Lat, point.Lng));
            AddMarker(new PointLatLng(point.Lat, point.Lng));
            CalculateDistance();
        }

        /// <summary>
        /// 删除路径点
        /// </summary>
        /// <param name="index"></param>
        public void DeletePoint(int index)
        {
            base.Points.RemoveAt(index);
            _overlayMng.Overlay.Markers.Remove(_markers[index]);
            this._markers.RemoveAt(index);
            CalculateDistance();

        }

        public void CalculateDistance()
        {
            double total = 0;
            double temp = 0;
            for (int i = 0; i < _markers.Count; i++)
            {
                temp = 0;
                _markers[i].ToolTipMode = MarkerTooltipMode.Always;

                if (i == 0)
                {
                    _markers[0].ToolTipText = "起点";
                    _markers[0].ShowImage = _first_Visible;
                    if (!_first_Visible)
                    {
                        _markers[0].ToolTipMode = MarkerTooltipMode.Never;
                    }
                }
                else
                {
                    temp = Common.GetDistance(_markers[i - 1].Position.Lat, _markers[i - 1].Position.Lng, _markers[i].Position.Lat, _markers[i].Position.Lng);
                    total += temp;
                    _markers[i].ToolTipText = GetText(temp, total);
                }
            }
        }

        private string GetText(double m1,double m2)
        {
            string text = string.Empty;
            if (_showTotal)
            {
                text = string.Format("{0}公里,总:{1}公里", Math.Round(m1, 1).ToString(), Math.Round(m2, 1).ToString());
            }
            else
            {
                text = string.Format("{0}公里", Math.Round(m1, 1).ToString());
            }
              
            return text;
        }

        private void AddMarker(PointLatLng point)
        {
            MapMarkerImage _marker = new MapMarkerImage(point, Properties.Resources.location_24px_ball, Guid.NewGuid().ToString(), _overlayMng);
            _marker.IsEdit = false;
            _marker.CPType = ECPType.Child;
            _marker.MainID = _ctrlID;
            _overlayMng.Overlay.Markers.Add(_marker);

            _markers.Add(_marker);
        }
    }
}
