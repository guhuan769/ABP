using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using GMap.NET.WindowsForms;

namespace GMap.NET.Mng
{
    public class GMapMng
    {
        public GMapMng(GMapControl gMapControl)
        {
            _gMapControl = gMapControl;
            _overlayCollection = new OverlayMngCollection(this);
            _ctrlList = new Dictionary<string, string>();

            _gMapControl.OnPositionChanged += gMapControl1_OnPositionChanged;
            _gMapControl.OnMapZoomChanged += gMapControl_OnMapZoomChanged;
            _gMapControl.MouseDown += gMapControl1_MouseDown;
            _gMapControl.MouseMove += gMapControl1_MouseMove;
            _gMapControl.MouseUp += gMapControl1_MouseUp;
            _gMapControl.MouseDoubleClick += gMapControl1_MouseDoubleClick;

            _gMapControl.OnMarkerDoubleClick += gMapControl1_OnMarkerDoubleClick;
            _gMapControl.OnMarkerClick += gMapControl1_OnMarkerClick;
            _gMapControl.OnMarkerEnter += gMapControl1_OnMarkerEnter;
            _gMapControl.OnMarkerLeave += gMapControl1_OnMarkerLeave;

            _gMapControl.OnPolygonClick += gMapControl_OnPolygonClick;
            _gMapControl.OnPolygonEnter += gMapControl_OnPolygonEnter;
            _gMapControl.OnPolygonLeave += gMapControl_OnPolygonLeave;

            _gMapControl.OnRouteClick += gMapControl_OnRouteClick;
            _gMapControl.OnRouteEnter += gMapControl_OnRouteEnter;
            _gMapControl.OnRouteLeave += gMapControl_OnRouteLeave;


            _sizeKnob = new GMapOverlay("SizeKnob");
            _gMapControl.Overlays.Add(_sizeKnob);
            _UISKnob = new UISizeKnob(_sizeKnob, this);
            _alarmMng = new AlarmMng(this);

            //_gMapControl.CanDragMap = false;
        }

        public delegate void MapPositionChangedHandle(PointLatLng point);
        public delegate void MapClickHandle(GMapMarker item, MouseEventArgs e);
        public delegate void MapMouseMoveHandle(object sender, MouseEventArgs e);
        public event MapPositionChangedHandle MapPositionChangedEvent;
        public event MapPositionChangedHandle MapMouseDownPositionEvent;
        public event MapPositionChangedHandle MapMouseUpPositionEvent;
        public event MapClickHandle MapCtrlDoubleClickEvent;
        public event MapMouseMoveHandle MapMouseMoveEvent;

        private GMapControl _gMapControl;
        private Dictionary<string, string> _ctrlList; //控件对应所在图层
        private ICtrlBase _currentCtrl;
        private ICtrlBase _focusCtrl;
        private UISizeKnob _UISKnob;
        private GMapOverlay _sizeKnob;
        private AlarmMng _alarmMng;
        private ICtrlBase _newCtrl = null;

        public ECreateType CreateType = ECreateType.None;
        public bool IsEdit = true;
        public bool IsFixedCenter = false; //是否编辑控件时，固定中心

        public ICtrlBase FocusCtrl
        {
            get { return _focusCtrl; }
        }

        public ICtrlBase CurrentCtrl
        {
            get { return _currentCtrl; }
        }

        public GMapControl GMapControl
        {
            get { return _gMapControl; }
        }

        public AlarmMng AlarmMng
        {
            get { return _alarmMng; }
        }

        private OverlayMngCollection _overlayCollection;
        public OverlayMngCollection OverlayCollection
        {
            get { return _overlayCollection; }
        }

        public ICtrlBase GetCtrl(string mid)
        {
            if (_ctrlList.ContainsKey(mid))
            {
                return _overlayCollection[_ctrlList[mid]].GetCtrl(mid);
            }
            else
            {
                return null;
            }
        }

