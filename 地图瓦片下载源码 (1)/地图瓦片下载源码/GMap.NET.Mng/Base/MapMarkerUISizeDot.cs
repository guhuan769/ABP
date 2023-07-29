using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace GMap.NET.Mng
{
    public class MapMarkerUISizeDot : GMapMarker, IUISizeDot
    {
        public MapMarkerUISizeDot(GMap.NET.PointLatLng p, int width, int height, string ctrlID)
            : base(p)
        {
            _ctrlID = ctrlID;
            _mainID = ctrlID;
            Size = new System.Drawing.Size(8, 8);
            Pen = new Pen(Color.White, 1);
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
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

        private ECPType _cpType = ECPType.Tool;
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

        private Pen _pen;
        public Pen Pen
        {
            get
            {
                return _pen;
            }
            set
            {
                _pen = value;
            }
        }

        private DotType _type = DotType.WestNorth;
        public DotType Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }

        private System.Windows.Forms.Cursor _cursor;
        public System.Windows.Forms.Cursor Cursor
        {
            get { return _cursor; }
            set
            {
                _cursor = value;
            }
        }

        public override void OnRender(Graphics g)
        {
            if (_pen != null)
            {
                g.FillRectangle(Brushes.Black, new System.Drawing.Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height));
                g.DrawRectangle(_pen, new System.Drawing.Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width - 1, Size.Height - 1));
            }
        }

        public override void Dispose()
        {
            if (_pen != null)
            {
                _pen.Dispose();
                _pen = null;
            }

            base.Dispose();
        }
    }
}
