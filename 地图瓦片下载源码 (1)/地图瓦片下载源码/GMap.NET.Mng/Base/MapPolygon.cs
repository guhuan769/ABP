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
    public class MapPolygon : GMapPolygon, ICtrlBase
    {
        public MapPolygon(List<PointLatLng> points, string ctrlID, string text)
            : base(points, ctrlID)
        {
            _ctrlID = ctrlID;
            _mainID = ctrlID;
            _showText = text;

            SetCenter();
        }
        private Label _label;


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
                if (_isEnter || _isFocus)
                {
                    if (_label != null)
                    {
                        _label.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (_label != null)
                    {
                        _label.ForeColor = _ctrlColor;
                    }
                }
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
                if (_isEnter || _isFocus)
                {
                    if (_label != null)
                    {
                        _label.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (_label != null)
                    {
                        _label.ForeColor = _ctrlColor;
                    }
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
                if (_isShowText)
                {
                    if (_label == null)
                    {
                        _label = new Label();
                        _label.AutoSize = true;
                        _label.Font = new Font("宋体", 10, FontStyle.Bold);
                        _label.ForeColor = _ctrlColor;
                        _label.BackColor = Color.Transparent;
                        this.Overlay.Control.Controls.Add(_label);
                    }

                    SetCenter();
                    SetText();
                }
                else
                {
                    if (_label != null)
                    {
                        _label.Visible = false;
                    }
                }
            }
        }

        private string _showText = string.Empty;
        public string ShowText
        {
            get { return _showText; }
            set
            {
                _showText = value;
                if (_isShowText && !string.IsNullOrEmpty(_showText))
                {
                    SetCenter();
                    SetText();
                }
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
        public void UpdateUI(ICtrlBase child) { }
        public void UpdateUI()
        {
            SetCenter();
            SetText();
        }
        public void DeleteCtrl() 
        {
            if (_label != null)
            {
                this.Overlay.Control.Controls.Remove(_label);
            }
        }

        private PointLatLng _center;
        public PointLatLng Center
        {
            get { return _center; }
        }

        public void SetText()
        {
            if (_isShowText && !string.IsNullOrEmpty(_showText))
            {
                GPoint p1 = this.Overlay.Control.FromLatLngToLocal(_center);
                _label.Text = _showText;
                _label.Left = (int)p1.X - _label.Width / 2;
                _label.Top = (int)p1.Y - _label.Height / 2;
                _label.Visible = true;
            }
        }

        public void SetCenter()
        {
            if (this.Points.Count == 0)
            {
                _center = new PointLatLng(0, 0);
            }

            double minLat = double.MaxValue, maxLat = double.MinValue, minLng = double.MaxValue, maxLng = double.MinValue;

            foreach (PointLatLng p in this.Points)
            {
                if (p.Lat < minLat) minLat = p.Lat;
                if (p.Lat > maxLat) maxLat = p.Lat;
                if (p.Lng < minLng) minLng = p.Lng;
                if (p.Lng > maxLng) maxLng = p.Lng;
            }

            double lat = (minLat + maxLat) / 2;
            double lng = (minLng + maxLng) / 2;
            _center = new PointLatLng(lat, lng);
        }
    }
}
