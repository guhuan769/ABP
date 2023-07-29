using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GMap.NET.Mng
{
    public class AlarmMng
    {
        public AlarmMng(GMapMng gmapMng)
        {
            _gmapMng = gmapMng;
            _ctrlList = new Dictionary<string, MapMarkerImage>();
        }

        private GMapMng _gmapMng;
        private System.Timers.Timer _timer = null; 
        private int _circleMax = 5;
        private int _circleCount = 0;
        private Dictionary<string, MapMarkerImage> _ctrlList;

        private bool _isAlarm = false;

        public void AddCtrl(MapMarkerImage ctrl)
        {
            if (!string.IsNullOrEmpty(((ICtrlBase)ctrl).CtrlID) && !_ctrlList.ContainsKey(((ICtrlBase)ctrl).CtrlID))
            {
                _ctrlList.Add(((ICtrlBase)ctrl).CtrlID, ctrl);
            }
        }

        public void DeleteCtrl(string ctrlID)
        {
            _ctrlList.Remove(ctrlID);
        }

        public bool IsAlarm
        {
            get { return _isAlarm; }
            set 
            { 
                _isAlarm = value;

                if (_isAlarm)
                {
                    if (_timer == null)
                    {
                        _timer = new System.Timers.Timer(200);
                        _timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsedEvent);
                        _timer.AutoReset = true;
                        _timer.Enabled = true;
                    }

                    _circleCount = 0;
                    _isAlarm = true;
                    _timer.Start();
                }
                else
                {
                    if (_timer != null)
                    {
                        _circleCount = 0;
                        _isAlarm = false;
                    }
                }
            }
        }

        public void TimerElapsedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                _timer.Stop();
                if (_circleCount < _circleMax)
                {
                    _circleCount++;
                }
                else
                {
                    _circleCount = 0;
                }

                foreach (MapMarkerImage item in _ctrlList.Values)
                {
                    item.AlarmCircleCount = _circleCount;
                    item.AlarmPen = new Pen(Color.Red, 2);
                }

                if (_isAlarm)
                {
                    _timer.Start();
                }
                else
                {
                    foreach (MapMarkerImage item in _ctrlList.Values)
                    {
                        item.AlarmCircleCount = 0;
                        item.AlarmPen = null;
                    }
                }

                _gmapMng.GMapControl.Invoke(new EventHandler(delegate
                {
                    _gmapMng.GMapControl.Refresh();

                }));
            }
            catch { }
        }
    }
}
