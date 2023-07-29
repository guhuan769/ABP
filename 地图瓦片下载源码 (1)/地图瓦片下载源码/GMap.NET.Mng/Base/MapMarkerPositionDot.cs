using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace GMap.NET.Mng
{
    public class MapMarkerPositionDot : GMapMarker, ICtrlBase
    {
        public MapMarkerPositionDot(GMap.NET.PointLatLng p, Image image, string ctrlID)
            : this(p, ctrlID)
        {
            Image = image;
            InitImage(image);
        }

        public MapMarkerPositionDot(GMap.NET.PointLatLng p, string ctrlID)
            : base(p)
        {
            _ctrlID = ctrlID;
            _mainID = ctrlID;

            if (this._image == null)
            {
                Size = new System.Drawing.Size(20, 20);
                Pen = new Pen(Color.Black,2);
                Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
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
        private Image _image;
        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                if (_image != null)
                {
                    InitImage(_image);
                }
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
        private Color _ctrlColor = Color.Black;
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

        private int _circleR = 1;
        private int _circleD = 2;

        private void InitImage(Image image)
        {
            if (image.Width <= image.Height)
            {
                _circleR = image.Width / 2;
            }
            else
            {
                _circleR = image.Height / 2;
            }

            _circleD = 2 * _circleR;

            //有效区域取窄边宽度
            Size = new System.Drawing.Size(_circleD, _circleD);
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
        }

        public override void OnRender(Graphics g)
        {
            if (_isEnter)
            {
                _pen.Color = Color.Red;
            }
            else
            {
                _pen.Color = _ctrlColor;
            }

            if (_image == null)
            {
                g.DrawRectangle(_pen, new System.Drawing.Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height));
            }
            else
            {
                //通过有效区域，绘制图片位置
                if (_image.Width <= _image.Height)
                {
                    Rectangle rect = new Rectangle(LocalPosition.X, LocalPosition.Y - (_image.Height - _circleR), _image.Width, _image.Height);
                    g.DrawImage(_image, rect);
                }
                else
                {
                    Rectangle rect = new Rectangle(LocalPosition.X - (_image.Width - _image.Height) / 2, LocalPosition.Y - _circleR, _image.Width, _image.Height);
                    g.DrawImage(_image, rect);
                }

                if (_isEnter)
                {
                    g.DrawEllipse(_pen, LocalPosition.X, LocalPosition.Y, _circleD, _circleD);
                }
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