        public void AddCtrl(ICtrlBase ctrl)
        {
            if (ctrl != null
                && !string.IsNullOrEmpty(_overlayCollection.CurrentOverlayID)
                && !_ctrlList.ContainsKey(ctrl.CtrlID))
            {
                _ctrlList.Add(ctrl.CtrlID, _overlayCollection.CurrentOverlayID);
                _overlayCollection[_overlayCollection.CurrentOverlayID].AddCtrl(ctrl);
            }
        }

        public void DeleteCtrl()
        {
            if (_focusCtrl != null)
            {
                string ctrlID = _focusCtrl.CtrlID;
                RestCtrl();
                _overlayCollection[_ctrlList[ctrlID]].DeleteCtrl(ctrlID);
                _ctrlList.Remove(ctrlID);
            }
        }

        public void DeleteDot()
        {
            if (_focusCtrl != null
                && _focusCtrl is MapPolygon 
                && _currentCtrl != null 
                && _currentCtrl is MapMarkerPositionDot
                && ((MapPolygon)_focusCtrl).Points.Count > 3)
            {
                int index = _UISKnob.IndexOf((MapMarkerPositionDot)_currentCtrl);
                if (index >= 0)
                {
                    ((MapPolygon)_focusCtrl).Points.RemoveAt(index);
                    _gMapControl.UpdatePolygonLocalPosition((GMapPolygon)_focusCtrl);
                    _UISKnob.ShowUIPositionDot(((GMapPolygon)_focusCtrl).Points, true, true);
                    _focusCtrl.UpdateUI();
                }
            }
        }

        public void DeleteRouteChild()
        {
            if (_focusCtrl != null
                && _focusCtrl is MapRouteDistance
                && ((MapRouteDistance)_focusCtrl).Points.Count > 2
                && _currentCtrl != null
                && _currentCtrl.CPType == ECPType.Child
                && _overlayCollection.CurrentOverlayMng.GetCtrl(_currentCtrl.MainID) == _focusCtrl)
            {
                int index = ((MapRouteDistance)_focusCtrl).Markers.IndexOf((MapMarkerImage)_currentCtrl);
                if (index >= 0)
                {
                    ((MapRouteDistance)_focusCtrl).DeletePoint(index);
                    _gMapControl.UpdateRouteLocalPosition((MapRouteDistance)_focusCtrl);
                }
            }
        }

        public void DeleteAllCtrl()
        {
            _overlayCollection.DeleteAllCtrl();
            _ctrlList.Clear();
            _focusCtrl = null;
            _UISKnob.ShowUISizeDots(null, false, false);
        }

        public void RestCtrl()
        {
            _UISKnob.ShowUISizeDots(null, false, false);
            _UISKnob.ShowUIPositionDot(null, false, false);
            if (_focusCtrl != null)
            {
                _focusCtrl.IsFocus = false;
                _focusCtrl = null;
            }
            if (_currentCtrl != null)
            {
                _currentCtrl.IsEnter = false;
                _currentCtrl = null;
            }
            _newCtrl = null;
            CreateType = ECreateType.None;
        }

