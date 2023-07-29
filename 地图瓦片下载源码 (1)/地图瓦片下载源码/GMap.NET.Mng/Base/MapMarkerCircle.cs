using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace GMap.NET.Mng
{
    [Serializable]
    public class MapMarkerCircle : GMapMarker, ISerializable, ICtrlDraw
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

        private bool _isFilled = false;

        public bool IsFilled
        {
            get { return _isFilled; }
            set 
            { 
                _isFilled = value;
                if(_isFilled)
                {
                    _fill = new SolidBrush(Color.FromArgb(120, Color.AliceBlue));
                }
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
        public void UpdateUI(ICtrlBase child) { }
        public void UpdateUI() { }
        public void DeleteCtrl() { }

        private int _radiusW = 0;

        public int RadiusW
        {
            get { return _radiusW; }
        }
        private int _radiusH = 0;

        public int RadiusH
        {
            get { return _radiusH; }
        }

        private int _w = 100;
        private int _h = 100;

        [NonSerialized]
        private Pen _pen;
        public Pen Pen
        {
            get { return _pen; }
            set { _pen = value; }
        }

        /// <summary>
        /// specifies how the outline is painted
        /// </summary>
        //[NonSerialized]
        //public Pen Stroke = new Pen(Color.FromArgb(155, Color.MidnightBlue));

        /// <summary>
        /// background color
        /// </summary>
        [NonSerialized]
        private Brush _fill = null;

        private Font _font = new Font("宋体", 10, FontStyle.Bold);

        public MapMarkerCircle(PointLatLng p, string ctrlID)
            : base(p)
        {
            _ctrlID = ctrlID;
            _mainID = ctrlID;
            Pen = new Pen(Color.FromArgb(155, Color.MidnightBlue), 2);
        }

        public MapMarkerCircle(PointLatLng p, int radiusW, int radiusH, string mid)
            : this(p, mid)
        {
            _radiusW = radiusW;
            _radiusH = radiusH;
        }

        public void SetSize(int width, int height)
        {
            _w = width;
            _h = height;
            _radiusW = 0;
            _radiusH = 0;
        }

        public override void OnRender(Graphics g)
        {
            if (_radiusW == 0)
            {
                _radiusW = (int)((_w / 2) * Overlay.Control.MapProvider.Projection.GetGroundResolution((int)Overlay.Control.Zoom, Position.Lat));
                _radiusH = (int)((_h / 2) * Overlay.Control.MapProvider.Projection.GetGroundResolution((int)Overlay.Control.Zoom, Position.Lat));
            }
            else
            {
                _w = (int)((_radiusW) / Overlay.Control.MapProvider.Projection.GetGroundResolution((int)Overlay.Control.Zoom, Position.Lat)) * 2;
                _h = (int)((_radiusH) / Overlay.Control.MapProvider.Projection.GetGroundResolution((int)Overlay.Control.Zoom, Position.Lat)) * 2;
            }

            Size = new System.Drawing.Size(_w, _h);
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);

            if (_isFilled)
            {
                g.FillEllipse(_fill, new System.Drawing.Rectangle(LocalPosition.X, LocalPosition.Y, _w, _h));
            }
            if (_isFocus)
            {
                Pen.Color = Color.Red;
                System.Drawing.Pen P = new System.Drawing.Pen(Color.Red);
                P.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(P, new System.Drawing.Rectangle(LocalPosition.X, LocalPosition.Y, _w, _h));
            }
            else
            {
                if (_isEnter)
                {
                    Pen.Color = Color.Red;
                }
                else
                {
                    Pen.Color = _ctrlColor;
                }
            }

            if (_isShowText && !string.IsNullOrEmpty(_showText))
            {
                SizeF siF = g.MeasureString(_showText, _font);
                g.DrawString(_showText, _font, _pen.Brush, LocalPosition.X + Size.Width / 2 - siF.Width / 2, LocalPosition.Y + Size.Height / 2 - siF.Height / 2);
            }

            g.DrawEllipse(Pen, new System.Drawing.Rectangle(LocalPosition.X, LocalPosition.Y, _w, _h));

            //if (_isFocus)
            //{
            //    System.Drawing.Pen P = new System.Drawing.Pen(Color.Red);
            //    P.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //    g.DrawRectangle(P, new System.Drawing.Rectangle(LocalPosition.X, LocalPosition.Y, _w, _h));
            //}
        }

        public override void Dispose()
        {
            if (Pen != null)
            {
                Pen.Dispose();
                Pen = null;
            }

            if (_fill != null)
            {
                _fill.Dispose();
                _fill = null;
            }

            base.Dispose();
        }

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            // TODO: Radius, IsFilled
        }

        protected MapMarkerCircle(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // TODO: Radius, IsFilled
        }

        #endregion

    }
}
