using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET;

namespace GMap.NET.Mng
{
    [Serializable]
    public class MapMarkerChart : GMapMarker, ISerializable, ICtrlDraw, ICtrlBase
    {
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
        private bool _isEnter = false;
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

        private bool _isFocus = false;
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

        private bool _isFilled = false;

        public bool IsFilled
        {
            get { return _isFilled; }
            set
            {
                _isFilled = value;
                if (_isFilled)
                {
                    _fill = new SolidBrush(Color.FromArgb(255, Color.White));
                }
            }
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
        private Color _ctrlColor = Color.FromArgb(155, Color.MidnightBlue);
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
        public void UpdateUI(ICtrlBase child)
        {
            //_route.Points[0] = new PointLatLng(this.Position.Lat, this.Position.Lng);
            _route.Points[1] = new PointLatLng(_marker.Position.Lat, _marker.Position.Lng);
            _overlayMng.Overlay.Control.UpdateRouteLocalPosition(_route);
        }
        public void UpdateUI() 
        {
            _route.Points[0] = new PointLatLng(this.Position.Lat, this.Position.Lng);
            //_route.Points[1] = new PointLatLng(_marker.Position.Lat, _marker.Position.Lng);
            _overlayMng.Overlay.Control.UpdateRouteLocalPosition(_route);
        }

        public void DeleteCtrl() 
        {
            this.Overlay.Routes.Remove(_route);
            this.Overlay.Markers.Remove(_marker);
        }

        private int _w = 300;
        private int _h = 150;

        [NonSerialized]
        private Brush _fill = null;

        [NonSerialized]
        private Pen _pen;
        public Pen Pen
        {
            get { return _pen; }
            set { _pen = value; }
        }
        public ChartMng Chart
        {
            get { return _chart; }
        }

        private OverlayMng _overlayMng;
        private ChartMng _chart;
        private GMapRoute _route;
        private MapMarkerImage _marker;

        public MapMarkerChart(PointLatLng p, string ctrlID, PointLatLng m, OverlayMng overlayMng)
            : base(p)
        {

            _ctrlID = ctrlID;
            _mainID = ctrlID;
            _overlayMng = overlayMng;
            Pen = new Pen(Brushes.Black, 1);
            _chart = new ChartMng(this);

            List<PointLatLng> pList = new List<PointLatLng>();
            pList.Add(this.Position);
            pList.Add(m);
            _route = new GMapRoute(pList, this._ctrlID + "_Route");
            _route.Stroke = new Pen(Color.FromArgb(144, Color.MidnightBlue), 3);
            _overlayMng.Overlay.Routes.Add(_route);

            _marker = new MapMarkerImage(m, Properties.Resources.location_24px_ball, Guid.NewGuid().ToString(), _overlayMng);
            _marker.IsEdit = false;
            _marker.CPType = ECPType.Child;
            _marker.MainID = _ctrlID;
            _overlayMng.Overlay.Markers.Add(_marker);
        }

        public void SetSize(int width, int height)
        {
            _w = width;
            _h = height;
        }

        public override void OnRender(Graphics g)
        {
            Size = new System.Drawing.Size(_w, _h);
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);

            if (_isFocus)
            {
                _pen.Color = Color.Red;
            }
            else
            {
                if (_isEnter)
                {
                    _pen.Color = Color.Red;
                }
                else
                {
                    _pen.Color = _ctrlColor;
                }
            }

            if (_isFilled)
            {
                g.FillRectangle(_fill, new System.Drawing.Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height));
            }

            g.DrawRectangle(_pen, new System.Drawing.Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height));

            _chart.DrawText(g, LocalPosition.X, LocalPosition.Y);
        }

        public override void Dispose()
        {
            if (Pen != null)
            {
                Pen.Dispose();
                Pen = null;
            }

            base.Dispose();
        }

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        protected MapMarkerChart(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}