        public void SetTipText()
        {
            if (_focusCtrl != null && _focusCtrl is GMapMarker)
            {
                Form_ShowText form = new Form_ShowText("设置提示信息");
                form.ShowText = ((GMapMarker)_focusCtrl).ToolTipText;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(form.ShowText))
                    {
                        ((GMapMarker)_focusCtrl).ToolTipMode = MarkerTooltipMode.Always;
                        ((GMapMarker)_focusCtrl).ToolTipText = form.ShowText;
                    }
                    else
                    {
                        ((GMapMarker)_focusCtrl).ToolTipMode = MarkerTooltipMode.Never;
                    }
                } 
            }
        }

        public void SetShowText()
        {
            if (_focusCtrl != null)
            {
                Form_ShowText form = new Form_ShowText("设置文本信息");
                form.ShowText = _focusCtrl.ShowText;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _focusCtrl.ShowText = form.ShowText;
                }
            }
        }

        #region 地图事件

        private ICtrlBase CreateCtrl(PointLatLng point)
        {
            ICtrlBase ctrl = null;

            switch (CreateType)
            {
                case ECreateType.Marker:
                    ctrl = new MapMarkerImage(new PointLatLng(point.Lat, point.Lng), Properties.Resources.Location_24px, Guid.NewGuid().ToString(), _overlayCollection.CurrentOverlayMng);
                    AddCtrl(ctrl);
                    break;
                case ECreateType.AlarmMarker:
                    ctrl = new MapMarkerImage(new PointLatLng(point.Lat, point.Lng), Properties.Resources.Alarm_24px, Guid.NewGuid().ToString(), _overlayCollection.CurrentOverlayMng);
                    AddCtrl(ctrl);
                    _alarmMng.AddCtrl((MapMarkerImage)ctrl);
                    break;
                case ECreateType.Rectangle:
                    ctrl = new MapMarkerRect(new PointLatLng(point.Lat, point.Lng), Guid.NewGuid().ToString());
                    ctrl.IsShowText = true;
                    AddCtrl(ctrl);
                    break;
                case ECreateType.Circle:
                    ctrl = new MapMarkerCircle(new PointLatLng(point.Lat, point.Lng), Guid.NewGuid().ToString());
                    ctrl.IsShowText = true;
                    ctrl.IsFilled = true;
                    AddCtrl(ctrl);
                    break;
                case ECreateType.Polygon:
                    List<PointLatLng> points = new List<PointLatLng>();
                    GPoint p = _gMapControl.FromLatLngToLocal(point);
                    points.Add(new PointLatLng(point.Lat, point.Lng));
                    points.Add(_gMapControl.FromLocalToLatLng((int)p.X + 100, (int)p.Y));
                    points.Add(_gMapControl.FromLocalToLatLng((int)p.X + 100, (int)p.Y + 100));
                    ctrl = new MapPolygon(points, Guid.NewGuid().ToString(), "");
                    AddCtrl(ctrl);
                    ctrl.IsShowText = true;
                    break;
                case ECreateType.Chart:
                    GPoint chartPoint = _gMapControl.FromLatLngToLocal(point);
                    ctrl = new MapMarkerChart(_gMapControl.FromLocalToLatLng((int)chartPoint.X - 50, (int)chartPoint.Y - 200), Guid.NewGuid().ToString(), point, _overlayCollection.CurrentOverlayMng);
                    ctrl.IsFilled = true;
                    AddCtrl(ctrl);
                    break;
                case ECreateType.Route:
                    List<PointLatLng> routePoints = new List<PointLatLng>();
                    GPoint gp = _gMapControl.FromLatLngToLocal(point);
                    routePoints.Add(new PointLatLng(point.Lat, point.Lng));
                    routePoints.Add(_gMapControl.FromLocalToLatLng((int)gp.X + 100, (int)gp.Y));
                    ctrl = new MapRouteDistance(routePoints, Guid.NewGuid().ToString(), _overlayCollection.CurrentOverlayMng);
                    ((MapRouteDistance)ctrl).Stroke = new Pen(Color.FromArgb(144, Color.MidnightBlue), 3);
                    ((MapRouteDistance)ctrl).IsHitTestVisible = true;
                    AddCtrl(ctrl);
                    break;
                case ECreateType.DownRectangle:
                    ctrl = new MapMarkerRect(new PointLatLng(point.Lat, point.Lng), Guid.NewGuid().ToString());
                    ((MapMarkerRect)ctrl).SetSize(200, 200);
                    ctrl.IsShowText = true;
                    ctrl.ShowText = "双击下载";
                    ctrl.IsFilled = true;
                    ctrl.IsDoubleClick = true;
                    AddCtrl(ctrl);
                    break;
            }

            return ctrl;
        }

        private void gMapControl_OnMapZoomChanged()
        {
            _overlayCollection.OnMapPositionChanged();
            _UISKnob.ShowUISizeDots(null, false, false);
        }
        private void gMapControl1_OnPositionChanged(PointLatLng point)
        {
            _overlayCollection.OnMapPositionChanged();

            if (MapPositionChangedEvent != null)
            {
                MapPositionChangedEvent(point);
            }

            this._gMapControl.Refresh();
        }

        private MouseDownState _mouseDownState = new MouseDownState();

        private void gMapControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointLatLng point = _gMapControl.FromLocalToLatLng(e.X, e.Y);


                //if (CreateType != ECreateType.None && !(_currentCtrl != null && _currentCtrl.CPType == ECPType.Tool))
                if (CreateType != ECreateType.None && (_currentCtrl == null || _currentCtrl.CPType == ECPType.Main))
                {
                    if (CreateType == ECreateType.Polygon && _focusCtrl != null && _focusCtrl is MapPolygon)
                    {
                        ((MapPolygon)_focusCtrl).Points.Add(new PointLatLng(point.Lat, point.Lng));
                        _gMapControl.UpdatePolygonLocalPosition((GMapPolygon)_focusCtrl);
                        _UISKnob.ShowUIPositionDot(((GMapPolygon)_focusCtrl).Points, true, true);
                        _focusCtrl.UpdateUI();
                    }
                    else if (CreateType == ECreateType.Route && _focusCtrl != null && _focusCtrl is MapRouteDistance)
                    {
                        ((MapRouteDistance)_focusCtrl).AddPoint(new PointLatLng(point.Lat, point.Lng));
                        _gMapControl.UpdateRouteLocalPosition((GMapRoute)_focusCtrl);
                    }
                    else
                    {
                        _newCtrl = CreateCtrl(point);
                    }

                    if (CreateType != ECreateType.Polygon && CreateType != ECreateType.Route)
                    {
                        CreateType = ECreateType.None;
                    }

                    return;
                }

                _mouseDownState.IsMouseDown = true;
                _mouseDownState.ClickAtX = e.X;
                _mouseDownState.ClickAtY = e.Y;

                if (_currentCtrl == null || _currentCtrl.CPType == ECPType.Main)
                {
                    if (_focusCtrl != null)
                    {
                        _focusCtrl.IsFocus = false;
                    }

                    if (_currentCtrl != null)
                    {
                        _focusCtrl = _currentCtrl;
                        _focusCtrl.IsFocus = true;
                    }
                    else
                    {
                        _focusCtrl = null;
                    }
                }

                if (_focusCtrl != null)
                {
                    if (_focusCtrl is GMapMarker)
                    {
                        _mouseDownState.CtrlWidth = ((GMapMarker)_focusCtrl).Size.Width;
                        _mouseDownState.CtrlHeight = ((GMapMarker)_focusCtrl).Size.Height;
                        _mouseDownState.CtrlPositionX = ((GMapMarker)_focusCtrl).LocalPosition.X;
                        _mouseDownState.CtrlPositionY = ((GMapMarker)_focusCtrl).LocalPosition.Y;
                        GPoint gpoint = _gMapControl.FromLatLngToLocal(((GMapMarker)_focusCtrl).Position);
                        _mouseDownState.CtrlGPointX = (int)gpoint.X;
                        _mouseDownState.CtrlGPointY = (int)gpoint.Y;
                    }
                    else if (_focusCtrl is GMapPolygon)
                    {
                        _mouseDownState.SetPoints(((GMapPolygon)_focusCtrl).Points);
                    }
                }

                if (MapMouseDownPositionEvent != null)
                {
                    MapMouseDownPositionEvent(point);
                }

                //if (_isPolygon)
                //{
                //    //MapMarkerUISizeDot pic = new MapMarkerUISizeDot(point, Properties.Resources.Location_24px);
                //    //pic.Pen = new Pen(Color.Red);
                //    MapMarkerPositionDot pic = new MapMarkerPositionDot(gMapControl1.FromLocalToLatLng(e.X, e.Y));
                //    pic.ToolTipMode = MarkerTooltipMode.Always;
                //    pic.ToolTipText = pic.Position.ToString();

                //    _polygonPoints.Add(pic);
                //    RegeneratePolygon();
                //}

                //if (_isRoute)
                //{
                //    _routePoints.Add(point);
                //}
            }
        }

        private void gMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left
                && _mouseDownState.IsMouseDown
                && _currentCtrl != null
                && ((ICtrlBase)_currentCtrl).IsMove)
            {
                if (_currentCtrl is GMapMarker)
                {
                    ((GMapMarker)_currentCtrl).Position = _gMapControl.FromLocalToLatLng(e.X, e.Y);
                }
                else if (_currentCtrl is GMapPolygon)
                {
                    int offsetX = e.X - _mouseDownState.ClickAtX;
                    int offsetY = e.Y - _mouseDownState.ClickAtY;

                    PointLatLng tempP;
                    GPoint tempG;
                    for (int i = 0; i < _mouseDownState.Points.Count; i++)
                    {
                        tempG = _gMapControl.FromLatLngToLocal(_mouseDownState.Points[i]);
                        tempP = _gMapControl.FromLocalToLatLng((int)(tempG.X) + offsetX, (int)(tempG.Y) + offsetY);
                        ((GMapPolygon)_currentCtrl).Points[i] = tempP;
                    }

                    _gMapControl.UpdatePolygonLocalPosition((GMapPolygon)_currentCtrl);
                }

                if (_currentCtrl is IUISizeDot)
                {
                    if (_focusCtrl != null && _focusCtrl is ICtrlDraw)
                    {
                        //_UISKnob.UISizeDot_MouseMove((IUISizeDot)_currentCtrl, (ICtrlDraw)_focusCtrl, 2 * (e.X - _mouseDownState.ClickAtX), 2 * (e.Y - _mouseDownState.ClickAtY), _mouseDownState);
                        _UISKnob.UISizeDot_MouseMove((IUISizeDot)_currentCtrl, (ICtrlDraw)_focusCtrl, e.X - _mouseDownState.ClickAtX, e.Y - _mouseDownState.ClickAtY, _mouseDownState);
                        _focusCtrl.UpdateUI();
                        _gMapControl.Refresh(); //此处刷新后 UISizeDot 显示位置才正确
                        _UISKnob.SetUISizeDotsPosition((ICtrlDraw)_focusCtrl);
                    }
                }
                else if (_currentCtrl is MapMarkerPositionDot)
                {
                    if (_focusCtrl != null && _focusCtrl is MapPolygon)
                    {
                        int index = _UISKnob.IndexOf((MapMarkerPositionDot)_currentCtrl);
                        if (index >= 0 && ((MapPolygon)_focusCtrl).Points.Count > index)
                        {
                            ((MapPolygon)_focusCtrl).Points[index] = new PointLatLng(((MapMarkerPositionDot)_currentCtrl).Position.Lat, ((MapMarkerPositionDot)_currentCtrl).Position.Lng);
                            _gMapControl.UpdatePolygonLocalPosition((MapPolygon)_focusCtrl);
                        }
                    }
                }

                _currentCtrl.UpdateUI();
                if (_currentCtrl.CPType == ECPType.Child)
                {
                    _overlayCollection.CurrentOverlayMng.GetCtrl(_currentCtrl.MainID).UpdateUI(_currentCtrl);
                }
                _gMapControl.Refresh(); // force instant invalidation
            }

            if (MapMouseMoveEvent != null)
            {
                MapMouseMoveEvent(sender, e);
            }
        }

        private void gMapControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_newCtrl != null)
                {
                    //if (_newCtrl is GMapMarker)
                    //{
                    //    Form_ShowText form = new Form_ShowText("设置提示信息");
                    //    if (form.ShowDialog() == DialogResult.OK)
                    //    {
                    //        if (!string.IsNullOrEmpty(form.ShowText))
                    //        {
                    //            ((GMapMarker)_newCtrl).ToolTipMode = MarkerTooltipMode.Always;
                    //            ((GMapMarker)_newCtrl).ToolTipText = form.ShowText;
                    //        }
                    //    }
                    //}

                    if (_focusCtrl != null)
                    {
                        _focusCtrl.IsFocus = false;
                    }
                    _focusCtrl = _newCtrl;
                    _focusCtrl.IsFocus = true;
                    _newCtrl = null;
                }

                if (_focusCtrl != null && _focusCtrl is ICtrlDraw && ((ICtrlBase)_focusCtrl).IsEdit)
                {
                    _UISKnob.ShowUISizeDots((ICtrlDraw)_focusCtrl, true, true);
                    _UISKnob.ShowUIPositionDot(null, false, false);
                }
                else if (_focusCtrl != null && _focusCtrl is GMapPolygon && ((ICtrlBase)_focusCtrl).IsEdit)
                {
                    _UISKnob.ShowUIPositionDot(((GMapPolygon)_focusCtrl).Points, true, true);
                    _UISKnob.ShowUISizeDots(null, false, false);

                    ((MapPolygon)_focusCtrl).SetCenter();
                    ((MapPolygon)_focusCtrl).SetText();
                }
                else
                {
                    _UISKnob.ShowUISizeDots(null, false, false);
                    _UISKnob.ShowUIPositionDot(null, false, false);
                }

                _gMapControl.Refresh();
                _mouseDownState.IsMouseDown = false;
                //_gMapControl.CanDragMap = true;
            }

            if (MapMouseUpPositionEvent != null)
            {
                MapMouseUpPositionEvent(_gMapControl.FromLocalToLatLng(e.X, e.Y));
            }
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (_focusCtrl != null)
                {
                    if(_focusCtrl is MapPolygon)
                    {
                        DeleteDot();
                    }
                    else if (_focusCtrl is MapRouteDistance)
                    {
                        DeleteRouteChild();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //var cc = new GMapMarkerCircle(gMapControl1.FromLocalToLatLng(e.X, e.Y));
            //objects.Markers.Add(cc);

            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    //objects.Markers.Clear();
            //    PointLatLng point = gMapControl1.FromLocalToLatLng(e.X, e.Y);
            //    //GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.green);
            //    Bitmap bitmap = Properties.Resources._1人;
            //    //GMapMarker marker = new GMarkerGoogle(point, bitmap);
            //    GMapMarker marker = new GMapMarkerImage(point, bitmap);
            //    marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            //    marker.ToolTipText = string.Format("{0},{1}", point.Lat, point.Lng);
            //    objects.Markers.Add(marker);
            //}
        }

        private void gMapControl1_OnMarkerDoubleClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (item is ICtrlBase && ((ICtrlBase)item).IsDoubleClick)
                {
                    if (MapCtrlDoubleClickEvent != null)
                    {
                        MapCtrlDoubleClickEvent(item, e);
                    }
                }
            }
        }

        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (item is MapMarkerImage)
                {
                    //MessageBox.Show(((MapMarkerImage)item).ToolTipText);

                    //GeoCoderStatusCode status;
                    //var pos = GMapProviders.GoogleMap.GetPlacemark(item.Position, out status);
                    //if (status == GeoCoderStatusCode.G_GEO_SUCCESS && pos != null)
                    //{
                    //    GMapMarkerRect v = item as GMapMarkerRect;
                    //    {
                    //        v.ToolTipText = pos.Value.Address;
                    //    }
                    //    gMapControl1.Invalidate(false);
                    //}
                }

                //if (item is GMapMarkerRect)
                //{
                //    GeoCoderStatusCode status;
                //    var pos = GMapProviders.GoogleMap.GetPlacemark(item.Position, out status);
                //    if (status == GeoCoderStatusCode.G_GEO_SUCCESS && pos != null)
                //    {
                //        GMapMarkerRect v = item as GMapMarkerRect;
                //        {
                //            v.ToolTipText = pos.Value.Address;
                //        }
                //        gMapControl1.Invalidate(false);
                //    }
                //}
                //else
                //{
                //    if (item.Tag != null)
                //    {
                //        if (currentTransport != null)
                //        {
                //            currentTransport.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                //            currentTransport = null;
                //        }
                //        currentTransport = item;
                //        currentTransport.ToolTipMode = MarkerTooltipMode.Always;
                //    }
                //}
            }
        }
        private void gMapControl1_OnMarkerLeave(GMapMarker item)
        {
            if (!_mouseDownState.IsMouseDown)
            {
                if (item is ICtrlBase)
                {
                    ((ICtrlBase)item).IsEnter = false;
                }

                _gMapControl.MyCursor = System.Windows.Forms.Cursors.Arrow;
                _currentCtrl = null;
            }
        }

        private void gMapControl1_OnMarkerEnter(GMapMarker item)
        {
            //主控件、辅助控件
            if (!_mouseDownState.IsMouseDown
                && (_overlayCollection.CurrentOverlayMng.GetCtrl(((ICtrlBase)item).MainID) != null 
                || ((ICtrlBase)item).CPType == ECPType.Tool))
            {
                if (_currentCtrl != null)
                {
                    ((ICtrlBase)_currentCtrl).IsEnter = false;
                }
                _currentCtrl = (ICtrlBase)item;
                _currentCtrl.IsEnter = true;

                if (item is IUISizeDot)
                {
                    _gMapControl.MyCursor = ((IUISizeDot)item).Cursor;
                }
            }
            else if (_overlayCollection.CurrentOverlayMng.GetCtrl(((ICtrlBase)item).MainID) == null)
            {
                //不同层元素，光标不体现
                _gMapControl.MyCursor = Cursors.Arrow;
            }
        }

        private void gMapControl_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {
            //MessageBox.Show("点击");
        
        }

        private void gMapControl_OnPolygonEnter(GMapPolygon item)
        {
            if (!_mouseDownState.IsMouseDown
    && _overlayCollection.CurrentOverlayMng.GetCtrl(((ICtrlBase)item).MainID) != null)
            {
                if (_currentCtrl != null)
                {
                    ((ICtrlBase)_currentCtrl).IsEnter = false;
                }
                _currentCtrl = (ICtrlBase)item;
                _currentCtrl.IsEnter = true;
                _gMapControl.CanDragMap = false;
            }
            else if (_overlayCollection.CurrentOverlayMng.GetCtrl(((ICtrlBase)item).MainID) == null)
            {
                //不同层元素，光标不体现
                _gMapControl.MyCursor = Cursors.Arrow;
            }
        }

        private void gMapControl_OnPolygonLeave(GMapPolygon item)
        {
            if (!_mouseDownState.IsMouseDown || (item is ICtrlBase && !((ICtrlBase)item).IsMove))
            {
                if (item is ICtrlBase)
                {
                    ((ICtrlBase)item).IsEnter = false;
                }

                _gMapControl.CanDragMap = true;
                _gMapControl.MyCursor = System.Windows.Forms.Cursors.Arrow;
                _currentCtrl = null;
            }
        }


        private void gMapControl_OnRouteClick(GMapRoute item, MouseEventArgs e)
        {
            //MessageBox.Show("点击");
        }

        private void gMapControl_OnRouteEnter(GMapRoute item)
        {
            if (!_mouseDownState.IsMouseDown
    && _overlayCollection.CurrentOverlayMng.GetCtrl(((ICtrlBase)item).MainID) != null)
            {
                if (_currentCtrl != null)
                {
                    ((ICtrlBase)_currentCtrl).IsEnter = false;
                }
                _currentCtrl = (ICtrlBase)item;
                _currentCtrl.IsEnter = true;
            }
            else if (_overlayCollection.CurrentOverlayMng.GetCtrl(((ICtrlBase)item).MainID) == null)
            {
                //不同层元素，光标不体现
                _gMapControl.MyCursor = Cursors.Arrow;
            }
        }

        private void gMapControl_OnRouteLeave(GMapRoute item)
        {
            if (!_mouseDownState.IsMouseDown || (item is ICtrlBase && !((ICtrlBase)item).IsMove))
            {
                if (item is ICtrlBase)
                {
                    ((ICtrlBase)item).IsEnter = false;
                }

                _gMapControl.MyCursor = System.Windows.Forms.Cursors.Arrow;
                _currentCtrl = null;
            }
        }

        #endregion
    }
}
