using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace GMap.NET.Mng
{
    public class MapMarkerImage : GMapMarker, ICtrlBase
    {
        public MapMarkerImage(GMap.NET.PointLatLng p, Image image, string ctrlID, OverlayMng overlayMng)
            : base(p)
        {
            _overlayMng = overlayMng;
            _ctrlID = ctrlID;
            _mainID = ctrlID;
            _image = image;
            InitImage(image);
            _pen = new Pen(Color.Red);
            AlarmPen = null;
        }

        private OverlayMng _overlayMng;

        private bool _showImage = true;

        public bool ShowImage
        {
            get { return _showImage; }
            set { _showImage = value; }
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
        public void UpdateUI() 
        {
            if (_cpType == ECPType.Child)
            {
                _overlayMng.GetCtrl(_mainID).UpdateUI();
            }
        }
        public void DeleteCtrl() { }

        private Pen _pen;
        public Pen Pen
        {
            get
            {
                return _pen;
            }
        }

        public Pen AlarmPen
        {
            get;
            set;
        }

        private int _alarmCircleCount = 0;

        public int AlarmCircleCount
        {
            get { return _alarmCircleCount; }
            set { _alarmCircleCount = value; }
        }

        private int _alarmCircleR = 10;

        public int AlarmCircleR
        {
            get { return _alarmCircleR; }
            set { _alarmCircleR = value; }
        }

        private bool _isCenter = false;

        public bool IsCenter
        {
            get { return _isCenter; }
            set { _isCenter = value; }
        }

        private int _circleR = 1;
        private int _circleD = 2;

        private void InitImage(Image image)
        {
            if (image.Width <= image.Height)
            {
                if (_isCenter)
                {
                    _circleR = image.Height / 2;
                }
                else
                {
                    _circleR = image.Width / 2;
                }
            }
            else
            {
                if (_isCenter)
                {
                    _circleR = image.Width / 2;
                }
                else
                {
                    _circleR = image.Height / 2;
                }
            }

            _circleD = 2 * _circleR;

            //有效区域取窄边宽度
            Size = new System.Drawing.Size(_circleD, _circleD);
            Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
        }

        public override void OnRender(Graphics g)
        {
            if (_image == null)
                return;

            if (_showImage)
            {
                if (_isCenter)
                {
                    //通过有效区域，绘制图片位置
                    if (_image.Width <= _image.Height)
                    {
                        Rectangle rect = new Rectangle(LocalPosition.X + (_image.Height - _image.Width) / 2, LocalPosition.Y, _image.Width, _image.Height);
                        g.DrawImage(_image, rect);
                    }
                    else
                    {
                        Rectangle rect = new Rectangle(LocalPosition.X, LocalPosition.Y + (_image.Width - _image.Height) / 2, _image.Width, _image.Height);
                        g.DrawImage(_image, rect);
                    }
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
                }
            }

            if (_isFocus || _isEnter)
            {
                g.DrawEllipse(Pen, LocalPosition.X, LocalPosition.Y, _circleD, _circleD);
            }

            if (AlarmPen != null)
            {
                for (int i = 1; i <= _alarmCircleCount; i++)
                {
                    g.DrawEllipse(AlarmPen, LocalPosition.X + (_circleR - _alarmCircleR) - (i - 1) * _alarmCircleR, LocalPosition.Y + (_circleR - _alarmCircleR) - (i - 1) * _alarmCircleR, 2 * _alarmCircleR * i, 2 * _alarmCircleR * i);
                }
            }
        }

        public override void Dispose()
        {
            if (Pen != null)
            {
                Pen.Dispose();
            }

            if (AlarmPen != null)
            {
                AlarmPen.Dispose();
                AlarmPen = null;
            }

            base.Dispose();
        }
    }
}
